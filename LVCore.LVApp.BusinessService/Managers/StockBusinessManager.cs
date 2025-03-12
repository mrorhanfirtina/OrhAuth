using AutoMapper;
using LVCore.LVApp.BusinessService.AutoMapper;
using LVCore.LVApp.BusinessService.Base;
using LVCore.LVApp.BusinessService.Services;
using LVCore.LVApp.DataAccessService.Repositories.Abstract;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.Shared.Dtos.ResponseDtos.StockDtos;
using LVCore.LVApp.Shared.Entities;
using LVCore.LVApp.Shared.Pagination;
using System.Threading.Tasks;

namespace LVCore.LVApp.BusinessService.Managers
{
    public class StockBusinessManager : BaseBusinessManager, IStockBusinessService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockBusinessManager(IStockRepository stockRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork) // 📌 BaseBusinessManager'ın constructor'ına UnitOfWork gönderiyoruz
        {
            _stockRepository = stockRepository;
            _mapper = AutoMapperConfig.GetMapper(); // 📌 AutoMapper buradan çekiliyor
        }

        /// <summary>
        /// 📌 Stok verisini ID'ye göre getirir.
        /// </summary>
        public async Task<StockResponseDto> GetStockByIdAsync(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            return _mapper.Map<StockResponseDto>(stock); // 📌 DTO dönüşümü burada gerçekleşiyor
        }

        

        /// <summary>
        /// 📌 Yeni bir stok ekler (transaction içinde)
        /// </summary>
        public async Task<int> AddStockAsync(Stock stock)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                int result = await _stockRepository.InsertAsync(stock);

                await CommitAsync(); // 📌 İşlemi onayla (UnitOfWork içinden)
                return result;
            }
            catch
            {
                Rollback(); // 📌 Hata durumunda rollback
                throw;
            }
        }

        public async Task<PaginatedList<StockResponseDto>> GetAllStockByPagination(int pageIndex, int pageSize)
        {
            var paginatedStock = await _stockRepository.GetAllWithPaginationAsync(pageIndex, pageSize);

            return _mapper.Map<PaginatedList<StockResponseDto>>(paginatedStock);
        }
    }
}
