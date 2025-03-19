using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Models.Models
{
    public class Set
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string? Folder { get; set; } = "Default";
        [Required]
        public Guid UserId { get; set; } //foreign key - always required
        public User User { get; set; }
        [Required]
        public Language TermLanguage { get; set; }

        [Required]
        public Language TranslationLanguage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        //public Guid AuthorId { get; set; }  // Foreign key for Author
        //public User Author { get; set; } = null!; // Navigation property to User

        //public Guid OwnerId { get; set; }  // Foreign key for Owner
        //public User Owner { get; set; } = null!; // Navigation property to User

        public List<Term> Terms { get; set; } = new List<Term>(); // Ordered list of terms //at least two terms to ceate - in app as condition
        public Set() { }
        public Set(Guid userId, string name, Language termLanguage, Language translationLanguage, string? folder = "Default")
        {
            UserId = userId;
            Name = name;
            TermLanguage = termLanguage;
            TranslationLanguage = translationLanguage;
            Folder = folder;
        }
    }
}
