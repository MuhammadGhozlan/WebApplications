using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConvenientStoreWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Product's name")]
        public string? ProductName { get; set; }

        [Range(0, 200,ErrorMessage ="Item's Numbers can't be more than 200 or less than 0")]
        [DisplayName("Number of Items")]
        public double Quantity { get; set; }
        [DisplayName("Entered Date")]
        public DateTime ItemDate { get; set; }= DateTime.Now;
                
     
    }
}
