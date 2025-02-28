using MarchApi.Enums;

namespace MarchApi.Models;

public class ToDoItem
{
    // main properties
    public long Id { get; set; } = -1;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsDone { get; set; } = false;
    public PriorityEnum Priority { get; set; } = PriorityEnum.Medium;

    // audit properties
    public DateTime? CreatedTime { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public string? UpdatedBy { get; set; }

    public ToDoItem()
    {    
    }

    public ToDoItem(long id, string name, string? description = null, bool isDone = false, PriorityEnum priority = PriorityEnum.Medium)
    {
        Id = id;
        Name = name;
        Description = description;
        IsDone = isDone;
        Priority = priority;
    }
}
