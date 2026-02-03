using System;
using Application.DTOs;

namespace dv_apicommerce.Models.Dtos;

public class UserLoginResponseDto
{
    public UserDataDto? User { get; set; }
    public string? Token { get; set; }
    public string? Message { get; set; }
}
