using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QuanLyTaiSan.Common.Entities
{
    /// <summary>
    /// Dữ liệu trả về tầng BL
    /// </summary>
    /// Created by: TTTuan
    /// Date: 20/11/2022
    public class ServiceResponse
    {
        /// <summary>
        /// Response có thành công không?
        /// </summary>
        /// Created by: TTTuan
        /// Date: 20/11/2022
        public int Success { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>        
        /// Created by: TTTuan
        /// Date: 20/11/2022
        public object? Data { get; set; }

    }
}
