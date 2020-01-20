using FileChanger3.Abstraction;
using FileChanger3.Dal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileChanger3.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<string, object> _repositories;
        public DbContext Context { get; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public Repository<TEntity, TPKey> Repository<TEntity, TPKey>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(TEntity).Name;
            Type[] typeArgs = { typeof(TEntity), typeof(TPKey) };

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeArgs), Context);

                _repositories.Add(type, repositoryInstance);
            }

            return _repositories[type] as Repository<TEntity, TPKey>;
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
