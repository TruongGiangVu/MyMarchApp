using MarchApi.Models;

namespace MarchApi.Dtos;

public class LoginResDto : AppUser
{
    public string AccessToken { get; set; } = string.Empty;

    public LoginResDto(AppUser? appUser, string? accessToken)
    {
        UserId = appUser?.UserId ?? string.Empty;
        UserName = appUser?.UserName ?? string.Empty;
        Role = appUser?.Role ?? string.Empty;
        AccessToken = accessToken ?? string.Empty;
    }
};
