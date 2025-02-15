using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoringWebApplication.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(100),EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public Student()
        {
            Payments = new List<Payment>();
            Enrollments = new List<Enrollment>();
        }
    }
}
