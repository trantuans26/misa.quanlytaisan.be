using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.QuanLyTaiSan.Common.Entities;
using MISA.QuanLyTaiSan.Common.Enums;
using MISA.QuanLyTaiSan.Common.Resources;
using MISA.QuanLyTaiSan.Common.Attributes;
using MISA.QuanLyTaiSan.DL;

namespace MISA.QuanLyTaiSan.BL
{
    public class FixedAssetBL : BaseBL<FixedAsset>, IFixedAssetBL
    {
        #region Field

        private IFixedAssetDL _fixedAssetDL;

        #endregion

        #region Constructor
        public FixedAssetBL(IFixedAssetDL fixedAssetDL) : base(fixedAssetDL)
        {
            _fixedAssetDL = fixedAssetDL;
        }
        #endregion

        #region Method
        #region API Get
        /// <summary>
        /// Lấy danh sách tất cả tài sản
        /// </summary>
        /// <returns>Danh sách tất cả tài sản</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        //public IEnumerable<dynamic> GetAllFixedAssets()
        //{
        //    return _fixedAssetDL.GetAllFixedAssets();
        //}

        /// <summary>
        /// Lấy thông tin 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID"></param>
        /// <returns>Thông tin 1 tài sản theo ID</returns>
        /// Created by: Tuan
        /// Date: 10/11/2022
        public FixedAsset GetFixedAssetByID(Guid fixedAssetID)
        {
            return _fixedAssetDL.GetFixedAssetByID(fixedAssetID);
        }

        /// <summary>
        /// Lấy danh sách tài sản theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fixedAssetCategoryId"></param>
        /// <param name="departmentId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetFixedAssetsByFilter(string? keyword, Guid? fixedAssetCategoryId, Guid? departmentId, int? pageSize, int? pageIndex)
        {
            return _fixedAssetDL.GetFixedAssetsByFilter(keyword, fixedAssetCategoryId, departmentId, pageSize, pageIndex);
        }
        #endregion

        #region API Post
        /// <summary>
        /// API thêm mới một tài sản
        /// </summary>
        /// <param name="fixedAsset">Đối tượng tài sản cần thêm mới</param>
        /// <returns>ID của tài sản vừa thêm mới</returns>
        /// Created by: Tuan (7/11/2022)
        public ServiceResponse InsertFixedAsset(FixedAsset fixedAsset)
        {
            var validateResult = ValidateRequestData(fixedAsset);
            if (validateResult != null && validateResult.Success == (int)StatusResponse.Done)
            {
                var validateResultDetail = ValidateDetail(fixedAsset);
                if (validateResultDetail != null && validateResultDetail.Success == (int)StatusResponse.Done)
                {
                    var numberOfRowsAffected = _fixedAssetDL.InsertFixedAsset(fixedAsset);

                    if (numberOfRowsAffected > 0)
                    {
                        return new ServiceResponse
                        {
                            Success = (int)StatusResponse.Done,
                            Data = fixedAsset.fixed_asset_id
                        };
                    }
                    else
                    {
                        return new ServiceResponse
                        {
                            Success = (int)StatusResponse.Exception,
                            Data = new ErrorResult(
                            ErrorCode.InvalidInput,
                            Resource.DevMsg_ValidateFailed,
                            Resource.UserMsg_ValidateFailed,
                            Resource.MoreInfo_ValidateFailed)
                        };
                    }
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = (int)StatusResponse.Failed,
                        Data = validateResultDetail?.Data
                    };
                }
            }
            else
            {
                return new ServiceResponse
                {
                    Success = (int)StatusResponse.Failed,
                    Data = validateResult?.Data
                };
            }
        }
        #endregion

