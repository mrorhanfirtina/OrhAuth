using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.Repositories.Base;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Entities;

namespace LVCore.LVApp.DataAccessService.Repositories.Concrete
{
    public class OrderShipmentRepository : BaseRepository<OrderShipment>, IOrderShipmentRepository
    {
        public OrderShipmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
