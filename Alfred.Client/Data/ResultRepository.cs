using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Alfred.Client.Data.Interfaces;
using Alfred.Client.Dtos.Results;
using Alfred.Client.Models;
using Alfred.Client.Services.Interfaces;
using System.Collections.Generic;

namespace Alfred.Client.Data
{
    public class ResultRepository : IResultRepository
    {
        private readonly IApiService _apiService;
        private readonly ICustomNotification _notification;
        private const string _url = "/events/api/Result";
        private readonly IStateService _state;

        public ResultRepository(IApiService apiService, ICustomNotification notification, IStateService state)
        {
            _apiService = apiService;
            _notification = notification;
            _state = state;
        }        

        public async Task<List<ResultsForViewDto>> GetResults(int eventId)
        {
            var resultsFromRepo = await _apiService.GetFromJsonAsync<ResultsFromRepo>($"{_url}/event/{eventId}");
            if (resultsFromRepo == null) return new List<ResultsForViewDto>();
            return resultsFromRepo.Results;
        }

        public async Task<ResultsForViewDto> AddResult(DataForAddingResultDto newResult)
        {
            return await _apiService.PostJsonAsync<ResultsForViewDto>(_url, newResult);
        }

        public async Task<ResultsForViewDto> UpdateResult(ResultsForViewDto result)
        {
            return await _apiService.PutJsonAsync<ResultsForViewDto>(_url, result);
        }

        public async Task DeleteResult(ResultsForViewDto result)
        {
            try
            {
                var deletedResult = await _apiService.DeleteJsonAsync<ResultsForViewDto>(_url, new { Id = result.Id });                
            }
            catch
            {
                _notification.Error("Something went wrong");
            }
        }

        public async Task DeleteAllResult(int eventId)
        {
            try
            {
                var deletedResults = await _apiService.DeleteJsonAsync<List<ResultsForViewDto>>($"{_url}/event/{eventId}");
            }
            catch
            {
                _notification.Error("Something went wrong");
            }
        }
        
    }
}