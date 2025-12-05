using CadastroProdutos.DTOs.Users;
using CadastroProdutos.Models;

namespace CadastroProdutos.Services
{
    public interface IUsersService
    {
        public List<UserResponseDto> GetAll();
        public UserResponseDto GetById(int id);
        public User GetByUsername(string username);
        public void Add(User newUser);
        public UserResponseDto Update(int id, UpdateUserDto updatedUser);
        public bool Delete(int id);
    }
}
