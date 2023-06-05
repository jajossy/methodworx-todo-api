using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApplication.IServices;
using TodoApplication.Models.Custom;
using TodoApplication.Models.Generated;

namespace TodoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authorize")]
        public async Task<JwtResponse> AuthorizeUser([FromBody] UserInput user)
        {
            return await _userService.AuthorizeUser(user);
        }

        [HttpPost("userbymail")]
        public async Task<User> GetByMail([FromBody] string email)
        {
            return await _userService.GetUser(email);
        }        

        [HttpPost]
        public async Task<User> Post([FromBody] User user)
        {
            return await _userService.CreateUser(user);
        }
    }
}
