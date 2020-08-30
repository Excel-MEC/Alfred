using System.Threading.Tasks;
using Alfred.Client.Models;

namespace Alfred.Client.Data.Interfaces
{
    public interface IEventHeadRepository
    {
        Task<EventHead> AddEventHead(EventHead newEventHead);
        Task UpdateEventHead(EventHead eventHead);
        Task<EventHead> DeleteEventHead(EventHead eventHead);
    }
}