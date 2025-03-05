using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarchApi.Models;

[Table("TODO_TAG")]
public class ToDoTag
{
    [Key]
    [Column("ID", TypeName = "VARCHAR(100)")]
    public string Id { get; set; } = string.Empty;

    [Column("TEXT", TypeName = "VARCHAR(50)")]
    public string Text { get; set; } = string.Empty;

    [Column("COLOR", TypeName = "VARCHAR(50)")]
    public string Color { get; set; } = string.Empty;

    // relation
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ToDoItem>? ToDoItems { get; set; }

    // audit properties
    [Column("CREATED_TIME", TypeName = "DATE")]
    public DateTime? CreatedTime { get; set; }

    [Column("CREATED_BY", TypeName = "VARCHAR(100)")]
    public string? CreatedBy { get; set; }

    [Column("UPDATED_TIME", TypeName = "DATE")]
    public DateTime? UpdatedTime { get; set; }

    [Column("UPDATED_BY", TypeName = "VARCHAR(100)")]
    public string? UpdatedBy { get; set; }

    public ToDoTag(string id, string text, string color)
    {
        Id = id;
        Text = text;
        Color = color;
    }

}

