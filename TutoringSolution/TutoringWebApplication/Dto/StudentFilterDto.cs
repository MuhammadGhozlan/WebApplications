using System.ComponentModel.DataAnnotations;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Dto
{
    public class StudentFilterDto
    {     
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }          
    }
}
