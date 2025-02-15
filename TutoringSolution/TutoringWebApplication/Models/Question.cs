using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutoringWebApplication.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public int QuizId { get; set; }
        [ForeignKey(nameof(QuizId))]
        public Quiz Quiz { get; set; }
        public ICollection<Option> Options { get; set; }
        public Question()
        {
            Options=new List<Option>();
        }
    }
}
