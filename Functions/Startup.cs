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
                new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<WhiteUnityProfile>();
                    cfg.AddProfile<WhiteUnityProfile_Npm>();
                })));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IRepository<PackageModel>, EfRepository<PackageModel>>();

            services.AddTransient<IPagingService, PagingService>();

            services.AddTransient<IPackageSearchService, PackageSearchService>();
            services.AddTransient<IPackageGlobalSearchService, PackageGlobalSearchService>();
            services.AddTransient<IPackageCreateService, PackageCreateService>();
            services.AddTransient<INpmPackageInfoAccessService, NpmPackageInfoAccessService>();

            services.AddDbContext<PackagesDbContext>(options =>
            {
                var config = GetConfig();

                var connectionString = config["SqlConnectionString"];
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<DbContext>(ctx => ctx.GetService<PackagesDbContext>());

            services.AddTransient<IConfiguration>(ctx => GetConfig());
        }

        private IConfiguration GetConfig() => new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

    }
}