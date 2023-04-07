using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Patient Name")]
        public string? Name { get; set; }

        [Required]
        [Range(0, 120,ErrorMessage ="Age Can't be less than 0 and more than 120 years")]
        public int Age { get; set;}
    }
}
