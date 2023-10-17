using Domain.Entities;

namespace Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(int Id);
    Task<IEnumerable<T>> GetPagination(int pageNumber, int pageSize);
    Task<T> Insert(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(T entity);
    Task<IEnumerable<T>> InsertRange(IEnumerable<T> entities);
    Task<IEnumerable<T>> UpdateRange(IEnumerable<T> entities);
    Task<IEnumerable<T>> DeleteRange(IEnumerable<T> entities);
}