using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutoringWebApplication.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        public Enrollment()
        {
            Date = DateOnly.FromDateTime(DateTime.Today);
        }
    }
}
