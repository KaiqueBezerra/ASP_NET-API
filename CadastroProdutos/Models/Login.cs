using System.ComponentModel.DataAnnotations;

namespace CadastroProdutos.Models
{
    public class Login
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
