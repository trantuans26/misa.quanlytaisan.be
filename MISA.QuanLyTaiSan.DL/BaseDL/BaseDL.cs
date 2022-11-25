using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.QuanLyTaiSan.Common.Resources;

namespace MISA.QuanLyTaiSan.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region API GET
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        public IEnumerable<T> GetAllRecords()
        {
            // Khai báo tên stored procedure
            string storedProcedureName = String.Format(Resource.Proc_GetAll, typeof(T).Name);

            // Khởi tạo kết nối tới DB MySQL
            using (var mysqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                // Thực hiện gọi vào DB
                var records = mysqlConnection.Query<T>(
                    storedProcedureName,
                    commandType: System.Data.CommandType.StoredProcedure);

                return records;
            }
        }
        #endregion

        #region API POST
        #endregion

        #region API PUT
        #endregion

        #region API DELETE
        #endregion
    }
}
