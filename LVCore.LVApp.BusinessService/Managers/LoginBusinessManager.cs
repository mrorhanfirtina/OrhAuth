using AutoMapper;
using LVCore.LVApp.BusinessService.Services;
using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVAppService.Utilities;

namespace LVCore.LVApp.BusinessService.Managers
{
    public class LoginBusinessManager : ILoginBusinessService
    {
        public readonly CCrypto _crypto;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public LoginBusinessManager(IUsersRepository usersRepository)
        {
            _crypto = new CCrypto();
            _usersRepository = usersRepository;

        }

        public bool Login(string userName, string password)
        {
            try
            {
                return true;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
