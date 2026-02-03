using Application.Accounts.DTOs;

namespace WebApi.DTOs;

public class OpenAccountRequestDto
{
    public PersonRequestDto Person { get; set; } = default!;
    public string AccountType { get; set; } = default!;
}
