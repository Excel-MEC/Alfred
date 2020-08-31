using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Alfred.Client.Dtos.Accounts;
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
        private List<StaffForListViewDto> Staffs { get; set; }
        public Action StateChanged { get; set; }


        public StateService(IApiService apiService, IMapper mapper, ICustomNotification notification)
        {
            _apiService = apiService;
            _mapper = mapper;
        }

        public async Task<List<EventForListViewDto>> GetEventList(bool refresh = false)
        {
            if (!refresh && EventsList != null) return EventsList;
            EventsList = await _apiService.GetFromJsonAsync<List<EventForListViewDto>>("events/api/events");
            return EventsList;
        }

        public async Task<Constants> GetConstants(bool refresh = false)
        {
            if (!refresh && Constants != null) return Constants;
            Constants = await _apiService.GetFromJsonAsync<Constants>("events/api/constants");
            return Constants;
        }

        public async Task<List<EventHead>> GetEventHeads(bool refresh = false)
        {
            if (!refresh && EventHeads != null) return EventHeads;
            EventHeads = await _apiService.GetFromJsonAsync<List<EventHead>>("events/api/eventhead");
            return EventHeads;
        }

        public async Task<List<Highlight>> GetHighlights(bool refresh = false)
        {
            if (!refresh && Highlights != null) return Highlights;
            Highlights = await _apiService.GetFromJsonAsync<List<Highlight>>("events/api/highlights");
            return Highlights;
        }

        public async Task<List<StaffForListViewDto>> StaffList(bool refresh = false)
        {
            if (!refresh && Staffs != null) return Staffs;
            var staffResponse =
                await _apiService.GetFromJsonAsync<List<UserForListViewDto>>(
                    "accounts/api/admin/staffs");
            Staffs = new List<StaffForListViewDto>();
            foreach (var staff in staffResponse)
                Staffs.Add(_mapper.Map<StaffForListViewDto>(staff));
            return Staffs;
        }
    }
}