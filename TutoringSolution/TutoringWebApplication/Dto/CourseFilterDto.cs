using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Dto
{
    public class CourseFilterDto
    {       
        public int Id { get; set; }        
        public string Title { get; set; }     
        public CourseStatus CourseStatus { get; set; }        
    }
}
