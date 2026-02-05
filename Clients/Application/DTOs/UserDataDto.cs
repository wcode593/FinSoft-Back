using System;

namespace Application.DTOs;

public class UserDataDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string Identification { get; set; } = default!;
    public bool State { get; set; } = true;
}
