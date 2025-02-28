using MarchApi.Dtos;
using MarchApi.Models;

namespace MarchApi.Repositories;

/// <summary> Auth repository </summary>
public interface IAuthRepository
{
    AppUser? GetUserWithPassword(LoginReqDto loginDto);
}
