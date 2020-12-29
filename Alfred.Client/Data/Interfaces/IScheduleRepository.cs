using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Client.Dtos.Schedule;

namespace Alfred.Client.Data.Interfaces
{
    public interface IScheduleRepository
    {
        public Task<List<ScheduleDto>> GetSchedule();
        public Task<DataForAddingScheduleDto> AddSchedule(DataForAddingScheduleDto newSchedule);
        public Task<DataForAddingScheduleDto> DeleteSchedule(DataForDeletingScheduleDto dataForDeletingSchedule);
    }
}