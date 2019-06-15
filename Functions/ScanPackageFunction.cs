using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using WhiteUnity.BusinessLogic.Abstractions;
using WhiteUnity.DataAccess.Abstraction;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace Functions
{
    public static class ScanPackageFunction
    {
        [FunctionName("scan")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Inject] IPackageGlobalSearchService globalSearch,
            [Inject] IPackageCreateService createService,
            [Inject] IPackageSearchService searchService,
            [Inject] IUnitOfWork uow
        ){
            string n = null;
            n = req.Query[nameof(n)];

            if(string.IsNullOrEmpty(n))
            {
                return new BadRequestObjectResult(new {
                    Error = $"bad request. Missing {nameof(n)} property in request"
                });
            }

            var scanResult = await globalSearch.GlobalSearch(n);
            if (scanResult == null)
            {
                return new NotFoundObjectResult(new {
                    error = "package not found"
                });
            }

            var existPackage = await searchService.BestMatch(scanResult.Name);
            if (existPackage != null)
            {
                return new ConflictObjectResult(new {
                    error = "package with this name alredy exist"
                });
            }

            var newId = await createService.Create(scanResult);
            scanResult.Id = newId;

            await uow.SaveChanges();

            return new OkObjectResult(scanResult);
        }

    }
}