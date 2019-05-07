using System.IO;
using AzureFunctionsTest.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PackagesDbContext>
    {
        public PackagesDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<PackagesDbContext>();
            var connectionString = "Server=localhost";//configuration.GetConnectionString("SqlConnectionString");
            builder.UseSqlServer(connectionString);
            return new PackagesDbContext(builder.Options);
        }
    }
}