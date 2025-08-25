using Kaushal_Darpan.Models.DateConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.MasterConfiguration
{
    public class FeeConfigurationIdModel
    {
        public Int32 FeeID { get; set; } = 0;
    }
    public class FeeConfigurationModel : FeeConfigurationIdModel
    {
        public Int32 TypeID { get; set; }
        public Int32? SemesterID { get; set; }
        public Int32? StreamID { get; set; }
        public Int32 DepartmentID { get; set; }
        public Int32 EndTermID { get; set; }
        public Int32 FinancialYearID { get; set; }
        public Int32? CourseTypeID { get; set; }
        public Int32? CasteCategoryID { get; set; }
        public decimal FeeAmount { get; set; } = 0;
        public decimal LateFeeAmount { get; set; } = 0;
        public string Remark { get; set; } = "";
        public Int32 ModifyBy { get; set; }
        public Int32 RoleID { get; set; }=0;

        public int BackSubjectCount { get; set; }
        public decimal BackFeeAmount { get; set; }

    }

    public class SerialMasterIdModel
    {
        public Int32 SerialID { get; set; }

    }
    public class SerialMasterModel : SerialMasterIdModel
    {
        public Int32 SemesterID { get; set; }
        public Int32 TypeID { get; set; } = 0;
        public string StaticVal { get; set; } = "";
        public Int32 StartFrom { get; set; } = 0;
        public string Remark { get; set; } = "";
        public Int32 ModifyBy { get; set; }
        public Int32 DepartmentID { get; set; }
        public Int32 CourseTypeID { get; set; }
        public Int32 EndTermID { get; set; }
        public Int32 FinancialYearID { get; set; }
        public Int32 RoleID { get; set; }
        public Int32 PartitionSize { get; set; } = 0;
        public Int32 CCcodeLength { get; set; } = 0;
    }



    public class M_CommonCaste : RequestBaseModel
    {
        public int CommontypeID { get; set; }
        public string CommonTypeName { get; set; }
        public string? fees { get; set; }
        public string Remark { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public List<M_CommonCaste_Details> commonCasteDetails { get; set; }// child
    }


    public class M_CommonCaste_Details
    {
        public int CommonCasteDetailsID { get; set; }
        public int CommonCastetID { get; set; }
        public int TypeID { get; set; }
       // public int StreamID { get; set; }
        public string Fees { get; set; } = "";

    }
}
