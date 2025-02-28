using MarchApi.Dtos;
using MarchApi.Models;

namespace MarchApi.Repositories;

/// <summary> Auth repository </summary>
public interface IAuthRepository
{
    /// <summary>
    /// truy vấn user với mật khẩu
    /// </summary>
    /// <param name="loginDto"> Thông tin đăng nhập </param>
    /// <returns> Thông tin user hoặc null </returns>
    AppUser? GetUserWithPassword(LoginReqDto loginDto);
}
