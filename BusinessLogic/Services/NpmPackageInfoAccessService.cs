using System.IO;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Newtonsoft.Json;
using WhiteUnity.BusinessLogic.Abstractions;

namespace WhiteUnity.BusinessLogic
{
    public class NpmPackageInfoAccessService : INpmPackageInfoAccessService
    {
        public async Task<NpmPackageObject> TryGetPackageInfo(string url)
        {
            var tempDir = GetTemporaryDirectory();
            
            await Task.Run(() => Repository.Clone(url, tempDir)); //TODO: Обратотка ошибок
            
            string pakcageContent = null; 
            using (var repo = new Repository(tempDir))
            {
                var treeEntry = repo.Head.Tip["package.json"];

                if (treeEntry == null)
                {
                    return null;
                }
                
                var blob = treeEntry.Target as Blob;
                using (var content = new StreamReader(blob.GetContentStream(), Encoding.UTF8))
                {
                    pakcageContent = content.ReadToEnd();
                }
            }
            
            var packageObject = JsonConvert.DeserializeObject<NpmPackageObject>(pakcageContent);
            Directory.Delete(tempDir, true);
            
            return packageObject;
        }
        
        private static string GetTemporaryDirectory()
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
}