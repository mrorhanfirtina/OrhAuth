using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.Repositories.Base;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Entities;

namespace LVCore.LVApp.DataAccessService.Repositories.Concrete
{
    public class MSRByDimElementsFieldsRepository : BaseRepository<MSRByDimElementsFields>, IMSRByDimElementsFieldsRepository
    {
        public MSRByDimElementsFieldsRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
