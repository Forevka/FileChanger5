using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileChanger3.Abstraction;
using FileChanger3.Dal;
using FileChanger3.DAL;
using FileChanger3.Dal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Routes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private ContextFactory contextFactory = new ContextFactory();
        private UnitOfWork unitOfWork;

        private IRepository<Person, Guid> personRepo;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            contextFactory.RegisterContext<PublicContext>("public");
            var unit = new UnitOfWork(contextFactory.GetContext("public"));
            personRepo = unit.Repository<Person, Guid>();
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return personRepo.GetAll();
        }
    }
}
