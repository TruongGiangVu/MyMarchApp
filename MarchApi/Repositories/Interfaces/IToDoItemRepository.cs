using MarchApi.Dtos;
using MarchApi.Models;

namespace MarchApi.Repositories.Interfaces;

/// <summary> ToDo item repository </summary>
public interface IToDoItemRepository
{
    List<ToDoItem>? GetAll(ToDoItemSearchDto search);
    ToDoItem? GetById(string id);
    DbReturn Insert(ToDoItemDto dto);
    DbReturn Update(ToDoItemDto dto);
    DbReturn Delete(string id);

    bool IsExist(string id);
    ToDoItem? FindById(string id);
}
