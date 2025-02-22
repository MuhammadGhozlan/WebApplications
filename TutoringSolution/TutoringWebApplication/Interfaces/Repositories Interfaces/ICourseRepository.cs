using TutoringWebApplication.Dto;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Interfaces.Repositories_Interfaces
{
    public interface ICourseRepository
    {
        Task<CourseDto> CreateCourse(CourseDto courseDto);
        Task<bool> DeleteCourse(int id);
        Task<CourseDto> EditCourse(int id, CourseDto courseDto);
        Task<CourseDto> GetCourse(int id);
        Task<ICollection<CourseDto>> GetCourses();
        Task<ICollection<CourseDto>> GetFilteredCourses(CourseFilterDto CourseFilterDto);
    }
}
