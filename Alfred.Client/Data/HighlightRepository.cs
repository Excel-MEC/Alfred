using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Alfred.Client.Data.Interfaces;
using Alfred.Client.Dtos.Highlights;
using Alfred.Client.Models;
using Alfred.Client.Models.Components;
using Alfred.Client.Services;
using Alfred.Client.Services.Interfaces;
using Radzen;

namespace Alfred.Client.Data
{
    public class HighlightRepository : IHighlightRepository
    {
        private readonly IApiService _apiService;
        private readonly ICustomNotification _notification;
        private const string _url = "/events/api/highlights";
        private readonly IStateService _state;

        public HighlightRepository(IApiService apiService, ICustomNotification notification, IStateService state)
        {
            _apiService = apiService;
            _notification = notification;
            _state = state;
        }

        public async Task AddHighlight(DataForAddingHighlightDto newHighlight)
        {
            try
            {
                var client = await _apiService.Client();
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(newHighlight.Image.Data, (int) newHighlight.Image.Data.Length), "Image",
                    newHighlight.Image.Name);
                content.Add(new StringContent(newHighlight.Name), "Name");
                var response = await client.PostAsync(_url, content);
                if (response.IsSuccessStatusCode)
                    _notification.Success("Successfully Added new Highlight");
                else
                    _notification.Error("Something Went Wrong");

                client.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _notification.Error("Something Went Wrong");
            }
        }

        public async Task DeleteHighlight(Highlight highlight)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_url),
                Content = new StringContent(JsonSerializer.Serialize(new {Id = highlight.Id, Name = highlight.Name}),
                    Encoding.UTF8, "application/json")
            };
            var client = await _apiService.Client();
            var highlights = await _state.GetHighlights();
            await client.SendAsync(request);
            highlights.Remove(highlight);
            _notification.Success("Successfully Deleted");
            client.Dispose();
        }
    }
}