﻿using SMS.Domain.Abstractions;
using SMS.Infrastructure.Data;

namespace SMS.Infrastructure.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        CourseRepository = new CourseRepository(context);
    }

    public ICourseRepository CourseRepository { get; init; }

    public async Task<int> Complete(CancellationToken cancelleationToken = default)
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose() =>
        _context.Dispose();
}