using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.Report
{
    public class ITISearchDataModel : RequestBaseModel
    {
        public string? Code { get; set; }
    }


    public class ITIStateTradeCertificateModel
    {
        public string RollNo { get; set; }
        public string EnrollmentNo { get; set; }
        public int EndTermID { get; set; }
        public int ExamYearID { get; set; }
        public int StudentID { get; set; }
        public int TradeScheme { get; set; }


    }
    public class ITIAddmissionReportSearchModel
    {

        public int DivisionID { get; set; }
        public int DistrictID { get; set; }
        public int TradeLevelID { get; set; }
        public int AcademicYearID { get; set; }

    }


    public class FinalAdmissionListRequestModel
    {
        public int TradeLevelId { get; set; }
        public int AllotmentStatus { get; set; } = 1;
        public int EndTerms { get; set; }
        public int FinancialYearID { get; set; }
    }

    public class FinalAdmissionGenderWiseRequestModel
    {
        public int TradeLevelId { get; set; }
        public int AllotmentStatus { get; set; }
        public int EndTerms { get; set; }
        public int FinancialYearID { get; set; }
    }

    public class ZoneDistrictSeatUtilizationRequestModel
    {
        public int AcademicYearID { get; set; }
        public int DistrictID { get; set; }
        public int DivisionID { get; set; }
        public int id { get; set; }
    }

    public class ZoneDistrictSeatUtilizationByGenderRequestModel
    {
        public int AcademicYearID { get; set; }
        public int DistrictID { get; set; }
        public int DivisionID { get; set; }
        public int Id { get; set; }
    }

    public class VacantSeatReportRequestModel
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; } = 0;
        public int DivisionID { get; set; } = 0;
        public int DistrictID { get; set; } = 0;
        public string ITICode { get; set; } = "";
        public int CollegeId { get; set; } = 0;
    }


    public class AllottedReportedRequestModel
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; }
        public int DivisionID { get; set; }
        public int DistrictID { get; set; }
        public string? ITICode { get; set; }
        public int CollegeId { get; set; }
    }

    public class StudentDataAgeBetween15And29RequestModel
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; }
    }

    public class AllotmentReportCollegeRequestModel
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; }
        public int TradeTypeID { get; set; }
        public int TradeId { get; set; }
        public int CollegeId { get; set; }
        public int AllotmentStatus { get; set; }
    }

    public class AllotmentReportCollegeForAdminRequestModel
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; }
        public int TradeTypeID { get; set; }
        public int TradeId { get; set; }
        public int CollegeId { get; set; }
    }


    public class ITIAddmissionWomenReportSearchModel
    {

        public int DivisionID { get; set; }
        public int DistrictID { get; set; }
        public int TradeLevelID { get; set; }
        public int AcademicYearID { get; set; }
        public int CourseTypeID { get; set; }
        public string? ITICode { get; set; }
        public int ITICollegeID { get; set; }
        public string? TradeCode { get; set; }
        public int ITITradeID { get; set; }
        public int StatusID { get; set; }
        public int CollegeId { get; set; }
        public int TradeId { get; set; }
        public int TradeTypeID { get; set; }
        //public string? AllotedCategory { get; set; }
    }

    public class DirectAdmissionReportRequestModel
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; }
        public int TradeTypeID { get; set; }
        public int TradeId { get; set; }
        public int CollegeId { get; set; }
    }


    public class IMCAllotmnentReportRequestModel
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; }
        public int TradeTypeID { get; set; }
        public int TradeId { get; set; }
        public int CollegeId { get; set; }
        public int StatusID { get; set; }
        public string CollegeName { get; set; } = string.Empty;
    }

    public class InternalSlidingForAdminReport
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; }
        public int TradeTypeID { get; set; }
        public int TradeId { get; set; }
        public int CollegeId { get; set; }
    }

    public class SwappingForAdminReport
    {
        public int AcademicYearID { get; set; }
        public int TradeLevelID { get; set; }
        public int TradeTypeID { get; set; }
        public int TradeId { get; set; }
        public int CollegeId { get; set; }
    }
    public class BTER_EstablishManagementReportSearchModel
    {
        public int UserID { get; set; }
        public int DepartmentID { get; set; }
        public int StaffUserID { get; set; }
        public int StaffType { get; set; }
        public int RoleID { get; set; }
        public int InstituteID { get; set; }
        public string SSOID { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
    }


    public class CollegeInformationReportSearchModel
    {
        public int AcademicYearID { get; set; }
    }

    public class EWSReportSearchModel
    {
        public int AcademicYearID { get; set; }
        public int StreamID { get; set; }
        public int InstituteID { get; set; }
    }

    public class UFMStudentReportSearchModel
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
    }


    public class GetSessionalFailStudentReport
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int SemesterID { get; set; }

    }


    public class RMIFailStudentReport
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int SemesterID { get; set; }
    }


    public class TheoryFailStudentReport
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int SemesterID { get; set; }
    }

    public class RevaluationStudentDetailReport
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int SemesterID { get; set; }
    }

    public class searchCenterSuperintendentAttendance
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
       
        public int RoleID { get; set; }

        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int InstituteID { get; set; }
        public int CenterAssignedID { get; set; }
        public string ExamDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CSName { get; set; }

             
    }

    public class StudentExaminerDetailReport
    {
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int SemesterID { get; set; }
    }

    public class CentarlSupridententDistrictRequestModel
    {
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int DistrictID { get; set; }
        public string Name { get; set; }
        public string? CenterCode { get; set; }
    }
}
