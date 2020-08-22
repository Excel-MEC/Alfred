using System.Net.Http;
using System.Threading.Tasks;

namespace Alfred.Client.Services.Interfaces
{
    public interface IApiService
    {
        Task<HttpClient> Client();
    }
}