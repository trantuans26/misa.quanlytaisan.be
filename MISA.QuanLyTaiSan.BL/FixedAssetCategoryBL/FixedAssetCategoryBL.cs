using MISA.QuanLyTaiSan.Common.Entities;
using MISA.QuanLyTaiSan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QuanLyTaiSan.BL
{
    public class FixedAssetCategoryBL : BaseBL<FixedAssetCategory>, IFixedAssetCategoryBL
    {
        #region Field

        private IFixedAssetCategoryDL _fixedAssetCategoryDL;

        #endregion

        #region Contructor

        public FixedAssetCategoryBL(IFixedAssetCategoryDL fixedAssetCategoryDL) : base(fixedAssetCategoryDL)
        {
            _fixedAssetCategoryDL = fixedAssetCategoryDL;
        }

        #endregion
    }
}
