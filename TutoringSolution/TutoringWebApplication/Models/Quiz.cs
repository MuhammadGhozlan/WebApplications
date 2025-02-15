using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutoringWebApplication.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
        public ICollection<Question> Questions { get; set; }

        public Quiz()
        {
            Questions = new List<Question>();
        }
    }
}
