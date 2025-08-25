using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.PreExamStudent;

namespace Kaushal_Darpan.Models.DTEApplicationDashboardModel
{
    public class DTEApplicationDashboardModel
    {
        public int ApplicationID { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int UserID { get; set; }
        public int EndTermID { get; set; }
        public int InstituteID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public string? Menu { get; set; }
        public int Status { get; set; }
        public int FinancialYearID { get; set; }
        public int HostelID { get; set; }
        public int GuestHouseID { get; set; }
        public string UrlStatus { get; set; }
    }

    public class GroupCenterMappingModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int InstituteID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int CCCode { get; set; }
        public int CenterCode { get; set; }
        public string? SubjectCode { get; set; }
        public string? SSOID { get; set; }
        public int GroupCode { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int CenterID { get; set; }
        public int SubjectID { get; set; }
        public int Marks { get; set; }
        public int IsPresent { get; set; }

        public string? Action { get; set; }
        public string? Type { get; set; }





    }

    public class OnlineMarkingSearchModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int InstituteID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int CCCode { get; set; }
        public int CenterCode { get; set; }
        public string? SubjectCode { get; set; }
        public int GroupCode { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int CenterID { get; set; }
        public int SubjectID { get; set; }
        public int Marks { get; set; }
        public int IsPresentTheory { get; set; }
    }

    public class DTEAdminDashApplicationSearchModel
    {
        public string UrlStatus { get; set; }
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


    public class ExaminationsReportsMenuWiseModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int InstituteID { get; set; }
        public int Eng_NonEng { get; set; }
        public int RoleID { get; set; }
        public int CCCode { get; set; }
        public int CenterCode { get; set; }
        public string? SubjectCode { get; set; }
        public string? SSOID { get; set; }
        public int GroupCode { get; set; }
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int CenterID { get; set; }
        public int SubjectID { get; set; }
        public int Marks { get; set; }
        public int IsPresent { get; set; }

        public string? Action { get; set; }
        public string? SP { get; set; }
        public string? Type { get; set; }





    }

    public class DownloadFileModel
    {
        public int FinancialYearID { get; set; } = 0;
        public int Eng_NonEng { get; set; } = 0;
    }

    public class DownloadZipDocumentModel : RequestBaseModel
    {
        public string FinancialYear { get; set; }
        public List<int> ApplicationIDs { get; set; }
    }


    public class ITI_DispatchAdmin_ByExaminer_RptSearchModel
    {
        public string Action { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public int EndTermID { get; set; }
        public int Status { get; set; }
        public int UserID { get; set; }
        public int ExaminerID { get; set; }
    }
}



