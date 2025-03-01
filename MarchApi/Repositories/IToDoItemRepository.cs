using MarchApi.Dtos;
using MarchApi.Models;

namespace MarchApi.Repositories;

/// <summary> ToDo item repository </summary>
public interface IToDoItemRepository
{
    List<ToDoItem>? GetAll(ToDoItemSearchDto search);
    ToDoItem? GetById(string id);
    DbReturn Insert(ToDoItem entity);
    DbReturn Update(ToDoItem entity);
    DbReturn Delete(string id);
}
