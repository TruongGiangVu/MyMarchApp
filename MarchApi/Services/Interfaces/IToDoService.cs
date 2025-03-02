using MarchApi.Dtos;

namespace MarchApi.Services.Interfaces;

/// <summary> Service for ToDo module </summary>
public interface IToDoService
{
    ResponseDto CreateToDoItem(ToDoItemDto input);
    ResponseDto UpdateToDoItem(ToDoItemDto input);
}
