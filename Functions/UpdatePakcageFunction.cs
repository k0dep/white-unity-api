using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using WhiteUnity.BusinessLogic.Abstractions;
using WhiteUnity.DataAccess.Abstraction;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace WhiteUnity.Functions
{
    public static class UpdatePakcageFunction
    {
        [FunctionName("update")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "update/{name}")] HttpRequest req,
            string name,
            [Inject] IPackageGlobalSearchService globalSearch,
            [Inject] IPackageUpdateService createService,
            [Inject] IPackageSearchService updateService,
            [Inject] IUnitOfWork uow
        )
        {
            if (string.IsNullOrEmpty(name))
            {
                return new BadRequestObjectResult(new {
                    Error = $"bad request. Missing {nameof(name)} property in request"
                });
            }

            var scanResult = await updateService.BestMatch(name);
            if (scanResult == null)
            {
                return new NotFoundObjectResult(new {
                    error = "package not found"
                });
            }

            var refreshPackage = await globalSearch.GlobalSearch(scanResult.UrlForManifest);
            refreshPackage.Id = scanResult.Id;

            var package = await createService.Update(refreshPackage);

            await uow.SaveChanges();

            return new OkObjectResult(package);

        }
    }
}