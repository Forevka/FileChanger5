using FileChanger3.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FileChanger3.DAL
{
    public class ContextFactory : IContextFactory
    {
        private readonly Dictionary<string, Type> _contexts = new Dictionary<string, Type>();

        public DbContext GetContext(string dbName)
        {
            if (!_contexts.ContainsKey(dbName))
                throw new NotImplementedException($"context for {dbName} doesn't register or exist");

            return Activator.CreateInstance(_contexts[dbName]) as DbContext;
        }

        public DbContext GetContext(string dbName, bool needMigrate)
        {
            if (!_contexts.ContainsKey(dbName))
                throw new NotImplementedException($"context for {dbName} doesn't register or exist");

            var context = Activator.CreateInstance(_contexts[dbName]) as IContext;
            if (needMigrate)
                context.Migrate();
            return context as DbContext;
        }

        public void RegisterContext<T>(string dbName) where T : IContext
        {
            _contexts.Add(dbName, typeof(T));
        }
    }
}
