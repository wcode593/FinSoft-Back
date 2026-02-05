using System;
using System.ComponentModel.DataAnnotations;

namespace dv_apicommerce.Models.Dtos;

public class UserLoginDto
{
    [Required(ErrorMessage = "El campo email es requerido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "El campo password es requerido")]
    public string? Password { get; set; }
}
