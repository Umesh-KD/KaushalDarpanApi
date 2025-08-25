using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.DocumentDetails;

namespace Kaushal_Darpan.Models.BterApplication
{
    public class PreviewApplicationFormmodel
    {
        public class PreviewApplicationModel
        {
            public int ApplicationID { get; set; }
            public string? ApplicationNo { get; set; }
            public string StudentName { get; set; }
            public string StudentNameHindi { get; set; }
            public string FatherName { get; set; }
            public string FatherNameHindi { get; set; }
            public string MotherName { get; set; }
            public string MotherNameHindi { get; set; }
            public string AadharNo { get; set; }
            public string Gender { get; set; }
            public string ParentIncome { get; set; }
            public string ApplyScheme { get; set; }
            public int IsEWS { get; set; }
            public string Residence { get; set; }
            public decimal ParentIncomeAMT { get; set; }
            public string MaritalStatusName { get; set; }
            public string Religion { get; set; }
            public string DOB { get; set; }
            public string Email { get; set; }
            public string CategoryA { get; set; }
            public string CategoryB { get; set; }
            public string CategoryC { get; set; }
            public string PrefCategory { get; set; }
            public string MobileNo { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string AddressLine3 { get; set; }
            public string CorsAddressLine1 { get; set; }
            public string CorsAddressLine2 { get; set; }
            public string CorsAddressLine3 { get; set; }
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string BlockName { get; set; }
            public string Pincode { get; set; }
            public string CorsPincode { get; set; }
            public string CityVillage { get; set; }
            public string CorsCityVillage { get; set; }
            public string Age { get; set; }
            public string StudentPhoto { get; set; }
            public string SignaturePhoto { get; set; }
            public string MotherDepCertificate { get; set; }
            public string PrefCategoryPhoto { get; set; }
            public string AadharPhoto { get; set; }
            public int status { get; set; }
            public int IsfinalSubmit { get; set; }
            public decimal ApplicationFees { get; set; }
            public int ServiceID { get; set; }
            public int DepartmentID { get; set; }
            public int CourseTypeID { get; set; }
            public int UniqueServiceID { get; set; }
            public List<QualificationViewDetails> QualificationViewDetails { get; set; }
            public List<OptionalviewData> optionalviewDatas { get; set; }
            public List<PendingDataModel> PendingDataModel { get; set; }
            public List<DocumentDetailsModel>? DocumentDetailList { get; set; }
            public List<SupplementaryviewDetails>? SupplementaryDetailList { get; set; }
            public bool IsFinalPay { get; set; }
            //public string? CategoryD { get; set; }

            public int TransactionID { get; set; }
            public string PRN { get; set; } = string.Empty;
            public string TransctionStatus { get; set; } = string.Empty;
            public string? IsPH { get; set; }
            public string? IsKM { get; set; }
            public string? CategoryAbyChecker { get; set; }
            public string? categoryD_ID { get; set; }


            public int CategoryAId { get; set; }
            public string? PrefentialCategoryType { get; set; }
            public int PrefentialCategoryTypeId { get; set; }
            public string? PrefentialCategory { get; set; }
            public int PrefentialCategoryId { get; set; }
            public int MaritialStatsu { get; set; }
            public int GenderId { get; set; }
            public int FormCommision { get; set; }
            public string? STSubCategory { get; set; }
            public int STSubCategoryId { get; set; }
            public string? IdentityProofType { get; set; }
            public string? IndentyProff { get; set; }
            public string? Nationality { get; set; }
            public string? Category_E { get; set; }
            public string? ActionName { get; set; }

            public string? CasteCertificateNo { get; set; }
            public string? CertificateGeneratDate { get; set; }

            public int Action { get; set; }
            public int IsHighestQualification { get; set; }
            public int? DirectAdmission { get; set; }
            public bool? IsSportsQuota { get; set; }


        }
        public class QualificationViewDetails
        {
            public int ApplicationID { get; set; }
            public string QualificationID { get; set; }
            public string Qualification { get; set; }
            public string StateName { get; set; }
            public string BoardTypeName { get; set; }
            public string PassingYear { get; set; }
            public string RollNumber { get; set; }
            public string AggMaxMarks { get; set; }
            public string AggObtMarks { get; set; }
            public decimal Percentage { get; set; }
            public bool IsSupplement { get; set; }
            public string Marksheet { get; set; }
            public string Marktype { get; set; }


            public string BoardStateName   { get; set; }
            public string BoardName { get; set; }
            public string BoardExamName    { get; set; }    
            public string UniversityBoard  { get; set; }
            public string ClassSubject     { get; set; }
            public string SchoolCollegeName { get; set; }
            public string Subjects { get; set; }
            public string Branches { get; set; }
            public string? CoreBranchName { get; set; }
            public string? BranchName { get; set; }
            public int QualificationPriority { get; set; }


        }

        public class SupplementaryviewDetails
        {
            public int SupplementaryID { get; set; }
            public string Qualification { get; set; }
            public string StateNames { get; set; }
            public string BoardNames { get; set; }
            public string PassingYear { get; set; }
            public string RollNumber { get; set; }
            public string SubjectName { get; set; }
            public decimal? MaxMarksSupply { get; set; }
            public decimal? ObtMarksSupply { get; set; }
        }

        public class OptionalviewData
        {
            public string College_TypeName { get; set; }
            public string DistrictName { get; set; }
            public string InstituteName { get; set; }
            public int BranchID { get; set; }
            public string BranchName { get; set; }
        }

        public class PendingDataModel
        {
            public string Pending { get; set; }
            public int Index { get; set; }
        }

    }

}

