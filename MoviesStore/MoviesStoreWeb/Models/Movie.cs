using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MoviesStoreWeb.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MovieName { get; set; }

        [DisplayName("Time of Selling")]
        public DateTime TimeOfSelling { get; set; }=DateTime.Now;

        [Range(1, 100, ErrorMessage = "Number of Copies can't be less than 1 and can't exceed 100")]
        [DisplayName("Number of Copies")]
        public int NumbersOfCopies { get; set; }
     

        [Range(1, 100, ErrorMessage = "Quantity can't be less than 1 and can't exceed 100")]
        public int Quantity { get; set; }
        

        public int Copies()
        {
            return NumbersOfCopies - Quantity;
        }

        
    }
}
