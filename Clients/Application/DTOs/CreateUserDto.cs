using System;
using System.ComponentModel.DataAnnotations;

namespace dv_apicommerce.Models.Dtos;

public class CreateUserDto
{
    [Required(ErrorMessage = "El campo username es requerido")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "El campo email es requerido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "El campo Identificacion es requerido")]
    public string Identification { get; set; } = default!;

    [Required(ErrorMessage = "El campo password es requerido")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "El campo role es requerido")]
    public string? Role { get; set; }
    [Required(ErrorMessage = "El campo PhoneNumber es requerido")]
    public string? phoneNumber { get; set; }
}
