using Microsoft.EntityFrameworkCore;
using MoviesStoreWeb.Models;

namespace MoviesStoreWeb.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}
