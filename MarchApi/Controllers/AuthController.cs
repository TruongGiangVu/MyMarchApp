using MarchApi.Dtos;
using MarchApi.Enums;
using MarchApi.Models;
using MarchApi.Services;

using Microsoft.AspNetCore.Mvc;

namespace MarchApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly Serilog.ILogger _log = Log.ForContext<AuthController>();
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary> Đăng nhập và trả về jwt token </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseDto<LoginResDto?>), StatusCodes.Status200OK)]
    public IActionResult Login([FromBody] LoginReqDto input)
    {
        _log.Information($"{nameof(Login)} userId:{input.UserId}");
        var response = new ResponseDto<LoginResDto?>();

        (ErrorCode code, string message, AppUser? user, string? token) = _authService.Authenticate(input);

        if (code == ErrorCode.Success)
        {
            response.SetProperties(code, message);
            response.AttachPayload(new LoginResDto(user, token));
            return Ok(response);
        }
        else
        {
            response.SetProperties(code, message);
            return Ok(response);
        }
    }
}
