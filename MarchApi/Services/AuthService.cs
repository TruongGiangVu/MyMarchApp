using MarchApi.Dtos;
using MarchApi.Enums;
using MarchApi.Models;
using MarchApi.Repositories;

namespace MarchApi.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IAuthRepository _authRepository;

    public AuthService(ITokenService tokenService, IAuthRepository authRepository)
    {
        _tokenService = tokenService;
        _authRepository = authRepository;
    }

    public (ErrorCode code, string message, AppUser? user, string? token) Authenticate(LoginReqDto input)
    {
        throw new NotImplementedException();
    }
}
