using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Commons.Helper;
using Application.DTOs;
using Application.IRepository;
using Domain;
using dv_apicommerce.Models.Dtos;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Repository;


public class UserRepository : IUserRepository
{
    private readonly ClientsDbContext _db;
    private readonly string? secretKey;
    private readonly UserManager<Client> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly PersonsHelper _personsHelper;


    public UserRepository(
        ClientsDbContext db,
        IConfiguration configuration,
        UserManager<Client> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper,
        PersonsHelper personsHelper
    )
    {
        _db = db;
        secretKey = configuration.GetValue<string>("JwtSettings:Secret") ?? throw new InvalidOperationException("Secret key isn't configured in appsettings.json");
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _personsHelper = personsHelper;
    }

    public async Task<Client?> GetUserAsync(string id) => await _db.Clients.FirstOrDefaultAsync(x => x.Id == id && x.State);
    public async Task<List<Client>> GetUsersAsync() => await _db.Clients.AsNoTracking().Where(u => u.State).OrderBy(u => u.UserName).ToListAsync();
    public bool IsUniqueUser(string username) => !_db.Users.Any(u => u.UserName!.ToLower().Trim() == username.ToLower().Trim());

    public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
    {
        var errorHand = (string message) => new UserLoginResponseDto { User = null, Token = "", Message = message };

        if (string.IsNullOrWhiteSpace(userLoginDto.Email)) return errorHand("Email is required");
        if (string.IsNullOrWhiteSpace(userLoginDto.Password)) return errorHand("Password is required");

        var user = await _db.Clients.FirstOrDefaultAsync(u => u.Email != null && u.Email.ToUpper().Trim() == userLoginDto.Email.ToUpper().Trim());

        if (user == null) return errorHand("User not found");

        bool isValid = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

        if (!isValid) return errorHand("Invalid Password");
        if (string.IsNullOrWhiteSpace(secretKey)) throw new InvalidOperationException("Secret key isn't configured");

        var roles = await _userManager.GetRolesAsync(user);
        var key = Encoding.UTF8.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim("id", user.Id),
            new Claim("username", user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty)
        }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);

        return new UserLoginResponseDto
        {
            Token = handler.WriteToken(token),
            User = _mapper.From(user).AdaptToType<UserDataDto>(),
            Message = "User logged successfully"
        };
    }


    public async Task<UserDataDto> Register(CreateUserDto createUserDto)
    {
        var errors = (string message) => new ArgumentNullException(message);
        var appErrors = (string message) => new ApplicationException(message);

        if (string.IsNullOrWhiteSpace(createUserDto.Username)) throw errors("Username is required");
        if (string.IsNullOrWhiteSpace(createUserDto.Password)) throw errors("Password is required");
        if (string.IsNullOrWhiteSpace(createUserDto.Identification)) throw errors("Identification is required");

        var cedulaExists = await _personsHelper.CedulaExistsAsync(createUserDto.Identification);

        if (!cedulaExists) throw appErrors("The ID card does not exist in the banking system.");
        if (!IsUniqueUser(createUserDto.Username)) throw appErrors("Username already exists");

        var user = new Client
        {
            UserName = createUserDto.Username,
            Email = createUserDto.Email,
            NormalizedEmail = createUserDto.Email!.ToUpper(),
            Identification = createUserDto.Identification,
            PhoneNumber = createUserDto.phoneNumber,
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
        {
            var errs = string.Join(",", result.Errors.Select(e => e.Description));
            throw appErrors($"Register creation failed: {errs}");
        }

        var userRole = createUserDto.Role ?? "User";
        if (!await _roleManager.RoleExistsAsync(userRole)) await _roleManager.CreateAsync(new IdentityRole(userRole));
        await _userManager.AddToRoleAsync(user, userRole);
        return _mapper.From(user).AdaptToType<UserDataDto>();
    }
}

