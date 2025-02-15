using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TutoringWebApplication.Models
{
    public enum UserRole
    {
        [JsonIgnore]
        Admin=1,
        Instructor=2,
        Student = 3
    }
}
