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
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup))]
namespace WhiteUnity
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var services = builder.AddDependencyInjection(ConfigureContainer);
        }

        private void ConfigureContainer(IServiceCollection services)
        {
            services.AddSingleton<IMapper>(ctx =>
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<WhiteUnityProfile>())));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRepository<PackageModel>, EfRepository<PackageModel>>();

            services.AddTransient<IPagingService, PagingService>();

            services.AddTransient<IPackageSearchService, PackageSearchService>();

            services.AddDbContext<PackagesDbContext>(options =>
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

                var connectionString = config["SqlConnectionString"];
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<DbContext>(ctx => ctx.GetService<PackagesDbContext>());

        }
    }
}