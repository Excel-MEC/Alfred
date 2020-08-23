using System;
using System.Net.Http;
using System.Threading.Tasks;
using Alfred.Client.Services.Interfaces;
using Microsoft.JSInterop;

namespace Alfred.Client.Services
{
    public class ApiService: IApiService
    {
        private readonly IJSRuntime _jsRuntime;
        private string _jwt;

        public ApiService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<HttpClient> Client()
        {
            if (string.IsNullOrEmpty(_jwt))
                _jwt = await _jsRuntime.InvokeAsync<string>("getJwt");
            var client = new HttpClient()
            {
                BaseAddress = new Uri("https://staging.apis.excelmec.org")
            };
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_jwt}");
            return client;
        }
    }
}