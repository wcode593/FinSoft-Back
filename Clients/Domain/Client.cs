using System;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Client : IdentityUser
{
    public string Identification { get; set; } = default!;
    public bool State { get; set; } = true;
}
