using Langchips.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Models.Interfaces
{
    public interface ITranslatable
    {
        string Content { get; set; }
        Language Language { get; set; }
    }
}
