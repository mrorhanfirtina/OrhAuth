using Dapper;
using LVCore.LVApp.Shared.Config;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LVCore.LVApp.DataAccessService.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork()
        {
            _connectionString = AppConfigManager.GetConnectionString();
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        /// <summary>
        /// 📌 Yeni Transaction başlatır.
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// 📌 **Transaction'ı commit eder.**
        /// </summary>
        public void Commit()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        /// <summary>
        /// 📌 **Transaction'ı async olarak commit eder.**
        /// </summary>
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await Task.Run(() => _transaction.Commit());
                _transaction.Dispose();
                _transaction = null;
            }
        }

        /// <summary>
        /// 📌 **Transaction'ı geri alır (Rollback).**
        /// </summary>
        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        /// <summary>
        /// 📌 **SQL sorgusu çalıştırır (Dapper).**
        /// </summary>
        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            return await _connection.ExecuteAsync(sql, param, _transaction);
        }

        /// <summary>
        /// 📌 **Bağlantıyı temizler ve kaynakları serbest bırakır.**
        /// </summary>
        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
