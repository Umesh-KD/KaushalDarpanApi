using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.RPPPayment;

namespace Kaushal_Darpan.Models.ITIApplication
{
    public class ItiApplicationPreviewDataModel
    {
        public class ItiApplicationPreviewModel
        {
            public int ApplicationID { get; set; }
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
            public bool IsEWS { get; set; }
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
            public string StateName { get; set; }
            public string DistrictName { get; set; }
            public string BlockName { get; set; }
            public string Pincode { get; set; }
            public string CorsPincode { get; set; }
            public string CityVillage { get; set; }
            public string Age { get; set; }
            public string StudentPhoto { get; set; }
            public string SignaturePhoto { get; set; }
            public bool IsMinority { get; set; }
            public decimal ApplicationFees { get; set; }
            public int ServiceID { get; set; }
            public int IsfinalSubmit { get; set; }
            public int status { get; set; }            
            public int DepartmentID { get; set; }            

            public List<ItiQualificationViewDetails> QualificationViewDetails { get; set; }
            public List<ItiOptionsviewData> OptionsViewData { get; set; }
            public List<ItiPendingDataModel> PendingDataModel { get; set; }
            public List<DocumentDetailsModel>? DocumentDetailList { get; set; }
            public DataTable? EmitraTransactionsModelList { get; set; }

            public bool IsFinalPay { get; set; }
            public int TransactionID { get; set; }
            public string PRN { get; set; } = string.Empty;
            public string TransctionStatus { get; set; } = string.Empty;
                  public int UniqueServiceID { get; set; }
            public string? Apaarid { get; set; }
           public decimal ProcessingFee { get; set; }
            public decimal FormCommision { get; set; }
            public string ApplicationNo { get; set; } = string.Empty;
            

        }

        public class ItiQualificationViewDetails
        {
            public int ApplicationID { get; set; }
            public string QualificationID { get; set; }
            public string QualificationName { get; set; }
            public string StateName { get; set; }
            public string BoardName { get; set; }
            public string SchoolCollegeName { get; set; }
            public string PassingYear { get; set; }
            public string RollNumber { get; set; }
            public string AggMaxMarks { get; set; }
            public string AggObtMarks { get; set; }
            public decimal Percentage { get; set; }
            public string Marktype { get; set; }
            public string MarktypeName { get; set; }
            public decimal MathsMaxMarks { get; set; }
            public decimal MathsObtMarks { get; set; }
            public decimal ScienceMaxMarks { get; set; }
            public decimal ScienceObtMarks { get; set; }
        }

        public class ItiOptionsviewData
        {
            public string ManagementTypeName { get; set; }
            public string DistrictName { get; set; }
            public string InstituteName { get; set; }
            public int BranchID { get; set; }
            public int TradeLevel { get; set; }
            public string TradeName { get; set; }
            public string? MinPercentageInMath { get; set; }
            public string? MinPercentageInScience { get; set; }
            public string? IsMathsScienceCompulsory { get; set; }

           

        }

        public class ItiPendingDataModel
        {
            public string Pending { get; set; }
            public int Index { get; set; }
        }
    }
}
