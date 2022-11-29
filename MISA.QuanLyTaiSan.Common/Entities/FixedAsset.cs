using MISA.QuanLyTaiSan.Common.Attributes;

namespace MISA.QuanLyTaiSan.Common.Entities
{
    public class FixedAsset
    {
        /// <summary>
        /// ID tài sản
        /// </summary>
        [PrimaryKey]
        public Guid fixed_asset_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        [IsNotNullOrEmpty("Cần nhập thông tin Mã tài sản ")]
        public string? fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        [IsNotNullOrEmpty("Cần nhập thông tin Tên tài sản ")]
        public string? fixed_asset_name { get; set; }

        /// <summary>
        /// ID loại tài sản
        /// </summary>
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [IsNotNullOrEmpty("Cần nhập thông tin Mã phòng ban ")]
        public string? department_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string? department_name { get; set; }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [IsNotNullOrEmpty("Cần nhập thông tin Mã loại tài sản ")]
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? fixed_asset_category_name { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTime? purchase_date { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        [IsNotNullOrEmpty("Cần nhập thông tin Nguyên giá ")]
        public float? cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        [IsNotNullOrEmpty("Cần nhập thông tin Số lượng ")]
        public int? quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        [IsNotNullOrEmpty("Cần nhập thông tin Tỷ lệ hao mòn ")]
        public float? depreciation_rate { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi tài sản trên phần mềm
        /// </summary>
        public int? tracked_year { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        [IsNotNullOrEmpty("Cần nhập thông tin Số năm sử dụng ")]
        public int? life_time { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        public DateTime? production_date { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        public int? active { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? created_by { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? created_date { get; set; }

        /// <summary>
        /// Người sửa 
        /// </summary>
        public string? modified_by { get; set; }

        /// <summary>
        /// Ngày sửa 
        /// </summary>
        public DateTime? modified_date { get; set; }

    }
}
