using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QuanLyTaiSan.BL;
using MISA.QuanLyTaiSan.Common.Entities;
using MISA.QuanLyTaiSan.Common.Enums;
using MISA.QuanLyTaiSan.Common.Resources;
using MySqlConnector;

namespace MISA.QuanLyTaiSan.API.Controllers
{
    [Route("api/v1/[controller]")] //Attribute
    [ApiController]
    public class FixedAssetsController : BasesController<FixedAsset> // extends, implements
    {
        #region Field

        private IFixedAssetBL _fixedAssetBL;

        #endregion

        #region Constructor

        public FixedAssetsController(IFixedAssetBL fixedAssetBL) : base(fixedAssetBL)
        {
            _fixedAssetBL = fixedAssetBL;
        }

        #endregion

        #region Method

        #region API GET

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
                var fixedAsset = _fixedAssetBL.GetFixedAssetByID(fixedAssetID);

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
        /// <param name="pageSize"> Số bản ghi muốn lấy</param>
        /// <param name="pageIndex"> Vị trí dấu trang</param>
        /// <returns>Danh sách nhân viên và tổng số bản ghi</returns>
        /// Created by: TTTuan (7/11/2022)
        [HttpGet("filter")]
        public IActionResult GetFixedAssetsByFilter(
            [FromQuery] string? keyword,
            [FromQuery] Guid? fixedAssetCategoryID,
            [FromQuery] Guid? departmentID,
            [FromQuery] int? pageSize,
            [FromQuery] int? pageIndex
            )
        {
            try
            {
                var fixedAssets = _fixedAssetBL.GetFixedAssetsByFilter(keyword, fixedAssetCategoryID, departmentID, pageSize, pageIndex);

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
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                      FixedAssetErrorCode.Exception,
                      Resource.DevMsg_Exception,
                      Resource.UserMsg_Exception,
                      Resource.MoreInfo_Exception,
                      HttpContext.TraceIdentifier));
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
                int numberOfRowsAffected = _fixedAssetBL.InsertFixedAsset(fixedAsset);
                 
                // Xử lý kết quả trả về
                if (numberOfRowsAffected > 0) 
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(
                    FixedAssetErrorCode.DuplicateCode,
                    Resource.DevMsg_ValidateFailed,
                    Resource.UserMsg_ValidateFailed,
                    Resource.MoreInfo_ValidateFailed,
                    HttpContext.TraceIdentifier));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    FixedAssetErrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier));
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
        public IActionResult UpdateFixedAssetByID([FromRoute] Guid fixedAssetID, [FromBody] FixedAsset fixedAsset)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connectionString = "Server=localhost;Port=3306;Database=qlts_fresher_core;Uid=root;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_FixedAsset_UpdateByID";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();

                parameters.Add("$FixedAssetID", fixedAssetID);
                parameters.Add("$FixedAssetCode", fixedAsset.fixed_asset_code);
                parameters.Add("$FixedAssetName", fixedAsset.fixed_asset_name);
                parameters.Add("$OrganizationID", fixedAsset.organization_id);
                parameters.Add("$OrganizationCode", fixedAsset.organization_code);
                parameters.Add("$OrganizationName", fixedAsset.organization_name);
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

        #endregion 
    }
}
