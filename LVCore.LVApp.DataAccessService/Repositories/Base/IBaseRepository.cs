using LVCore.LVApp.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LVCore.LVApp.DataAccessService.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> InsertAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);

        // Yeni metod: Belirtilen koşula göre veri çekme
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<PaginatedList<T>> GetAllWithPaginationAsync(int pageIndex, int pageSize);
        Task<PaginatedList<T>> GetByConditionWithPaginationAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);
    }
}
