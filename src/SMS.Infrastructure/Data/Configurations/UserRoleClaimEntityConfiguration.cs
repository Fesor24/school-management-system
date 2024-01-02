using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Aggregates.UserRoleClalimAggregates;

namespace SMS.Infrastructure.Data.Configurations;
internal sealed class UserRoleClaimEntityConfiguration : IEntityTypeConfiguration<UserRoleClaim>
{
    public void Configure(EntityTypeBuilder<UserRoleClaim> builder)
    {
        builder.ToTable(nameof(UserRoleClaim), SchoolDbContext.USER_SCHEMA);
    }
}
