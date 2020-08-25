using System.Threading.Tasks;
using Alfred.Client.Dtos.Events;

namespace Alfred.Client.Data.Interfaces
{
    public interface IEventHeadRepository
    {
        Task AddEventHead(EventHead newEventHead);
        Task UpdateEventHead(EventHead eventHead);
        Task DeleteEventHead(EventHead eventHead);
    }
}