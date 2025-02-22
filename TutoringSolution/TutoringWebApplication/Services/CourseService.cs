using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TutoringWebApplication.Dto;
using TutoringWebApplication.Interfaces.Repositories_Interfaces;
using TutoringWebApplication.Interfaces.Services_Interfaces;

namespace TutoringWebApplication.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseService> _logger;
        public CourseService(ICourseRepository courseRepository,
                             IMapper mapper,
                             ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CourseDto?> CreateCourse(CourseDto courseDto)
        {
            try
            {
                if(courseDto == null)
                {
                    _logger.LogError("Course is null");
                    return null;
                }                
                return await _courseRepository.CreateCourse(courseDto);
            }
            catch(Exception ex)
            {
                _logger.LogError("Course cannot be created " + ex.ToString());
                return null;
            }
        }

        public async Task<bool> DeleteCourse(int id)
        {
            try
            {
                if(id == 0)
                {
                    _logger.LogError("Id is null");
                    return false;
                }                
                return await _courseRepository.DeleteCourse(id);
            }
            catch(Exception ex)
            {
                _logger.LogError("Course cannot be deleted " + ex.ToString());
                return false;
            }
        }

        public async Task<CourseDto?> EditCourse(int id, CourseDto courseDto)
        {
            try
            {
                if(id == 0)
                {
                    _logger.LogError("Id is null");
                    return null;
                }
                if(courseDto == null)
                {
                    _logger.LogError("course is null");
                    return null;
                }                
                return await _courseRepository.EditCourse(id, courseDto);
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError("Course cannot be edited " + ex.ToString());
                return null;
            }
        }

        public async Task<CourseDto?> GetCourse(int id)
        {
            try
            {
                if(id == 0)
                {
                    _logger.LogError("Id is null");
                    return null;
                }                
                return await _courseRepository.GetCourse(id);
            }
            catch(Exception ex)
            {
                _logger.LogError("Course cannot be retrieved " + ex.ToString());
                return null;
            }
        }

        public async Task<ICollection<CourseDto>?> GetCourses()
        {
            try
            {                
                var coursesDto = await _courseRepository.GetCourses();
                return coursesDto;
            }
            catch(Exception ex)
            {
                _logger.LogError("Courses cannot be retrieved " + ex.ToString());
                return new List<CourseDto>();
            }
        }

        public async Task<ICollection<CourseDto>> GetFilteredCourses(CourseFilterDto courseFilterDto)
        {
            try
            {
                var coursesDto = await _courseRepository.GetFilteredCourses(courseFilterDto);
                return coursesDto;
            }
            catch(Exception ex)
            {
                _logger.LogError("Courses cannot be retrieved " + ex.ToString());
                return new List<CourseDto>();
            }
        }
    }
}
