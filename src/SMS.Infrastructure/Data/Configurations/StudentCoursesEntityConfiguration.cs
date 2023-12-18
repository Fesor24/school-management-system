using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Aggregates.StudentCoursesAggregates;

namespace SMS.Infrastructure.Data.Configurations;
public class StudentCoursesEntityConfiguration : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder.ToTable(nameof(StudentCourse), SchoolDbContext.STUDENT_SCHEMA);

        builder.HasKey(sc => new { sc.StudentId, sc.CourseId });

        builder.Ignore(x => x.Id);

        builder.Ignore(x => x.DomainEvents);
    }
}
