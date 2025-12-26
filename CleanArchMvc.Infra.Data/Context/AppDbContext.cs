

using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set;}
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Category>()
        //     .HasMany(c => c.Products)
        //     .WithOne(p => p.Category)
        //     .HasForeignKey(p => p.CategoryId);
        

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}
