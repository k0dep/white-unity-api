using System.IO;
using WhiteUnity.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WhiteUnity.DataAccess.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PackagesDbContext>
    {
        public PackagesDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PackagesDbContext>();
            var connectionString = "Server=tcp:localhost,1433;User ID=SA;Password=SuperDuperPa$$wo3d";
            builder.UseSqlServer(connectionString);
            return new PackagesDbContext(builder.Options);
        }
    }
}