using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarchApi.Models;

public class ToDoTag
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    // relation
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ToDoItem>? ToDoItems { get; set; }

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

