using MarchApi.Dtos;
using MarchApi.Enums;
using MarchApi.Models;

namespace MarchApi.Services;

/// <summary> Service for Authenticate and Authorize </summary>
public interface IAuthService
{
    (ErrorCode code, string message, AppUser? user, string? token) Authenticate(LoginReqDto input);
}
