

using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.CategoryId);
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
            new Category(1, "Books"),
            new Category(2, "Electronics"),
            new Category(3, "Computers"),
            new Category(4, "Home Appliances")
        );
    }
}
