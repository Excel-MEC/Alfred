﻿using System;
using System.Collections.Generic;
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

        public AuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Authorize()
        {
            try
            {
                var token = await _jsRuntime.InvokeAsync<string>("getJwt");
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
                    Roles = result["role"].ToString().Split(",")
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}