using System;

namespace Domain.DTOs;

public class MovementActionDto
{
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public string Date { get; set; } = default!;
}

// DTO para las acciones agrupadas por cuenta
public class AccountMovementsDto
{
    public string AccountNumber { get; set; } = default!;
    public AccountActionsDto Acciones { get; set; } = new();
}

// DTO que contiene transacciones y retiros
public class AccountActionsDto
{
    public List<MovementActionDto> Transacciones { get; set; } = new();
    public List<MovementActionDto> Retiros { get; set; } = new();
}

public class MovementCreatedDto
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = default!;
    public string AccountType { get; set; } = default!;
    public string MovementType { get; set; } = default!;
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public DateTime Date { get; set; }
    public string DateFormatted => Date.ToString("yy-MM-dd");
}
