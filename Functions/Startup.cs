using System;
using WhiteUnity;
using WhiteUnity.DataAccess.Context;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhiteUnity.DataAccess.Abstraction;
using WhiteUnity.DataAccess.EntityFramework;
using WhiteUnity.DataAccess;
using WhiteUnity.DataAccess.Models;
using WhiteUnity.BusinessLogic.Abstraction;
using WhiteUnity.BusinessLogic;
using AutoMapper;
using WhiteUnity.BusinessLogic.Abstractions;
using WhiteUnity.BusinessLogic.Services;

[assembly: WebJobsStartup(typeof(Startup))]
namespace WhiteUnity  
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var services = builder.Services;

            services.AddSingleton<IMapper>(
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<WhiteUnityProfile>())));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepository<PackageModel>, EfRepository<PackageModel>>();

            services.AddScoped<IPagingService, PagingService>();

            services.AddScoped<IPackageSearchService>(ctx => {
                var r = ctx.GetService<IRepository<PackageModel>>();
                var m = ctx.GetService<IMapper>();
                var p = ctx.GetService<IPagingService>();

                return new PackageSearchService(r, m, p);
            });

            services.AddDbContext<PackagesDbContext>(options => {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

                var connectionString = config["SqlConnectionString"];
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<DbContext>(ctx => ctx.GetService<PackagesDbContext>());
        }
    }
}