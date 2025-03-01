using MarchApi.Models;

namespace MarchApi.Repositories;

public interface ITagRepository
{
    List<ToDoTag>? GetAll();
    ToDoTag? GetById(string id);
    DbReturn Insert(ToDoTag entity);
    DbReturn Update(ToDoTag entity);
    DbReturn Delete(string id);
}