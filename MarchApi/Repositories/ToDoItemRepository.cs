using MarchApi.Dtos;
using MarchApi.Enums;
using MarchApi.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MarchApi.Repositories;

public class ToDoItemRepository : IToDoItemRepository
{
    private readonly Serilog.ILogger _log = Log.ForContext<ToDoItemRepository>();
    private readonly MarchContext _context;
    public ToDoItemRepository(MarchContext context)
    {
        _context = context;
    }
    public List<ToDoItem>? GetAll(ToDoItemSearchDto search)
    {
        _log.Information($"{nameof(GetAll)} search:{search.ToJsonString()}");
        
        List<ToDoItem>? res = _context.ToDoItems.Where(p =>
            (string.IsNullOrWhiteSpace(search.Name) || p.Name.Contains(search.Name))
            && (search.Priority == null || p.Priority == search.Priority)
        ).ToList();

        _log.Information($"{nameof(GetAll)} return count: {res.Count}");
        return res;
    }

    public ToDoItem? GetById(long id)
    {
        ToDoItem? res = _context.ToDoItems.Where(p => p.Id == id).FirstOrDefault();
        _log.Information($"{nameof(GetById)} return: {res.ToJsonString()}");
        return res;
    }

    public DbReturn Insert(ToDoItem entity)
    {
        _log.Information($"{nameof(Insert)} input: {entity.ToJsonString()}");
        DbReturn dbReturn = new();
        _context.ToDoItems.Add(entity);
        _context.SaveChanges();
        dbReturn.SetProperties(ErrorCode.Success);

        _log.Information($"{nameof(Insert)} return: {dbReturn.ToJsonString()}");
        return dbReturn;
    }

    public DbReturn Update(ToDoItem entity)
    {
        _log.Information($"{nameof(Update)} input: {entity.ToJsonString()}");
        DbReturn dbReturn = new();

        bool isExist = IsExist(entity.Id);
        if (isExist)
        {
            EntityEntry<ToDoItem> entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _context.ToDoItems.Attach(entity);
            }
            entry.State = EntityState.Modified;
            _context.SaveChanges();
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
        ToDoItem? entity = FindById(id);
        if (entity is not null)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.ToDoItems.Attach(entity);
            }
            _context.ToDoItems.Remove(entity);
            _context.SaveChanges();
            dbReturn.SetProperties(ErrorCode.Success);
        }
        else
        {
            dbReturn.SetProperties(ErrorCode.NotFound, ErrorCode.NotFound.GetDisplay());
        }

        _log.Information($"{nameof(Delete)} return: {dbReturn.ToJsonString()}");
        return dbReturn;
    }

    public bool IsExist(long id)
    {
        return _context.ToDoItems.Any(p => p.Id == id);
    }

    public ToDoItem? FindById(long id)
    {
        var entity = _context.ToDoItems.Where(p => p.Id == id).FirstOrDefault();
        return entity;
    }
}
