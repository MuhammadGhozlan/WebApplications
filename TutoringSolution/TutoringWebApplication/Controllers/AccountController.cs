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
        private readonly UserDbContext _userDbContext;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 UserDbContext userDbContext,
                                 IConfiguration configuration
                                 )
        {
            _userManager= userManager;
            _signInManager= signInManager; 
            _userDbContext= userDbContext;
            _configuration= configuration;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task<ActionResult> Register(SignUpCredentials signUpCredentials, UserRole userRole)
        {
            var userExist=await _userManager.FindByEmailAsync(signUpCredentials.Email);
            var key= _configuration.GetValue<string>("signingKey");
            if(userExist != null)
            {
                return BadRequest(new {Message="User Already Exists"});
            }
            if(key==null)
            {
                return BadRequest(new { Message = "No signing Key" });
            }
            var newUser=new User
            {
                FirstName =signUpCredentials.FirstName,
                LastName =signUpCredentials.LastName,
                UserName=signUpCredentials.Email,
                Email=signUpCredentials.Email,                
            };
            await _userManager.CreateAsync(newUser,signUpCredentials.Password);
            if(userRole == 0)
            {
                userRole=UserRole.Student;
                await _userManager.AddToRoleAsync(newUser, userRole.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(newUser, userRole.ToString());
            }
            
            return Ok(new {Message="User has been created successfully" });

            

        }
    }
}
