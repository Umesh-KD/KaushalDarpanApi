using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITIBUDGET
{
    
    public class CollegeBudgetUtilizationModel
    {
        public int? BudgetUtilizationID { get; set; }
        public int? DistributedID { get; set; }
        public int? HeadID { get; set; }
        public string? HeadName { get; set; }
        public decimal? UtilizationAmount { get; set; } = 0;
        public string? UploadedFileName { get; set; }
        public int? CollegeID { get; set; }
        public int? FinYearID { get; set; }
        public int? CreatedBy { get; set; }
        public string? Remarks { get; set; }
        public decimal? AllotAmount { get; set; }
        public string? CommonFile { get; set; }
        public string? CommonFileName { get; set; }
    }

    public class CollegeBudgetUCDataModel
    {
        public int? CollegeBudgetUCID { get; set; }
        public int? DistributedID { get; set; }
        public int? UCHeadID { get; set; }
        public string? HeadName { get; set; }
        public decimal? UtilizationAmount { get; set; } = 0;
        public string? UploadedFileName { get; set; }
        public int? CollegeID { get; set; }
        public int? FinYearID { get; set; }
        public int? CreatedBy { get; set; }
        public string? Remarks { get; set; }
        public string? CommonFile { get; set; }
        public string? CommonFileName { get; set; }
    }

    public class CollegeBudgetAllotApproveDataModel
    {
        public int? DistributedID { get; set; }
        public int? ModifyBy { get; set; }
    }

    public class UnlockUtilizationDataModel
    {
        public int? DistributedID { get; set; }
        public int? UserID { get; set; }
    }

}
