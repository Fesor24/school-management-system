using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Aggregates.DepartmentAggregates;

namespace SMS.Infrastructure.Data.Configurations;
public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable(nameof(Department), "sms");

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Courses)
            .WithOne();

        builder.Property<List<Course>>("_courses")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Courses");

        builder.Ignore(x => x.DomainEvents);
    }
}
