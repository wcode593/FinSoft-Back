using System;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Person
{
    public Guid Id { get; set; }
    [MinLength(4)]
    public string Identification { get; set; } = default!;
    public string Name { get; set; } = default!;

    [Range(0, 120)]
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}
