using Chinook.Domain.Entities;

namespace Chinook.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}