using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using MarchApi.Models;
using MarchApi.Settings;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MarchApi.Services;

public class TokenService : ITokenService
{
    private readonly Serilog.ILogger _log = Log.ForContext<TokenService>();
    private readonly TokenSettings _tokenSettings;

    public TokenService(IOptions<TokenSettings> tokenSettings)
    {
        _tokenSettings = tokenSettings.Value;
    }
    public string GenerateToken(AppUser appUser)
    {
        _log.Information($"{nameof(GenerateToken)} for user {appUser.UserId}");

        try
        {
            // tạo list claim với 1 số thông tin cần thiết cho token, những thông này sẽ lưu trong token và có thể truy vấn được
            Claim[]? claims = [
                new Claim(ClaimTypes.NameIdentifier, appUser.UserId),
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(ClaimTypes.Role, appUser.Role)
            ];

            // tạo key với sign
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // tạo token
            var token = new JwtSecurityToken(
                _tokenSettings.Issuer, null,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenSettings.ExpiredToken),
                signingCredentials: signIn);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            // _log.Information($"{nameof(GenerateToken)} tokenValue: {tokenValue}");

            return tokenValue;
        }
        catch (Exception ex)
        {
            _log.Error($"{nameof(GenerateToken)} Exception: {ex}");
            return string.Empty;
        }
    }
}
