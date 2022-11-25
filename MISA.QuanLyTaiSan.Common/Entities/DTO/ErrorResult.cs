using MISA.QuanLyTaiSan.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QuanLyTaiSan.Common.Entities
{
    public class ErrorResult
    {
        #region Property

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public FixedAssetErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Lời nhắn cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Lời nhắn cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Một số thông tin khác
        /// </summary>
        public Object MoreInfo { get; set; }

        /// <summary>
        /// Số nhận dạng theo dõi
        /// </summary>
        public string? TraceId { get; set; }

        #endregion

        #region Constructor

        public ErrorResult() { }

        public ErrorResult(FixedAssetErrorCode errorCode, string devMsg, string userMsg, Object moreInfo, string? traceId = null)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceId = traceId;
        }

        #endregion
    }
}
