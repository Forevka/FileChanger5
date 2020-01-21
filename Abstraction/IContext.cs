using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FileChanger3.Abstraction
{
    public interface IContext
    {
        public string SchemaName { get; }
        public void Migrate();
    }
}
