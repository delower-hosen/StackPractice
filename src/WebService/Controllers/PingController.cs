using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    public class PingController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Healthy");
    }
}
