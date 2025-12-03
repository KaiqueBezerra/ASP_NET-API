using CadastroProdutos.Database;
using CadastroProdutos.Models;

namespace CadastroProdutos.Services
{
    public class ProductsDatabaseService : IProductsService
    {
        private ApplicationDbContext db;

        public ProductsDatabaseService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public void Add(Product newProduct)
        {
            ValidateProducts(newProduct);
            db.Products.Add(newProduct);
            db.SaveChanges();
        }

        public bool Delete(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return false;
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return true;
        }

        public List<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public Product GetById(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return null;
            }

            return product;
        }

        public Product Update(int id, Product updatedProduct)
        {
            ValidateProducts(updatedProduct);
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return null;
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;

            db.SaveChanges();

            return product;
        }

        private void ValidateProducts(Product product)
        {
            if (product.Name == "Default Product")
            {
                throw new Exception("Product name cannot be 'Default Product'");
            }

            if (product.Stock > 1000)
            {
                throw new Exception("Product stock cannot exceed 1000 units");
            }
        }
    }
}
