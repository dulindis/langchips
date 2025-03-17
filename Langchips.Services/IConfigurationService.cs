using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Services
{
    public interface IConfigurationService
    {
        string GetConnectionString(string name);
    }
}
