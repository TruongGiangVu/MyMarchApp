using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarchApi.Models;

[Table("TODO_CHECKLIST")]
public class ToDoCheckList
{
    [Key]
    [Column("ID", TypeName = "VARCHAR(100)")]
    public string Id { get; set; } = string.Empty;

    [Column("TEXT", TypeName = "VARCHAR(100)")]
    public string Text { get; set; } = string.Empty;

    [Column("IS_DONE", TypeName = "VARCHAR2(5)")]
    public bool IsDone { get; set; } = false;

    // relation
    [Column("ITEM_ID", TypeName = "VARCHAR(100)")]
    public string? ToDoItemId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ToDoItem? ToDoItem { get; set; } // Navigation Property

    // audit properties
    [Column("CREATED_TIME", TypeName = "DATE")]
    public DateTime? CreatedTime { get; set; }

    [Column("CREATED_BY", TypeName = "VARCHAR(100)")]
    public string? CreatedBy { get; set; }

    [Column("UPDATED_TIME", TypeName = "DATE")]
    public DateTime? UpdatedTime { get; set; }

    [Column("UPDATED_BY", TypeName = "VARCHAR(100)")]
    public string? UpdatedBy { get; set; }

    public ToDoCheckList(string id, string text, bool isDone)
    {
        Id = id;
        Text = text;
        IsDone = isDone;
    }

}

