using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using MISA.QuanLyTaiSan.Common.Entities;
using MISA.QuanLyTaiSan.Common.Resources;
using MISA.QuanLyTaiSan.Common.Attributes;
using System.Data;
using System.Transactions;

namespace MISA.QuanLyTaiSan.DL
{
    public class FixedAssetDL : BaseDL<FixedAsset>, IFixedAssetDL
    {
        #region API Get
        /// <summary>
        /// Lấy thông tin 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID"></param>
        /// <returns>Thông tin 1 tài sản theo ID</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        public FixedAsset GetFixedAssetByID(Guid fixedAssetID)
        {
            using(var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_FixedAsset_GetByID";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("$FixedAssetID", fixedAssetID);

                // Thực hiện gọi vào DB
                var fixedAsset = mySqlConnection.QueryFirstOrDefault<FixedAsset>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                return fixedAsset;
            }
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
        public IEnumerable<dynamic> GetFixedAssetsByFilter(string? keyword, Guid? fixedAssetCategoryID, Guid? departmentID, int? pageSize, int? pageIndex)
        {
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = Resource.Proc_GetFilter;

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
        #endregion

        #region API Post
        /// <summary>
        /// API thêm mới một tài sản
        /// </summary>
        /// <param name="fixedAsset">Đối tượng tài sản cần thêm mới</param>
        /// <returns>ID của tài sản vừa thêm mới</returns>
        /// Created by: Tuan (7/11/2022)
        public int InsertFixedAsset(FixedAsset fixedAsset)
        {
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = Resource.Proc_Insert;

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();

                var newFixedAssetID = Guid.NewGuid();
                parameters.Add("$FixedAssetId", newFixedAssetID);
                parameters.Add("$FixedAssetCode", fixedAsset.fixed_asset_code);
                parameters.Add("$FixedAssetName", fixedAsset.fixed_asset_name);
                parameters.Add("$DepartmentId", fixedAsset.department_id);
                parameters.Add("$DepartmentCode", fixedAsset.department_code);
                parameters.Add("$DepartmentName", fixedAsset.department_name);
                parameters.Add("$FixedAssetCategoryId", fixedAsset.fixed_asset_category_id);
                parameters.Add("$FixedAssetCategoryCode", fixedAsset.fixed_asset_category_code);
                parameters.Add("$FixedAssetCategoryName", fixedAsset.fixed_asset_category_name);
                parameters.Add("$PurchaseDate", fixedAsset.purchase_date);
                parameters.Add("$Cost", fixedAsset.cost);
                parameters.Add("$Quantity", fixedAsset.quantity);
                parameters.Add("$DepreciationRate", fixedAsset.depreciation_rate);
                parameters.Add("$TrackedYear", fixedAsset.tracked_year);
                parameters.Add("$LifeTime", fixedAsset.life_time);
                parameters.Add("$ProductionDate", fixedAsset.production_date);
                parameters.Add("$Active", fixedAsset.active);
                parameters.Add("$CreatedBy", Resource.DefaultUser);
                parameters.Add("$CreatedDate", DateTime.Now);
                parameters.Add("$ModifiedBy", Resource.DefaultUser);
                parameters.Add("$ModifiedDate", DateTime.Now);

                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
               
                // Xử lý kết quả trả về
                return numberOfRowsAffected;
            }
        }


        #endregion

        #region API Put
        /// <summary>
        /// API sửa thông tin 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID">ID tài sản muốn sửa</param>
        /// <param name="fixedAsset">Đối tượng tài sản muốn sửa</param>
        /// <returns>ID của tài sản vừa sửa</returns>
        /// Created by: TTTuan (7/11/2022)
        public int UpdateFixedAssetByID(Guid fixedAssetID, FixedAsset fixedAsset)
        {
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = Resource.Proc_UpdateByID;

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();

                parameters.Add("$FixedAssetID", fixedAssetID);
                parameters.Add("$FixedAssetCode", fixedAsset.fixed_asset_code);
                parameters.Add("$FixedAssetName", fixedAsset.fixed_asset_name);
                parameters.Add("$DepartmentID", fixedAsset.department_id);
                parameters.Add("$DepartmentCode", fixedAsset.department_code);
                parameters.Add("$DepartmentName", fixedAsset.department_name);
                parameters.Add("$FixedAssetCategoryID", fixedAsset.fixed_asset_category_id);
                parameters.Add("$FixedAssetCategoryCode", fixedAsset.fixed_asset_category_code);
                parameters.Add("$FixedAssetCategoryName", fixedAsset.fixed_asset_category_name);
                parameters.Add("$PurchaseDate", fixedAsset.purchase_date);
                parameters.Add("$Cost", fixedAsset.cost);
                parameters.Add("$Quantity", fixedAsset.quantity);
                parameters.Add("$DepreciationRate", fixedAsset.depreciation_rate);
                parameters.Add("$TrackedYear", fixedAsset.tracked_year);
                parameters.Add("$LifeTime", fixedAsset.life_time);
                parameters.Add("$ProductionDate", fixedAsset.production_date);
                parameters.Add("$Active", fixedAsset.active);
                parameters.Add("$ModifiedBy", "Trần Thái Tuấn");
                parameters.Add("$ModifiedDate", DateTime.Now);

                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                return numberOfRowsAffected;
            }
        }
        #endregion

        #region API Delete
        /// <summary>
        /// API xoá 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID">ID của tài sản muốn xoá</param>
        /// <returns>ID của tài sản vừa xoá</returns>
        /// Created by: TTTuan (7/11/2022)
        public int DeleteFixedAssetByID(Guid fixedAssetID)
        {
            var numberOfAffectedRows = 0;
            //khởi tạo kết nối db
            string connectionString = DataContext.ConnectionString;
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                //nếu như kết nối đang đóng thì tiến hành mở lại
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                }
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        string storedProcedureName = Resource.Proc_DeleteByID;

                        var parameters = new DynamicParameters();
                        parameters.Add("$FixedAssetID", fixedAssetID);

                        numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                        if (numberOfAffectedRows > 0)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                            numberOfAffectedRows = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        //nếu thực hiện không thành công thì rollback
                        transaction.Rollback();
                        numberOfAffectedRows = 0;
                    }
                    finally
                    {
                        mySqlConnection.Close();
                    }
                }

            }
            return numberOfAffectedRows;
        }

        /// <summary>
        /// API xoá nhiều tài sản theo IDs
        /// </summary>
        /// <param name="listFixedAssetID">IDs của tài sản muốn xoá</param>
        /// <returns>ID của tài sản vừa xoá</returns>
        /// Created by: TTTuan (7/11/2022)
        public int DeleteFixedAssetByIDs(ListFixedAssetID listFixedAssetID)
        {
            var numberOfRowsAffected = 0;

            string connectionString = DataContext.ConnectionString;
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                //Nếu kết nối đang đóng thì tiến hành mở lại
                if (mySqlConnection.State != ConnectionState.Open)
                {
                    mySqlConnection.Open();
                }
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        string storedProcedureName = Resource.Proc_DeleteByIDs;
                        var parameters = new DynamicParameters();
                        parameters.Add("$FixedAssetIDs", listFixedAssetID.FixedAssetIDs);

                        numberOfRowsAffected = mySqlConnection.Execute(storedProcedureName, parameters, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                        if (numberOfRowsAffected > 0)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                            numberOfRowsAffected = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        transaction.Rollback();
                        numberOfRowsAffected = 0;
                    }
                    finally
                    {
                        mySqlConnection.Close();
                    }
                }
            }
            return numberOfRowsAffected;
        }
        #endregion
    }
}
