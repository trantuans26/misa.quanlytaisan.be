﻿namespace MISA.QuanLyTaiSan.Common.Attributes
{

    /// <summary>
    /// Attribute dùng để xác định 1 property là khóa chính 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {

    }

    /// <summary>
    /// Attribute dùng để xác định 1 property cần phải nhập thông tin
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IsNotNullOrEmptyAttribute : Attribute
    {
        #region Field

        /// <summary>
        /// Message lỗi trả về cho client
        /// </summary>
        public string ErrorMessage;

        #endregion

        #region Constructor

        public IsNotNullOrEmptyAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
    }

    /// <summary>
    /// Attribute dùng để xác định 1 property không được trùng lặp
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IsNotDuplicateAttribute : Attribute
    {
        #region Field

        /// <summary>
        /// Message lỗi trả về cho client
        /// </summary>
        public string ErrorMessage;

        #endregion

        #region Constructor

        public IsNotDuplicateAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        #endregion
    }

}
