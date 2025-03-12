using LVCore.LVApp.Shared.Config;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using Dapper;

namespace LVCore.LVApp.DataAccessService.Extensions
{
    public static class SequenceManager
    {
        private static readonly string _connectionString = AppConfigManager.GetConnectionString();

        /// <summary>
        /// 📌 Belirtilen tablo için seq_Value değerini +1 arttırır ve yeni değeri döndürür.
        /// </summary>
        public static async Task<int> GetNextSequenceValueAsync(string tableName)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"
                    UPDATE LV_Sequences
                    SET seq_Value = seq_Value + 1
                    OUTPUT inserted.seq_Value
                    WHERE seq_Table = @TableName";

                int newSequenceValue = await db.ExecuteScalarAsync<int>(sql, new { TableName = tableName });

                if (newSequenceValue == 0)
                    throw new Exception($"🚨 Hata: {tableName} için LV_Sequences tablosunda kayıt bulunamadı!");

                return newSequenceValue;
            }
        }
    }
}
