using Alfred.Client.Dtos.Accounts;
using Alfred.Client.Models;

namespace Alfred.Client.Dtos.Events
{
    public class RegistrationFromRepoDto
    {
        public int Id { get; set; }
        public int ExcelId { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public UserForListViewDto User { get; set; }
    }
}