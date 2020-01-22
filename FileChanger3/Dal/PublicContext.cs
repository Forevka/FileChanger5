using System;
using FileChanger3.Abstraction;
using FileChanger3.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace FileChanger3.Dal
{
    public class PublicContext : DbContext, IContext
    {
        public string SchemaName => "public";

        public PublicContext() : base() { }

        public PublicContext(bool needMigrate) : base()
        {
            if (needMigrate)
                Migrate();
        }

        public void Migrate()
        {
            var createScript = Database.GenerateCreateScript();

            foreach (var sqlCode in createScript.Split("\r\n\r\n"))
            {
                try
                { 
                    var trans = Database.BeginTransaction();
                    Database.ExecuteSqlRaw(sqlCode);
                    trans.Commit();
                }
                catch (Npgsql.PostgresException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddProvider(new DbLoggerProvider());
        });


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);
            optionsBuilder.UseNpgsql("Host=82.146.37.127;Database=postgres;Username=postgres;Password=werdwerd");
        }

        #region Model Sets
        public virtual Microsoft.EntityFrameworkCore.DbSet<House> Houses { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<Person> Persons { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);

            base.OnModelCreating(modelBuilder);


            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Person>()
                .Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Person>()
                .ToTable("Person")
                .HasKey(x => x.Id);

            modelBuilder.Entity<House>()
                .Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<House>()
                .HasKey(x => x.Id);
        }
    }
}
