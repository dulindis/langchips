using Langchips.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Models.Models
{
    public class TextEntry : ITranslatable
    {
        public string Content { get; set; }
        public Language Language { get; set; }
    }
}
