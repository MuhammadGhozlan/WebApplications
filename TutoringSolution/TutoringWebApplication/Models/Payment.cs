using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutoringWebApplication.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required, Range(0.0, 200.0)]
        public decimal Amount { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public PaymentStatus PaymentStatus { get; set; }
        [ForeignKey(nameof(StudentId))]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public Payment()
        {
            PaymentDate = DateTime.Now;
        }
    }
}
