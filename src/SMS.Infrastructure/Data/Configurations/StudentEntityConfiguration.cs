using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Aggregates.StudentAggregates;

namespace SMS.Infrastructure.Data.Configurations;
internal sealed class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable(nameof(Student), SchoolDbContext.STUDENT_SCHEMA);

        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.DomainEvents);

        var navigation = builder.Metadata.FindNavigation(nameof(Student.Courses));

        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
