using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using Alfred.Client.Data.Interfaces;
using Alfred.Client.Dtos.Admin;
using Alfred.Client.Services;
using Alfred.Client.Services.Interfaces;

namespace Alfred.Client.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ICustomNotification _notification;
        private readonly IApiService _apiService;

        public UserRepository(IApiService apiService, ICustomNotification notification)
        {
            _apiService = apiService;
            _notification = notification;
        }

        public async Task UpdateRole(DataForUpdatingRoleDto dataForUpdatingRole)
        {
            var client = await _apiService.Client();
            if (string.IsNullOrEmpty(dataForUpdatingRole.Role) || string.IsNullOrWhiteSpace(dataForUpdatingRole.Role))
                dataForUpdatingRole.Role = "User";
            try
            {
                var response = await client.PostAsJsonAsync(
                    "https://staging.accounts.excelmec.org/api/admin/users/permission",
                    dataForUpdatingRole);
                if (response.IsSuccessStatusCode)
                    _notification.Success("Successfully updated the Role");
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                    _notification.Warning("You don't have the permission to do that");
                else
                    _notification.Error("Failed to update the Role");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _notification.Error("Something went wrong");
            }

            client.Dispose();
        }

        public async Task<List<UserForListViewDto>> GetUsers(GetUserQueryParams queryParams)
        {
            var properties = from p in queryParams.GetType().GetProperties()
                where p.GetValue(queryParams, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(queryParams, null).ToString());
            string queryString = String.Join("&", properties.ToArray());
            var client = await _apiService.Client();
            var users = await client.GetFromJsonAsync<UserListResponseDto>($"https://staging.accounts.excelmec.org/api/admin/users?{queryString}");
            client.Dispose();
            if(!users.Data.Any())
                _notification.Warning("No matching users");
            return users.Data;
        }
    }
}