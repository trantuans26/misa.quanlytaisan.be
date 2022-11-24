using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using MISA.QuanLyTaiSan.Common.Entities;

namespace MISA.QuanLyTaiSan.DL
{
    public class FixedAssetDL : IFixedAssetDL
    {
        /// <summary>
        /// Lấy danh sách tất cả tài sản
        /// </summary>
        /// <returns>Danh sách tất cả tài sản</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        public IEnumerable<dynamic> GetAllFixedAssets()
        {
            // Khởi tạo kết nối tới DB MySQL
            var connectionString = DataContext.ConnectionString;
            var mySqlConnection = new MySqlConnection(connectionString);

            // Chuẩn bị câu lệnh SQ
            string storeProcedureName = "Proc_GetAllFixedAssets";

            // Chuẩn bị tham số đầu vào

            // Thực hiện gọi vào DB
            var fixedAssets = mySqlConnection.Query(storeProcedureName, commandType: System.Data.CommandType.StoredProcedure);

            // Xử lý kết quả trả về
            if (fixedAssets != null)
            {
                return fixedAssets;
            }
            return new List<FixedAsset>();
        }

        /// <summary>
        /// Lấy thông tin 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID"></param>
        /// <returns>Thông tin 1 tài sản theo ID</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        public FixedAsset GetFixedAssetByID(Guid fixedAssetID)
        {
            // Khởi tạo kết nối tới DB MySQL
            var connectionString = DataContext.ConnectionString;
            var mySqlConnection = new MySqlConnection(connectionString);

            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = "Proc_GetFixedAssetById";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$FixedAssetId", fixedAssetID);

            // Thực hiện gọi vào DB
            var fixedAsset = mySqlConnection.QueryFirstOrDefault<FixedAsset>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            // Xử lý kết quả trả về
            return fixedAsset;
        }

        /// <summary>
        /// Lấy danh sách tài sản theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fixedAssetCategoryID"></param>
        /// <param name="departmentID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetFixedAssetsFilter(string? keyword, Guid? fixedAssetCategoryID, Guid? departmentID, int pageSize = 20, int pageIndex = 1)
        {
            // Khởi tạo kết nối tới DB MySQL
            var connectionString = DataContext.ConnectionString;
            var mySqlConnection = new MySqlConnection(connectionString);

            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = "Proc_GetFixedAssetsFilter";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$Keyword", keyword);
            parameters.Add("$DepartmentId", departmentID);
            parameters.Add("$fixedAssetCategoryId", fixedAssetCategoryID);
            parameters.Add("$PageSize", pageSize);
            parameters.Add("$PageIndex", pageIndex);

            // Thực hiện gọi vào DB
            var fixedAssets = mySqlConnection.Query(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            // Xử lý kết quả trả về
            if (fixedAssets != null)
            {
                return fixedAssets;
            }
            return new List<FixedAsset>();
        }
    }
}
