using TutoringWebApplication.Dto;

namespace TutoringWebApplication.Interfaces.Services_Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> CreateStudent(StudentDto studentDto);
        Task<bool> DeleteStudent(int id);
        Task<StudentDto> EditStudent(int id, StudentDto studentDto);
        Task<StudentDto> GetStudent(int id);
        Task<ICollection<StudentDto>> GetStudents();
        Task<ICollection<StudentDto>> GetFilteredStudents(StudentFilterDto studentFilterDto);
    }
}
