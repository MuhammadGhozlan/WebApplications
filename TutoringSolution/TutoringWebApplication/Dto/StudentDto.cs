using System.ComponentModel.DataAnnotations;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Dto
{
    public class StudentDto
    {     
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Payment>? Payments { get; set; }       
    }
}
