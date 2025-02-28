using MarchApi.Dtos;
using MarchApi.Enums;
using MarchApi.Models;

namespace MarchApi.Repositories;

public class ToDoItemRepository : IToDoItemRepository
{
    private readonly Serilog.ILogger _log = Log.ForContext<ToDoItemRepository>();
    private List<ToDoItem> data = [
        new(id:1, name:"Học lập trình", description:"Mô tả Học lập trình", isDone: true, priority : PriorityEnum.Medium),
        new(id:2, name:"Viết tài liệu", description:"Mô tả Viết tài liệu", isDone: false, priority : PriorityEnum.Low),
    ];

    public List<ToDoItem>? GetAll(ToDoItemSearchDto search)
    {
        List<ToDoItem>? res = data.Where(p =>
            (string.IsNullOrWhiteSpace(search.Name) || p.Name.Contains(search.Name))
            && (search.Priority is null || p.Priority == search.Priority)
        ).ToList();
        _log.Information($"{nameof(GetAll)} return count: {res.Count}");
        return res;
    }

    public ToDoItem? GetById(long id)
    {
        ToDoItem? res = data.Where(p => p.Id == id).FirstOrDefault();
        _log.Information($"{nameof(GetById)} return: {res.ToJsonString()}");
        return res;
    }

    public DbReturn Insert(ToDoItem entity)
    {
        _log.Information($"{nameof(Insert)} input: {entity.ToJsonString()}");
        DbReturn dbReturn = new();
        data.Add(entity);
        dbReturn.SetProperties(ErrorCode.Success);

        _log.Information($"{nameof(Insert)} return: {dbReturn.ToJsonString()}");
        return dbReturn;
    }

    public DbReturn Update(ToDoItem entity)
    {
        _log.Information($"{nameof(Update)} input: {entity.ToJsonString()}");
        DbReturn dbReturn = new();

        int idx = data.FindIndex(p => p.Id == entity.Id);
        if (idx >= 0)
        {
            data[idx] = entity;
            dbReturn.SetProperties(ErrorCode.Success);
        }
        else
        {
            dbReturn.SetProperties(ErrorCode.NotFound, ErrorCode.NotFound.GetDisplay());
        }

        _log.Information($"{nameof(Update)} return: {dbReturn.ToJsonString()}");
        return dbReturn;
    }

    public DbReturn Delete(long id)
    {
        _log.Information($"{nameof(Delete)} id: {id}");
        DbReturn dbReturn = new();
        int idx = data.FindIndex(p => p.Id == id);
        if (idx >= 0)
        {
            data.RemoveAt(idx);
            dbReturn.SetProperties(ErrorCode.Success);
        }
        else
        {
            dbReturn.SetProperties(ErrorCode.NotFound, ErrorCode.NotFound.GetDisplay());
        }

        _log.Information($"{nameof(Delete)} return: {dbReturn.ToJsonString()}");
        return dbReturn;
    }
}
