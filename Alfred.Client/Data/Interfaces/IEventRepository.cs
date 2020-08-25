using System.Threading.Tasks;
using Alfred.Client.Dtos.Events;

namespace Alfred.Client.Data.Interfaces
{
    public interface IEventRepository
    {
        Task<EventForDetailedViewDto> GetEvent(int id);
        Task<DataForAddingEventDto> AddEvent(DataForAddingEventDto newEvent);
        Task<DataForAddingEventDto> UpdateEvent(DataForAddingEventDto updatedEvent, int id);
        Task DeleteEvent(EventForListViewDto eventForDelete);
    }
}