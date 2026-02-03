using System;

namespace Domain.Models.Movements;

public class Movement
{
    public Guid Id { get; set; }
    public string AccountType { get; set; } = default!;
    public string MovementType { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow.Date;
}
