using MarchApi.Models;

namespace MarchApi.Services;

/// <summary> Service for managing token </summary>
public interface ITokenService
{
    string GenerateToken(AppUser appUser);
}
