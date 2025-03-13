using LVCore.LVApp.Shared.Entities;
using System.Threading.Tasks;

namespace LVCore.LVApp.BusinessService.Services
{
    public interface ILoginBusinessService
    {
        Task<Users> Login(string userLogin, string password);
    }
}
