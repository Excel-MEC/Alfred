using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Alfred.Client.Models.Custom;
using Alfred.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Alfred.Client.Services
{
    public class ApiService : IApiService
    {
        private readonly IJSRuntime _jsRuntime;
        private ICustomNotification _notification;
        public HttpStatusCode JwtExpiry => (HttpStatusCode) Enum.Parse(typeof(HttpStatusCode), "455", true);

        public ApiService(IJSRuntime jsRuntime, ICustomNotification notification)
        {
            _jsRuntime = jsRuntime;
            _notification = notification;
        }

        public async Task<HttpClient> Client(string header = null)
        {
            var jwt = header;
            if (string.IsNullOrEmpty(jwt))
            {
                jwt = await _jsRuntime.InvokeAsync<string>("getJwt");
                if (string.IsNullOrEmpty(jwt) || jwt == "null")
                    jwt = await GetNewJwt();
            }

            var client = new HttpClient()
            {
                BaseAddress = new Uri("https://staging.apis.excelmec.org")
            };
            client.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {jwt}");
            return client;
        }

        public async Task<TItem> GetFromJsonAsync<TItem>(string url)
        {
            var response = await GetAsync(url);
            return JsonConvert.DeserializeObject<TItem>(response);
        }

        public async Task<string> GetAsync(string url)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = GetUri(url),
            };
            return await SendAsync(request);
        }

        public async Task<TItem> PostJsonAsync<TItem>(string url, object content)
        {
            var response = await PostJsonAsync(url, content);
            return JsonConvert.DeserializeObject<TItem>(response);
        }

        public async Task<string> PostJsonAsync(string url, object content)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = GetUri(url),
                Content = GetStringContent(content)
            };
            return await SendAsync(request);
        }

        public async Task<TItem> PostFormAsync<TItem>(string url, MultipartFormDataContent content)
        {
            var response = await PostFormAsync(url, content);
            return JsonConvert.DeserializeObject<TItem>(response);

        }

        public async Task<string> PostFormAsync(string url, MultipartFormDataContent content)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = GetUri(url),
                Content = content
            };
            return await SendAsync(request);
        }

        public async Task<TItem> PutJsonAsync<TItem>(string url, object content)
        {
            var response = await PutJsonAsync(url, content);
            return JsonConvert.DeserializeObject<TItem>(response);
        }

        public async Task<string> PutJsonAsync(string url, object content)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = GetUri(url),
                Content = GetStringContent(content)
            };
            return await SendAsync(request);
        }

        public async Task<TItem> PutFormAsync<TItem>(string url, MultipartFormDataContent content)
        {
            var response = await PutFormAsync(url, content);
            return JsonConvert.DeserializeObject<TItem>(response);

        }

        public async Task<string> PutFormAsync(string url, MultipartFormDataContent content)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = GetUri(url),
                Content = content
            };
            return await SendAsync(request);
        }

        public async Task<TItem> DeleteJsonAsync<TItem>(string url, object content = null)
        {
            var response = await DeleteJsonAsync(url, content);
            return JsonConvert.DeserializeObject<TItem>(response);
        }

        public async Task<string> DeleteJsonAsync(string url, object content = null)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = GetUri(url),
            };
            if (content != null)
                request.Content = GetStringContent(content);
            return await SendAsync(request);
        }


        private async Task<string> SendAsync(HttpRequestMessage message)
        {
            var client = await Client();
            var response = await client.SendAsync(message);
            var responseText = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == JwtExpiry)
            {
                client.Dispose();
                var newJwt = await GetNewJwt();
                if (newJwt == null) return null;
                var newClient = await Client(newJwt);
                var newMessage = new HttpRequestMessage()
                {
                    Method = message.Method,
                    RequestUri = message.RequestUri,
                    Content = message.Content,
                };
                var newResponse = await newClient.SendAsync(newMessage);
                ShowNotification(newMessage.Method, newResponse.StatusCode);
                var newResponseText = await newResponse.Content.ReadAsStringAsync();
                newClient.Dispose();
                return newResponseText;
            }
            ShowNotification(message.Method, response.StatusCode);

            client.Dispose();
            return responseText;
        }

        public async Task<string> GetNewJwt()
        {
            var refreshToken = await _jsRuntime.InvokeAsync<string>("getRefreshToken");
            if (string.IsNullOrEmpty(refreshToken) || refreshToken == "null")
                return null;
            var client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = GetUri("/accounts/api/auth/refresh"),
                Content = new StringContent(JsonSerializer.Serialize(new {RefreshToken = refreshToken}), Encoding.UTF8,
                    "application/json")
            };
            var response = await client.SendAsync(request);
            var newTokens = await response.Content.ReadFromJsonAsync<Jwt>();
            client.Dispose();
            await _jsRuntime.InvokeVoidAsync("setJwt", newTokens.AccessToken);
            _notification.Info("Jwt renewed");
            return newTokens.AccessToken;
        }

        private Uri GetUri(string url)
        {
            if (!url.StartsWith("/"))
                url = $"/{url}";
            if (url.StartsWith("/accounts/"))
            {
                var newUrl = url.Replace("/accounts", "");
                return new Uri($"https://staging.accounts.excelmec.org{newUrl}");
            }
            else if (url.StartsWith("/events/"))
            {
                return new Uri($"https://staging.apis.excelmec.org{url}");
            }
            else
            {
                return new Uri(url);
            }
        }

        private StringContent GetStringContent(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        private void ShowNotification(HttpMethod method, HttpStatusCode responseStatus)
        {
            if (responseStatus == HttpStatusCode.OK)
            {
                string message = null;
                if (method == HttpMethod.Post)
                    message = "Successfully added";
                else if (method == HttpMethod.Put)
                    message = "Successfully updated";
                else if (method == HttpMethod.Delete)
                    message = "Successfully deleted";
                else
                    return;
                _notification.Success(message);
            }
            else if (responseStatus == HttpStatusCode.Forbidden)
            {
                _notification.Warning("You don't have permission to do that");
            }
            else
            {
                string message = null;
                if (method == HttpMethod.Get)
                    message = "Failed to fetch data";
                if (method == HttpMethod.Post)
                    message = "Failed to add data";
                else if (method == HttpMethod.Put)
                    message = "Failed to update data";
                else if (method == HttpMethod.Delete)
                    message = "Failed to delete data";
                else
                    return;
                _notification.Error(message);
            }
        }
    }
}