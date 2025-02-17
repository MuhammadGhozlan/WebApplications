using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TutoringWebApplication.Models;

namespace TutoringWebApplication.Data
{
    public class UserDbContext:IdentityDbContext<User>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        
    }
}
