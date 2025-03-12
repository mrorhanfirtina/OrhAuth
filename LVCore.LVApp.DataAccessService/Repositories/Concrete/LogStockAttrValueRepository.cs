using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.Repositories.Base;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Entities;

namespace LVCore.LVApp.DataAccessService.Repositories.Concrete
{
    public class LogStockAttrValueRepository : BaseRepository<LogStockAttrValue>, ILogStockAttrValueRepository
    {
        public LogStockAttrValueRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
