using Kaushal_Darpan.Models.CommonSubjectMaster;
using Kaushal_Darpan.Models.StudentMaster;

namespace Kaushal_Darpan.Models.PreExamStudent
{
    public class PreExamStudentModel : RequestBaseModel
    {
        public string EnrollmentNo { get; set; }
        public string ApplicationNo { get; set; }
        public string Name { get; set; }
        public string InstituteID { get; set; }
        public int ManagementTypeID { get; set; }
        public string MobileNo { get; set; }
        public int BranchID { get; set; }
        public int Year_SemID { get; set; }
        public int StudentTypeID { get; set; }
        public int StudentStatusID { get; set; }
        public int StudentFilterStatusId { get; set; }
        public int ExamCategoryID { get; set; }
        public int FinacialYearID { get; set; }
        public string OptionalSubjectStatus { get; set; }
        public int BridgeCourseID { get; set; }
        public int CreatedBy { get; set; }
        public int EndTermID { get; set; }
        public int? IsYearly { get; set; }
        public int EligibilityStatus { get; set; }
        public int StreamID { get; set; }
        public string? AdmittStatus { get; set; }
    }

    public class PreExam_UpdateEnrollmentNoModel :RequestBaseModel
    {
        public string? StudentID { get; set; }
        public string? EnrollmentNo { get; set; }
        public string? InstituteID { get; set; }
        public int StreamID { get; set; }
        public string? OrderNo { get; set; }
        public string? OrderDate { get; set; }
        public string? UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string? Action { get; set; }
        public int StudentExamID { get; set; }
        public bool IsUpdate { get; set; }
    }

    public class Student_DataModel
    {
        public int StudentSubjectID { get; set; }
        public int StudentId { get; set; }
        public int StreamId { get; set; }
        public int SemesterId { get; set; }
        //public int FeeAmount { get; set; }
        public int status { get; set; }
        public int ModifyBy { get; set; }
        public int Action { get; set; }
        public bool IsParent { get; set; }
        public int EndTermID { get; set; }
        public decimal FeeAmount { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public bool Selected { get; set; }
        public string? IPAddress { get; set; }



    }

    public class PreExamStudentSubjectRequestModel
    {
        // List of subjects that are part of the pre-exam configuration



        public List<CommonSubjectDetailsMasterModel> Subjects { get; set; }

        // List of students associated with the subjects
        public List<Student_DataModel> Students { get; set; }


    }

    public class PreExamSubjectModel
    {

        // List of subjects that are part of the pre-exam configuration

        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string Dis_DOB { get; set; }
        public string Gender { get; set; }
        public string EnrollmentNo { get; set; }
        public string CasteCategoryName { get; set; }


        public List<SubjectModel> Subjects { get; set; }

        // List of students associated with the subjects




    }
    public class SubjectModel
    {
        public int StudentID { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

    }
    public class OptionalSubjectModel:RequestBaseModel
    {
        public Int32 StudentID { get; set; }
        public string RowJson { get; set; } = "[]";
        public string? IPAddress { get; set; }
        public int CreatedBy { get; set; }
    }

    public class AnnexureDataModel
    {
        public int DepartmentID { get; set; }
        public int Eng_NonEng { get; set; }
        public int EndTermID { get; set; }
        public int StudentExamType { get; set; }
        public int InstitueID { get; set; }
    }


    public class PreExamSearchModel:RequestBaseModel
    {
        public int StudentExamType { get; set; }
        public int InstitueID { get; set; }
        public int StudentID { get; set; }
        public int StudentExamID { get; set; }
        public int statusId { get; set; }
        public int status { get; set; }
    }

    public class StudentAttendenceModel:RequestBaseModel
    {
        public int StudentExamID { get; set; }
        public int AttendenceID { get; set; }
        public int FaMark { get; set; }

        public string? Remarks { get; set; }
        public int CreatedBy { get; set; }
        public string ReceiptNo { get; set; } = string.Empty;
        public string DepositDate { get; set; } = string.Empty;
        public string EligibilityStatus { get; set; } = string.Empty;
    }

    public class RevertDataModel
    {
        public int StudentExamID { get; set; }
        public int status { get; set; }

    }

    public class StudentMarksheetSearchModel
    {
        public string RollNo { get; set; }
        public string DOB { get; set; }
        public int EndTermID { get; set; }
        public int ExamYearID { get; set; }
        public int PassFailID { get; set; }
        public int TradeScheme { get; set; }
    }


    public class StudentMarksheetDataModel
    {
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string DateOfBirth { get; set; }
        public string RegistrationNo { get; set; }
        public string RollNo { get; set; }
        public string TradeName { get; set; }
        public string ExamMonthYear { get; set; }
        public string ITINameAndAddress { get; set; }
        public int TotalMarks { get; set; }
        public int TotalPassMarks { get; set; }
        public string Result { get; set; }
        public string ResultDeclarationDate { get; set; }

        public List<ExamPaperMarkDataModel> Marksheets { get; set; }
    }

    public class ExamPaperMarkDataModel
    {
        public string SubjectName { get; set; }           
        public int MaxMarks { get; set; }
        public int MinPassMarks { get; set; }
        public int MarksSecured { get; set; }
        public string Remarks { get; set; }              
    }

    public class ITIExamination_UpdateEnrollmentNoModel
    {

            public int StudentID { get; set; } = 0;
            public int StudentExamID { get; set; } = 0;
           
           
         
            public string OrderNo { get; set; }
            public string OrderDate { get; set; }
           
            public int CreatedBy { get; set; } = 0;
            public string? FolderName { get; set; }
            public string FileName { get; set; }
        public bool IsDropout { get; set; }

        }

    


}
