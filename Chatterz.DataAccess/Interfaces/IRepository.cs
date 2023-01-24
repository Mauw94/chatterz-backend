using System.Linq.Expressions;

namespace Chatterz.DataAccess.Interfaces
{
    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="T">Entity T.</typeparam>
    public interface IRepository<T> where T : class, new()
    {
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAllAsQueryable();
        IQueryable<T> GetAllAsQueryable(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveAsync(int id);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
