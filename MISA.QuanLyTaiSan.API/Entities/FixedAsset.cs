namespace MISA.QuanLyTaiSan.API.Entities
{
    public class FixedAsset
    {
        /// <summary>
        /// ID tài sản
        /// </summary>
        public Guid fixed_asset_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string? fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string? fixed_asset_name { get; set; }

        /// <summary>
        /// ID đơn vị
        /// </summary>
        public Guid organization_id { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string? organization_code { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? organization_name { get; set; }

        /// <summary>
        /// ID loại tài sản
        /// </summary>
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
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
        public float? cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int? quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        public float? depreciation_rate { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi tài sản trên phần mềm
        /// </summary>
        public int? tracked_year { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? life_time { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        public int? production_year { get; set; }

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
