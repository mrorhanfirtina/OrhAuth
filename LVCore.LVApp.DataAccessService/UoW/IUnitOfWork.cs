using System;
using System.Data;
using System.Threading.Tasks;

namespace LVCore.LVApp.DataAccessService.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        void BeginTransaction();
        void Commit();
        Task CommitAsync(); // ✅ Async commit eklendi
        void Rollback();
        Task<int> ExecuteAsync(string sql, object param = null);
    }
}
