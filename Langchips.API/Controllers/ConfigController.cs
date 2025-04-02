using Microsoft.AspNetCore.Mvc;

namespace Langchips.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("apiBaseUrl")]
        public IActionResult GetApiBaseUrl()
        {
            var apiBaseUrl = _configuration["ApiBaseUrl"];
            if (apiBaseUrl == null)
            {
                return NotFound("API Base URL not configured.");
            }

            return Ok(apiBaseUrl);  // Return the ApiBaseUrl from the server-side configuration
        }
    }

}
