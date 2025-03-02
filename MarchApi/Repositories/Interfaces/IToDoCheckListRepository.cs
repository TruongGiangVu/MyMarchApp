using MarchApi.Models;

namespace MarchApi.Repositories.Interfaces;

/// <summary> ToDo check list repository </summary>
public interface IToDoCheckListRepository
{
    List<ToDoCheckList>? GetAll();
    ToDoCheckList? GetById(string id);
    DbReturn Insert(ToDoCheckList entity);
    DbReturn Update(ToDoCheckList entity);
    DbReturn Delete(string id);

    bool IsExist(string id);
    ToDoCheckList? FindById(string id);
}
