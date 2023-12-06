using Microsoft.EntityFrameworkCore;
using SMS.Domain.Abstractions;
using SMS.Domain.Primitives;
using SMS.Infrastructure.Data;

namespace SMS.Infrastructure.Repository;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity: Entity, new()
{
    private readonly ApplicationDbContext _context;

    protected GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TEntity entity) =>
        await _context.Set<TEntity>().AddAsync(entity);

    public void Delete(TEntity entity) =>
        _context.Set<TEntity>().Remove(entity);

    public async Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default) =>
        await _context.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

    public void Update(TEntity entity)
    {
        _context.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}
