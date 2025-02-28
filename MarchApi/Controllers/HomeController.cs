using Microsoft.AspNetCore.Mvc;

namespace MarchApi.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("service is running");
    }
}
