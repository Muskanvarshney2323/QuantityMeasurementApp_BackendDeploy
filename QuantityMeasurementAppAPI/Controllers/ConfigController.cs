using Microsoft.AspNetCore.Mvc;

namespace QuantityMeasurementApp.Controllers
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

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new
            {
                success = true,
                message = "API is running",
                port = 5020,
                baseUrl = "http://localhost:5020/api"
            });
        }
    }
}
