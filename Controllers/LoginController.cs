using System.Threading.Tasks;
using ApiAuth.Models;
using ApiAuth.Repositories;
using ApiAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {   
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Autheticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username,model.Password);

            if(user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos"});
            }

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new{
                User = user,
                Token = token
            };
            

        }
    }
}