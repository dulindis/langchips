using Langchips.Data;
using Langchips.Models;
using Langchips.Models.DTOs;
using Langchips.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Langchips.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _apiBaseUrl;
        public UsersController(AppDbContext context,
            IConfiguration configuration
            )
        {
            _context = context;
            _apiBaseUrl = configuration["ApiBaseUrl"];
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            Console.WriteLine($"API Base URL: {_apiBaseUrl}");
            var users = await _context.Users.OrderByDescending(c => c.Id).ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDTO userDTO)
        {
            var otherUserWithSameEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            var otherUserWithSameUsername = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);

            if (otherUserWithSameEmail != null)
            {
                ModelState.AddModelError("Email", "User with this email already exists.");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            if(otherUserWithSameUsername != null)
            {
                ModelState.AddModelError("Username", "User with this username already exists.");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            if(userDTO.Password.Length < 8)
            {
                ModelState.AddModelError("Password", "Password must be at least 8 characters long.");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }

            var user = new User
            {
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Email = userDTO.Email,
                Username = userDTO.Username,
                //Phone = userDTO.Phone ?? ""
            };

            user.SetPassword(userDTO.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> EditUser(Guid id, UserDTO userDTO)
        {
            var otherUser = await _context.Users.FirstOrDefaultAsync(u => u.Id != id && u.Email == userDTO.Email);
            if (otherUser != null)
            {
                ModelState.AddModelError("Email", "The email address is already used.");
                var validation = new ValidationProblemDetails(ModelState);
                return BadRequest(validation);
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = userDTO.Name;
            user.Surname = userDTO.Surname;
            user.Email = userDTO.Email;
            user.Username = userDTO.Username;
            user.SetPassword(userDTO.Password);
            //user.Phone = userDTO.Phone ?? "";
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
    }
}

  