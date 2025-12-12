using CadastroProdutos.DTOs.Users;
using CadastroProdutos.Models;

namespace CadastroProdutos.Services
{
    public interface IUsersService
    {
        public Task<List<UserResponseDto>> GetAll();
        public Task<UserResponseDto> GetById(int id);
        public Task<User> GetByEmail(string email);
        public Task Add(User newUser);
        public Task<UserResponseDto> Update(int id, UpdateUserDto updatedUser);
        public Task<bool> Delete(int id);
    }
}
