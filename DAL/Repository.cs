using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FileChanger3.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FileChanger3.Dal
{
    public class Repository<TEntity, PKType> : IRepository<TEntity, PKType> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }


        public TEntity Get(PKType id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public async Task<TEntity> GetAsync(PKType id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _context.Set<TEntity>().ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Return first or default element from context
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> FindAsQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? _context.Set<TEntity>().Where(predicate) : _context.Set<TEntity>();
        }

        public IQueryable<TEntity> FindAllAsQueryable()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate = null)
        {
            return FindAsQueryable(predicate).AsNoTracking();
        }

        public IQueryable<TEntity> FindWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include)
        {
            IQueryable<TEntity> entities = _entities;
            return include.Aggregate(entities, (current, inc) => current.Include(inc)).Where(predicate);
        }

        public Task<List<TEntity>> FindWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include)
        {
            return FindWithInclude(predicate, include).ToListAsync();
        }


        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                return null;

            var res  = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return res.Entity;
        }

        public void AddOnSave(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                return;
            }

            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }


        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void RemoveOnSave(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                return;
            }

            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public virtual void UpdateOnSave(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
