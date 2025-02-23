using AutoMapper;
using TutoringWebApplication.Dto;

namespace TutoringWebApplication.Interfaces.Repositories_Interfaces
{
    public interface IStudentRepository
    {
        Task<StudentDto> CreateStudent(StudentDto studentDto);
        Task<bool> DeleteStudent(int id);
        Task<StudentDto> EditStudent(int id, StudentDto studentDto);
        Task<StudentDto> GetStudent(int id);
        Task<ICollection<StudentDto>> GetStudents();
        Task<ICollection<StudentDto>> GetFilteredStudents(StudentFilterDto studentFilterDto);
    }
}
