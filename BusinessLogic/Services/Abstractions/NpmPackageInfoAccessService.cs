using System.IO;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Newtonsoft.Json;

namespace WhiteUnity.BusinessLogic.Services.Abstractions
{
    public class NpmPackageInfoAccessService : INpmPackageInfoAccessService
    {
        public async Task<NpmPackageObject> TryGetPakcageInfo(string url)
        {
            var tempDir = GetTemporaryDirectory();
            
            await Task.Run(() => Repository.Clone(url, tempDir)); //TODO: Обратотка ошибок

            using (var repo = new Repository(tempDir))
            {
                var blob = repo.Head.Tip["package.json"].Target as Blob;
                using (var content = new StreamReader(blob.GetContentStream(), Encoding.UTF8))
                {
                    return JsonConvert.DeserializeObject<NpmPackageObject>(content.ReadToEnd());
                }
            }
        }
        
        private static string GetTemporaryDirectory()
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
}