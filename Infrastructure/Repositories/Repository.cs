using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DataContext _dataContext;
    private readonly DbSet<T> _entities;

    private readonly int[] AllowedPageSizes = { 10, 25, 50, 100 };

    public Repository(DataContext dataContext)
    {
        _dataContext = dataContext;
        _entities = _dataContext.Set<T>();
    }

    public async Task<T> Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        EntityEntry<T> entityEntry = _entities.Remove(entity);
        await _dataContext.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<T> Get(int Id)
    {
        return await _entities.SingleOrDefaultAsync(e => e.Id == Id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _entities.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetPagination(int pageNumber, int pageSize)
    {
        if (pageNumber < 0 || pageSize <= 0)
        {
            throw new ArgumentException("Invalid page number or page size.");
        }

        if (!AllowedPageSizes.Contains(pageSize))
        {
            throw new ArgumentException("Invalid page size. Allowed page sizes: 10, 25, 50, 100.");
        }

        return await _entities.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<T> Insert(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        EntityEntry<T> insertedEntity = await _entities.AddAsync(entity);
        await _dataContext.SaveChangesAsync();
        return insertedEntity.Entity;
    }

    public async Task<T> Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        EntityEntry<T> updatedEntity = _entities.Update(entity);
        await _dataContext.SaveChangesAsync();
        return updatedEntity.Entity;
    }
}
