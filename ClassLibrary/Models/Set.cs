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
        public Guid UserId { get; set; } //foreign key - always required
        public Folder? Folder { get; set; }//optional

        [Required]
        public Language TermLanguage { get; set; }

        [Required]
        public Language TranslationLanguage { get; set; }
        public ICollection<Term> Terms { get; set; } = new List<Term>();//at least two terms to ceate - in app as condition
        public Set() { }
        public Set(Guid userId, string name, Language termLanguage, Language translationLanguage, Folder? folder = null)
        {
            UserId = userId;
            Name = name;
            TermLanguage = termLanguage;
            TranslationLanguage = translationLanguage;
            Folder = folder;
        }
    }
}
