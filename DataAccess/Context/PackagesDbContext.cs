using WhiteUnity.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace WhiteUnity.DataAccess.Context
{
    public class PackagesDbContext : DbContext
    {
        public DbSet<PackageModel> Packages { get; set; }

        public PackagesDbContext(DbContextOptions<PackagesDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}