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

namespace k0dep.test1
{
    public class MatchPackageFunction
    {
        private readonly IPackageSearchService _packageSearch;

        public MatchPackageFunction(IPackageSearchService packageSearch)
        {
            _packageSearch = packageSearch;
        }

        [FunctionName("match")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            if(name == null)
            {
                return new BadRequestResult();
            }

            var package = await _packageSearch.BestMatch(name);

            if (package == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(package);
        }
    }
}