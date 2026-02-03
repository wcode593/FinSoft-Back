using System;

namespace WebApi.DTOs;

public class WithdrawRequestDto
{
    public string AccountIdOrNumber { get; set; } = default!;
    public decimal Amount { get; set; }
}
