using Microsoft.EntityFrameworkCore;

namespace FileChanger3.Abstraction
{
    public interface IContextFactory
    {
        DbContext GetContext(string dbName);

        void RegisterContext<T>(string dbName);
    }
}
