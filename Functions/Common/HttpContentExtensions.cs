using System.Net.Http;
using System.Threading.Tasks;

namespace WhiteUnity.Functions.Common
{
    public static class HttpContentExtensions
    {
        public static async Task<T> GetData<T>(this HttpRequestMessage request)
        {
            try
            {
                var data = await request.Content.ReadAsAsync<T>();
                return data;
            }
            catch
            {
                return default(T);
            }
        }
    }
}