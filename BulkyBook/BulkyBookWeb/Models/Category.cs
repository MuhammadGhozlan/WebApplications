using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order Can't be Less Than 1 And More Than 100")]
        public int DisplayOrder { get; set; }

        [DisplayName("Created Date and Time")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
