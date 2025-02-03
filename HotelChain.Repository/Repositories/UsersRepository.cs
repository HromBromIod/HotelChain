using System.Linq.Expressions;
using HotelChain.DataAccess;
using HotelChain.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelChain.Repository.Repositories;

public class UsersRepository : IRepository<UserEntity>
{
    private readonly IDbContextFactory<HotelChainDbContext> _contextFactory;

    public UsersRepository(IDbContextFactory<HotelChainDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public IEnumerable<UserEntity> GetAll()
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<UserEntity>().AsNoTracking().Include(e => e.Permissions).ToList();
    }

    public IEnumerable<UserEntity> GetAll(Expression<Func<UserEntity, bool>> predicate)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<UserEntity>().AsNoTracking().Include(e => e.Permissions).Where(predicate).ToList();
    }

    public UserEntity? GetById(int id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<UserEntity>().AsNoTracking().Include(e => e.Permissions).FirstOrDefault(e => e.Id == id);
    }

    public UserEntity? GetById(Guid id)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        return dbContext.Set<UserEntity>().AsNoTracking().Include(e => e.Permissions).FirstOrDefault(e => e.ExternalId == id);
    }

    public UserEntity Save(UserEntity entity)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        if (dbContext.Set<UserEntity>().AsNoTracking().FirstOrDefault(e => e.Id == entity.Id) == null)
        {
            entity.ExternalId = Guid.NewGuid();
            entity.CreationTime = DateTime.UtcNow;
            entity.ModificationTime = DateTime.UtcNow;
            var result = dbContext.Set<UserEntity>().Add(entity);
            dbContext.SaveChanges();
            return result.Entity;
        }
        else
        {
            entity.ModificationTime = DateTime.UtcNow;
            var result = dbContext.Set<UserEntity>().Update(entity);
            dbContext.SaveChanges();
            return result.Entity;
        }
    }

    public void Delete(UserEntity entity)
    {
        using var dbContext = _contextFactory.CreateDbContext();
        dbContext.Set<UserEntity>().Remove(entity);
        dbContext.SaveChanges();
    }
}