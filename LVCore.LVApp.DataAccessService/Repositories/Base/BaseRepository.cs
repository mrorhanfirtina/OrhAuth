using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using LVCore.LVApp.Shared.Attributes;
using LVCore.LVApp.DataAccessService.UoW;
using LVCore.LVApp.DataAccessService.Extensions;
using LVCore.LVApp.Shared.Pagination;

namespace LVCore.LVApp.DataAccessService.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly string _tableName;
        private readonly List<string> _columns;
        private readonly string _primaryKeyColumn;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            // 📌 Tablo adını belirle
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
            if (tableAttr == null)
                throw new InvalidOperationException($"[Table] attribute eksik: {typeof(T).Name}");

            _tableName = tableAttr.Name;

            // 📌 Sütun adlarını belirle
            var columnProperties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<ColumnAttribute>() != null)
                .ToList();

            _columns = columnProperties
                .Select(p => p.GetCustomAttribute<ColumnAttribute>().Name)
                .ToList();

            // 📌 Primary Key sütununu bul
            var primaryKeyProperty = columnProperties.FirstOrDefault(p => p.GetCustomAttribute<ColumnAttribute>().IsPrimaryKey);
            if (primaryKeyProperty == null)
                throw new InvalidOperationException($"Primary Key belirtilmemiş: {typeof(T).Name}");

            _primaryKeyColumn = primaryKeyProperty.GetCustomAttribute<ColumnAttribute>().Name;

            // 📌 Dapper için manuel sütun eşleştirme yap
            Dapper.SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(
                typeof(T),
                (type, columnName) =>
                {
                    return type.GetProperties()
                        .FirstOrDefault(prop => prop.GetCustomAttribute<ColumnAttribute>()?.Name == columnName);
                }));
        }

        /// <summary>
        /// 📌 Tüm kayıtları getirir
        /// </summary>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            string sql = $"SELECT {string.Join(", ", _columns)} FROM {_tableName}";
            return await _unitOfWork.Connection.QueryAsync<T>(sql, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// 📌 ID’ye göre veri getirir.
        /// </summary>
        public async Task<T> GetByIdAsync(int id)
        {
            string sql = $"SELECT {string.Join(", ", _columns)} FROM {_tableName} WHERE {_primaryKeyColumn} = @Id";
            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id }, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// 📌 Yeni kayıt ekler.
        /// </summary>
        public async Task<int> InsertAsync(T entity)
        {
            // 📌 Eğer Primary Key manuel olarak atanıyorsa, SequenceManager ile yeni ID al
            var primaryKeyProperty = typeof(T).GetProperties()
                .FirstOrDefault(p => p.GetCustomAttribute<ColumnAttribute>()?.IsPrimaryKey == true);

            if (primaryKeyProperty != null)
            {
                int newPrimaryKeyValue = await SequenceManager.GetNextSequenceValueAsync(_tableName);
                primaryKeyProperty.SetValue(entity, newPrimaryKeyValue);
            }

            var columns = string.Join(", ", _columns);
            var parameters = string.Join(", ", _columns.Select(c => "@" + c));

            string sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({parameters})";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// 📌 Veriyi günceller.
        /// </summary>
        public async Task<int> UpdateAsync(T entity)
        {
            var setClause = string.Join(", ", _columns.Select(c => $"{c} = @{c}"));

            string sql = $"UPDATE {_tableName} SET {setClause} WHERE {_primaryKeyColumn} = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, entity, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// 📌 Veriyi siler.
        /// </summary>
        public async Task<int> DeleteAsync(int id)
        {
            string sql = $"DELETE FROM {_tableName} WHERE {_primaryKeyColumn} = @Id";
            return await _unitOfWork.Connection.ExecuteAsync(sql, new { Id = id }, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// 📌 Dinamik filtreleme ile belirli bir koşula göre veri getirir.
        /// </summary>
        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            var whereClause = new List<string>();
            var parameters = new DynamicParameters();

            if (predicate.Body is BinaryExpression body)
            {
                var propertyName = ((MemberExpression)body.Left).Member.Name;
                var columnAttribute = GetColumnMappings(typeof(T)).FirstOrDefault(c => c.PropertyName == propertyName);

                if (string.IsNullOrEmpty(columnAttribute.DbColumnName))
                {
                    throw new Exception($"'{propertyName}' için veritabanında karşılık gelen bir sütun bulunamadı.");
                }

                var columnName = columnAttribute.DbColumnName;
                whereClause.Add($"{columnName} = @{propertyName}");
                parameters.Add(propertyName, Expression.Lambda(body.Right).Compile().DynamicInvoke());
            }

            string sqlQuery = $"SELECT {string.Join(", ", _columns)} FROM {_tableName} WHERE {string.Join(" AND ", whereClause)}";
            return await _unitOfWork.Connection.QueryAsync<T>(sqlQuery, parameters, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// 📌 Pagination ile verileri çekmek.
        /// </summary>
        public async Task<PaginatedList<T>> GetAllWithPaginationAsync(int pageIndex, int pageSize)
        {
            if (pageIndex < 1) throw new ArgumentException("Page number must be greater than 0", nameof(pageIndex));
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than 0", nameof(pageSize));

            int offset = (pageIndex - 1) * pageSize;

            // 📌 Toplam kayıt sayısını al
            string countSql = $"SELECT COUNT(*) FROM {_tableName}";
            int totalCount = await _unitOfWork.Connection.ExecuteScalarAsync<int>(countSql, transaction: _unitOfWork.Transaction);

            // 📌 Sayfalı veri çek
            string sql = $@"
                            SELECT {string.Join(", ", _columns)}
                            FROM {_tableName}
                            ORDER BY {_primaryKeyColumn} 
                            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var items = await _unitOfWork.Connection.QueryAsync<T>(
                sql,
                new { Offset = offset, PageSize = pageSize },
                transaction: _unitOfWork.Transaction
            );

            return PaginatedList<T>.Create(items, totalCount, pageIndex, pageSize);
        }


        /// <summary>
        /// 📌 Dinamik filtreleme ile belirli bir koşula göre verileri getirir ve pagination uygular.
        /// </summary>
        public async Task<PaginatedList<T>> GetByConditionWithPaginationAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
        {
            if (pageIndex < 1) throw new ArgumentException("Page number must be greater than 0", nameof(pageIndex));
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than 0", nameof(pageSize));

            var whereClause = new List<string>();
            var parameters = new DynamicParameters();

            if (predicate.Body is BinaryExpression body)
            {
                var propertyName = ((MemberExpression)body.Left).Member.Name;
                var columnAttribute = GetColumnMappings(typeof(T)).FirstOrDefault(c => c.PropertyName == propertyName);

                if (string.IsNullOrEmpty(columnAttribute.DbColumnName))
                {
                    throw new Exception($"'{propertyName}' için veritabanında karşılık gelen bir sütun bulunamadı.");
                }

                var columnName = columnAttribute.DbColumnName;
                whereClause.Add($"{columnName} = @{propertyName}");
                parameters.Add(propertyName, Expression.Lambda(body.Right).Compile().DynamicInvoke());
            }

            string whereStatement = whereClause.Any() ? $"WHERE {string.Join(" AND ", whereClause)}" : "";

            // 📌 Toplam kayıt sayısını al
            string countSql = $"SELECT COUNT(*) FROM {_tableName} {whereStatement}";
            int totalCount = await _unitOfWork.Connection.ExecuteScalarAsync<int>(countSql, parameters, transaction: _unitOfWork.Transaction);

            // 📌 Sayfalı veri çek
            int offset = (pageIndex - 1) * pageSize;
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", pageSize);

            string sqlQuery = $@"
                                 SELECT {string.Join(", ", _columns)}
                                 FROM {_tableName}
                                 {whereStatement}
                                 ORDER BY {_primaryKeyColumn}
                                 OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var items = await _unitOfWork.Connection.QueryAsync<T>(sqlQuery, parameters, transaction: _unitOfWork.Transaction);

            return PaginatedList<T>.Create(items, totalCount, pageIndex, pageSize);
        }


        /// <summary>
        /// 📌 `ColumnAttribute` içindeki veritabanı sütun adlarını döndüren metod (private)
        /// </summary>
        private List<(string PropertyName, string DbColumnName)> GetColumnMappings(Type type)
        {
            return type.GetProperties()
                .Select(p =>
                {
                    var columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                    return (p.Name, columnAttr?.Name ?? p.Name);
                })
                .ToList();
        }
    }
}
