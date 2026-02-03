namespace Application.DTOs;

public record MovementCreatedDto
{
    public Guid Id { get; init; }
    public string AccountNumber { get; init; } = default!;
    public string AccountType { get; init; } = default!;
    public string MovementType { get; init; } = default!;
    public decimal Amount { get; init; }
    public decimal Balance { get; init; }
    public DateTime Date { get; init; }

    public string DateFormatted => Date.ToString("yyyy-MM-dd");
}
