using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.Repositories.Base;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Entities;

namespace LVCore.LVApp.DataAccessService.Repositories.Concrete
{
    public class FormAttributesValuesRepository : BaseRepository<FormAttributesValues>, IFormAttributesValuesRepository
    {
        public FormAttributesValuesRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
