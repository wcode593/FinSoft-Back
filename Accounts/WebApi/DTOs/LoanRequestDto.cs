using System;

namespace WebApi.DTOs;

public class LoanRequestDto
{
    public string AccountId { get; set; }
    public decimal Amount { get; set; }
}
