using System;

namespace WebApi.DTOs;

public class TransferRequestDto
{
    public string FromAccountIdOrNumber { get; set; } = default!;
    public string ToAccountIdOrNumber { get; set; } = default!;
    public decimal Amount { get; set; }
}
