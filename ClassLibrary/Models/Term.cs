using Langchips.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Langchips.Models.Models
{
    public class Term
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string InputPhrase { get; set; }
        //TODO: think about sound file

        [Required]
        public int SetId { get; set; }
        public Set Set { get; set; }
        public List<Translation> Translations { get; set; } = new List<Translation>(); // Up to 3 translations
    }
}
