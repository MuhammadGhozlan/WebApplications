using Microsoft.AspNetCore.Identity;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> Registration(SignUpCredentials signUpCredentials);
        Task<SignInResult> Login(SignInCredentials signUpCredentials);
    }
}
