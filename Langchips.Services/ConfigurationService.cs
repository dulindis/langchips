using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langchips.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnectionString(string name = "DefaultConnection")
        {
            return _configuration.GetConnectionString(name)
                   ?? throw new InvalidOperationException($"Database connection string '{name}' is missing.");
        }
    }
}
