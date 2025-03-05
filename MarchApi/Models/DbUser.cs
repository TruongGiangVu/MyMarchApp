using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarchApi.Models;

[Table("DB_USER")]
public class DbUser
{
    [Key]
    [Column("USER_ID", TypeName = "VARCHAR(100)")] // Set column name & type
    public string UserId { get; set; } = string.Empty;

    [Column("USER_NAME", TypeName = "VARCHAR(100)")]
    public string UserName { get; set; } = string.Empty;

    [Column("ROLE", TypeName = "VARCHAR(50)")]
    public string Role { get; set; } = string.Empty;

    [Column("PASSWORD", TypeName = "VARCHAR(100)")]
    public string Password { get; set; } = string.Empty;

    public DbUser(string userId, string userName, string role, string password)
    {
        UserId = userId;
        UserName = userName;
        Role = role;
        Password = password;
    }
}
