using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TutoringWebApplication.Models
{
    public class User:IdentityUser
    {
        [Required, MaxLength(120)]
        public string FirstName { get; set; }
        [Required, MaxLength(120)]
        public string LastName { get; set; }
    }
}
