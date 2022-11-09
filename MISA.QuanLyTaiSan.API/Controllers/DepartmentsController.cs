using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QuanLyTaiSan.API.Entities;

namespace MISA.QuanLyTaiSan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// API thêm mới một phòng ban 
        /// </summary>
        /// <param name="department">Đối tượng phòng ban cần thêm mới</param>
        /// <returns>ID của phòng ban vừa thêm mới</returns>
        /// Created by: TTTuan (7/11/2022)
        [HttpPost]
        public IActionResult InsertDepartment([FromBody] Department department) 
        {
            return StatusCode(StatusCodes.Status201Created, Guid.NewGuid());
        }
 
        /// <summary>
        /// API sửa thông tin phòng ban theo ID
        /// </summary>
        /// <param name="departmentID">ID phòng ban muốn sửa</param>
        /// <param name="department">Đối tượng phòng ban muốn sửa</param>
        /// <returns>ID của phòng ban vừa sửa</returns>
        /// Created by: TTTuan (7/11/2022)
        [HttpPut("{departmentID}")]
        public IActionResult UpdateDepartment([FromRoute] Guid departmentID, [FromBody] Department department)
        {
            return StatusCode(StatusCodes.Status200OK, departmentID);
        }


    }
}
