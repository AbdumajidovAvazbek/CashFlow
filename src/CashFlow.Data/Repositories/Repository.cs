using CashFlow.Data.DbContexts;
using CashFlow.Data.IRepositories;
using CashFlow.Domain.Commons;
using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    protected readonly CashFlowDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

        _dbSet.Remove(entity);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var model = await _dbSet.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return model.Entity;
    }

    public IQueryable<TEntity> SelectAll() =>
        _dbSet;
    

    public Task<TEntity> SelectByIdAsync(long id) =>
        _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var model = _dbSet.Update(entity);

        await _dbContext.SaveChangesAsync();

        return model.Entity;
    }
}
