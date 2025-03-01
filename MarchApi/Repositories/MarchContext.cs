using MarchApi.Models;

using Microsoft.EntityFrameworkCore;

namespace MarchApi.Repositories;

public class MarchContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; } = default!;
    public DbSet<ToDoTag> ToDoTags { get; set; } = default!;
    public DbSet<DbUser> DbUsers { get; set; } = default!;
    
    public MarchContext(DbContextOptions<MarchContext> options)
            : base(options)
    {
    }
}