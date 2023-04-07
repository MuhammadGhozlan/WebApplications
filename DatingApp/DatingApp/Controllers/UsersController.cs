using DatingApp.Data;
using DatingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public AppDbContext _context { get; set; }
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name ="Users")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users=await _context.AppUsers.ToListAsync();
            return users;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user=await _context.AppUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            //var specific_user = await _context.AppUsers.SingleOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}
