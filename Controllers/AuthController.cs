using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        public AuthController(IAuthRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string UserName, string Password)
        {
            UserName = UserName.ToLower();
            if(await _repository.UserExists(UserName))
                return BadRequest("UserName already exists");
            
            User user = new User{
                UserName = UserName
            };

            await _repository.Register(user, Password);
            
            return StatusCode(201);
        }
    }
}