using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutoringWebApplication.Dto;
using TutoringWebApplication.Interfaces.Services_Interfaces;

namespace TutoringWebApplication.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;
        public CourseController(ICourseService courseService,
                                ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("create")]
        public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CourseDto courseDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var course = await _courseService.CreateCourse(courseDto);

                    if(course != null)
                    {
                        return Ok(course);
                    }
                }
                return BadRequest("Course cannot be created");

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the course");
                return StatusCode(StatusCodes.Status500InternalServerError, "Course cannot be created");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("delete/{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                if(id != 0 && await _courseService.DeleteCourse(id))
                {
                    return Ok("Course has been deleted Successfully");
                }

                return BadRequest("Cannot Delete Course");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the course");
                return StatusCode(StatusCodes.Status500InternalServerError, "Course cannot be deleted");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("update/{id}")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(int id, [FromBody] CourseDto courseDto)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("Invalid course ID");
                }
                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid course data");
                }

                _logger.LogInformation($"Updating course with ID: {id}");

                var course = await _courseService.EditCourse(id, courseDto);
                if(course != null)
                {
                    return Ok(course);
                }

                _logger.LogError($"Course update failed: ID {id} not found or update unsuccessful.");
                return BadRequest("Course Not Updated");
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while editing the course");
                return StatusCode(StatusCodes.Status500InternalServerError, "Course cannot be edited");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("getCourse/{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("Id is not valid");
                }

                var course = await _courseService.GetCourse(id);
                if(course != null)
                {
                    return Ok(course);
                }

                _logger.LogError($"Course retrieval failed: ID {id} not found.");
                return NotFound($"Course with ID {id} not found.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the course");
                return StatusCode(StatusCodes.Status500InternalServerError, "Course cannot be retrieved");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("getCourses")]
        public async Task<ActionResult<ICollection<CourseDto>>> GetCourses()
        {
            try
            {
                var courses = await _courseService.GetCourses();
                return Ok(courses);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving courses");
                return StatusCode(StatusCodes.Status500InternalServerError, "Courses cannot be retrieved");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("getFilteredCourses")]
        public async Task<ActionResult<ICollection<CourseDto>>> GetFilteredCourses([FromBody] CourseFilterDto courseFilterDto)
        {
            try
            {
                if(courseFilterDto == null)
                {
                    return BadRequest("Invalid filter criteria");
                }

                var courses = await _courseService.GetFilteredCourses(courseFilterDto);
                return Ok(courses);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving filtered courses");
                return StatusCode(StatusCodes.Status500InternalServerError, "Courses cannot be retrieved");
            }
        }
    }
}
