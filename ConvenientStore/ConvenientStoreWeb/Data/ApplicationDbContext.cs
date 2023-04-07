using ConvenientStoreWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ConvenientStoreWeb.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
