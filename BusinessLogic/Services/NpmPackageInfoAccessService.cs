using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WhiteUnity.BusinessLogic.Abstractions;

namespace WhiteUnity.BusinessLogic
{
    public class NpmPackageInfoAccessService : INpmPackageInfoAccessService
    {
        public readonly ILogger Logger;

        public NpmPackageInfoAccessService(ILogger logger)
        {
            Logger = logger;
        }

        public async Task<PackageMetaInfo> TryGetPackageInfo(string url)
        {
            var tempDir = GetTemporaryDirectory();

            var cloneOptions = new CloneOptions()
            {
                IsBare = true,
                FetchOptions = new FetchOptions
                {
                    TagFetchMode = TagFetchMode.All
                }
            };
            
            await Task.Run(() => Repository.Clone(url, tempDir, cloneOptions)); //TODO: Обратотка ошибок
            
            string pakcageContent = null;
            var metaInfo = new PackageMetaInfo();
            
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

                metaInfo.Branches = repo.Branches.Where(t => !t.IsRemote).Select(b => b.FriendlyName).ToArray();
                metaInfo.Tags = repo.Tags.Select(b => b.FriendlyName).ToArray();
            }
            
            metaInfo.Info = JsonConvert.DeserializeObject<NpmPackageObject>(pakcageContent);
            DeleteTempDir(tempDir);

            return metaInfo;
        }

        private void DeleteTempDir(string tempDir)
        {
            try
            {
                Directory.Delete(tempDir, true);
            }
            catch (Exception e)
            {
                Logger.LogWarning(e, $"Cant delete temp dir {tempDir}");
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