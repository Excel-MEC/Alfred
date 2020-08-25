using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Alfred.Client.Data.Interfaces;
using Alfred.Client.Dtos.Events;
using Alfred.Client.Services;
using Alfred.Client.Services.Interfaces;
using Radzen;

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

        public async Task AddEventHead(EventHead newEventHead)
        {
            try
            {
                var client = await _apiService.Client();
                var response = await client.PostAsJsonAsync(Url, newEventHead);
                if (response.IsSuccessStatusCode)
                    _notification.Success("Successfully added new EventHead");
                else
                    _notification.Error("Failed to add new Eventhead");
                client.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _notification.Error("Something Went Wrong");
            }
        }

        public async Task UpdateEventHead(EventHead eventHead)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(Url),
                    Content = new StringContent(JsonSerializer.Serialize(eventHead), Encoding.UTF8, "application/json")
                };
                var client = await _apiService.Client();
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    _notification.Success("Successfully updated");
                else
                    _notification.Error("Failed to Update");
                client.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _notification.Error("Something went wrong");
            }
        }

        public async Task DeleteEventHead(EventHead eventHead)
        {
            try
            {
                var eventHeads = await _stateService.GetEventHeads();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(Url),
                    Content = new StringContent(JsonSerializer.Serialize(eventHead), Encoding.UTF8, "application/json")
                };
                var client = await _apiService.Client();
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    _notification.Success("Successfully Deleted");
                    eventHeads.Remove(eventHead);
                }
                else
                    _notification.Error("Failed to Delete");
                client.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _notification.Error("Something Went Wrong");
            }
        }
    }
}