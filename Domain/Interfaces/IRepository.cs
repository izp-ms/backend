using Domain.Entities;

namespace Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(int Id);
    Task<T> Insert(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(T entity);
}