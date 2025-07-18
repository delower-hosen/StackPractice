using Microsoft.AspNetCore.Mvc;
using Shared.Attributes;

namespace WebService.Controllers
{
    public class PingController : ApiControllerBase
    {
        [HttpGet]
        [RateLimit("60s", 3)]
        public IActionResult Get() => Ok("Healthy");
    }
}
