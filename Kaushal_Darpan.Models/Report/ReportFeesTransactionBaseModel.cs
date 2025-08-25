namespace Kaushal_Darpan.Models.ReportFeesTransactionModel
{
    public class ReportFeesTransaction : RequestBaseModel
    {
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        public string? ApplicationNo { get; set; }
    }
    public class ReportFeesTransactionSearchModel
    {
       
  
        public string? Status { get; set; } 
        
        public int? StudentExamID { get; set; }     
        public int? DepartmentID { get; set; }
        public int? AcademicYearID { get; set; }
        public int TransactionType { get; set; }
        public int CourseType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? TransctionStatus { get; set; }
        public int TransactionId { get; set; }
        public string? ApplicationID { get; set; }
        public string? TransactionNo { get; set; }
        public string? StudentName { get; set; }
        public string? MobileNo { get; set; }
        public string? AadharNo { get; set; }
        public string? DOB { get; set; }
        public string? FeeFor { get; set; }
        public string? PRN { get; set; }

        public int InstituteID { get; set; }
        public int StudentID { get; set; }
        public string TransctionDate { get; set; }
        public string EnrollmentNo { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public int PaymentServiceID { get; set; }
    }

    public class GetApplicationFeesTransactionSearchModel
    {
        public int? DepartmentID { get; set; }
        public int? AcademicYearID { get; set; }
        public int TransactionType { get; set; }
        public int CourseType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? TransctionStatus { get; set; }
        public int TransactionId { get; set; }
        public string? ApplicationID { get; set; }
        public string? TransactionNo { get; set; }
        public string? StudentName { get; set; }
        public string? MobileNo { get; set; }
        public string? AadharNo { get; set; }
        public string? DOB { get; set; }
        public string? FeeFor { get; set; }
        public string? TransctionDate { get; set; }
        public string? PRN { get; set; }

    }

    public class EmitraFeesTransactionSearchModel
    {


        public string? Status { get; set; }

        public int? StudentExamID { get; set; }
        public int? DepartmentID { get; set; }
        public int? AcademicYearID { get; set; }
        public int TransactionType { get; set; }
        public int CourseType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? TransctionStatus { get; set; }
        public int TransactionId { get; set; }
        public string? ApplicationID { get; set; }
        public string? TransactionNo { get; set; }
        public string? StudentName { get; set; }
        public string? MobileNo { get; set; }
        public string? AadharNo { get; set; }
        public string? DOB { get; set; }
        public string? FeeFor { get; set; }
        public string? PRN { get; set; }
        public string? SSOID { get; set; }

        public int InstituteID { get; set; }
        public int StudentID { get; set; }
        public string TransctionDate { get; set; }
        public string EnrollmentNo { get; set; }
        public int StreamID { get; set; }
        public int SemesterID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
    }


}
