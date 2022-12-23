using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAcess.Context
{
    public class FruitContext : DbContext
    {
        public FruitContext(DbContextOptions<FruitContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DbConnection"));
        }

        public DbSet<Fruit> Fruits { get; set; }

        public DbSet<FruitType> FruitTypes { get; set; }
    }
}
