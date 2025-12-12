using CadastroProdutos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CadastroProdutos.Models;
using CadastroProdutos.DTOs.Users;

namespace CadastroProdutos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<UserResponseDto>>> Get()
        {
            try
            {
                var users = await usersService.GetAll();

                return Ok(users);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetById(int id)
        {
            try
            {
                var user = await usersService.GetById(id);
                if (user == null)
                {
                    return NotFound($"user with ID {id} not found!");
                }
                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [AllowAnonymous]
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post(CreateUserDto dto)
        {
            try
            {
                var newUser = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Password = dto.Password,
                    Role = (User.UserRole)dto.Role
                };

                await usersService.Add(newUser);
                return Created();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> Put(int id, UpdateUserDto updatedUser)
        {
            try
            {
                var user = await usersService.Update(id, updatedUser);

                if (user is null)
                {
                    return NotFound($"user with ID {id} not found!");
                }

                return Ok(user);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        //[AllowAnonymous]
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool deleted = await usersService.Delete(id);
                if (deleted is false)
                {
                    return NotFound($"Product with ID {id} not found!");
                }
                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}
