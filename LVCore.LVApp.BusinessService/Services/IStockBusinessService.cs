using LVCore.LVApp.Shared.Dtos.ResponseDtos.StockDtos;
using LVCore.LVApp.Shared.Pagination;
using System.Threading.Tasks;

namespace LVCore.LVApp.BusinessService.Services
{
    public interface IStockBusinessService
    {
        Task<StockResponseDto> GetStockByIdAsync(int id);
        Task<PaginatedList<StockResponseDto>> GetAllStockByPagination(int pageIndex, int pageSize);
    }
}
