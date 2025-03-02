using System.Security.Claims;

namespace MarchApi.Utilities;

public static class Helper
{
    /// <summary> Truy vấn userId từ jwt token </summary>
    public static string? GetUserIdFromJwt(this HttpContext context)
    {
        // lấy value của claim có key là ClaimTypes.NameIdentifier, nếu không tồn tại thì để là ẩn danh "Anonymous"
        return context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
    }
}
