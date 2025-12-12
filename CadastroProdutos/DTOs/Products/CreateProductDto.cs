using System.ComponentModel.DataAnnotations;

namespace CadastroProdutos.DTOs.Products
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Name of the product is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }
    }
}
