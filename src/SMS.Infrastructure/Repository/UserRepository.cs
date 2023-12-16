using SMS.Domain.Aggregates.UserAggregates;
using SMS.Infrastructure.Data;

namespace SMS.Infrastructure.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(SchoolDbContext context) : base(context)
    {
        
    }
}
