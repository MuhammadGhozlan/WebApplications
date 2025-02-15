using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TutoringWebApplication.Models
{
    public enum UserRole
    {
        Admin=1,
        Instructor=2,
        Student = 3
    }
}