        #region API Put
        /// <summary>
        /// API sửa thông tin 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID">ID tài sản muốn sửa</param>
        /// <param name="fixedAsset">Đối tượng tài sản muốn sửa</param>
        /// <returns>ID của tài sản vừa sửa</returns>
        /// Created by: TTTuan (7/11/2022)
        public ServiceResponse UpdateFixedAssetByID(Guid fixedAssetID, FixedAsset fixedAsset)
        {
            var validateResult = ValidateRequestData(fixedAsset);

            if (validateResult != null && validateResult.Success == (int)StatusResponse.Done)
            {
                var validateResultDetail = ValidateDetail(fixedAsset);
                if (validateResultDetail != null && validateResultDetail.Success == (int)StatusResponse.Done)
                {
                    var numberOfRowsAffected = _fixedAssetDL.UpdateFixedAssetByID(fixedAssetID, fixedAsset);

                    if (numberOfRowsAffected != -1)
                    {
                        return new ServiceResponse
                        {
                            Success = (int)StatusResponse.Done,
                            Data = fixedAssetID
                        };
                    }
                    else
                    {
                        return new ServiceResponse
                        {
                            Success = (int)StatusResponse.Exception,
                            Data = new ErrorResult(
                            ErrorCode.Exception,
                            Resource.DevMsg_UpdateFailed,
                            Resource.UserMsg_UpdateFailed,
                            Resource.MoreInfo_UpdateFailed)
                        };
                    }
                }
                else
                {
                    return new ServiceResponse
                    {
                        Success = (int)StatusResponse.Failed,
                        Data = validateResultDetail?.Data
                    };
                }
            }
            else
            {
                return new ServiceResponse
                {
                    Success = (int)StatusResponse.Failed,
                    Data = validateResult?.Data
                };
            }
        }
        #endregion

        #region API Delete
        /// <summary>
        /// API xoá 1 tài sản theo ID
        /// </summary>
        /// <param name="fixedAssetID">ID của tài sản muốn xoá</param>
        /// <returns>ID của tài sản vừa xoá</returns>
        /// Created by: TTTuan (7/11/2022)
        public int DeleteFixedAssetByID(Guid fixedAssetID)
        {
            return _fixedAssetDL.DeleteFixedAssetByID(fixedAssetID);
        }


        /// <summary>
        /// API xoá nhiều tài sản theo IDs
        /// </summary>
        /// <param name="listFixedAssetID">IDs của tài sản muốn xoá</param>
        /// <returns>ID của tài sản vừa xoá</returns>
        /// Created by: TTTuan (7/11/2022)
        public int DeleteFixedAssetByIDs(ListFixedAssetID listFixedAssetID)
        {
            return _fixedAssetDL.DeleteFixedAssetByIDs(listFixedAssetID);
        }
        #endregion

        /// <summary>
        /// Validate nghiệp vụ dữ liệu truyền vào
        /// </summary>
        /// <param name="fixedAsset">thông tin tài sản</param>
        /// <returns>Đối tượng ServiceResponse mỗ tả thành công hay thất bại</returns>
        private ServiceResponse ValidateDetail(FixedAsset fixedAsset)
        {
            var validateFailures = new List<string>();

            if ((fixedAsset.depreciation_rate * 0.01 * fixedAsset.cost) > fixedAsset.cost)
            {
                validateFailures.Add(Resource.validateDepreciationYear);
            }

            float depreciationRate = (float)fixedAsset?.depreciation_rate;
            if ((int)fixedAsset?.life_time > 0)
            {
                float checkDepreciationRate = 1 / (float)(int)fixedAsset.life_time * 100;
                float result = (float)Math.Round(checkDepreciationRate, 2);
                if (result != depreciationRate)
                {
                    validateFailures.Add(Resource.validateDepreciationRate);
                }
            }
            else
            {
                if (depreciationRate > 0)
                {
                    validateFailures.Add(Resource.validateDepreciationRate);
                }
            }
            if (validateFailures.Count > 0)
            {
                validateFailures.Add(Resource.MoreInfo_ValidateFailed);
                return new ServiceResponse
                {
                    Success = (int)StatusResponse.Failed,
                    Data = new ErrorResult(
                    ErrorCode.InvalidInput,
                    Resource.UserMsg_ValidateFailed,
                    Resource.UserMsg_ValidateFailed,
                    validateFailures)
                };
            }
            return new ServiceResponse { Success = (int)StatusResponse.Done };


        }

        /// <summary>
        /// Validate trống dữ liệu truyền lên từ API
        /// </summary>
        /// <param name="fixedAsset">Đối tượng tài sản cần validate</param>
        /// <returns>Đối tượng ServiceResponse mỗ tả thành công hay thất bại</returns>
        /// Author: Tuan
        /// Date: 20/11/2022
        private ServiceResponse ValidateRequestData(FixedAsset fixedAsset)
        {
            // Validate dữ liệu đầu vào
            var properties = typeof(FixedAsset).GetProperties();
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(fixedAsset);
                var IsNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                if (IsNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                {
                    validateFailures.Add(IsNotNullOrEmptyAttribute.ErrorMessage);
                }
            }

            if (validateFailures.Count > 0)
            {
                return new ServiceResponse
                {
                    Success = (int)StatusResponse.Failed,
                    Data = validateFailures
                };
            }
            return new ServiceResponse
            {
                Success = (int)StatusResponse.Done
            };
        }
        #endregion
    }
}
