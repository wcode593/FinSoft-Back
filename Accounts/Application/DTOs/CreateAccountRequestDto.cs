namespace Application.Accounts.DTOs;

public class CreateAccountRequestDto
{
    public string AccountType { get; set; } = default!;
    public PersonRequestDto Person { get; set; } = default!;
}
