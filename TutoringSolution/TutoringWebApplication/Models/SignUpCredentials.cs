using System.ComponentModel.DataAnnotations;

namespace TutoringWebApplication.Models
{
    public class SignUpCredentials
    {        
        public string FirstName { get; set; }       
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
    }
}
