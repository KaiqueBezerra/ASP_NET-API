using CadastroProdutos.Models;

namespace CadastroProdutos.Services
{
    public interface IProductsService
    {
        public Task<List<Product>> GetAll();
        public Task<Product> GetById(int id);
        public Task Add(Product newProduct);
        public Task<Product> Update(int id, Product updatedProduct);
        public Task<bool> Delete(int id);
    }
}
