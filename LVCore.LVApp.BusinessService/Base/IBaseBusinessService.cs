using System.Threading.Tasks;
using System;

namespace LVCore.LVApp.BusinessService.Base
{
    public interface IBaseBusinessService : IDisposable
    {
        Task CommitAsync();
        void Rollback();
    }
}
