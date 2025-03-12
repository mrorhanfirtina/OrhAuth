using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.Repositories.Base;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Entities;

namespace LVCore.LVApp.DataAccessService.Repositories.Concrete
{
    public class OrderItemAttrValueRepository : BaseRepository<OrderItemAttrValue>, IOrderItemAttrValueRepository
    {
        public OrderItemAttrValueRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
