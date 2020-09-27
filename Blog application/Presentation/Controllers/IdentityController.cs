using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Application.Authentication.Commands.UserLogin;
using Application.Authentication.Commands.UserRegister;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class IdentityController : BaseController
    {
        /// <summary>
        /// Register new user to system
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<NoContentResult> Register(UserRegistrationCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// User login with username or email and password
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginDto>> Login(UserLoginCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
