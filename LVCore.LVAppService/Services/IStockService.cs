using System.Data;

namespace LVCore.LVAppService.Services
{
    public interface IStockService
    {
        DataSet OpenStockbyID(int StockID, object frm);
    }
}
