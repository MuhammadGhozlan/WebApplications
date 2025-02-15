using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TutoringWebApplication.Data;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 IConfiguration configuration
                                 )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task<ActionResult> Register(SignUpCredentials signUpCredentials)
        {
            var userExists = await _userManager.FindByEmailAsync(signUpCredentials.Email);
            if(userExists != null)
            {
                return BadRequest("User Already Exists!");
            }
            var user = new User
            {
                FirstName = signUpCredentials.FirstName,
                LastName = signUpCredentials.LastName,
                Email = signUpCredentials.Email,
                UserName = signUpCredentials.Email
            };
            var result = await _userManager.CreateAsync(user, signUpCredentials.Password);
            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            if(signUpCredentials.UserRole == 0)
            {
                await _userManager.AddToRoleAsync(user, UserRole.Student.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(user, signUpCredentials.UserRole.ToString());
            }
            return Ok(new { Message = $"User has been registered with a role of {signUpCredentials.UserRole}" });
        }
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(SignInCredentials signInCredentials)
        {
            var user = await _userManager.FindByEmailAsync(signInCredentials.Email);
            if(user == null)
            {
                return NotFound("User was not found");
            }
            var result = await _signInManager.PasswordSignInAsync(user, signInCredentials.Password, false, false);
            if(!result.Succeeded)
            {
                return Unauthorized("Check the credentials and try again");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Email,user.Email!)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrinciple = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrinciple, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
            });
            return Ok(new { Message = "User Logged In Successfully" });
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
