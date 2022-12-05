using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QuanLyTaiSan.BL;
using MISA.QuanLyTaiSan.Common.Entities;
using MISA.QuanLyTaiSan.Common.Enums;
using MISA.QuanLyTaiSan.Common.Resources;

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
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                      ErrorCode.Exception,
                      Resource.DevMsg_Exception,
                      Resource.UserMsg_Exception,
                      Resource.MoreInfo_Exception,
                      HttpContext.TraceIdentifier));
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
                      ErrorCode.Exception,
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
                var response = _fixedAssetBL.InsertFixedAsset(fixedAsset);
                 
                // Xử lý kết quả trả về
                if (response.Success == (int)StatusResponse.Done) 
                {
                    return StatusCode(StatusCodes.Status201Created, response.Data);
                }
                return StatusCode(StatusCodes.Status400BadRequest, response.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
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
                var response = _fixedAssetBL.UpdateFixedAssetByID(fixedAssetID, fixedAsset);
                if (response.Success == (int)StatusResponse.Failed)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, response.Data);
                }
                else if (response.Success == (int)StatusResponse.Done)
                {
                    return StatusCode(StatusCodes.Status200OK, response.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                        ErrorCode.Exception,
                        Resource.DevMsg_Exception,
                        Resource.UserMsg_Exception,
                        Resource.MoreInfo_Exception,
                        HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier));
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
        public IActionResult DeleteFixedAssetByID([FromRoute] Guid fixedAssetID)
        {
            try
            {
                var numberOfAffectedRows = _fixedAssetBL.DeleteFixedAssetByID(fixedAssetID);
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, fixedAssetID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.DeleteFailed,
                    Resource.DevMsg_DeleteFailed,
                    Resource.UserMsg_DeleteFailed,
                    Resource.MoreInfo_DeleteFailed,
                    HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_DeleteFailed,
                    HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// API xoá nhiều tài sản theo danh sách ID
        /// </summary>
        /// <param name="FixedAssetIDs">Danh sách ID của các tài sản muốn xoá</param>
        /// <returns>Status code 200</returns> Hỏi a advisor: trong dự án họ return về cái gì, 
        /// có nhiều dự án họ return về danh sách ID của các bạn xoá
        /// Created by: TTTuan (7/11/2022)
        [HttpPost("deleteBatch")]
        public IActionResult DeleteFixedAssetByIDs([FromBody] ListFixedAssetID listFixedAssetID)
        {
            try
            {
                var numberOfAffectedRows = _fixedAssetBL.DeleteFixedAssetByIDs(listFixedAssetID);
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, listFixedAssetID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.DeleteFailed,
                    Resource.DevMsg_DeleteFailed,
                    Resource.UserMsg_DeleteFailed,
                    listFixedAssetID.FixedAssetIDs,
                    HttpContext.TraceIdentifier));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    Resource.DevMsg_Exception,
                    Resource.UserMsg_Exception,
                    Resource.MoreInfo_DeleteFailed,
                    HttpContext.TraceIdentifier));
            }
        }
        #endregion

        #endregion 
    }
}
