using MISA.QuanLyTaiSan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QuanLyTaiSan.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method
        #region API GET
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }
        #endregion API GET

        #region API POST
        #endregion

        #region API PUT
        #endregion

        #region API DELETE
        #endregion
        #endregion
    }
}
