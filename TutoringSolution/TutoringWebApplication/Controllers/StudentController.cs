using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TutoringWebApplication.Dto;
using TutoringWebApplication.Interfaces.Services_Interfaces;

namespace TutoringWebApplication.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentService studentService,
                                ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("create")]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] StudentDto studentDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid student data");
                }

                var student = await _studentService.CreateStudent(studentDto);

                if(student != null)
                {
                    return Ok(student);
                }

                return BadRequest("student cannot be created");

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the student");
                return StatusCode(StatusCodes.Status500InternalServerError, "student cannot be created");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("delete/{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                if(id != 0 && await _studentService.DeleteStudent(id))
                {
                    return Ok("student has been deleted Successfully");
                }

                return BadRequest("Cannot Delete student");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the student");
                return StatusCode(StatusCodes.Status500InternalServerError, "student cannot be deleted");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("update/{id}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent(int id, [FromBody] StudentDto studentDto)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("Invalid student ID");
                }
                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid student data");
                }

                _logger.LogInformation($"Updating student with ID: {id}");

                var student = await _studentService.EditStudent(id, studentDto);
                if(student != null)
                {
                    return Ok(student);
                }

                _logger.LogError($"student update failed: ID {id} not found or update unsuccessful.");
                return BadRequest("student Not Updated");
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while editing the student");
                return StatusCode(StatusCodes.Status500InternalServerError, "student cannot be edited");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("GetStudent/{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("Id is not valid");
                }

                var student = await _studentService.GetStudent(id);
                if(student != null)
                {
                    return Ok(student);
                }

                _logger.LogError($"student retrieval failed: ID {id} not found.");
                return NotFound($"student with ID {id} not found.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the student");
                return StatusCode(StatusCodes.Status500InternalServerError, "student cannot be retrieved");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("GetStudents")]
        public async Task<ActionResult<ICollection<StudentDto>>> GetStudents()
        {
            try
            {
                var students = await _studentService.GetStudents();
                return Ok(students);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving students");
                return StatusCode(StatusCodes.Status500InternalServerError, "students cannot be retrieved");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("getFilteredStudents")]
        public async Task<ActionResult<ICollection<StudentDto>>> GetFilteredStudents([FromBody] StudentFilterDto studentFilterDto)
        {
            try
            {
                if(studentFilterDto == null)
                {
                    return BadRequest("Invalid filter criteria");
                }

                var students = await _studentService.GetFilteredStudents(studentFilterDto);
                return Ok(students);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving filtered students");
                return StatusCode(StatusCodes.Status500InternalServerError, "students cannot be retrieved");
            }
        }
    }
}
