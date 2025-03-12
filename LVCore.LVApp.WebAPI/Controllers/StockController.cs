using LVCore.LVApp.BusinessService.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace LVCore.LVApp.WebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/v2/Stock")]
    [Authorize]
    public class StockController : ApiController
    {
        private readonly IStockBusinessService _stockBusinessService;

        public StockController(IStockBusinessService stockBusinessService)
        {
            _stockBusinessService = stockBusinessService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetStock/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> GetStockAsync(int id)
        {
            var response = await _stockBusinessService.GetStockByIdAsync(id);
            return Ok(response);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetAllStock/{pageIndex}/{pageSize}")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> GetAllStock(int pageIndex, int PageSize)
        {
            var response = await _stockBusinessService.GetAllStockByPagination(pageIndex, PageSize);
            return Ok(response);
        }
    }
}