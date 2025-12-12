using CadastroProdutos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CadastroProdutos.Models;
using CadastroProdutos.DTOs.Products;

namespace CadastroProdutos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            try
            {
                return Ok(await productsService.GetAll());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            try
            {
                var product = await productsService.GetById(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found!");
                }
                return Ok(product);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post(CreateProductDto dto)
        {
            try
            {
                var newProduct = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Stock = dto.Stock
                };

                await productsService.Add(newProduct);

                return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, UpdateProductDto dto)
        {
            try
            {
                var updatedProduct = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    Stock = dto.Stock
                };

                var product = await productsService.Update(id, updatedProduct);

                if (product is null)
                {
                    return NotFound($"Product with ID {id} not found!");
                }

                return Ok(product);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deleted = await productsService.Delete(id);
                if (deleted is false)
                {
                    return NotFound($"Product with ID {id} not found!");
                }
                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}
