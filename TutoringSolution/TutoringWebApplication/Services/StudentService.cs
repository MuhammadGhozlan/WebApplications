using TutoringWebApplication.Data;
using TutoringWebApplication.Dto;
using TutoringWebApplication.Interfaces.Repositories_Interfaces;
using TutoringWebApplication.Interfaces.Services_Interfaces;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Services
{
    public class StudentService : IStudentService
    {        
        private readonly ILogger<StudentService> _logger;
        private readonly IStudentRepository _studentRepository;

        public StudentService(ILogger<StudentService> logger,
                              IStudentRepository studentRepository)
        {            
            _logger = logger;
            _studentRepository = studentRepository;
        }
        public async Task<StudentDto?> CreateStudent(StudentDto studentDto)
        {
            var studentCreated = await _studentRepository.CreateStudent(studentDto);
            if(studentCreated == null)
            {
                _logger.LogError("Student was not created");
                return null;
            }
            return studentCreated;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                if(id == 0)
                {
                    return false;
                }

                var result = await _studentRepository.DeleteStudent(id);
                if(result == false)
                {
                    _logger.LogError("Couldn't delete the sudent");
                    return false;
                }
                return true;

            }
            catch(Exception ex)
            {
                _logger.LogError("Student was not deleted " + ex.Message);
                return false;
            }
        }

        public async Task<StudentDto?> EditStudent(int id, StudentDto studentDto)
        {
            try
            {
                if(id == 0)
                {
                    _logger.LogError("Id is zero");
                    return null;
                }
                var studentUpdated = await _studentRepository.EditStudent(id, studentDto);

                return studentUpdated;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Student cannot be updated. {ex.Message}");
                return null;
            }
        }

        public async Task<ICollection<StudentDto>> GetFilteredStudents(StudentFilterDto studentFilterDto)
        {
            try
            {
                var students = await _studentRepository.GetFilteredStudents(studentFilterDto);

                return students;
            }
            catch(Exception ex)
            {
                _logger.LogError($"List cannot be retrieved {ex.Message}");
                return new List<StudentDto>();
            }
        }

        public async Task<StudentDto?> GetStudent(int id)
        {
            try
            {
                if(id == 0)
                {
                    return null;
                }
                var student = await _studentRepository.GetStudent(id);
                if(student == null)
                {
                    _logger.LogError("No student found");
                    return null;
                }

                return student;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occured trying to retrieve the student: {ex.Message}");
                return null;
            }
        }

        public async Task<ICollection<StudentDto>> GetStudents()
        {
            try
            {
                var students = await _studentRepository.GetStudents();

                return students;
            }
            catch(Exception ex)
            {
                _logger.LogError($"List cannot be retrieved {ex.Message}");
                return new List<StudentDto>();
            }
        }
    }
}
