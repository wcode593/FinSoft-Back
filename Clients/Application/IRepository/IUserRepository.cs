using System;
using Application.DTOs;
using dv_apicommerce.Models.Dtos;

namespace Application.IRepository;

public interface IUserRepository
{
    ICollection<ApplicationUser> GetUsers();
    ApplicationUser? GetUser(string id);
    bool IsUniqueUser(string username);
    Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
    Task<UserDataDto> Register(CreateUserDto createUserDto);
}
