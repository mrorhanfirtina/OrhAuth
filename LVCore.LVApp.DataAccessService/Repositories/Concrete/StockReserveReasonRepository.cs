using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.Repositories.Base;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Entities;

namespace LVCore.LVApp.DataAccessService.Repositories.Concrete
{
    public class StockReserveReasonRepository : BaseRepository<StockReserveReason>, IStockReserveReasonRepository
    {
        public StockReserveReasonRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
