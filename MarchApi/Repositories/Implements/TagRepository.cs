using MarchApi.Enums;
using MarchApi.Models;
using MarchApi.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MarchApi.Repositories.Implements;

public class TagRepository : BaseRepository, ITagRepository
{
    private readonly Serilog.ILogger _log = Log.ForContext<TagRepository>();
    private readonly MarchContext _context;
    public TagRepository(MarchContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _context = context;
    }
    public List<ToDoTag>? GetAll()
    {
        // thực hiện tìm kiếm theo class search
        List<ToDoTag>? res = _context.ToDoTags.ToList();

        _log.Information($"{nameof(GetAll)} return count: {res.Count}");
        return res;
    }

    public ToDoTag? GetById(string id)
    {
        ToDoTag? res = _context.ToDoTags.Where(p => p.Id == id).FirstOrDefault();
        _log.Information($"{nameof(GetById)} return: {res.ToJsonString()}");
        return res;
    }

    public DbReturn Insert(ToDoTag entity)
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
        _context.ToDoTags.Add(entity);
        _context.SaveChanges();

        // nếu thành công set lại code và message trả về
        dbReturn.SetProperties(ErrorCode.Success);

        _log.Information($"{nameof(Insert)} return: {dbReturn.ToJsonString()}");
        return dbReturn;
    }

    public DbReturn Update(ToDoTag entity)
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
            EntityEntry<ToDoTag> entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _context.ToDoTags.Attach(entity);
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
        ToDoTag? entity = FindById(id);

        if (entity is not null) // nếu entity này tìm thấy xóa entity này
        {
            // thực hiện xóa entity này và commit lại
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.ToDoTags.Attach(entity);
            }
            _context.ToDoTags.Remove(entity);
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
        return _context.ToDoTags.Any(p => p.Id == id);
    }

    public ToDoTag? FindById(string id)
    {
        ToDoTag? entity = _context.ToDoTags.Where(p => p.Id == id).FirstOrDefault();
        return entity;
    }
}
