namespace MarchApi.Models;

public class DbUser
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public DbUser(string userId, string userName, string role, string password)
    {
        UserId = userId;
        UserName = userName;
        Role = role;
        Password = password;
    }
}
