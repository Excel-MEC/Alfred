using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Client.Dtos.Accounts;
using Alfred.Client.Dtos.Events;
using Alfred.Client.Models;

namespace Alfred.Client.Services.Interfaces
{
    public interface IStateService
    {
        Task<List<EventForListViewDto>> GetEventList(bool refresh = false);
        Task<Constants> GetConstants(bool refresh = false);
        Task<List<EventHead>> GetEventHeads(bool refresh = false);
        Task<List<Highlight>> GetHighlights(bool refresh = false);
        Task<List<StaffForListViewDto>> StaffList(bool refresh = false);
        Task<Dictionary<int, Institution>> GetColleges(bool refresh = false);
        Task<Dictionary<int, Institution>> GetSchools(bool refresh = false);
        public Action StateChanged { get; set; }
    }
}