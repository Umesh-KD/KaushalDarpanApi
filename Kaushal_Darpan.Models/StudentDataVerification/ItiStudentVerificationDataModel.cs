using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.studentve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.StudentDataVerification
{
    public class ItiDocumentScrutinyDataModel
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
        public string? DetailID { get; set; }
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
        public int status { get; set; }
        public string? Remark { get; set; }
        public int ParentIncome { get; set; }
        public bool IsMinority { get; set; }
        public int IsEWSCategory { get; set; }
        public int Eligible8thTradesID { get; set; }
        public int Eligibl10thTradesID { get; set; }
        public int PWDCategoryID { get; set; }

        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public DateTime? RTS { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public bool IsTSP { get; set; }
        public bool IsSaharia { get; set; }
        public int TspDistrictID { get; set; }
        public int IsDevnarayan { get; set; }
        public int DevnarayanTehsilID { get; set; }
        public int DevnarayanDistrictID { get; set; }
        public int TSPTehsilID { get; set; }
        public string? ApplicationNo { get; set; }
        public List<QualificationDetailsDataModel> QualificationDetailsDataModel { get; set; }
        public List<VerificationDocumentDetailList>? VerificationDocumentDetailList { get; set; }
    }
}
