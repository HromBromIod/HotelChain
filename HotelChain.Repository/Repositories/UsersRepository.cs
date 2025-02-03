using System.Linq.Expressions;
using HotelChain.DataAccess;
using HotelChain.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HotelChain.Repository.Repositories;

public class UsersRepository : IRepository<UserEntity>
{
    private readonly IDbContextFactory<HotelChainDbContext> _contextFactory;

    public UsersRepository(IDbContextFactory<HotelChainDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        return await dbContext.Set<UserEntity>().AsNoTracking().Include(e => e.Permissions).ToListAsync();
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        return await dbContext.Set<UserEntity>().AsNoTracking().Include(e => e.Permissions).Where(predicate).ToListAsync();
    }

    public async Task<UserEntity?> GetByIdAsync(int id)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        return await dbContext.Set<UserEntity>().AsNoTracking().Include(e => e.Permissions).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<UserEntity?> GetByIdAsync(Guid id)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        return await dbContext.Set<UserEntity>().AsNoTracking().Include(e => e.Permissions).FirstOrDefaultAsync(e => e.ExternalId == id);
    }

    public async Task<UserEntity> SaveAsync(UserEntity entity)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        EntityEntry<UserEntity> result;
        if (await dbContext.Set<UserEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == entity.Id) == null)
        {
            entity.ExternalId = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = DateTime.UtcNow;
            result = await dbContext.Set<UserEntity>().AddAsync(entity);
        }
        else
        {
            entity.ModificationTime = DateTime.UtcNow;
            result = dbContext.Set<UserEntity>().Update(entity);
        }
        await dbContext.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task DeleteAsync(UserEntity entity)
    {
        await using var dbContext = await _contextFactory.CreateDbContextAsync();
        dbContext.Set<UserEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}