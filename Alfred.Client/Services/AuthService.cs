using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
                var jwtHandler = new JwtSecurityTokenHandler();
                var tokens = jwtHandler.ReadToken(token) as JwtSecurityToken;
                User = new User()
                {
                    Id = Convert.ToInt32(tokens.Claims.First(x => x.Type == "user_id").Value),
                    Email = tokens.Claims.First(x => x.Type == "email").Value,
                    Roles = tokens.Claims.First(x => x.Type == "role").Value.Split(',')
                };
            }
            catch (Exception e)
            {
                
            }
        }
    }
}