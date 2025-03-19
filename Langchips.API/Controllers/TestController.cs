using Microsoft.AspNetCore.Mvc;

namespace Langchips.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: api/test
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = "API connection successful!" });
        }
    }
}
