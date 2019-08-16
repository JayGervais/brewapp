using System.Threading.Tasks;
using BrewApp.API.Data;
using BrewApp.API.Dtos;
using BrewApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrewApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // TO DO: validate request

            userForRegisterDto.Email = userForRegisterDto.Email.ToLower();

            if(await _repo.UserExists(userForRegisterDto.Email))
            {
                return BadRequest("User already exists");
            }

            var userToCreate = new User
            {
                Email = userForRegisterDto.Email
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
    }
}