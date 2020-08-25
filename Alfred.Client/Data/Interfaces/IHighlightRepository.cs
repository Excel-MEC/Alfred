using System.Threading.Tasks;
using Alfred.Client.Dtos.Events;
using Alfred.Client.Dtos.Highlights;
using Alfred.Client.Models;
using Alfred.Client.Models.Components;

namespace Alfred.Client.Data.Interfaces
{
    public interface IHighlightRepository
    {
        Task AddHighlight(DataForAddingHighlightDto newHighlight);
        Task DeleteHighlight(Highlight highlight);
    }
}