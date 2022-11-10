using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QuanLyTaiSan.API.Entities;
using MISA.QuanLyTaiSan.API.Entities.DTO;
using MySqlConnector;

namespace MISA.QuanLyTaiSan.API.Controllers
{
    [Route("api/v1/[controller]")] //Attribute
    [ApiController]
    public class FixedAssetsController : ControllerBase // extends, implements
    {
        #region API GET
        /// <summary>
        /// API lấy danh sách tất cả tài sản
        /// </summary>
        /// <returns>Danh sách tất cả tài sản</returns>
        /// Created by: TTTuan (7/11/2022)
        [HttpGet] // attribute get data
        public IActionResult GetAllFixedAssets()
        {
            try
            {  
                // Khởi tạo kết nối tới DB MySQL
                var connectionString = "Server=localhost;Port=3306;Database=qlts_fresher_core;Uid=root;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_GetAllFixedAssets";

                // Chuẩn bị tham số đầu vào

                // Thực hiện gọi vào DB
                var fixedAssets = mySqlConnection.Query(storeProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (fixedAssets != null)
                {
                    return StatusCode(StatusCodes.Status200OK, fixedAssets);
                }
                return StatusCode(StatusCodes.Status200OK, new List<FixedAsset>());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception",
                    UseMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA",
                    MoreInfor = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }

        /// <summary>
        /// API lấy một tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID">ID tài sản muốn lấy</param>
        /// <returns>Thông tin nhân viên muốn lấy</returns>
        /// Created by: TTTuan (7/11/2022)
        [HttpGet("{fixedAssetID}")]
        public IActionResult GetFixedAssetByID([FromRoute] Guid fixedAssetID)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connectionString = "Server=localhost;Port=3306;Database=qlts_fresher_core;Uid=root;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_GetFixedAssetById";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("$FixedAssetId", fixedAssetID);

                // Thực hiện gọi vào DB
                var fixedAsset = mySqlConnection.QueryFirstOrDefault<FixedAsset>(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (fixedAsset != null)
                {
                    return StatusCode(StatusCodes.Status200OK, fixedAsset);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception",
                    UseMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA",
                    MoreInfor = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }

        /// <summary>
        /// API lấy danh sách tài sản theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khoá muốn tìm kiếm</param>
        /// <param name="fixedAssetCategoryID">ID loại tài sản</param>
        /// <param name="departmentID">ID bộ phận sử dụng</param>
        /// <param name="limit"> Số bản ghi muốn lấy</param>
        /// <param name="offset"> Vị trí bắt đầu lấy</param>
        /// <returns>Danh sách nhân viên và tổng số bản ghi</returns>
        /// Created by: TTTuan (7/11/2022)
        [HttpGet("filter")]
        public IActionResult GetFixedAssetsFilter(
            [FromQuery] string? keyword,
            [FromQuery] Guid? fixedAssetCategoryId,
            [FromQuery] Guid? departmentId,
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 1
            )
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connectionString = "Server=localhost;Port=3306;Database=qlts_fresher_core;Uid=root;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_GetFixedAssetsFilter";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("$Keyword", keyword);
                parameters.Add("$DepartmentId", departmentId);
                parameters.Add("$fixedAssetCategoryId", fixedAssetCategoryId);               
                parameters.Add("$PageSize", limit);
                parameters.Add("$PageIndex", offset);

                // Thực hiện gọi vào DB
                var fixedAssets = mySqlConnection.Query(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (fixedAssets != null)
                {
                    return StatusCode(StatusCodes.Status200OK, fixedAssets);
                }
                return StatusCode(StatusCodes.Status200OK, new List<FixedAsset>());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception",
                    UseMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA",
                    MoreInfor = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }
        #endregion

        #region API POST
        /// <summary>
        /// API thêm mới một tài sản
        /// </summary>
        /// <param name="fixedAsset">Đối tượng tài sản cần thêm mới</param>
        /// <returns>ID của tài sản vừa thêm mới</returns>
        /// Created by: Tuan (7/11/2022)
        [HttpPost]
        public IActionResult InsertFixedAsset([FromBody] FixedAsset fixedAsset)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connectionString = "Server=localhost;Port=3306;Database=qlts_fresher_core;Uid=root;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_InsertFixedAsset";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();

                var newFixedAssetID = Guid.NewGuid();
                parameters.Add("$FixedAssetId", newFixedAssetID);
                parameters.Add("$FixedAssetCode", fixedAsset.fixed_asset_code);
                parameters.Add("$FixedAssetName", fixedAsset.fixed_asset_name);
                parameters.Add("$OrganizationId", fixedAsset.organization_id);
                parameters.Add("$OrganizationCode", fixedAsset.organization_code);
                parameters.Add("$OrganizationName", fixedAsset.organization_name);
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
                parameters.Add("$ProductionYear", fixedAsset.production_year);
                parameters.Add("$Active", fixedAsset.active);
                parameters.Add("$CreatedBy", "Trần Thái Tuấn");
                parameters.Add("$CreatedDate", DateTime.Now);
                parameters.Add("$ModifiedBy", "Trần Thái Tuấn");
                parameters.Add("$ModifiedDate", DateTime.Now);

                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                // Xử lý kết quả trả về
                if (numberOfRowsAffected > 0) 
                {
                    return StatusCode(StatusCodes.Status201Created, newFixedAssetID);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    ErrorCode = 2,
                    DevMsg = "Database insert failed",
                    UseMsg = "Thêm mới tài sản thất bại",
                    MoreInfor = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception",
                    UseMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA",
                    MoreInfor = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }
        #endregion

        #region API PUT
        /// <summary>
        /// API sửa thông tin 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID">ID tài sản muốn sửa</param>
        /// <param name="fixedAsset">Đối tượng tài sản muốn sửa</param>
        /// <returns>ID của tài sản vừa sửa</returns>
        /// Created by: TTTuan (7/11/2022)
        [HttpPut("{fixedAssetID}")]
        public IActionResult UpdateFixedAsset([FromRoute] Guid fixedAssetID, [FromBody] FixedAsset fixedAsset)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connectionString = "Server=localhost;Port=3306;Database=qlts_fresher_core;Uid=root;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_UpdateFixedAssetById";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();

                parameters.Add("$FixedAssetId", fixedAssetID);
                parameters.Add("$FixedAssetCode", fixedAsset.fixed_asset_code);
                parameters.Add("$FixedAssetName", fixedAsset.fixed_asset_name);
                parameters.Add("$OrganizationId", fixedAsset.organization_id);
                parameters.Add("$OrganizationCode", fixedAsset.organization_code);
                parameters.Add("$OrganizationName", fixedAsset.organization_name);
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
                parameters.Add("$ProductionYear", fixedAsset.production_year);
                parameters.Add("$Active", fixedAsset.active);
                parameters.Add("$ModifiedBy", "Trần Thái Tuấn");
                parameters.Add("$ModifiedDate", DateTime.Now);

                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, fixedAssetID);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    ErrorCode = 2,
                    DevMsg = "Database update failed",
                    UseMsg = "Sửa tài sản thất bại",
                    MoreInfor = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception",
                    UseMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA",
                    MoreInfor = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }
        #endregion

        #region API DELETE
        /// <summary>
        /// API xoá 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID">ID của tài sản muốn xoá</param>
        /// <returns>ID của tài sản vừa xoá</returns>
        /// Created by: TTTuan (7/11/2022)
        [HttpDelete("{fixedAssetID}")]
        public IActionResult DeleteFixedAsset([FromRoute] Guid fixedAssetID)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connectionString = "Server=localhost;Port=3306;Database=qlts_fresher_core;Uid=root;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_DeleteFixedAssetById";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();

                parameters.Add("$FixedAssetId", fixedAssetID);

                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, fixedAssetID);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 3,
                    DevMsg = "Database delete failed",
                    UseMsg = "Xoá tài sản thất bại",
                    MoreInfor = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception",
                    UseMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA",
                    MoreInfor = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier,
                });
            }
        }

        /// <summary>
        /// API xoá nhiều tài sản theo danh sách ID
        /// </summary>
        /// <param name="listFixedAssetID">Danh sách ID của các tài sản muốn xoá</param>
        /// <returns>Status code 200</returns> Hỏi a advisor: trong dự án họ return về cái gì, 
        /// có nhiều dự án họ return về danh sách ID của các bạn xoá
        /// Created by: TTTuan (7/11/2022)
        [HttpPost("deleteBatch")]
        public IActionResult DeleteMultipleFixedAssets([FromBody] ListFixedAssetID listFixedAssetID)
        {
            return StatusCode(StatusCodes.Status200OK);
        }
        #endregion
    }
}
