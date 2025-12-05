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
        public ActionResult<List<UserResponseDto>> Get()
        {
            return Ok(usersService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<UserResponseDto> GetById(int id)
        {
            var user = usersService.GetById(id);
            if (user == null)
            {
                return NotFound($"user with ID {id} not found!");
            }
            return Ok(user);
        }

        [AllowAnonymous]
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Post(User newUser)
        {
            try
            {
                usersService.Add(newUser);

                return Created();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ActionResult<UserResponseDto> Put(int id, UpdateUserDto updatedUser)
        {
            try
            {
                var user = usersService.Update(id, updatedUser);

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
        public ActionResult Delete(int id)
        {
            var deleted = usersService.Delete(id);
            if (deleted is false)
            {
                return NotFound($"Product with ID {id} not found!");
            }
            return Ok($"Product with ID {id} successfully deleted!");
        }

    }
}
