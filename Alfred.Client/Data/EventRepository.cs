using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Alfred.Client.Data.Interfaces;
using Alfred.Client.Dtos.Events;
using Alfred.Client.Services.Interfaces;

namespace Alfred.Client.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly IApiService _apiService;
        private readonly IStateService _stateService;
        private readonly ICustomNotification _notification;
        private const string Url = "/events/api/events";

        public EventRepository(IApiService apiService, IStateService stateService, ICustomNotification notification)
        {
            _apiService = apiService;
            _stateService = stateService;
            _notification = notification;
        }

        public async Task<EventForDetailedViewDto> GetEvent(int id)
        {
            var client = await _apiService.Client();
            var eventForView = await client.GetFromJsonAsync<EventForDetailedViewDto>($"{Url}/{id}");
            client.Dispose();
            return eventForView;
        }

        public async Task<DataForAddingEventDto> AddEvent(DataForAddingEventDto newEvent)
        {
            try
            {
                var client = await _apiService.Client();
                if (newEvent.Icon.Data == null)
                {
                    _notification.Error("Icon Should not be null");
                    throw new Exception("Icon Should not be null");
                }

                var content = GetFormDataContent(newEvent);
                var response = await client.PostAsync("/events/api/events", content);
                if (response.IsSuccessStatusCode)
                {
                    _notification.Success("Successfully Added new Event");
                    newEvent.Icon.Data?.Dispose();
                    client.Dispose();
                    return new DataForAddingEventDto();
                }

                _notification.Error("Error During uploading");

                client.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _notification.Error("Something Went Wrong");
            }

            return newEvent;
        }

        public async Task<DataForAddingEventDto> UpdateEvent(DataForAddingEventDto updatedEvent, int id)
        {
            try
            {
                var client = await _apiService.Client();
                var content = GetFormDataContent(updatedEvent);
                content.Add(new StringContent(id.ToString()), "Id");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(Url),
                    Content = content
                };
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    _notification.Success("Successfully Updated Event");
                else
                    _notification.Error("Error During uploading");

                client.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _notification.Error("Something Went Wrong");
            }

            return updatedEvent;
        }

        public async Task DeleteEvent(EventForListViewDto eventForDelete)
        {
            try
            {
                var events = await _stateService.GetEventList();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(Url),
                    Content = new StringContent(
                        JsonSerializer.Serialize(new {Id = eventForDelete.Id, Name = eventForDelete.Name}),
                        Encoding.UTF8,
                        "application/json")
                };
                var client = await _apiService.Client();
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    _notification.Success("Successfully Deleted Event");
                else
                    _notification.Error("Failed to Delete Event");

                events.Remove(eventForDelete);
                client.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _notification.Error("Something Went Wrong");
            }
        }

        private MultipartFormDataContent GetFormDataContent(DataForAddingEventDto newEvent)
        {
            var content = new MultipartFormDataContent();
            content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            content.Add(new StringContent(newEvent.Name), "Name");
            if (newEvent.Icon.Data != null)
                content.Add(new StreamContent(newEvent.Icon.Data, (int) newEvent.Icon.Data.Length), "Icon",
                    newEvent.Icon.Name);
            content.Add(new StringContent(newEvent.CategoryId.ToString()), "CategoryId");
            content.Add(new StringContent(newEvent.EventTypeId.ToString()), "EventTypeId");
            content.Add(new StringContent(newEvent.About), "About");
            if (!string.IsNullOrEmpty(newEvent.Format))
                content.Add(new StringContent(newEvent.Format), "Format");
            if (!string.IsNullOrEmpty(newEvent.Rules))
                content.Add(new StringContent(newEvent.Rules), "Rules");
            content.Add(new StringContent(newEvent.Venue), "Venue");
            content.Add(new StringContent(newEvent.Day.ToString()), "Day");
            content.Add(new StringContent(newEvent.Datetime.ToLongDateString()), "Datetime");
            content.Add(new StringContent(newEvent.NumberOfRounds.ToString()), "NumberOfRounds");
            content.Add(new StringContent(newEvent.CurrentRound.ToString()), "CurrentRound");
            content.Add(new StringContent(newEvent.EventStatusId.ToString()), "EventStatusId");
            content.Add(new StringContent(newEvent.EntryFee.ToString()), "EntryFee");
            content.Add(new StringContent(newEvent.PrizeMoney.ToString()), "PrizeMoney");
            content.Add(new StringContent(newEvent.NeedRegistration.ToString()), "NeedRegistration");
            content.Add(new StringContent(newEvent.IsTeam.ToString()), "IsTeam");
            content.Add(new StringContent(newEvent.EventHead1Id.ToString()), "EventHead1Id");
            content.Add(new StringContent(newEvent.EventHead2Id.ToString()), "EventHead2Id");
            return content;
        }
    }
}