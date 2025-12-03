using CadastroProdutos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CadastroProdutos.Models;

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
        public ActionResult<List<Product>> Get()
        {
            return Ok(productsService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = productsService.GetById(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found!");
            }
            return Ok(product);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Post(Product newProduct)
        {
            try
            {
                productsService.Add(newProduct);

                return Created($"/produtos/{newProduct.Id}", newProduct);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, Product updatedProduct)
        {
            try
            {
                var product = productsService.Update(id, updatedProduct);

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
        public ActionResult Delete(int id)
        {
            var deleted = productsService.Delete(id);
            if (deleted is false)
            {
                return NotFound($"Product with ID {id} not found!");
            }
            return Ok($"Product with ID {id} successfully deleted!");
        }

    }
}
