using System.Threading.Tasks;
using Alfred.Client.Models;

namespace Alfred.Client.Services.Interfaces
{
    public interface IAuthService
    {
        public User User { get; set; }
        public Task Authorize();
    }
}