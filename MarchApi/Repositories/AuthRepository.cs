using MarchApi.Dtos;
using MarchApi.Models;

namespace MarchApi.Repositories;

public class AuthRepository : IAuthRepository
{
    public AppUser? GetUserWithPassword(LoginReqDto loginDto)
    {
        throw new NotImplementedException();
    }
}
