using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Client.Data.Interfaces;
using Alfred.Client.Dtos.Schedule;
using Alfred.Client.Services.Interfaces;

namespace Alfred.Client.Data
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IApiService _apiService;
        private readonly ICustomNotification _notification;

        public ScheduleRepository(IApiService apiService, ICustomNotification notification)
        {
            _apiService = apiService;
            _notification = notification;
        }

        public async Task<List<ScheduleDto>> GetSchedule()
        {
            var schedule = await _apiService.GetFromJsonAsync<List<ScheduleDto>>("/events/api/Schedule");
            return schedule;
        }

        public async Task<DataForAddingScheduleDto> AddSchedule(DataForAddingScheduleDto newSchedule)
        {
            var schedule =
                await _apiService.PostJsonAsync<DataForAddingScheduleDto>("/events/api/Schedule", newSchedule);
            return schedule;
        }

        public async Task<DataForAddingScheduleDto> DeleteSchedule(DataForDeletingScheduleDto dataForDeletingSchedule)
        {
            var schedule = await _apiService.DeleteJsonAsync<DataForAddingScheduleDto>("/events/api/Schedule", dataForDeletingSchedule);
            return schedule;
        }
    }
}