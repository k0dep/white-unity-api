using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WhiteUnity.BusinessLogic.Abstractions;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace k0dep.test1
{
    public static class MatchPackageFunction
    {
        [FunctionName("match")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "match/{name}")] HttpRequest req,
            ILogger log,
            string name,
            [Inject] IPackageSearchService packageSearch
        ){
            if(name == null)
            {
                return new BadRequestResult();
            }

            var package = await packageSearch.BestMatch(name);

            if (package == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(package);
        }
    }
}