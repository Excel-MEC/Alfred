using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Alfred.Client.Models;
using Alfred.Client.Services.Interfaces;
using Microsoft.JSInterop;

namespace Alfred.Client.Services
{
    public class AuthService : IAuthService
    {
        public User User { get; set; }
        private readonly IJSRuntime _jsRuntime;
        private readonly IApiService _apiService;

        public AuthService(IJSRuntime jsRuntime, IApiService apiService)
        {
            _jsRuntime = jsRuntime;
            _apiService = apiService;
        }

        public async Task Authorize()
        {
            try
            {
                var token = await _jsRuntime.InvokeAsync<string>("getJwt");
                if (string.IsNullOrEmpty(token) || token == "null")
                {
                    token = await _apiService.GetNewJwt();
                }

                string[] jwtEncodedSegments = token.Split('.');
                var payloadSegment = jwtEncodedSegments[1].Trim();
                payloadSegment =
                    payloadSegment.PadRight(payloadSegment.Length + (4 - payloadSegment.Length % 4) % 4, '=');

                var decodePayload = System.Convert.FromBase64String(payloadSegment);
                var decodedUtf8Payload = Encoding.UTF8.GetString(decodePayload);
                var result = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(decodedUtf8Payload);
                User = new User()
                {
                    Id = Convert.ToInt32(result["user_id"].ToString()),
                    Email = result["email"].ToString(),
                };
                try
                {
                    User.Roles = JsonSerializer.Deserialize<string[]>(result["role"].ToString());
                }
                catch
                {
                    User.Roles = result["role"].ToString().Split(",").Select(x => x.Trim()).ToArray();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}