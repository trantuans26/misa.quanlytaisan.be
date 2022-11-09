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
                string sqlCommand = "SELECT * FROM fixed_asset";

                // Chuẩn bị tham số đầu vào

                // Thực hiện gọi vào DB
                var fixedAssets = mySqlConnection.Query(sqlCommand);

                // Xử lý kết quả trả về
                if (fixedAssets != null)
                {
                    return StatusCode(StatusCodes.Status200OK, fixedAssets);
                }
                return StatusCode(StatusCodes.Status200OK, new List<FixedAsset>());


                // Try catch Exepction
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
                string sqlCommand = "SELECT * FROM fixed_asset";

                // Chuẩn bị tham số đầu vào

                // Thực hiện gọi vào DB
                var fixedAssets = mySqlConnection.Query(sqlCommand);

                // Xử lý kết quả trả về
                if (fixedAssets != null)
                {
                    return StatusCode(StatusCodes.Status200OK, fixedAssets);
                }
                return StatusCode(StatusCodes.Status200OK, new List<FixedAsset>());


                // Try catch Exepction
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

            return StatusCode(StatusCodes.Status200OK, new FixedAsset
            {
                FixedAssetId = fixedAssetID,
                FixedAssetCode = "TS000001",
                FixedAssetName = "Laptop Lenovo IdeaPad L340",
                OrganizationId = Guid.NewGuid(),
                OrganizationCode = "O00000001",
                OrganizationName = "MISA Corp",
                DepartmentId = Guid.NewGuid(),
                DepartmentCode = "P00001",
                DepartmentName = "Phòng nhân sự",
                FixedAssetCategoryId = Guid.NewGuid(),
                FixedAssetCategoryCode = "LTS000001",
                FixedAssetCategoryName = "Máy tính xách tay",
                PurchaseDate = DateTime.Now,
                Cost = 10000,
                Quantity = 10,
                DepreciationRate = 1.2f,
                TrackedYear = DateTime.Now.Year,
                LifeTime = 1,
                ProductionYear = DateTime.Now.Year,
                Active = 0,
                CreatedBy = "Trần Thái Tuấn",
                CreatedDate = DateTime.Now,
                ModifiedBy = "Trần Thái Tuấn",
                ModifiedDate = DateTime.Now
            });
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
            [FromQuery] Guid? fixedAssetCategoryID,
            [FromQuery] Guid? departmentID,
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 1
            )
        {
            return StatusCode(StatusCodes.Status200OK, new PagingResult
            {
                Data = new List<FixedAsset>
                {
                    new FixedAsset
                {
                    FixedAssetId = Guid.NewGuid(),
                    FixedAssetCode = "TS000001",
                    FixedAssetName = "Laptop Lenovo IdeaPad L340",
                    OrganizationId = Guid.NewGuid(),
                    OrganizationCode = "O00000001",
                    OrganizationName = "MISA Corp",
                    DepartmentId = Guid.NewGuid(),
                    DepartmentCode = "P00001",
                    DepartmentName = "Phòng nhân sự",
                    FixedAssetCategoryId = Guid.NewGuid(),
                    FixedAssetCategoryCode = "LTS000001",
                    FixedAssetCategoryName = "Máy tính xách tay",
                    PurchaseDate = DateTime.Now,
                    Cost = 10000,
                    Quantity = 10,
                    DepreciationRate = 1.2f,
                    TrackedYear = DateTime.Now.Year,
                    LifeTime = 1,
                    ProductionYear = DateTime.Now.Year,
                    Active = 0,
                    CreatedBy = "Trần Thái Tuấn",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "Trần Thái Tuấn",
                    ModifiedDate = DateTime.Now
                },
                    new FixedAsset
                {
                    FixedAssetId = Guid.NewGuid(),
                    FixedAssetCode = "TS000002",
                    FixedAssetName = "Laptop Lenovo ThinkPad X1 Carbon",
                    OrganizationId = Guid.NewGuid(),
                    OrganizationCode = "O00000001",
                    OrganizationName = "MISA Corp",
                    DepartmentId = Guid.NewGuid(),
                    DepartmentCode = "P00002",
                    DepartmentName = "Phòng hành chính",
                    FixedAssetCategoryId = Guid.NewGuid(),
                    FixedAssetCategoryCode = "LTS000001",
                    FixedAssetCategoryName = "Máy tính xách tay",
                    PurchaseDate = DateTime.Now,
                    Cost = 10000,
                    Quantity = 10,
                    DepreciationRate = 1.2f,
                    TrackedYear = DateTime.Now.Year,
                    LifeTime = 1,
                    ProductionYear = DateTime.Now.Year,
                    Active = 0,
                    CreatedBy = "Trần Thái Tuấn",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "Trần Thái Tuấn",
                    ModifiedDate = DateTime.Now
                },
                    new FixedAsset
                {
                    FixedAssetId = Guid.NewGuid(),
                    FixedAssetCode = "TS000003",
                    FixedAssetName = "Laptop Lenovo Legion 5",
                    OrganizationId = Guid.NewGuid(),
                    OrganizationCode = "O00000001",
                    OrganizationName = "MISA Corp",
                    DepartmentId = Guid.NewGuid(),
                    DepartmentCode = "P00003",
                    DepartmentName = "Phòng kế toán",
                    FixedAssetCategoryId = Guid.NewGuid(),
                    FixedAssetCategoryCode = "LTS000001",
                    FixedAssetCategoryName = "Máy tính xách tay",
                    PurchaseDate = DateTime.Now,
                    Cost = 10000,
                    Quantity = 10,
                    DepreciationRate = 1.2f,
                    TrackedYear = DateTime.Now.Year,
                    LifeTime = 1,
                    ProductionYear = DateTime.Now.Year,
                    Active = 0,
                    CreatedBy = "Trần Thái Tuấn",
                    CreatedDate = DateTime.Now,
                    ModifiedBy = "Trần Thái Tuấn",
                    ModifiedDate = DateTime.Now
                }
                },
                TotalCount = 3
            });
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
            return StatusCode(StatusCodes.Status201Created, Guid.NewGuid());
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
            return StatusCode(StatusCodes.Status200OK, fixedAssetID);
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
            return StatusCode(StatusCodes.Status200OK, fixedAssetID);
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
