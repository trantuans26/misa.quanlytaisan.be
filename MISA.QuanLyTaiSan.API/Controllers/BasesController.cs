using Microsoft.AspNetCore.Mvc;
using MISA.QuanLyTaiSan.BL;
using MISA.QuanLyTaiSan.Common.Entities;
using MISA.QuanLyTaiSan.Common.Enums;
using MISA.QuanLyTaiSan.Common.Resources;

namespace MISA.QuanLyTaiSan.API.Controllers
{
    [Route("api/v1/[controller]")] //Attribute
    [ApiController]
    public class BasesController<T> : Controller
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method
        #region API Get
        /// <summary>
        /// API lấy danh sách tất cả tài sản
        /// </summary>
        /// <returns>Danh sách tất cả tài sản</returns>
        /// Created by: TTTuan (7/11/2022)
        [HttpGet] // attribute get data
        public IActionResult GetAllRecords()
        {
            try
            {
                var records = _baseBL.GetAllRecords();

                // Xử lý kết quả trả về
                if (records != null)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
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

        #region API Post
        #endregion

        #region API Put
        #endregion

        #region API Delete
        #endregion
        #endregion
    }
}
