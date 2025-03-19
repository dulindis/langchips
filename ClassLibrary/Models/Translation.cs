using Langchips.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Models.Models
{
    public class Translation : ITranslatable
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        [Required]
        public Language Language { get; set; }

        public int TermId { get; set; }
        public Term Term { get; set; } = null!;
    }
}
