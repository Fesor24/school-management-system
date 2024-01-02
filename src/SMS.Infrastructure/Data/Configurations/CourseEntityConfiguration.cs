using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Aggregates.DepartmentAggregates;

namespace SMS.Infrastructure.Data.Configurations;
internal sealed class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable(nameof(Course), SchoolDbContext.DEFAULT_SCHEMA);

        builder.OwnsOne(x => x.CourseInfo, courseInfoBuilder =>
        {
            courseInfoBuilder.Property(x => x.Code).HasMaxLength(20).HasColumnName("Code");
            courseInfoBuilder.Property(x => x.Name).HasMaxLength(150).HasColumnName("Name");
        });

        builder.Ignore(x => x.DomainEvents);
    }
}
