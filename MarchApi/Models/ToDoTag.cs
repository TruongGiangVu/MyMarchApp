namespace MarchApi.Models;

public class ToDoTag
{
    public string Id { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    // audit properties
    public DateTime? CreatedTime { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public string? UpdatedBy { get; set; }

    public ToDoTag(string id, string text, string color)
    {
        Id = id;
        Text = text;
        Color = color;
    }

}

