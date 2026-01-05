using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.ItiInvigilator
{
    public  class ItiInvigilatorDataModel:RequestBaseModel
    {
        public int InvigilatorID { get; set; }
        public int UserID { get; set; }
        public int InstituteID { get; set; }
        public int StaffID { get; set; }
        public int TimeTableID { get; set; }
        public string? RollNoFrom { get; set; }
        public string? RollNoTo { get; set; }
        public int SemesterID { get; set; }
        public int ShiftID { get; set; }
        public string? SubjectName { get; set; }
        public string? SSOID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }

        public List<InvigilatorStudentList>? StudentList { get; set; }


    }
    public class InvigilatorStudentList
    {
        public int StudentExamPaperMarksID { get; set; }
    }


    public class ItiInvigilatorSearchModel:RequestBaseModel
    {
        public int TimeTableID { get; set; }
        public int InstituteID { get; set; }
        public int InvigilatorID { get; set; }
        public int UserID { get; set; }
      
        public string? Action { get; set; }
        public string? SSOID { get; set; }
    }


    public class ItiTheoryStudentMaster
    {
        public int SemesterID { get; set; }
        public int StreamID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int MarkEnter { get; set; }
        public int InternalPracticalID { get; set; }
        public string? RollNo { get; set; }
        public string? SubjectName { get; set; }
        public int GroupCodeID { get; set; }
        public int SSOID { get; set; }
        public int InstituteID { get; set; }
        public int? EndtermID { get; set; }
        public int? EngNong { get; set; }

    }

    public class ITI_InvigilatorPDFViewModal
    {
        public int InstituteID { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int InvigilatorID { get; set; }
        public string SSOID { get; set; }
        public int RoleID { get; set; }
        public int Status { get; set; }
        public int Userid { get; set; }
        public string ITIInvigilatorIDs { get; set; }
    }


    public class ITI_InvigilatorPDFForwardModal
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }        
        public string? FileName { get; set; }
        public int EndTermID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public string  ITIInvigilatorID { get; set; }

       
    }

    public class ITI_AdminInvigilatorRemunerationDetailModal
    {
         public string SSOID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
        public int Userid { get; set; }
        public int RemunerationPKID { get; set; } = 0;
        public string Remarks { get; set; } = "";

    }


    public class CenterwisePersentabsentHeardeModel
    {
        public string ?CenterName { get; set; }
        public int CenterID { get; set; }
        public string ?CenterCode { get; set; }
        public string ?MobileNo { get; set; }

        public string ?ExamName { get; set; }
        public int TimeTableID { get; set; }

        public string? Dis_Date { get; set; }          // Dispatch / Display Date
        public string? ExamDateTime { get; set; }

        public int ShiftID { get; set; }
        public string ?ExamShift { get; set; }
        public string ?ExamShiftWithTime { get; set; }

        public string ?SemesterName { get; set; }
        public string ?FinancialYear { get; set; }
        public string ?EndTerm { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string ?TradeName { get; set; }
        public string ?SubjectName { get; set; }
        public string ?Invigilators { get; set; }

        public int SemesterID { get; set; }
        public string ?ExaminerName { get; set; }

        public int StudentCount { get; set; }
        public int PresentStudentCount { get; set; }
        public int AbsentStudentCount { get; set; }
        public string? ReportName { get; set; }
        public string? subTitleName { get; set; }

    }

    public class CenterwisePersentabsentStudentDataModel 
    {
        public int SrNo { get; set; }

        public int CenterID { get; set; }
        public string? CenterCode { get; set; }
        public string? CenterName { get; set; }

        public int EndTermID { get; set; }

        public string? CCCode { get; set; }
        public string? CenterCode1 { get; set; }

        public int InstituteID { get; set; }

        public string? StudentName { get; set; }
        public string? RollNo { get; set; }

        public string? CourseType { get; set; }

        public int StudentExamID { get; set; }
        public int SemesterID { get; set; }

        public string? FatherName { get; set; }

        public string? StudentType { get; set; }
        public int StudentTypeID { get; set; }

        public string? InstituteName { get; set; }

        public string DOB { get; set; }

        public int SubjectId { get; set; }
        public string? SubjectCode { get; set; }
        public string? SubjectName { get; set; }

        public decimal MaxMarks { get; set; }
        public decimal MinMarks { get; set; }
        public decimal ObtainedMarks { get; set; }

        public string? PresentStatus { get; set; }

        public string? UserName { get; set; }
        public string ActionDate { get; set; }

        public string? SemesterName { get; set; }
        public string? TradeName { get; set; }
    }




}
