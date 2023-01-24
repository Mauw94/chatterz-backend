using System.Linq.Expressions;

namespace Chatterz.Services.Interfaces
{
    /// <summary>
    /// Generic service.
    /// </summary>
    /// <typeparam name="T">Entity T.</typeparam>
    public interface IService<T> where T : class, new()
    {
        Task<T> GetAsync(int id);
        IQueryable<T> GetAllAsQueryable();
        IQueryable<T> GetAllAsQueryable(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<int> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveAsync(int id);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
