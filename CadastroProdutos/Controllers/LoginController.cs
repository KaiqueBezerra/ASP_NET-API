using CadastroProdutos.Models;
using CadastroProdutos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadastroProdutos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUsersService usersService;

        public LoginController(IConfiguration config, IUsersService usersService)
        {
            this.configuration = config;
            this.usersService = usersService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(Login login)
        {
            // Buscar usuário no banco
            var user = await usersService.GetByEmail(login.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                return Unauthorized("Invalid email or password!");
            }

            // Definir role dinamicamente (opcional)
            string role = user.Role.ToString() == "admin" ? "admin" : "user";

            // Criar Token JWT
            var jwtConfig = configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtConfig["Key"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("email", user.Email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = jwtConfig["Issuer"],
                Audience = jwtConfig["Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}
