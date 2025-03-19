using Langchips.Data;
using Langchips.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Langchips.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/users/id/{id}
        [HttpGet("id/{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        // GET api/users/username/{username}
        [HttpGet("username/{username}")]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {
            var user = await _context.Users
                                      .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/users/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> GetUserByUsernameAndPassword([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                                      .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Here, you should compare the password hash.
            // In a real-world application, you'd check if the password hash matches the stored hash.
            if (user.PasswordHash != request.Password)
            {
                return Unauthorized("Invalid password.");
            }

            return Ok(user);
        }

    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
