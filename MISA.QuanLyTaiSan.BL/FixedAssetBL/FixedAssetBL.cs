using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.QuanLyTaiSan.BL.BaseBL;
using MISA.QuanLyTaiSan.Common.Entities;
using MISA.QuanLyTaiSan.DL;

namespace MISA.QuanLyTaiSan.BL
{
    public class FixedAssetBL : BaseBL<FixedAsset>, IFixedAssetBL
    {
        #region Field

        private IFixedAssetDL _fixedAssetDL;

        #endregion

        #region Constructor
        public FixedAssetBL(IFixedAssetDL fixedAssetDL) : base(fixedAssetDL)
        {
            _fixedAssetDL = fixedAssetDL;
        }

        #endregion


        /// <summary>
        /// Lấy danh sách tất cả tài sản
        /// </summary>
        /// <returns>Danh sách tất cả tài sản</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        //public IEnumerable<dynamic> GetAllFixedAssets()
        //{
        //    return _fixedAssetDL.GetAllFixedAssets();
        //}

        /// <summary>
        /// Lấy thông tin 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID"></param>
        /// <returns>Thông tin 1 tài sản theo ID</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        public FixedAsset GetFixedAssetByID(Guid fixedAssetID)
        {
            return _fixedAssetDL.GetFixedAssetByID(fixedAssetID);
        }

        /// <summary>
        /// Lấy danh sách tài sản theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fixedAssetCategoryId"></param>
        /// <param name="departmentId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetFixedAssetsFilter(string? keyword, Guid? fixedAssetCategoryId, Guid? departmentId, int pageSize = 20, int pageIndex = 1)
        {
            return _fixedAssetDL.GetFixedAssetsFilter(keyword, fixedAssetCategoryId, departmentId, pageSize, pageIndex);
        }
    }
}
