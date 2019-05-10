using System.Net.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WhiteUnity.DataAccess.Context;
using WhiteUnity.BusinessLogic.Abstractions;
using WhiteUnity.BusinessLogic;
using WhiteUnity.BusinessLogic.Objects;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;
using WhiteUnity.Functions.Common;

namespace k0dep.test1
{
    public static class SearchPackagesFunction
    {
        [FunctionName("search")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req,
            ILogger log,
            [Inject] IPackageSearchService packageSearch
        )
        {
            var data = await req.GetData<PackageSearchRequestDto>();
            if(data == null)
            {
                return new BadRequestObjectResult(new {
                    Error = "bad request"
                });
            }

            return new OkObjectResult(await packageSearch.Search(data));
        }
    }
}