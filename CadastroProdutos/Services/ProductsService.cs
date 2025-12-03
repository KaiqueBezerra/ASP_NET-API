using CadastroProdutos.Models;

namespace CadastroProdutos.Services
{
    public class ProductsService : IProductsService
    {
        private static List<Product> products = new List<Product>()
        {
            new Product() { Id = 1, Name = "Notebook", Price = 3500.00M, Stock = 10 },
            new Product() { Id = 2, Name = "Smartphone", Price = 2000.00M, Stock = 25 },
        };

        public List<Product> GetAll()
        {
            return products;
        }

        public Product GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return null;
            }

            return product;
        }

        public void Add(Product newProduct)
        {
            products.Add(newProduct);
        }

        public Product Update(int id, Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return null;
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;

            return product;
        }

        public bool Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return false;
            }
            products.Remove(product);
            return true;
        }
    }
}
