using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.studentve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.DTE_AssignApplication
{
    public class VerifyStudentApplicationModel
    {
        public int ApplicationID { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int ModifyBy { get; set; }
        public string? IPAddress { get; set; }
    }

    public class VerifyStuAppSearchModel
    {
        public string action { get; set; }
        public string StudentName { get; set; }
        public int Status { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
    }

    public class VerifyStuAppDocumentsDataModel
    {
        public int ApplicationID { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public int ActionBy { get; set; }
        public List<VerifyStuAppDocumentDetailList> VerifyStuAppDocumentDetailList { get; set; } // Ensure it's a List, not an array
    }

    public class VerifyStuAppDocumentDetailList
    {
        public int TransactionID { get; set; }
        public int Status { get; set; }
        public string ColumnName { get; set; }
        public string Remark { get; set; }
        public string TableName { get; set; }
        public string FileName { get; set; }
        public string? DisFileName { get; set; }
        public string Folder { get; set; }
        public int DocumentID { get; set; }
        public int ModifyBy { get; set; }
    }

    public class VerifyStuAppDocumentScrutinyModel
    {

        public int ApplicationID { get; set; }
        public string SSOID { get; set; }
        public string StudentName { get; set; }
        public string StudentNameHindi { get; set; }
        public string FatherName { get; set; }
        public string FatherNameHindi { get; set; }
        public string MotherName { get; set; }
        public string MotherNameHindi { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string? WhatsNumber { get; set; }
        public string? LandlineNumber { get; set; }
        public int IndentyProff { get; set; }
        public string DetailID { get; set; }
        public int Maritial { get; set; }
        public int Religion { get; set; }
        public int Nationality { get; set; }
        public int CategoryA { get; set; }
        public int CategoryB { get; set; }
        public int CategoryC { get; set; }
        public int CategoryE { get; set; }
        public int Prefential { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public string? JanAadharMemberID { get; set; }
        public string? JanAadharNo { get; set; }
        public string? Dis_SignaturePhoto { get; set; }
        public string? Dis_StudentPhoto { get; set; }
        public string? SignaturePhoto { get; set; }
        public string? StudentPhoto { get; set; }
        public int QualificationID { get; set; }
        public int StateID { get; set; }
        public int BoardID { get; set; }
        public string PassingID { get; set; }
        public string RollNumber { get; set; }
        public int MarkType { get; set; }
        public decimal AggMaxMark { get; set; }
        public decimal Percentage { get; set; }
        public decimal AggObtMark { get; set; }
        public int status { get; set; }
        public string? Remark { get; set; }
        public List<SupplementaryDataModel>? SupplementaryDataModel { get; set; }

        public List<VerifyStuAppDocumentDetailList>? VerifyStuAppDocumentDetailList { get; set; }

        public bool IsSupplement { get; set; }
    }

    public class RejectApplicationModel
    {
        public int ApplicationID { get; set; }
        public int Action { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public string Remark { get; set; }
    }
}
