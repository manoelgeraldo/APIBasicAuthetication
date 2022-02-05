using APIBasicAuthentication.Authorization;
using APIBasicAuthentication.Models;
using APIBasicAuthentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIBasicAuthentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await userService.Authenticate(model.Username, model.Password);

            if (user is null)
                return BadRequest(new { message = "Username or password is incorrect!" });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetUsers();
            return Ok(users);
        }
    }
}
