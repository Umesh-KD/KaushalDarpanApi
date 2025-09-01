using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ITINodalOfficerExminerReport
{

    public class ITINodalOfficerExminerReport
    {
        public int? ID { get; set; } 
        public int? ExamCenterUnderYourAreaID { get; set; }
        public string? MediumQuestionPaperSent { get; set; }
        public string? Date { get; set; }  
        public string? ToDate { get; set; }  
        public bool CoordinatorReachOnTime { get; set; }
        public int? CoordinatorNotReached { get; set; }
        public string? Reason { get; set; }
        public string? SupportingDocument_file { get; set; }
        public string? SupportingDocument_fileName { get; set; }
        public bool InspectTheExaminationCenters { get; set; }
        public bool AdditionalDetails { get; set; }
        public string? UploadDocument_file { get; set; }
        public string? UploadDocument_fileName { get; set; }
        public bool ExamSmooth { get; set; }
        public bool DocumentsSubmitted { get; set; }
        public bool ExamIncident { get; set; }
        public string? ExamRemarks { get; set; }
        public string? FutureCentreRemarks { get; set; }
        public string? FutureExamSuggestions { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string? IPAddress { get; set; }
        public string? ExamDate { get; set; }
        public int EndTermID { get; set; }
        public int FinancialYearID { get; set; }

        public List<ITIInspectExaminationCenters> InspectExaminationCentersList = new List<ITIInspectExaminationCenters>();
    }

    public class ITIInspectExaminationCenters
    {
       

        public int? ID { get; set; } 
        public int? NameOfExaminationCentreID { get; set; } 
        public DateTime? DateAndTimeOfInspection { get; set; } 
        public int? TotalNumberOfCandidatesEnrolled { get; set; } 
        public int? CandidatesHadLeftAfterCompletingTheExam { get; set; }
        public int? JobsCreated { get; set; } 
        public int? JobsBeingCreated { get; set; } 
        public bool VivaConducted { get; set; } 
        public bool LineDiagramPrepared { get; set; } 
        public bool ReadingTaken { get; set; }
        public string? NameOfExaminationCentre { get; set; }
        public int ModifyBy { get; set; }

    }

    public class ITINodalOfficerExminerReportSearch
    {
        public int FinancialYearID { get; set; }
        public int ID { get; set; }
        public string? Date { get; set; }
        public int DistrictID { get; set; }
        public int EndTermID { get; set; }
        public int UserID { get; set; }
        public int CourseType { get; set; }
        public int InstituteID { get; set; }
        
    }


    public class ITINodalOfficerExminerReportByID
    {

        public ITINodalOfficerExminerReport ITINodalOfficerExminerReports = new ITINodalOfficerExminerReport();      

        public List<ITIInspectExaminationCenters> InspectExaminationCentersList = new List<ITIInspectExaminationCenters>();


    }

    public class Nodalsearchmodel
    {
        public int AcademicYearID { get; set; }
        public int EndTermID { get; set; }
        public int DistrictID { get; set; }
    }

    public class NodalExamMapping
    {
        public int AcademicYearID { get; set; }
        public int EndTermID { get; set; }
        public int DistrictID { get; set; }
        public int ExamNodalID { get; set; }
        public int ModifyBy { get; set; }
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? SSOID{ get; set; }
        public string? Email{ get; set; }
        public string? MobileNumber{ get; set; }
    }

}
