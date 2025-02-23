using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TutoringWebApplication.Data;
using TutoringWebApplication.Dto;
using TutoringWebApplication.Interfaces.Repositories_Interfaces;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMapper _mapper;
        private readonly ILogger<IStudentRepository> _logger;
        private readonly DataDbContext _dataDbContext;

        public StudentRepository(IMapper mapper,
                                 ILogger<IStudentRepository> logger,
                                 DataDbContext dataDbContext)
        {
            _mapper = mapper;
            _logger = logger;
            _dataDbContext = dataDbContext;
        }
        public async Task<StudentDto?> CreateStudent(StudentDto studentDto)
        {
            if(studentDto == null)
            {
                _logger.LogError("Student is null");
                return null;
            }
            var student = _mapper.Map<Student>(studentDto);
            await _dataDbContext.AddAsync(student);
            await _dataDbContext.SaveChangesAsync();
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<bool> DeleteStudent(int id)
        {
            if(id == 0)
            {
                _logger.LogError("Id is 0");
                return false;
            }

            var student = await _dataDbContext.Students.SingleOrDefaultAsync(s => s.Id == id);

            if(student == null)
            {
                _logger.LogError("Student is null");
                return false;
            }

            _dataDbContext.Students.Remove(student);
            await _dataDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<StudentDto?> EditStudent(int id, StudentDto studentDto)
        {
            if(id == 0)
            {
                _logger.LogError("Id is 0");
                return null;
            }
            if(studentDto == null)
            {
                _logger.LogError("Student is null");
                return null;
            }
            var student = await _dataDbContext.Students.SingleOrDefaultAsync(s => s.Id == id);
            if(student == null)
            {
                _logger.LogError("Student is null");
                return null;
            }
            _dataDbContext.Students.Update(_mapper.Map(studentDto, student));
            await _dataDbContext.SaveChangesAsync();
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<ICollection<StudentDto>?> GetFilteredStudents(StudentFilterDto studentFilterDto)
        {
            var query = _dataDbContext.Students.AsQueryable();
           
            if(studentFilterDto.Id != 0)
            {
                query = query.Where(q => q.Id == studentFilterDto.Id);
            }
            if(!String.IsNullOrWhiteSpace(studentFilterDto.Name))
            {
                query = query.Where(q => q.Name.ToLower() == studentFilterDto.Name.ToLower());
            }
            if(!String.IsNullOrWhiteSpace(studentFilterDto.Email))
            {
                query = query.Where(q => q.Email.ToLower() == studentFilterDto.Email.ToLower());
            }
            if(!String.IsNullOrWhiteSpace(studentFilterDto.PhoneNumber))
            {
                query = query.Where(q => q.PhoneNumber.ToLower() == studentFilterDto.PhoneNumber.ToLower());
            }

            var students = await query.ToListAsync();
            if(students == null)
            {
                return new List<StudentDto>();
            }
            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<StudentDto?> GetStudent(int id)
        {
            if(id == 0)
            {
                return null;
            }

            var student = await _dataDbContext.Students.SingleOrDefaultAsync(s => s.Id == id);

            if(student == null)
            {
                _logger.LogError("Student is not found");
                return null;
            }

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<ICollection<StudentDto>> GetStudents()
        {
            var students = await _dataDbContext.Students.ToListAsync();
            
            return _mapper.Map<List<StudentDto>>(students);
        }
    }
}
