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

            var type = _contexts[dbName];
            return Activator.CreateInstance(type) as DbContext;

        }

        public void RegisterContext<T>(string dbName)
        {
            _contexts.Add(dbName, typeof(T));
        }
    }
}
