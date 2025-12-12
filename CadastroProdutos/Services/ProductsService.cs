using CadastroProdutos.Database;
using CadastroProdutos.DTOs.Products;
using CadastroProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroProdutos.Services
{
    public class ProductsService : IProductsService
    {
        private ApplicationDbContext db;

        public ProductsService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public async Task Add(Product newProduct)
        {
            ValidateProducts(newProduct);
            await db.Products.AddAsync(newProduct);
            await db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var product = await db.Products.FindAsync(id);

            if (product is null)
            {
                return false;
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<List<Product>> GetAll()
        {
            return await db.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var product = await db.Products.FindAsync(id);

            if (product is null)
            {
                return null;
            }

            return product;
        }

        public async Task<Product> Update(int id, Product updatedProduct)
        {
            ValidateProducts(updatedProduct);
            var product = await db.Products.FindAsync(id);

            if (product is null)
            {
                return null;
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;

            db.Products.Update(product);
            await db.SaveChangesAsync();

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
