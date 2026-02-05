using System;
using Application.DTOs;
using Domain;
using dv_apicommerce.Models.Dtos;

namespace Application.IRepository;

public interface IUserRepository
{
    Task<List<Client>> GetUsersAsync();
    Task<Client?> GetUserAsync(string id);
    bool IsUniqueUser(string username);
    Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);
    Task<UserDataDto> Register(CreateUserDto createUserDto);
}
