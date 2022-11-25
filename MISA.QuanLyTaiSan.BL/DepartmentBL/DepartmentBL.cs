using MISA.QuanLyTaiSan.Common.Entities;
using MISA.QuanLyTaiSan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QuanLyTaiSan.BL
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        #region Field,

        private IDepartmentDL _departmentDL;

        #endregion

        #region Contructor

        public DepartmentBL(IDepartmentDL departmentDL) : base(departmentDL)
        {
            _departmentDL = departmentDL;
        }

        #endregion
    }
}
