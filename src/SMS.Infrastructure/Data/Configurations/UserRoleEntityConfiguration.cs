using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Aggregates.UserRoleAggregates;

namespace SMS.Infrastructure.Data.Configurations;
internal sealed class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRole", SchoolDbContext.USER_SCHEMA);
    }
}
