using MarchApi.Models;

using Microsoft.EntityFrameworkCore;

namespace MarchApi.Repositories.Implements;

public class MarchContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; } = default!;
    public DbSet<ToDoTag> ToDoTags { get; set; } = default!;
    public DbSet<ToDoCheckList> ToDoCheckLists { get; set; } = default!;
    public DbSet<DbUser> DbUsers { get; set; } = default!;

    public MarchContext(DbContextOptions<MarchContext> options)
            : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoCheckList>()
            .HasOne(checkList => checkList.ToDoItem)  // Each CheckList has one ToDoItem
            .WithMany(item => item.ToDoCheckLists) // Each ToDoItem has many ToDoCheckList
            .HasForeignKey(checkList => checkList.ToDoItemId) // Foreign key in ToDoCheckList table
            .OnDelete(DeleteBehavior.Cascade); // Nếu ToDoItem bị xóa, xóa luôn ToDoCheckList tương ứng

        // cấu hình quan hệ n-n giữa 2 class ToDoTag à ToDoItem bằng bảng ToDoTagItem với 2 key là ToDoItemId và ToDoTagId
        modelBuilder.Entity<ToDoTag>()
            .HasMany(tag => tag.ToDoItems)
            .WithMany(item => item.ToDoTags)
            .UsingEntity<Dictionary<string, object>>(
                "ToDoTagItem",
                j => j.HasOne<ToDoItem>().WithMany().HasForeignKey("ToDoItemId"),
                j => j.HasOne<ToDoTag>().WithMany().HasForeignKey("ToDoTagId")
            );
    }

}
