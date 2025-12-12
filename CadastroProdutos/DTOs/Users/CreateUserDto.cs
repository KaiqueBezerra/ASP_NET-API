using CadastroProdutos.Models;
using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
    public enum UserRole { admin, user }

    [MinLength(2, ErrorMessage = "The name must have at least 2 characters.")]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public UserRole Role { get; set; }
}
