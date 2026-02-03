using System;

namespace Application.DTOs;

public class UserRegisterDto
{
    public string? ID { get; set; }
    public required string Username { get; set; }
    public required string Identification { get; set; }
    public required string Password { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
}
