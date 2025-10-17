using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITI_DataMasterModel
{
    public class DataListSearchModel
    {
        //public String Username { get; set; }

        //public String Password { get; set; }

        //public int AcademicYearID { get; set; } = 0;
        public string CollegeCode { get; set; }
       // public string RequestType { get; set; }
        public string RequestType {  get; set; }
        public String SessionYear { get; set; }

  
    }

    public class SeatIntakesDataListModel
    {
        public int SeatIntakeID { get; set; }
        public int CollegeID { get; set; }
        public int TradeID { get; set; }
        public string Shift { get; set; }
        public int LastSession { get; set; }
        public int RemarkID { get; set; }
        public int TradeSchemeID { get; set; }
        public string UnitNo { get; set; }
        public int SanctionedID { get; set; }
        public int DepartmentID { get; set; }
        public int TradeLevel { get; set; }

        
    }


    public class CourseDetail
    {
        public string CourseType { get; set; }
        public string SeatIntakeID { get; set; }
        public string CourseName { get; set; }
        public string TradeName { get; set; }
        public string Shift { get; set; }
        public string CourseStatus { get; set; }
    }

    public class TechnicalDataModel
    {
        public string CollegeCode { get; set; }
        public string ApplicationID { get; set; }
        public string AcademicYearID { get; set; }
        public string InstituteName { get; set; }
        public string Address { get; set; }
        public string AreaCity { get; set; }
        public string DistrictNameEn { get; set; }
        //public string UNIVERSITYNAME_EN { get; set; }
        public int IS_GOVT { get; set; }
        public List<CourseDetail> CourseDetailsList { get; set; }
    }

    public class StudentCorrectionMasterSearchModel
    {
        public string Name { get; set; }
        public string? UIDNumber { get; set; }

        public int? CandidateID { get; set; }
        // public string Status { get; set; }

        //public int RoleID { get; set; }
        public int DepartmentID { get; set; }
        public int InstituteID { get; set; }
   
        // public int? AcademicYearID { get; set; }
        public string action { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? SortOrder { get; set; }
        public string? SortColumn { get; set; }


        public int? Gender { get; set; }
        public string? CandidateFatherName { get; set; }
        public string? CandidateMotherName { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }

        public int ModifyBy { get; set; }
        public int? RoleID { get; set; }
        public int? DistrictID { get; set; }

     
    }

    public class NCVT_APIDetailsModel
    {
        public string? TokenApiURL { get; set; }
        public string? DataPushApiUrl { get; set; }
        public string? action { get; set; }

    }


}
//