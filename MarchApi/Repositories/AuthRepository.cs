using MarchApi.Dtos;
using MarchApi.Models;

namespace MarchApi.Repositories;

public class AuthRepository : IAuthRepository
{
    // private readonly Serilog.ILogger _log = Log.ForContext<AuthRepository>();
    private readonly List<OracleUser> data = [
        new(userId:"giang", userName:"giang vu", role:Ct.Role.User, password: "123456"),
        new(userId:"admin", userName:"admin name",role: Ct.Role.Admin, password:"123456"),
    ];

    public AppUser? GetUserWithPassword(LoginReqDto loginDto)
    {
        AppUser? response = null;

        OracleUser? oracleUser = data.Where(p => p.UserId == loginDto.UserId && p.Password == loginDto.Password).FirstOrDefault();

        if (oracleUser is not null)
        {
            response = new AppUser(oracleUser);
        }

        return response;
    }
}
