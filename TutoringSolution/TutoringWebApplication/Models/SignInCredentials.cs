using System.ComponentModel.DataAnnotations;

namespace TutoringWebApplication.Models
{
    public class SignInCredentials
    {       
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
