using System;

namespace Kaushal_Darpan.Models.ITIMaster
{
    public class ITIMasterModel
    {
        public int TradeId { get; set; }

        public string TradeName { get; set; }
        public int TradeTypeId { get; set; }
        public int TradeLevelId { get; set; }
        public string MinPercentageInMath { get; set; }
        public string MinPercentageInScience { get; set; }
        public string DurationYear { get; set; }
        public string NoOfSemesters { get; set; }
        public string NoOfSanctionedSeats { get; set; }
        public string MinAgeLimit { get; set; }
        public string TradeCode { get; set; }
        public string QualificationDetails { get; set; }
        public bool IsMathsScienceCompulsory { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? RoleId { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string? IPAddress { get; set; }
        public int DepartmentID { get; set; }
        public bool OnlyForWomen { get; set; } = false;
        public bool IsAdmission { get; set; } = false;
        public string? Syllabuslink { get; set; } 

    }
    public class ITISearchModel
    {
        public int TradeId { get; set; }
        public string TradeName { get; set; } = string.Empty;
        public int TradeTypeId { get; set; }
        public int TradeLevelId { get; set; }
        public string DurationYear { get; set; } = string.Empty;
        public string TradeCode { get; set; } = string.Empty;
        public string Syllabuslink { get; set; } = string.Empty;
        public int CourseTypeID { get; set; }

    }

    public class SearchITIModelRequest
    {
        public string Class { get; set; }
        public int CategoryID { get; set; }
    }


    public class ITIFeesPerYearSearchModel
    {
        public int TradeId { get; set; }
        public int TradeSchemeId { get; set; }
        public int CollegeId { get; set; }
        public int FinancialYearID { get; set; }
        public int ReportingStatus { get; set; }
        public int FeeStatus { get; set; }
        public int? CourseTypeID { get; set; }
        public bool ActiveStatus { get; set; }
        public string TradeName { get; set; }
        public string TradeCode { get; set; }

    }


    public class CollegeLoginInfoSearchModel
    {
        public int TradeId { get; set; }
        public int TradeSchemeId { get; set; }
        public int CollegeId { get; set; }
        public int FinancialYearID { get; set; }
        public int ReportingStatus { get; set; }
        public int FeeStatus { get; set; }
        public int? CourseTypeID { get; set; }
        public bool ActiveStatus { get; set; }
        public string TradeName { get; set; }
        public string TradeCode { get; set; }
        public string collegeIdsString { get; set; }
        public string Password { get; set; }
        public string SSOID { get; set; }
        public string CollegeCode { get; set; }
        public int DepartmentID { get; set; }
        public int DistrictID { get; set; }
        public int ITItypeID { get; set; }

    }

    public class CenterWisePaperDetailModal 
    {
        public int Userid { get; set; }
        public int Roleid { get; set; }
        public int EndTermID { get; set; }
        public int FYID { get; set; }
        public int CenterID { get; set; } = 0;
        public int CourseTypeid { get; set; }   
        public int InstituteID { get; set; }  

    }


    public class DownloadPaperValidationModal
    {
        public int Userid { get; set; }
        public int Roleid { get; set; }
        public int PaperUploadID { get; set; }
        public int CenterID { get; set; }
        public int RequestFor { get; set; }



    }

    public class UpdateDownloadPaperFalgModal
    {
        public int Userid { get; set; }
        public int Roleid { get; set; }
        public int PaperUploadID { get; set; }
        public int CenterID { get; set; }

    }


    public class ITIPlanningBankGuarantee
    {
        public int BankGuaranteeID { get; set; }
        public int CollageId { get; set; }
        public string? BankGuaranteeNumber { get; set; }
        public string? BankName { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime Maturitydate { get; set; }
        public string? Duration { get; set; }
        public decimal Amount { get; set; }
        public string? BankAgreementDocument { get; set; }
        public int Status { get; set; }
        public string? Remarks { get; set; }

    }
    public class ITIPlanningBankGuaranteeSearch
    {
        public string? BankGuaranteeNumber { get; set; }
        public string? BankName { get; set; }
        

    }
}
