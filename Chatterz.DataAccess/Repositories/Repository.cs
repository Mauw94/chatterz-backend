using Chatterz.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chatterz.DataAccess.Repositories
{
    /// <summary>
    /// Generic repository.
    /// </summary>
    /// <typeparam name="T">Entity T.</typeparam>
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly ApplicationDbContext ApplicationDbContext;

        public Repository() { }

        public Repository(ApplicationDbContext context)
        {
            ApplicationDbContext = context;
        }

        /// <summary>
        /// Get all T items as queryable async.
        /// </summary>
        /// <returns>Queryable T items.</returns>
        public virtual IQueryable<T> GetAllAsQueryable()
        {
            try
            {
                return ApplicationDbContext.Set<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        /// <summary>
        /// Get all T items as queryable async.
        /// </summary>
        /// <param name="expression">Predicate.</param>
        /// <returns>Queryable T items.</returns>
        public virtual IQueryable<T> GetAllAsQueryable(Expression<Func<T, bool>> expression)
        {
            try
            {
                return ApplicationDbContext.Set<T>().Where(expression);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        /// <summary>
        /// Get T item async.
        /// </summary>
        /// <param name="id">Id of the item.</param>
        public virtual async Task<T> GetAsync(int id)
        {
            try
            {
                return await ApplicationDbContext.Set<T>().FindAsync(id); // null reference error is handled before this
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entity by id: {id}, {ex.Message}");
            }
        }

        /// <summary>
        /// Get T item async.
        /// </summary>
        /// <param name="expression"></param>
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                var entity = await ApplicationDbContext.Set<T>().Where(expression).FirstOrDefaultAsync();
                if (entity == null)
                    throw new Exception($"Couldn't retrieve entity with expression: {expression}");
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entity with expression: {expression}, {ex.Message}");
            }
        }

        /// <summary>
        /// Get all T items as IENumerable.
        /// </summary>
        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return ApplicationDbContext.Set<T>().AsEnumerable();
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn't retrieve entities: {e.Message}");
            }
        }

        /// <summary>
        /// Get all T items as IENumerable.
        /// </summary>
        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            try
            {
                return ApplicationDbContext.Set<T>().Where(expression).AsEnumerable();
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn't retrieve entities: {e.Message}");
            }
        }

        /// <summary>
        /// Get all T items async.
        /// </summary>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await ApplicationDbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        /// <summary>
        /// Get all T items async using predicate.
        /// </summary>
        /// <param name="expression">Predicate.</param>
        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await ApplicationDbContext.Set<T>().Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        /// <summary>
        /// Add T item async.
        /// </summary>
        /// <param name="entity">Item to add.</param>
        public virtual async Task<int> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(AddAsync)} entity can not be null.");

            try
            {
                await ApplicationDbContext.AddAsync(entity);
                return await ApplicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be added: {ex.Message}");
            }
        }

        /// <summary>
        /// Update T item async.
        /// </summary>
        /// <param name="entity">The item to update.</param>
        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity can not be null.");

            try
            {
                ApplicationDbContext.Update(entity);
                await ApplicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove T item async.
        /// </summary>
        public virtual async Task RemoveAsync(T entity)
        {
            try
            {
                ApplicationDbContext.Remove(entity);
                await ApplicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't delete entity: {entity}, {ex.Message}");
            }
        }

        /// <summary>
        /// Remove async by using the Id.
        /// </summary>
        public virtual async Task RemoveAsync(int id)
        {
            try
            {
                var entity = await ApplicationDbContext.Set<T>().FindAsync(id);
                if (entity != null)
                {
                    ApplicationDbContext.Remove(entity);
                    await ApplicationDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't delete entity with id: {id}, {ex.Message}");
            }
        }

        /// <summary>
        /// Add range of T items async.
        /// </summary>
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException($"{nameof(AddRangeAsync)} entities can not be null or empty.");

            try
            {
                await ApplicationDbContext.AddRangeAsync(entities);
                await ApplicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't add entities: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove range of T items async.
        /// </summary>
        public virtual async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
                throw new ArgumentNullException($"{nameof(RemoveRangeAsync)} entities can not be null or empty.");

            try
            {
                ApplicationDbContext.RemoveRange(entities);
                await ApplicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't remove entities: {ex.Message}");
            }
        }
    }
}
