using CadastroProdutos.Models;

namespace CadastroProdutos.Services
{
    public interface IProductsService
    {
        public List<Product> GetAll();
        public Product GetById(int id);
        public void Add(Product newProduct);
        public Product Update(int id, Product updatedProduct);
        public bool Delete(int id);
    }
}
