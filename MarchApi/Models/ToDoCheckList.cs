using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MarchApi.Models;

public class ToDoCheckList
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool IsDone { get; set; } = false;

    // relation
    public string? ToDoItemId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ToDoItem? ToDoItem { get; set; } // Navigation Property

    // audit properties
    public DateTime? CreatedTime { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public string? UpdatedBy { get; set; }

    public ToDoCheckList(string id, string text, bool isDone)
    {
        Id = id;
        Text = text;
        IsDone = isDone;
    }

}

