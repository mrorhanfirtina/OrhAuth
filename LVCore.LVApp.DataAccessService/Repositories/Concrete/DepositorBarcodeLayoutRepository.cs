using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.Repositories.Base;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Entities;

namespace LVCore.LVApp.DataAccessService.Repositories.Concrete
{
    public class DepositorBarcodeLayoutRepository : BaseRepository<DepositorBarcodeLayout>, IDepositorBarcodeLayoutRepository
    {
        public DepositorBarcodeLayoutRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
