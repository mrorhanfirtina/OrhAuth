using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.Repositories.Base;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Entities;

namespace LVCore.LVApp.DataAccessService.Repositories.Concrete
{
    public class MSRByDimensionRepository : BaseRepository<MSRByDimension>, IMSRByDimensionRepository
    {
        public MSRByDimensionRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
