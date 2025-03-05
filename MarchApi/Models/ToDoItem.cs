using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using MarchApi.Dtos;
using MarchApi.Enums;

namespace MarchApi.Models;

[Table("TODO_ITEM")]
public class ToDoItem
{

    // main properties
    [Key]
    [Column("ID", TypeName = "VARCHAR(100)")]
    public string Id { get; set; } = string.Empty;

    [Column("NAME", TypeName = "VARCHAR(100)")]
    public string Name { get; set; } = string.Empty;

    [Column("RATE", TypeName = "NUMBER")]
    public int? Rate { get; set; }

    [Column("DESCRIPTION", TypeName = "VARCHAR(200)")]
    public string? Description { get; set; }

    [Column("IS_DONE", TypeName = "VARCHAR2(5)")]
    public bool IsDone { get; set; } = false;

    [Column("PRIORITY", TypeName = "VARCHAR2(20)")]
    public PriorityEnum Priority { get; set; } = PriorityEnum.Medium;

    // relation
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ToDoCheckList>? ToDoCheckLists { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ToDoTag>? ToDoTags { get; set; }


    // audit properties
    [Column("CREATED_TIME", TypeName = "DATE")]
    public DateTime? CreatedTime { get; set; }

    [Column("CREATED_BY", TypeName = "VARCHAR(100)")]
    public string? CreatedBy { get; set; }

    [Column("UPDATED_TIME", TypeName = "DATE")]
    public DateTime? UpdatedTime { get; set; }

    [Column("UPDATED_BY", TypeName = "VARCHAR(100)")]
    public string? UpdatedBy { get; set; }

    public ToDoItem()
    {
    }

    public ToDoItem(string id,
                    string name,
                    string? description = null,
                    bool isDone = false,
                    int? rate = null,
                    PriorityEnum priority = PriorityEnum.Medium
                )
    {
        Id = id;
        Name = name;
        Description = description;
        IsDone = isDone;
        Priority = priority;
        Rate = rate;
    }

    public ToDoItem(ToDoItemDto dto)
    {
        ConvertFromDto(dto);
    }

    /// <summary> Map các trường từ ToDoItemDto sang ToDoItem </summary>
    public void ConvertFromDto(ToDoItemDto dto)
    {
        Id = dto.Id ?? string.Empty;
        Name = dto.Name;
        Description = dto.Description;
        IsDone = dto.IsDone;
        Priority = dto.Priority ?? PriorityEnum.Medium;
        Rate = dto.Rate;
    }
}
