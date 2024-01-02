using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Aggregates.DepartmentAggregates;

namespace SMS.Infrastructure.Data.Configurations;
internal sealed class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable(nameof(Department), SchoolDbContext.DEFAULT_SCHEMA);

        builder.HasKey(x => x.Id);

        var navigation = builder.Metadata.FindNavigation(nameof(Department.Courses));

        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Ignore(x => x.DomainEvents);

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasColumnName("Code")
            .HasMaxLength(30)
            .IsRequired();
    }
}
