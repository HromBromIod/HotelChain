using System.Linq.Expressions;
using HotelChain.DataAccess;
using HotelChain.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HotelChain.Repository.Repositories;

public class Repository<T> : IRepository<T> where T : class, IBaseEntity
{
    private readonly IDbContextFactory<HotelChainDbContext> _contextFactory;

    public Repository(IDbContextFactory<HotelChainDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        return await dbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        return await dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        return await dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        return await dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.ExternalId == id);
    }

    public async Task<T> SaveAsync(T entity)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        EntityEntry<T> result;
        if (await dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == entity.Id) == null)
        {
            entity.ExternalId = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = DateTime.UtcNow;
            result = await dbContext.Set<T>().AddAsync(entity);
        }
        else
        {
            entity.ModificationTime = DateTime.UtcNow;
            result = dbContext.Set<T>().Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }
        await dbContext.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task DeleteAsync(T entity)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}