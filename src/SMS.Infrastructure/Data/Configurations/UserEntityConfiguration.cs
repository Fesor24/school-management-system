using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS.Domain.Aggregates.UserAggregates;

namespace SMS.Infrastructure.Data.Configurations;
internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), SchoolDbContext.USER_SCHEMA);

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Address, addressBuilder =>
        {
            addressBuilder.Property(x => x.City).IsRequired();
            addressBuilder.Property(x => x.State).IsRequired();
            addressBuilder.Property(x => x.Street).IsRequired();
        });

        builder.Property(x => x.Gender)
            .HasConversion(g => g.ToString(),
            str => (Gender)Enum.Parse(typeof(Gender), str));

        builder.Property(x => x.RefreshToken)
            .IsRequired(false);
    }
}
