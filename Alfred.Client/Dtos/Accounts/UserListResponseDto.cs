using System.Collections.Generic;
using Alfred.Client.Models;
using Alfred.Client.Models.Custom;

namespace Alfred.Client.Dtos.Accounts
{
    public class UserListResponseDto
    {
        public List<UserForListViewDto> Data { get; set; }
        public Pagination Pagination { get; set; }

        public UserListResponseDto()
        {
            Data = new List<UserForListViewDto>();
            Pagination = new Pagination();
        }
    }
}