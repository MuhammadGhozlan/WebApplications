using System.ComponentModel.DataAnnotations;

namespace DatingApp.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
    }
}
