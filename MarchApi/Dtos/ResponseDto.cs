using System.Text.Json.Serialization;

using MarchApi.Enums;

namespace MarchApi.Dtos;

/// <summary>
/// wrapper class for responsing from api
/// </summary>
public class ResponseDto
{
    [JsonPropertyOrder(1)]
    public bool IsSuccess => ErrCode == ErrorCode.Success;

    [JsonPropertyOrder(2)]
    public string Code => ErrCode.ToErrorCodeString();

    [JsonIgnore]
    public ErrorCode ErrCode { get; set; } = ErrorCode.Unknow;

    [JsonPropertyOrder(3)]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyOrder(4)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Details { get; set; } = null;

    public ResponseDto(ErrorCode code = ErrorCode.Unknow, string? message = null, List<string>? details = null)
    {
        SetProperties(code, message, details);
    }

    public void SetProperties(ErrorCode code = ErrorCode.Unknow, string? message = null, List<string>? details = null)
    {
        ErrCode = code;
        Message = string.IsNullOrEmpty(message) ?
                            ErrCode.GetDisplay() :
                            message;
        Details = details;
    }
    public void Success()
    {
        SetProperties(ErrorCode.Success);
    }

    public string ToLogString()
    {
        return $"Code:{Code}, Message:{Message}, Details:{Details.ToJsonString()}";
    }
}

public class ResponseDto<T> : ResponseDto
{
    [JsonPropertyOrder(5)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Payload { get; set; } = default;

    public ResponseDto(ErrorCode code = ErrorCode.Unknow,
                    string? message = null,
                    T? payload = default,
                    List<string>? details = null) : base(code, message, details)
    {
        AttachPayload(payload);
    }

    public void AttachPayload(T? payload) => Payload = payload;

    public void Success(T? payload = default)
    {
        base.Success();
        AttachPayload(payload);
    }
}