using MarchApi.Models;

namespace MarchApi.Services.Interfaces;

/// <summary> Service for managing token </summary>
public interface ITokenService
{
    string GenerateToken(AppUser appUser);
}
