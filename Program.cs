using System;
using System.Linq;
using FileChanger3.Dal;
using FileChanger3.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace FileChanger3
{
    class Program
    {
        static DbContext _context = new DatabaseContext();

        static void Main(string[] args)
        {
            var houseRepo = new Repository<House, Guid>(_context);

            houseRepo.Add(new House()
            {
                Id = Guid.NewGuid(),
                Name = "test",
            });

            var oldHouse = houseRepo.Find(x => x.Id == new Guid("dbef8ae7-ccd6-4bbd-a64e-8106eb3867b0")).FirstOrDefault();

            Console.WriteLine(oldHouse.Name);

            var personRepo = new Repository<Person, Guid>(_context);

            Random rand = new Random();

            personRepo.Add(new Person()
            {
                Id = Guid.NewGuid(),
                Age = rand.Next(80),
            });
        }
    }
}
