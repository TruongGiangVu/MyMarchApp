using MarchApi.Dtos;
using MarchApi.Enums;
using MarchApi.Models;
using MarchApi.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MarchApi.Repositories.Implements;

public class ToDoItemRepository : BaseRepository, IToDoItemRepository
{
    private readonly Serilog.ILogger _log = Log.ForContext<ToDoItemRepository>();
    private readonly MarchContext _context;
    public ToDoItemRepository(MarchContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _context = context;
    }
    public List<ToDoItem>? GetAll(ToDoItemSearchDto search)
    {
        _log.Information($"{nameof(GetAll)} search:{search.ToJsonString()}");

        // thực hiện tìm kiếm theo class search
        List<ToDoItem>? res = _context.ToDoItems.Where(p =>
            (string.IsNullOrWhiteSpace(search.Name) || p.Name.Contains(search.Name))
            && (search.Priority == null || p.Priority == search.Priority)
        ).ToList();

        _log.Information($"{nameof(GetAll)} return count: {res.Count}");
        return res;
    }

    public ToDoItem? GetById(string id)
    {
        ToDoItem? res = _context.ToDoItems.Where(p => p.Id == id).FirstOrDefault();
        _log.Information($"{nameof(GetById)} return: {res.ToJsonString()}");
        return res;
    }

    public DbReturn Insert(ToDoItemDto dto)
    {
        DbReturn dbReturn = new();

        var entity = new ToDoItem(dto);
        // lấy userId hiện tại từ jwt token và set thời gian create update cho entity
        string? userId = GetCurrentUserId();
        DateTime time = DateTime.Now;
        entity.CreatedBy = userId;
        entity.CreatedTime = time;
        entity.UpdatedBy = userId;
        entity.UpdatedTime = time;

        // tạo id mới
        entity.Id = Guid.NewGuid().ToString();

        _log.Information($"{nameof(Insert)} input: {entity.ToJsonString()}");

        // thêm vào db và commit lại
        _context.ToDoItems.Add(entity);
        _context.SaveChanges();

        // nếu thành công set lại code và message trả về
        dbReturn.SetProperties(ErrorCode.Success);

        _log.Information($"{nameof(Insert)} return: {dbReturn.ToJsonString()}");
        return dbReturn;
    }

    public DbReturn Update(ToDoItemDto dto)
    {
        DbReturn dbReturn = new();

        // kiểm tra xem id này có tồn tại không
        ToDoItem? entity = FindById(dto.Id ?? string.Empty);

        if (entity is not null) // nếu tồn tại thì cập nhật
        {
            entity.ConvertFromDto(dto);
            // lấy userId hiện tại từ jwt token và set thời gian update cho entity
            entity.UpdatedBy = GetCurrentUserId();
            entity.UpdatedTime = DateTime.Now;

            _log.Information($"{nameof(Update)} input: {entity.ToJsonString()}");

            // cập nhật entity vào db và commit lại
            _context.ToDoItems.Update(entity);
            
            // EntityEntry<ToDoItem> entry = _context.Entry(entity);
            // if (entry.State == EntityState.Detached)
            // {
            //     _context.ToDoItems.Attach(entity);
            // }
            // entry.State = EntityState.Modified;

            _context.SaveChanges();
            dbReturn.SetProperties(ErrorCode.Success);
        }
        else // không tồn tại thì trả về notfound
        {
            dbReturn.SetProperties(ErrorCode.NotFound, ErrorCode.NotFound.GetDisplay());
        }

        _log.Information($"{nameof(Update)} return: {dbReturn.ToJsonString()}");
        return dbReturn;
    }

    public DbReturn Delete(string id)
    {
        _log.Information($"{nameof(Delete)} id: {id}");
        DbReturn dbReturn = new();

        // truy vấn entity này theo id
        ToDoItem? entity = FindById(id);

        if (entity is not null) // nếu entity này tìm thấy xóa entity này
        {
            // thực hiện xóa entity này và commit lại
            _context.ToDoItems.Remove(entity);
            
            // if (_context.Entry(entity).State == EntityState.Detached)
            // {
            //     _context.ToDoItems.Attach(entity);
            // }
            
            _context.SaveChanges();
            dbReturn.SetProperties(ErrorCode.Success);
        }
        else // không tìm thấy entity thì trả về not found
        {
            dbReturn.SetProperties(ErrorCode.NotFound, ErrorCode.NotFound.GetDisplay());
        }

        _log.Information($"{nameof(Delete)} return: {dbReturn.ToJsonString()}");
        return dbReturn;
    }

    public bool IsExist(string id)
    {
        return _context.ToDoItems.Any(p => p.Id == id);
    }

    public ToDoItem? FindById(string id)
    {
        ToDoItem? entity = _context.ToDoItems.Where(p => p.Id == id).FirstOrDefault();
        return entity;
    }
}
