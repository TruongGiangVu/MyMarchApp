using MarchApi.Enums;

namespace MarchApi.Dtos;

public class ToDoItemSearchDto
{
    public string? Name { get; set; }
    public PriorityEnum? Priority { get; set; }
}
