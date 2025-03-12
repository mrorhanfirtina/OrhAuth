using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.Repositories.Base;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Entities;

namespace LVCore.LVApp.DataAccessService.Repositories.Concrete
{
    public class OrderCustomerRepository : BaseRepository<OrderCustomer>, IOrderCustomerRepository
    {
        public OrderCustomerRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
