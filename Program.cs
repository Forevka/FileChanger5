using FileChanger3.Dal;
using FileChanger3.Dal.Models;
using FileChanger3.DAL;
using System;
using System.Linq;

namespace FileChanger3
{
    class Program
    {
        static ContextFactory contextFactory = new ContextFactory();

        static void Main(string[] args)
        {
            contextFactory.RegisterContext<PublicContext>("public");

            var unitOfWork = new UnitOfWork(contextFactory.GetContext("public"));

            var repo = unitOfWork.Repository<Person, Guid>();

            Console.WriteLine(repo.Find(x => x.Age >= 10).FirstOrDefault().Id);
        }
    }
}
