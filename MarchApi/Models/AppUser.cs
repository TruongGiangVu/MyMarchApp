namespace MarchApi.Models;

public class AppUser
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public AppUser()
    {
    }
    
    public AppUser(string userId, string userName, string role)
    {
        UserId = userId;
        UserName = userName;
        Role = role;
    }

    public AppUser(OracleUser oracleUser)
    {
        UserId = oracleUser.UserId;
        UserName = oracleUser.UserName;
        Role = oracleUser.Role;
    }
}
