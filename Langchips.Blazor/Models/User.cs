using System.ComponentModel.DataAnnotations;

namespace Langchips.Blazor.Models
{
    public class User
    {

        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        //public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
