using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Shared.Contracts;
using System.Linq.Expressions;

namespace QuickHire.Infrastructure.Persistence.Repositories;

internal class Repository : IRepository
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync<T>(T entity) where T : class
    {
        await GetDbSet<T>().AddAsync(entity);
    }

    public async Task DeleteAsync<T>(T entity) where T : class
    {
        if (typeof(ISoftDeletable).IsAssignableFrom(typeof(T)))
        {
            var softDeletableEntity = entity as ISoftDeletable;

            if(softDeletableEntity != null && !softDeletableEntity.IsDeleted)
            {
                softDeletableEntity.IsDeleted = true;
                softDeletableEntity.DeletedAt = DateTime.Now;
            }
        }
        else
        {
            GetDbSet<T>().Remove(entity);
        }

        await SaveChangesAsync();
    }

    public IQueryable<T>? GetAll<T>() where T : class
    {
        return GetDbSet<T>().AsQueryable();
    }

    public IQueryable<T> GetAllReadOnly<T>() where T : class
    {
        return GetDbSet<T>().AsQueryable().AsNoTracking();
    }

    public async Task<T>? GetByIdAsync<T, TId>(TId id) where T : class
    {
        return await GetDbSet<T>().FindAsync(id);
    }

    public async Task SaveChangesAsync() 
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync<T>(T entity) where T : class
    {
        GetDbSet<T>().Update(entity);
        await SaveChangesAsync();
    }

    private DbSet<T> GetDbSet<T>() where T : class
    {
        return _context.Set<T>();
    }

    public async Task<IEnumerable<T>> ToListAsync<T>(IQueryable<T> queryable) where T : class
    {
        return await queryable.ToListAsync();
    }

    public async Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable) where T : class
    {
        return await queryable.FirstOrDefaultAsync();
    }

    public IQueryable<T> GetAllIncluding<T>(params Expression<Func<T, object>>[] includes) where T : class
    {
        IQueryable<T> query = GetDbSet<T>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}
