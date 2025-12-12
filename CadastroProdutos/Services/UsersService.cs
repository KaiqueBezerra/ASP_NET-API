using CadastroProdutos.Database;
using CadastroProdutos.DTOs.Users;
using CadastroProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroProdutos.Services
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext db;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public async Task Add(User newUser)
        {
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            await db.Users.AddAsync(newUser);
            await db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var user = await db.Users.FindAsync(id);

            if (user is null)
            {
                return false;
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<List<UserResponseDto>> GetAll()
        {
            return await db.Users
                .Select(user => new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role.ToString()
                })
                .ToListAsync();
        }

        public async Task<UserResponseDto> GetById(int id)
        {
            var user = await db.Users.FindAsync(id);

            if (user == null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
            {
                return null;
            }

            return user;
        }

        public async Task<UserResponseDto> Update(int id, UpdateUserDto updatedUser)
        {
            var user = await db.Users.FindAsync(id);

            if (user is null)
            {
                return null;
            }

            user.Username = updatedUser.Username;

            db.Users.Update(user);
            await db.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }
    }
}
