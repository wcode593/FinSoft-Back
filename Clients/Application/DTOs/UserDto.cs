using System;

namespace dv_apicommerce.Models.Dtos;

public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string Identification { get; set; } = null!;
    public bool State { get; set; } = true;
}
