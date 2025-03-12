using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.Repositories.Base;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Entities;

namespace LVCore.LVApp.DataAccessService.Repositories.Concrete
{
    public class TaskPriorityRepository : BaseRepository<TaskPriority>, ITaskPriorityRepository
    {
        public TaskPriorityRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
