using E_Mart.Domain.Base;
using E_Mart.EFCore.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Mart.EFCore.Base;
public class GenericRepository<T,TDbContext> : IGenericRepository<T> where T : BaseEntity, new()
{
    private readonly EMartDbContext _eMartDbContext;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(EMartDbContext eMartDbContext)
    {
        _eMartDbContext = eMartDbContext;
        this._dbSet = _eMartDbContext.Set<T>();
    }
    public async Task<T> AddAsync(T entity)
    {
        entity.IsActive = true;
        entity.CreatedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(entity);
        await _eMartDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task SoftDeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsActive = false;
            _eMartDbContext.Entry(entity).Property(e => e.IsDeleted).IsModified = true;
            _eMartDbContext.Entry(entity).Property(e => e.UpdatedAt).IsModified = true;
            await _eMartDbContext.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _eMartDbContext.SaveChangesAsync();
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().Where(x => x.IsActive).ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.IsDeleted = false;
        _dbSet.Update(entity);
        await _eMartDbContext.SaveChangesAsync();
        return entity;
    }
}
