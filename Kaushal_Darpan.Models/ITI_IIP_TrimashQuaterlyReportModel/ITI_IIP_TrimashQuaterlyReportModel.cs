using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_IIP_TrimashQuaterlyReportModel
{
    public class ITI_IIP_TrimashQuaterlyReportModel
    {

        public int SrNo { get; set; }
        public decimal AmountOfFirstdate_3 { get; set; }
        public decimal AmountOfTrimash_4 { get; set; }
        public decimal AmountOfTrimash_5 { get; set; }
        public decimal AmountOfTrimash_6 { get; set; }
        public decimal AddAmountofTrimash { get; set; }
        public decimal CalculateofTriamsh_7 { get; set; }
        public decimal Remark { get; set; }
       

        // Additional Fields
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public string? CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
    }


    public class ITI_IIP_TrimashQuaterlyReportSearchModel
    {
        // Personal Details
        public string? Uid { get; set; }
        public string? Name { get; set; }
        public int? DepartmentID { get; set; }
        public int? InstituteID { get; set; }
        public int? RoleID { get; set; }
    }
}

