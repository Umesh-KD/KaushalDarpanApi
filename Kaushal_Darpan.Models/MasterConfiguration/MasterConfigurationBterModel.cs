
namespace Kaushal_Darpan.Models.MasterConfiguration
{
    public class FeeConfigurationBterIdModel
    {
        public Int32 FeeID { get; set; } = 0;
    }
    public class FeeConfigurationBterModel : FeeConfigurationBterIdModel
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

        public List<CasteCatogaryBterList>? CasteCatogaryList {  get; set; }

    }

    public class CasteCatogaryBterList
    {
        public int CasteCategoryID { get; set; }
        public string? CasteCategoryName  { get; set; }
    }

    public class SerialMasterBterIdModel
    {
        public Int32 SerialID { get; set; }

    }
    public class SerialMasterBterModel : SerialMasterBterIdModel
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



    public class M_CommonCasteBter : RequestBaseModel
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
        public List<M_CommonCaste_DetailsBter> commonCasteDetails { get; set; }// child
    }


    public class M_CommonCaste_DetailsBter
    {
        public int CommonCasteDetailsID { get; set; }
        public int CommonCastetID { get; set; }
        public int TypeID { get; set; }
       // public int StreamID { get; set; }
        public string Fees { get; set; } = "";

    }
}
