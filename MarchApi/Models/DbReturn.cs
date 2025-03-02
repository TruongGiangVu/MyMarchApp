using MarchApi.Enums;

namespace MarchApi.Models;

public class DbReturn
{
    public ErrorCode Code { get; set; } = ErrorCode.UnKnow;
    public string Message { get; set; } = string.Empty;

    public DbReturn(ErrorCode code = ErrorCode.UnKnow, string message = "")
    {
        SetProperties(code, message);
    }

    public void SetProperties(ErrorCode code = ErrorCode.UnKnow, string message = "")
    {
        Code = code;
        Message = message;
    }
}

public class DbReturn<T> : DbReturn
{
    public T? Payload { get; set; } = default;

    public DbReturn(ErrorCode code = ErrorCode.UnKnow, string message = "", T? payload = default)
    {
        Code = code;
        Message = message;
        Payload = payload;
    }
}
