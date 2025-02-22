using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> Registration(SignUpCredentials signUpCredentials);
        Task<ClaimsPrincipal> Login(SignInCredentials signUpCredentials);
    }
}
