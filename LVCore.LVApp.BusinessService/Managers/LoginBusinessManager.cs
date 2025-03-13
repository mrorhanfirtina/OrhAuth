using AutoMapper;
using LVCore.LVApp.BusinessService.Services;
using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.Shared.Entities;
using LVCore.LVAppService.Utilities;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Users> Login(string userLogin, string password)
        {
            try
            {
                var users = await _usersRepository.GetByConditionAsync(x => x.usr_Login == userLogin);

                if (users == null)
                {
                    return null;
                }

                var user = users.ToList().FirstOrDefault();

                var crpPassword = _crypto.EncryptString(password, true);

                if (user.usr_Password == crpPassword)
                {
                    return user;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
