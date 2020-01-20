using System.Data.Entity;
using FileChanger3.Dal.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace FileChanger3.Dal
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=82.146.37.127;Database=postgres;Username=postgres;Password=123456789");
        }

        #region Model Sets

        public virtual Microsoft.EntityFrameworkCore.DbSet<House> Houses { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<Person> Persons { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
