using FileChanger3.Dal;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FileChanger3.Abstraction
{
    interface IUnitOfWork
    {
        Repository<TEntity, TPKey> Repository<TEntity, TPKey>() where TEntity : class;

        DbContext Context { get; }

        int Complete();

        Task<int> CompleteAsync();
    }
}
