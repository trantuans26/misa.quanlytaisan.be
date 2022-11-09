using MISA.QuanLyTaiSan.API.Enums;

namespace MISA.QuanLyTaiSan.API.Entities
{
    /// <summary>
    /// Phòng ban
    /// </summary>
    public class Department
    {
        /// <summary>
        /// ID Phòng ban
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepratmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Có phải là cha không
        /// </summary>
        public IsParent IsParent { get; set; }

        /// <summary>
        /// Id phòng ban cha
        /// </summary>
        public Guid ParentID { get; set; }

        /// <summary>
        /// ID đơn vị
        /// </summary>
        public Guid OrganizationID { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime ModifiedDate { get; set; }


    }
}
