using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.QuanLyTaiSan.BL;
using MISA.QuanLyTaiSan.Common.Entities;
using MySqlConnector;

namespace MISA.QuanLyTaiSan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetCategoriesController : BasesController<FixedAssetCategory>
    {
        #region Field

        private IFixedAssetCategoryBL _fixedAssetCategoryBL;

        #endregion

        #region Contructor

        public FixedAssetCategoriesController(IFixedAssetCategoryBL fixedAssetCategoryBL) : base(fixedAssetCategoryBL)
        {
            _fixedAssetCategoryBL = fixedAssetCategoryBL;
        }

        #endregion
    }
}
