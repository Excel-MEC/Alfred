using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using Alfred.Client.Data.Interfaces;
using Alfred.Client.Dtos.Accounts;
using Alfred.Client.Models;
using Alfred.Client.Services;
using Alfred.Client.Services.Interfaces;
using AutoMapper;

namespace Alfred.Client.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ICustomNotification _notification;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;

        public UserRepository(IApiService apiService, ICustomNotification notification, IMapper mapper)
        {
            _apiService = apiService;
            _notification = notification;
            _mapper = mapper;
        }

        public async Task<StaffForListViewDto> UpdateRole(DataForUpdatingRoleDto dataForUpdatingRole)
        {
            if (string.IsNullOrEmpty(dataForUpdatingRole.Role) || string.IsNullOrWhiteSpace(dataForUpdatingRole.Role))
                dataForUpdatingRole.Role = "User";
            var updatedUser = await _apiService.PostJsonAsync<UserForListViewDto>(
                "accounts/api/admin/users/permission", dataForUpdatingRole);
            var updatedStaff = _mapper.Map<StaffForListViewDto>(updatedUser);
            return updatedStaff;
        }

        public async Task<List<UserForListViewDto>> GetUsers(GetUserQueryParams queryParams)
        {
            var properties = from p in queryParams.GetType().GetProperties()
                where p.GetValue(queryParams, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(queryParams, null).ToString());
            string queryString = String.Join("&", properties.ToArray());
            Console.WriteLine(queryString);
            var users = await _apiService.GetFromJsonAsync<UserListResponseDto>(
                $"accounts/api/admin/users?{queryString}");
            if (!users.Data.Any())
                _notification.Warning("No matching users");
            return users.Data;
        }
    }
}