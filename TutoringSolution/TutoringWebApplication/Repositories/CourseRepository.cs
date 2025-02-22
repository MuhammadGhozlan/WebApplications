using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TutoringWebApplication.Data;
using TutoringWebApplication.Dto;
using TutoringWebApplication.Interfaces.Repositories_Interfaces;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataDbContext _dataDbContext;
        private readonly ILogger<CourseRepository> _logger;
        private readonly IMapper _mapper;

        public CourseRepository(DataDbContext dataDbContext,
                                ILogger<CourseRepository> logger,
                                IMapper mapper)
        {
            _dataDbContext = dataDbContext;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CourseDto?> CreateCourse(CourseDto courseDto)
        {
            try
            {
                if(courseDto == null)
                {
                    _logger.LogError("course is null");
                    return null;
                }
                var course = _mapper.Map<Course>(courseDto);
                await _dataDbContext.Courses.AddAsync(course);
                await _dataDbContext.SaveChangesAsync();
                return _mapper.Map<CourseDto>(course);

            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to create course " + ex.ToString());
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
                var course = await _dataDbContext.Courses.Include(c => c.Enrollments)
                                                         .Include(c => c.Instructor)
                                                         .Include(c => c.Quizzes).SingleOrDefaultAsync(c => c.Id == id);
                if(course == null)
                {
                    _logger.LogError("course is null");
                    return false;
                }
                _dataDbContext.Courses.Remove(course);
                await _dataDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to delete course " + ex.ToString());
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
                var course = await _dataDbContext.Courses.Include(c => c.Enrollments)
                                                         .Include(c => c.Instructor)
                                                         .Include(c => c.Quizzes).SingleOrDefaultAsync(c => c.Id == id);

                if(course == null)
                {
                    _logger.LogError("course is null");
                    return null;
                }

                _mapper.Map(courseDto, course);
                _dataDbContext.Courses.Update(course);
                await _dataDbContext.SaveChangesAsync();


                return _mapper.Map<CourseDto>(course);
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError("Failed to edit course " + ex.ToString());
                return null;
            }
        }

        public async Task<CourseDto?> GetCourse(int id)
        {
            try
            {
                var course = await _dataDbContext.Courses.SingleOrDefaultAsync(c => c.Id == id);

                if(course == null)
                {
                    _logger.LogError("course is null");
                    return null;
                }

                return _mapper.Map<CourseDto>(course);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to get the course " + ex.ToString());
                return null;
            }
        }

        public async Task<ICollection<CourseDto>> GetCourses()
        {
            try
            {
                var courses = await _dataDbContext.Courses.ToListAsync();
                return _mapper.Map<List<CourseDto>>(courses);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to get courses" + ex.ToString());
                return new List<CourseDto>(); ;
            }

        }

        public async Task<ICollection<CourseDto>> GetFilteredCourses(CourseFilterDto courseFilterDto)
        {
            try
            {
                IQueryable<Course> query = _dataDbContext.Courses.AsQueryable();

                if(courseFilterDto.Id != 0)
                {
                    query = query
                                 .Where(c => c.Id == courseFilterDto.Id);
                }
                if(!String.IsNullOrWhiteSpace(courseFilterDto.Title))
                {
                    query = query.Where(c => c.Title == courseFilterDto.Title);
                }
                if(courseFilterDto.CourseStatus != 0)
                {
                    query = query.Where(c => c.CourseStatus == courseFilterDto.CourseStatus);
                }
                var courses = await query.ToListAsync();

                return _mapper.Map<List<CourseDto>>(courses);
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to get courses " + ex.ToString());
                return new List<CourseDto>();
            }
        }
    }
}
