using Microsoft.EntityFrameworkCore;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Data
{
    public class DataDbContext:DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options):base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Option> Options { get; set; }
    }
}
