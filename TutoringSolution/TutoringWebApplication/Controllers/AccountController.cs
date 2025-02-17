using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutoringWebApplication.Interfaces;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService,
                                 ILogger<AccountController> logger
                                 )
        {
            _accountService = accountService;
            _logger = logger;
        }
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task<ActionResult> Register(SignUpCredentials signUpCredentials)
        {
            var register = await _accountService.Registration(signUpCredentials);
            if(register == null || register.Succeeded == false)
            {
                foreach(var error in register.Errors)
                {
                    _logger.LogError(error.Description);
                }
                
                return BadRequest(register.Errors.Select(e=>e.Description));
            }
            return Ok(register);
        }
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(SignInCredentials signInCredentials)
        {
            var login = await _accountService.Login(signInCredentials);
            if(login == null)
            {
                _logger.LogError("Check the username and password?");
                return Unauthorized("Check the username and password?");
            }
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, login, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
            });
            //var authCookie = HttpContext.Response.Headers["Set-Cookie"].First(c=>c.StartsWith("AuthCookie"));
            return Ok(new {Message="User Logged in Successfully." });
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("accessdenied")]
        public ActionResult AccessDenied()
        {
            return StatusCode(StatusCodes.Status403Forbidden, "Access is denied, Try Again Later");
        }

    }
}
