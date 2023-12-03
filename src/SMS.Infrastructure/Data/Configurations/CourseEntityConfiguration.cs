using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Entities;

namespace SMS.Infrastructure.Data.Configurations;
public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable(nameof(Course), "sch");

        builder.Property(x => x.Code)
            .HasMaxLength(5)
            .HasColumnName("Code");

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .HasColumnName("Name");
    }
}
