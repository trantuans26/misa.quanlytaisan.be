using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QuanLyTaiSan.DL
{
    public interface IBaseDL<T>
    {
        #region API GET
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        public IEnumerable<T> GetAllRecords();
        #endregion

        #region API POST
        #endregion

        #region API PUT
        #endregion

        #region API DELETE
        #endregion
    }
}
