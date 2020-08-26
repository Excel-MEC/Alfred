using System.Collections.Generic;
using System.Threading.Tasks;
using Alfred.Client.Dtos.Admin;

namespace Alfred.Client.Data.Interfaces
{
    public interface IUserRepository
    {
        Task UpdateRole(DataForUpdatingRoleDto dataForUpdatingRole);
        Task<List<UserForListViewDto>> GetUsers(GetUserQueryParams queryParams);
    }
}