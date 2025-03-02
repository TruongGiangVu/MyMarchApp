using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarchApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    /// <summary> Api chỉ cho phép user có quyền admin only </summary>
    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [Authorize(Roles = Ct.Role.Admin)]
    public IActionResult Index()
    {
        return Ok("Allow admin only");
    }
}
