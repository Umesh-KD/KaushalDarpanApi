using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIAdminDashboard
{
    public class ITIAdminDashboardSearchModel
    {
        public int DepartmentID { get; set; }
        public int ApplicationID { get; set; }
        public string? StudentName { get; set; }
        public string? MobileNo { get; set; }
        public int Gender { get; set; } = 0;
        public int UserID { get; set; } = 0;
        public int InstituteID { get; set; } = 0;
        public int RoleID { get; set; } = 0;
        public int EndTermID { get; set; } = 0;
        public int Eng_NonEng { get; set; } = 0;
        public int DistrictID { get; set; }
        public int FinancialYearID { get; set; }
        public string? ActionName { get; set; }



    }
	public class ItiAdminDashApplicationSearchModel
	{
		public int UrlStatus { get; set; }
		public int DepartmentID { get; set; }
		public int ApplicationID { get; set; }
		public int Gender { get; set; }
		public int InstituteID { get; set; }
        public int RoleID { get; set; } = 0;
        public int EndTermID { get; set; } = 0;
        public int Eng_NonEng { get; set; } = 0;
        public int DistrictID { get; set; }
		public int CategoryA { get; set; }
		public int CategoryB { get; set; }
		public int CategoryC { get; set; }
		public int CategoryD { get; set; }
		public int FinancialYearID { get; set; }
		public string? StudentName { get; set; }
		public string? MobileNumber { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }

        public int? TradeLevelId { get; set; }
        public int? ManagementTypeId { get; set; }

        public string? ITICode { get; set; }
        public string? TradeCode { get; set; }          


    }

    public class ITIDashboardSearchModel
    {
        public int DepartmentID { get; set; } = 2;
        public int RoleID { get; set; } = 0;
        public int EndTermID { get; set; } = 0;
        public int Eng_NonEng { get; set; } = 0;



    }
}
