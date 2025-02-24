using AutoMapper;
using TutoringWebApplication.Dto;

namespace TutoringWebApplication.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourseDto, Course>();
            CreateMap<Course, CourseDto>();

            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentDto>();


        }
    }
}
