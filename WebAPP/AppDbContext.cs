using Microsoft.EntityFrameworkCore;
using WebAPP.Model;

namespace WebAPP;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ToDoList> ToDoLists { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id); // primary key
            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

        });

        modelBuilder.Entity<ToDoList>(entity =>
        {
            entity.HasKey(t => t.Id); 
            entity.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200); 
            entity.Property(t => t.Description)
                .HasMaxLength(500); 
            entity.Property(t => t.DueTime)
                .IsRequired(); 
            
            entity.Property(t => t.IsCompleted)
                .IsRequired(); 
            entity.Property(t => t.UpdateTime)
                .IsRequired(); 
            entity.Property(t => t.IsDeleted)
                .IsRequired(); 
            entity.Property(t => t.CreateTime)
                .IsRequired(); 
        });
    }
}