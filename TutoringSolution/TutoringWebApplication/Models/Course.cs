using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutoringWebApplication.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
        public int InstructorId { get; set; }
        [ForeignKey(nameof(InstructorId))]
        public Instructor Instructor { get; set; }
        public CourseStatus CourseStatus { get; set; }
        public Course()
        {
            Enrollments = new List<Enrollment>();
            Quizzes = new List<Quiz>();
        }
    }
}
