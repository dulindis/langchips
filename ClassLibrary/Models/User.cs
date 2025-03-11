using Langchips.Helpers;
using Langchips.Models.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        //TODO: pass stored as hash
        public string PasswordHash { get; private set; }

        [Required]
        [StringLength(50)]
        public string Salt { get; private set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; private set; }

        public ICollection<Folder> Folders { get; set; } = new List<Folder>();  // One-to-Many Relationship

        public User() {
            CreatedAt = DateTime.UtcNow;
        }

        public User(string name, string surname, string email, string password, string username)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Username = username; // TODO: ensure unique username <------
            SetPassword(password);
            //Folders = RetrieveUserFolders();
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            Salt = PasswordHasher.GenerateSalt();
            PasswordHash = PasswordHasher.HashPassword(password, Salt);
        }

        // Verify password
        public bool VerifyPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            return PasswordHasher.VerifyPassword(password, Salt, PasswordHash);
        }

        public void UpdateLastLogin()
        {
            LastLoginAt = DateTime.UtcNow;
        }
    }
}
