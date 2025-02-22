using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Dto
{
    public class CourseDto
    {       
        public int Id { get; set; }        
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
        public int InstructorId { get; set; }        
        public Instructor Instructor { get; set; }
        public CourseStatus CourseStatus { get; set; }        
    }
}
