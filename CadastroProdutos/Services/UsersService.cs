using CadastroProdutos.Database;
using CadastroProdutos.DTOs.Users;
using CadastroProdutos.Models;

namespace CadastroProdutos.Services
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext db;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public void Add(User newUser)
        {
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            db.Users.Add(newUser);
            db.SaveChanges();
        }

        public bool Delete(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);

            if (user is null)
            {
                return false;
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return true;
        }

        public List<UserResponseDto> GetAll()
        {
            return db.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Role = u.Role.ToString()
                })
                .ToList();
        }

        public UserResponseDto GetById(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role.ToString()
            };
        }

        public User GetByUsername(string username)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user is null)
            {
                return null;
            }

            return user;
        }

        public UserResponseDto Update(int id, UpdateUserDto updatedUser)
        {
            var user = db.Users.FirstOrDefault(p => p.Id == id);

            if (user is null)
            {
                return null;
            }

            user.Username = updatedUser.Username;

            db.SaveChanges();

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role.ToString()
            };
        }
    }
}
