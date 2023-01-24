using Chatterz.DataAccess.Interfaces;
using Chatterz.Services.Interfaces;
using System.Linq.Expressions;

namespace Chatterz.Services.Services
{
    /// <summary>
    /// Generic service.
    /// </summary>
    /// <typeparam name="T">Entity T.</typeparam>
    public class Service<T> : IService<T> where T : class, new()
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _repository.GetAll(expression);
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _repository.GetAllAsQueryable();
        }

        public IQueryable<T> GetAllAsQueryable(Expression<Func<T, bool>> expression)
        {
            return _repository.GetAllAsQueryable(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.GetAllAsync(expression);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            await _repository.RemoveAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            await _repository.RemoveAsync(id);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            await _repository.RemoveRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
