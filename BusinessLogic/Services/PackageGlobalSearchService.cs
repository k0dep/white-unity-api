using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WhiteUnity.BusinessLogic.Abstractions;

namespace WhiteUnity.BusinessLogic
{
    public class PackageGlobalSearchService : IPackageGlobalSearchService
    {
        public async Task<PackageInfoDto> GlobalSearch(string name)
        {
            var slashIndex = name.IndexOf("/");

            if(slashIndex <= 0)
            {
                return null;
            }

            var owner = name.Substring(0, slashIndex);
            var repo = name.Substring(slashIndex + 1);
            var repoUrl = $"https://api.github.com/repos/{owner}/{repo}";

            var response = await RequestGet(repoUrl);
            if (response == null)
            {
                return null;
            }

            var metaResponse = await RequestGet($"{repoUrl}/contents/package.json.meta");
            if (metaResponse == null)
            {
                return null;
            }

            dynamic repoData = JsonConvert.DeserializeObject(response);
            return new PackageInfoDto()
            {
                Name = repoData.name,
                FullName = repoData.full_name,
                ProjectUrl = repoData.html_url,
                UrlForManifest = repoData.clone_url
            };
        }

        private async Task<string> RequestGet(string url)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.UserAgent = "Nothing";

            try
            {
                var response = (HttpWebResponse) await request.GetResponseAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                using (var dataStream = response.GetResponseStream())
                {
                    var reader = new StreamReader(dataStream);
                    return reader.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}