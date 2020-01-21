using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileChanger3.Abstraction
{
    public interface IRepository<TEntity, PKType> where TEntity : class
    {
        // Set of functions to get
        TEntity Get(PKType id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(PKType id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> FindAllAsQueryable();
        IQueryable<TEntity> FindAsQueryable(Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate = null);
        IQueryable<TEntity> FindWithInclude(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] include);
        Task<List<TEntity>> FindWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include);

        // Set of functions to add data
        void Add(TEntity entity);
        void AddOnSave(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        // Functions to update data
        void Update(TEntity entity);

        void UpdateOnSave(TEntity entity);

        Task UpdateAsync(TEntity entity);

        // Set of functions to remove data
        void Remove(TEntity entity);
        void RemoveOnSave(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

    }
}
