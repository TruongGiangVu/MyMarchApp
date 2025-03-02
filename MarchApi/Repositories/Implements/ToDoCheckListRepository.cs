using MarchApi.Enums;
using MarchApi.Models;
using MarchApi.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MarchApi.Repositories.Implements;

public class ToDoCheckListRepository : BaseRepository, IToDoCheckListRepository
{
    private readonly Serilog.ILogger _log = Log.ForContext<ToDoCheckListRepository>();
    private readonly MarchContext _context;
    public ToDoCheckListRepository(MarchContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _context = context;
    }
    public List<ToDoCheckList>? GetAll()
    {
        // thực hiện tìm kiếm theo class search
        List<ToDoCheckList>? res = _context.ToDoCheckLists.ToList();

        _log.Information($"{nameof(GetAll)} return count: {res.Count}");
        return res;
    }

    public ToDoCheckList? GetById(string id)
    {
        ToDoCheckList? res = _context.ToDoCheckLists.Where(p => p.Id == id).FirstOrDefault();
        _log.Information($"{nameof(GetById)} return: {res.ToJsonString()}");
        return res;
    }

    public DbReturn Insert(ToDoCheckList entity)
    {
        _log.Information($"{nameof(Insert)} input: {entity.ToJsonString()}");
        DbReturn dbReturn = new();

        // lấy userId hiện tại từ jwt token và set thời gian create update cho entity
        string? userId = GetCurrentUserId();
        DateTime time = DateTime.Now;
        entity.CreatedBy = userId;
        entity.CreatedTime = time;
        entity.UpdatedBy = userId;
        entity.UpdatedTime = time;

        // tạo id mới
        entity.Id = Guid.NewGuid().ToString();

        // thêm vào db và commit lại
        _context.ToDoCheckLists.Add(entity);
        _context.SaveChanges();

        // nếu thành công set lại code và message trả về
        dbReturn.SetProperties(ErrorCode.Success);

        _log.Information($"{nameof(Insert)} return: {dbReturn.ToJsonString()}");
        return dbReturn;
    }

    public DbReturn Update(ToDoCheckList entity)
    {
        _log.Information($"{nameof(Update)} input: {entity.ToJsonString()}");
        DbReturn dbReturn = new();

        // kiểm tra xem id này có tồn tại không
        bool isExist = IsExist(entity.Id);

        if (isExist) // nếu tồn tại thì cập nhật
        {
            // lấy userId hiện tại từ jwt token và set thời gian update cho entity
            entity.UpdatedBy = GetCurrentUserId();
            entity.UpdatedTime = DateTime.Now;

            // cập nhật entity vào db và commit lại
            EntityEntry<ToDoCheckList> entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _context.ToDoCheckLists.Attach(entity);
            }
            entry.State = EntityState.Modified;
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
        ToDoCheckList? entity = FindById(id);

        if (entity is not null) // nếu entity này tìm thấy xóa entity này
        {
            // thực hiện xóa entity này và commit lại
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.ToDoCheckLists.Attach(entity);
            }
            _context.ToDoCheckLists.Remove(entity);
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
        return _context.ToDoCheckLists.Any(p => p.Id == id);
    }

    public ToDoCheckList? FindById(string id)
    {
        ToDoCheckList? entity = _context.ToDoCheckLists.Where(p => p.Id == id).FirstOrDefault();
        return entity;
    }
}
