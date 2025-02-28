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
        ErrorCode code = ErrorCode.Unknow;
        string? token = null;

        // truy vấn user với mật khẩu
        AppUser? user = _authRepository.GetUserWithPassword(input);

        // user có tồn tại thì tạo token
        if (user is not null)
        {
            token = _tokenService.GenerateToken(user);
            if (!string.IsNullOrEmpty(token))
            {
                code = ErrorCode.Success;
            }
        }
        else // user sai tài khoản hoặc mật khẩu
        {
            code = ErrorCode.InvalidUserPass;
        }

        string message = code.GetDisplay();
        return (code, message, user, token);
    }
}
