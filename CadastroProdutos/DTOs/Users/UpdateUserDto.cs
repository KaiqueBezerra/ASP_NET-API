using System.ComponentModel.DataAnnotations;

namespace CadastroProdutos.DTOs.Users
{
    public class UpdateUserDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "The name must have at least 2 characters.")]
        public string Username { get; set; }
    }
}
