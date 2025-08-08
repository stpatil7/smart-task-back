using System.Linq.Expressions;

namespace Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(object id);
        IQueryable<T> GetAllAsync();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        Task SaveAsync();
    }
}
