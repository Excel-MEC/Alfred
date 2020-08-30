using System;
using System.Linq;
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

        public async Task<Highlight> AddHighlight(DataForAddingHighlightDto newHighlight)
        {
            var content = new MultipartFormDataContent();
            content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            content.Add(new StreamContent(newHighlight.Image.Data, (int) newHighlight.Image.Data.Length), "Image",
                newHighlight.Image.Name);
            content.Add(new StringContent(newHighlight.Name), "Name");
            var highlight = await _apiService.PostFormAsync<Highlight>(_url, content);
            return highlight;
        }

        public async Task DeleteHighlight(Highlight highlight)
        {
            try
            {
                var deletedHighlight=
                    await _apiService.DeleteJsonAsync<Highlight>(_url, new {Id = highlight.Id, Name = highlight.Name});
                var highlights = await _state.GetHighlights();
                var highlightInState = highlights.FirstOrDefault(x => x.Id == deletedHighlight.Id);
                highlights.Remove(highlightInState);
            }
            catch
            {
                _notification.Error("Something went wrong");
            }
        }
    }
}