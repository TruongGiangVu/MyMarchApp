using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using MarchApi.Dtos;
using MarchApi.Enums;

namespace MarchApi.Models;

public class ToDoItem
{

    // main properties
    [Key]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? Rate { get; set; }
    public string? Description { get; set; }
    public bool IsDone { get; set; } = false;
    public PriorityEnum Priority { get; set; } = PriorityEnum.Medium;

    // relation
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ToDoCheckList>? ToDoCheckLists { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ToDoTag>? ToDoTags { get; set; }


    // audit properties
    public DateTime? CreatedTime { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedTime { get; set; }
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
