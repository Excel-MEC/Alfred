using System.Net.Http;
using System.Threading.Tasks;

namespace Alfred.Client.Services.Interfaces
{
    public interface IApiService
    {
        Task<HttpClient> Client(string header=null);
        Task<TItem> GetFromJsonAsync<TItem>(string url);
        Task<string> GetAsync(string url);
        Task<TItem> PostJsonAsync<TItem>(string url, object content, bool notification=true);
        Task<string> PostJsonAsync(string url, object content, bool notification=true);
        Task<TItem> PostFormAsync<TItem>(string url, MultipartFormDataContent content);
        Task<string> PostFormAsync(string url, MultipartFormDataContent content);
        Task<TItem> PutJsonAsync<TItem>(string url, object content);
        Task<string> PutJsonAsync(string url, object content);
        Task<TItem> PutFormAsync<TItem>(string url, MultipartFormDataContent content);
        Task<string> PutFormAsync(string url, MultipartFormDataContent content);
        Task<TItem> DeleteJsonAsync<TItem>(string url, object content = null);
        Task<string> DeleteJsonAsync(string url, object content = null);
        Task<string> GetNewJwt();
    }
}