using System.ComponentModel.DataAnnotations;

namespace TutoringWebApplication.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(100), EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        public string PhoneNumber { get; set; }
        public ICollection<Course> Courses { get; set; }
        public Instructor()
        {
            Courses = new List<Course>();
        }
    }
}
