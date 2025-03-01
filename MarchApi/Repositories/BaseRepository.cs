using MarchApi.Utilities;

namespace MarchApi.Repositories;

public class BaseRepository
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public BaseRepository(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <summary> Truy vấn userId hiện tại </summary>
    protected string? GetCurrentUserId() => httpContextAccessor.HttpContext?.GetUserIdFromJwt();
}