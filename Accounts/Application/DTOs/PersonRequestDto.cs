namespace Application.Accounts.DTOs;

public class PersonRequestDto
{
    public Guid Id { get; set; }
    public string Identification { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Age { get; set; }
    public string Gender { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}
