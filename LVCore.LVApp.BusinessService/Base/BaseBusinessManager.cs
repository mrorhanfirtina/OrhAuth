using LVCore.LVApp.DataAccessService.UoW;
using System.Threading.Tasks;
using System;

namespace LVCore.LVApp.BusinessService.Base
{
    public abstract class BaseBusinessManager : IBaseBusinessService
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseBusinessManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// 📌 Veritabanı işlemlerini commit eder.
        /// </summary>
        public async Task CommitAsync() // ✅ int yerine Task oldu
        {
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// 📌 İşlemleri geri alır (Rollback).
        /// </summary>
        public void Rollback()
        {
            _unitOfWork.Rollback();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
