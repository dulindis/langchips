using Langchips.Data;
using Langchips.Models.Models;
using Langchips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Services
{

    public class UserService
    {
        private readonly AppDbContext _context;



        //public void CreateUser(string name, string surname, string email, string password)
        //{
        //    var user = new User
        //    {
        //        Name = name,
        //        Surname = surname,
        //        Email = email
        //    };
        //    user.SetPassword(password);

        //    _context.Users.Add(user);
        //    _context.SaveChanges();  

        //    // Optional: Manually log user creation
        //    var log = new AuditLog
        //    {
        //        UserId = user.Id,  // Assuming user is now created and has an ID
        //        Action = "User Created",
        //        ActionDate = DateTime.UtcNow,
        //        Details = $"User {name} {surname} created with email {email}"
        //    };

        //    _context.AuditLogs.Add(log);
        //    _context.SaveChanges();
        //}
    }
}
