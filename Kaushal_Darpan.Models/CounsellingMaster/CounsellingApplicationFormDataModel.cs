using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Models.CounsellingMaster
{
    public class CounsellingApplicationFormDataModel
    {
        public int CandidateID { get; set; }
        public string? SSOID { get; set; }
        public string? CandidateName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public int GenderId { get; set; }
        public string? DOB { get; set; }
        public int CategoryA_ID { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int BlockID { get; set; }
        public string? Pincode { get; set; }
        public string? AadharNo { get; set; }
        public string? JanAadharNo { get; set; }
        public string? JanAadharMobileNo { get; set; }
        public string? JanAadharName { get; set; }
        public string? JanAadharMemberId { get; set; }
        public string? Remark { get; set; }
        public bool ActiveStatus { get; set; }
        public bool DeleteStatus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
        public int DepartmentID { get; set; }
        public string? CourseType { get; set; }
        public int ProfileStatus { get; set; }
        public string? ApplicationNo { get; set; }
        public int ReligionID { get; set; }
        public int NationalityID { get; set; }
        public int MaritialID { get; set; }
        public int PWDCategoryID { get; set; }
        public bool IsMinority { get; set; }
        public int IsFinalSubmit { get; set; }
        public string? DepartmentName { get; set; }
        public string? SubmittedStep { get; set; }
    }

    public class CounsellingApplicationSearchModel
    {
        public int? CandidateId { get; set; }
        public int? DepartmentID { get; set; }
        public string? SSOID { get; set; }
        public string? JanAadharMemberID { get; set; }
        public string? JanAadharNo { get; set; }
        public string? CandidateName { get; set; }

    }
}
