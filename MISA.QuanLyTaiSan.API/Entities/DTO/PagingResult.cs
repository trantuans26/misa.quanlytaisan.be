namespace MISA.QuanLyTaiSan.API.Entities.DTO
{
    /// <summary>
    /// Kết quả trả về của API lấy danh sách nhân viên theo bộ lọc
    /// </summary>
    public class PagingResult
    {
        /// <summary>
        /// Danh sách tài sản
        /// </summary>
        public List<FixedAsset> Data { get; set; }

        /// <summary>
        /// Số lượng tài sản
        /// </summary>
        public long TotalCount { get; set; }
    }
}
