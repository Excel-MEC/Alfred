using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Alfred.Client.Dtos.Events;
using Alfred.Client.Models;
using Alfred.Client.Services.Interfaces;

namespace Alfred.Client.Services
{
    public class StateService: IStateService
    {
        private readonly IApiService _apiService;
        private List<EventForListViewDto> EventsList { get; set; }
        private  List<EventHead> EventHeads { get; set; }
        private List<Highlight> Highlights { get; set; }
        private Constants Constants { get; set; }
        

        public StateService(IApiService apiService)
        {
            _apiService = apiService;
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
            if (!refresh && EventHeads!= null) return EventHeads;
            var client = await _apiService.Client();
            EventHeads = await client.GetFromJsonAsync<List<EventHead>>("events/api/eventhead");
            client.Dispose();
            return EventHeads;
        }

        public async Task<List<Highlight>> GetHighlights(bool refresh = false)
        {
            if (!refresh && Highlights!= null) return Highlights;
            var client = await _apiService.Client();
            Highlights = await client.GetFromJsonAsync<List<Highlight>>("events/api/highlights");
            client.Dispose();
            return Highlights;
        }
    }
}