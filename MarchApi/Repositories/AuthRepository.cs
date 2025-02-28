using MarchApi.Dtos;
using MarchApi.Models;

namespace MarchApi.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly MarchContext _context;

    // private readonly Serilog.ILogger _log = Log.ForContext<AuthRepository>();
    public AuthRepository(MarchContext context)
    {
        _context = context;
    }
    public AppUser? GetUserWithPassword(LoginReqDto loginDto)
    {
        AppUser? response = null;

        DbUser? oracleUser = _context.DbUsers.Where(p => p.UserId == loginDto.UserId && p.Password == loginDto.Password).FirstOrDefault();

        if (oracleUser is not null)
        {
            response = new AppUser(oracleUser);
        }

        return response;
    }
}
