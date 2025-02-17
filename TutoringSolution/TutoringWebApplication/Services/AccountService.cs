using Microsoft.AspNetCore.Identity;
using TutoringWebApplication.Interfaces;
using TutoringWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TutoringWebApplication.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountService> _logger;
        public AccountService(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              ILogger<AccountService> logger)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
        }
        public async Task<IdentityResult> Registration(SignUpCredentials signUpCredentials)
        {
            var userExists = await _userManager.FindByEmailAsync(signUpCredentials.Email);
            if(userExists != null)
            {
                _logger.LogError("user exists already");
                return IdentityResult.Failed(new IdentityError { Description = "User already exists" });
            }
            var user = new User
            {
                FirstName = signUpCredentials.FirstName,
                LastName = signUpCredentials.LastName,
                UserName=signUpCredentials.Email,
                Email = signUpCredentials.Email,
            };
            var result = await _userManager.CreateAsync(user, signUpCredentials.Password);
            if(!result.Succeeded)
            {
                _logger.LogInformation("checke the username and the password");
                return IdentityResult.Failed(new IdentityError{Description=result.Errors.First().Description });
            }
            if(signUpCredentials.UserRole == 0)
            {
                await _userManager.AddToRoleAsync(user, UserRole.Student.ToString());
            }
            else if(signUpCredentials.UserRole == UserRole.Admin)
            {
                _logger.LogInformation("Admin role is restricted");
                return IdentityResult.Failed(new IdentityError { Description="You cannot assign the admin role to the user"});
            }
            else
            {
                await _userManager.AddToRoleAsync(user, signUpCredentials.UserRole.ToString());
            }

            return IdentityResult.Success;
        }
        public async Task<ClaimsPrincipal?> Login(SignInCredentials signUpCredentials)
        {
            var user=await _userManager.FindByEmailAsync(signUpCredentials.Email);
            if(user == null)
            {
                return null;
            }
            var result=await _signInManager.PasswordSignInAsync(user, signUpCredentials.Password,
                                                                isPersistent:false, lockoutOnFailure:false);
            if(!result.Succeeded)
            {
                return null;
            }
            var claims=new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.Email,user.Email!),
            };
            var roles=await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role.ToString()));
            }
            var userIdentity=new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrinciple=new ClaimsPrincipal(userIdentity);
            
            return userPrinciple;
        }
    }
}
