using System;

namespace WebApi.DTOs;

public class DepositRequestDto
{
    public string AccountIdOrNumber { get; set; } = default!;
    public decimal Amount { get; set; }
}
