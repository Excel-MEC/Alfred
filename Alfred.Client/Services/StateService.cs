using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Alfred.Client.Dtos.Admin;
using Alfred.Client.Dtos.Events;
using Alfred.Client.Models;
using Alfred.Client.Services.Interfaces;
using AutoMapper;

namespace Alfred.Client.Services
{
    public class StateService : IStateService
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private List<EventForListViewDto> EventsList { get; set; }
        private List<EventHead> EventHeads { get; set; }
        private List<Highlight> Highlights { get; set; }
        private Constants Constants { get; set; }
        private UserListResponseDto Users { get; set; }
        private List<StaffForListViewDto> Staffs { get; set; }


        public StateService(IApiService apiService, IMapper mapper)
        {
            _apiService = apiService;
            _mapper = mapper;
        }

        public async Task<List<EventForListViewDto>> GetEventList(bool refresh = false)
        {
            if (!refresh && EventsList != null) return EventsList;
            var client = await _apiService.Client();
            EventsList = await client.GetFromJsonAsync<List<EventForListViewDto>>("/events/api/events");
            client.Dispose();
            return EventsList;
        }

        public async Task<Constants> GetConstants(bool refresh = false)
        {
            if (!refresh && Constants != null) return Constants;
            var client = await _apiService.Client();
            Constants = await client.GetFromJsonAsync<Constants>("events/api/constants");
            client.Dispose();
            return Constants;
        }

        public async Task<List<EventHead>> GetEventHeads(bool refresh = false)
        {
            if (!refresh && EventHeads != null) return EventHeads;
            var client = await _apiService.Client();
            EventHeads = await client.GetFromJsonAsync<List<EventHead>>("events/api/eventhead");
            client.Dispose();
            return EventHeads;
        }

        public async Task<List<Highlight>> GetHighlights(bool refresh = false)
        {
            if (!refresh && Highlights != null) return Highlights;
            var client = await _apiService.Client();
            Highlights = await client.GetFromJsonAsync<List<Highlight>>("events/api/highlights");
            client.Dispose();
            return Highlights;
        }

        public async Task<UserListResponseDto> UserList(bool refresh = false)
        {
            if (!refresh && Users != null) return Users;
            var client = await _apiService.Client();
            Users = await client.GetFromJsonAsync<UserListResponseDto>(
                "https://staging.accounts.excelmec.org/api/admin/users");
            client.Dispose();
            return Users;
        }

        public async Task<List<StaffForListViewDto>> StaffList(bool refresh = false)
        {
            if (!refresh && Staffs != null) return Staffs;
            var client = await _apiService.Client();
            var staffResponse =
                await client.GetFromJsonAsync<List<UserForListViewDto>>(
                    "https://staging.accounts.excelmec.org/api/admin/staffs");
            Staffs = new List<StaffForListViewDto>();
            foreach (var staff in staffResponse)
                Staffs.Add(_mapper.Map<StaffForListViewDto>(staff));
            client.Dispose();
            return Staffs;
        }
    }
}