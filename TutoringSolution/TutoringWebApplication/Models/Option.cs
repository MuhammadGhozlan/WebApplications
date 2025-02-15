using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutoringWebApplication.Models
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public bool IsCorrect { get; set; }        
        [Required]
        public string Text { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
    }
}
