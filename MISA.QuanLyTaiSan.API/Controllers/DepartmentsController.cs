using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QuanLyTaiSan.Common.Entities;
using MySqlConnector;

namespace MISA.QuanLyTaiSan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        #region API GET
        /// <summary>
        /// API lấy danh sách tất cả bộ phận sử dụng
        /// </summary>
        /// <returns>Danh sách tất cả bộ phận sử dụng</returns>
        /// Created by: TTTuan (7/11/2022)
        [HttpGet] // attribute get data
        public IActionResult GetAllDepartments()
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connectionString = "Server=localhost;Port=3306;Database=qlts_fresher_core;Uid=root;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQ
                string storeProcedureName = "Proc_GetAllDepartments";

                // Chuẩn bị tham số đầu vào

                // Thực hiện gọi vào DB
                var departments = mySqlConnection.Query(storeProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (departments != null)
                {
                    return StatusCode(StatusCodes.Status200OK, departments);
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
        #endregion API GET

    }
}
