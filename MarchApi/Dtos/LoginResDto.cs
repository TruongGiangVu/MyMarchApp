using MarchApi.Models;

namespace MarchApi.Dtos;

public class LoginResDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public LoginResDto(AppUser? appUser, string? accessToken)
    {
        UserId = appUser?.UserId ?? string.Empty;
        UserName = appUser?.UserName ?? string.Empty;
        Role = appUser?.Role ?? string.Empty;
        AccessToken = accessToken ?? string.Empty;
    }

    public string ToLogString()
    {
        return $"UserId:{UserId}, UserName:{UserName}, Role:{Role}";
    }
};
