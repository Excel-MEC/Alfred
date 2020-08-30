using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Client.Dtos.Admin;
using Alfred.Client.Models;

namespace Alfred.Client.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<StaffForListViewDto> UpdateRole(DataForUpdatingRoleDto dataForUpdatingRole);
        Task<List<UserForListViewDto>> GetUsers(GetUserQueryParams queryParams);
    }
}