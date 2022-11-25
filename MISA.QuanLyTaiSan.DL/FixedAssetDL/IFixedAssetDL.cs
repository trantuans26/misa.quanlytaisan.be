using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA.QuanLyTaiSan.Common.Entities;
using MySqlConnector;

namespace MISA.QuanLyTaiSan.DL
{
    public interface IFixedAssetDL : IBaseDL<FixedAsset>
    {
        #region API Get
        /// <summary>
        /// Lấy thông tin 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID"></param>
        /// <returns>Thông tin 1 tài sản theo ID</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        public FixedAsset GetFixedAssetByID(Guid fixedAssetID);

        /// <summary>
        /// Lấy danh sách tài sản theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fixedAssetCategoryID"></param>
        /// <param name="departmentID"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetFixedAssetsFilter(string? keyword, Guid? fixedAssetCategoryID, Guid? departmentID, int? pageSize, int? pageIndex);
        #endregion

        #region API Post
        /// <summary>
        /// API thêm mới một tài sản
        /// </summary>
        /// <param name="fixedAsset">Đối tượng tài sản cần thêm mới</param>
        /// <returns>ID của tài sản vừa thêm mới</returns>
        /// Created by: Tuan (7/11/2022)
        public int InsertFixedAsset(FixedAsset fixedAsset);
        #endregion

        #region API Put
        #endregion

        #region API Delete
        #endregion
    }
}
