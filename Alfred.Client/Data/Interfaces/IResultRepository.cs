using System.Threading.Tasks;
using Alfred.Client.Dtos.Results;
using System.Collections.Generic;

namespace Alfred.Client.Data.Interfaces
{
    public interface IResultRepository
    {
        
        Task<List<ResultsForViewDto>> GetResults(int eventId);
        Task<ResultsForViewDto> AddResult(DataForAddingResultDto newResult);
        Task<ResultsForViewDto> UpdateResult(ResultsForViewDto result);
        Task DeleteResult(ResultsForViewDto result);
        Task DeleteAllResult(int eventId);
    }
}