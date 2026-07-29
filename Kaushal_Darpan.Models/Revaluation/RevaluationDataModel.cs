using Kaushal_Darpan.Models.DTEInventoryModels;

namespace Kaushal_Darpan.Models.RevaluationDataModel
{
    public class RevaluationDataModel
    {

        public string DOB {  get; set; }
        public int? RollNo { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeIDs { get; set; }
        public int StudentID { get; set; }
        public int RoleID { get; set; }
        public string EnrollmentNo { get; set; }
        public string? MobilelNo { get; set; }
    }
    
    public class StudentDetailsByRollNoModel
    {

        public string DOB {  get; set; }
        public Int64? RollNo { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public int ServiceID { get; set; }
        public int SemesterID { get; set; }
        public int EndTermID { get; set; }
        public int StudentExamID { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string InstituteNameEnglish { get; set; }
        public string Name { get; set; }
        public string? MobileNo { get; set; }
        public string Year { get; set; }
        public string? EnrollmentNo { get; set; }
        public string StudentType { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeID { get; set; }
        public bool IsSelected { get; set; }
        public bool? IsKiosk { get; set; }
    }

    public class RVLStudentDetailsModel
    {
        public int StudentID { get; set; }
        public string RollNo { get; set; }
        public string EnrollNo { get; set; }
        public decimal PaymentAmount { get; set; }
        public int CreatedBy { get; set; }
        public string Remarks { get; set; }
        public int StudentExamID { get; set; }

        public List<ItemList> ItemList { get; set; } = new List<ItemList>();

        public int? RevalRequestID { get; set; }
        public bool? PaymentStatus { get; set; }
        public int? RevalStatus { get; set; }
    }

        

    public class ItemList
    {
        public int StudentExamID { get; set; }
        public int StudentExamPaperMarksID { get; set; }
        public decimal OldMarks { get; set; }
        public decimal NewMarks { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
    }

    public class ITIRevaluationDataModel
    {

        public string DOB { get; set; }
        public int? RollNo { get; set; }
        public int DepartmentID { get; set; }
        public int CourseTypeIDs { get; set; }

    }

    public class RVLStudentRevalRequestModel
    {
        public int RevalRequestID { get; set; }
        public int StudentID { get; set; }
        public string? RollNo { get; set; }
        public int RevalStatus { get; set; }
        public decimal PaymentAmount { get; set; }
        public string? Remarks { get; set; }
        public string? ApplicationNo { get; set; }
        public int ActionID { get; set; }
        public int StudentExamID { get; set; }
    }

    public class RevalationReportsearchModel
    {
        public string? EnrollmentNo { get; set; } 
        public DateTime? ResultDate { get; set; }
        public string? RollNumber { get; set; } 
        public string? SubjectCode { get; set; } 
        public string? RevaluationTxnNo { get; set; } 
        public string? RevaluationChallan { get; set; }
        public int SemesterID { get; set; }
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndTermID { get; set; }
        public int RoleID { get; set; }
    }

    public class StudentEnrollmentModel
    {
        public string Enrollment { get; set; }
    }

    public class UpdateStudentWithHistoryModel
    {
        public string EnrollmentNo { get; set; }
        public DateTime DOB { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string StudentName { get; set; }
        public string CreatedBy { get; set; }
        public int SelectedEndTermID { get; set; }
        public string CreatedSsoID { get; set; }
    }

    public class StudentDetailsByRollNoModel_WhatsApp
    {
        public int StudentID { get; set; }      
        public int SemesterID { get; set; }
        public int EndTermID { get; set; }
        public int StudentExamID { get; set; }
        public int CourseTypeID { get; set; }
        
    }

    public class FeeAmountModel_WhatsApp
    {
        public int StudentID { get; set; }
        public int SemesterID { get; set; }
        public int EndTermID { get; set; }
        public string? StudentExamID { get; set; }
        public int CourseTypeID { get; set; }
        public int Amount { get; set; }

    }
    public class FeeAmountResponseModelWhatsApp
    {
        public decimal? Amount { get; set; }
    }
}
