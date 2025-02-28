using System.Text.Json.Serialization;

using MarchApi.Enums;

namespace MarchApi.Dtos;

public class ResponseDto
{
    [JsonPropertyOrder(1)]
    public bool IsSuccess => Code == ErrorCode.Success;

    [JsonPropertyOrder(2)]
    public ErrorCode Code { get; set; } = ErrorCode.Unknow;

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
        Code = code;
        Message = string.IsNullOrEmpty(message) ?
                            Code.GetDisplay() :
                            message;
        Details = details;
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
}