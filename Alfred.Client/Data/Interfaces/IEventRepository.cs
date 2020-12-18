using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Client.Dtos.Accounts;
using Alfred.Client.Dtos.Events;
using Alfred.Client.Models;

namespace Alfred.Client.Data.Interfaces
{
    public interface IEventRepository
    {
        Task<EventForDetailedViewDto> GetEvent(int id);
        Task<Event> AddEvent(DataForAddingEventDto newEvent);
        Task<Event> UpdateEvent(DataForAddingEventDto updatedEvent, int id);
        Task DeleteEvent(EventForListViewDto eventForDelete);
        Task<List<RegistrationForViewDto>> Registrations(int eventId);
    }
}