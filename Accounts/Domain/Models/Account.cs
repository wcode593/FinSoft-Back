namespace Domain.Models;

public class Account
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public string AccountNumber { get; set; } = default!;
    public string OwnerName { get; set; } = default!;
    public string AccountType { get; set; } = default!;

    public decimal Balance { get; set; }

    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.Date;
}
