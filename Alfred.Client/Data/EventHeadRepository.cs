using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Alfred.Client.Data.Interfaces;
using Alfred.Client.Models;
using Alfred.Client.Services.Interfaces;

namespace Alfred.Client.Data
{
    public class EventHeadRepository : IEventHeadRepository
    {
        private readonly ICustomNotification _notification;
        private readonly IApiService _apiService;
        private readonly IStateService _stateService;
        private const string Url = "/events/api/eventhead";

        public EventHeadRepository(ICustomNotification notification, IApiService apiService, IStateService stateService)
        {
            _notification = notification;
            _apiService = apiService;
            _stateService = stateService;
        }

        public async Task<EventHead> AddEventHead(EventHead newEventHead)
        {
            return await _apiService.PostJsonAsync<EventHead>(Url, newEventHead);
        }

        public async Task UpdateEventHead(EventHead eventHead)
        {
            try
            {
                await _apiService.PutJsonAsync(Url, eventHead);
            }
            catch
            {
                _notification.Error("Something went wrong");
            }
        }

        public async Task<EventHead> DeleteEventHead(EventHead eventHead)
        {
            return await _apiService.DeleteJsonAsync<EventHead>(Url, eventHead);
        }
    }
}