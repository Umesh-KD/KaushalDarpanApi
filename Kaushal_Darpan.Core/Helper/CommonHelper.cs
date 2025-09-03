using Kaushal_Darpan.Models.CommonFunction;
using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace Kaushal_Darpan.Core.Helper
{
    // use here all the common things except functions  
    public static class CommonHelper
    {

    }

    #region Constants
    public class Constants
    {
        // pagination
        public const int MAX_PAGE_SIZE = 100;

        // common message
        public const string MSG_UNAUTHORIZED_ACCESS = "Unauthorized access!";
        public const string MSG_UNAUTHORIZED_ACCESS_FOR_ROLE = "Unauthorized access for the role!";
        public const string MSG_FILE_NOT_FOUND = "File not found!";
        public const string MSG_DATA_NOT_FOUND = "Data not found!";
        public const string MSG_VALIDATION_FAILED = "Validation failed!";
        public const string MSG_DATA_LOAD_SUCCESS = "Data load successfully.";
        public const string MSG_SAVE_SUCCESS = "Saved successfully.";
        public const string MSG_SAVE_Duplicate = "Duplicate entry.";
        public const string MSG_INVALID_REQUEST = "Invalid Request!";
        public const string MSG_DUPLICATE_CENTER = "Center Name Already exist!";
        public const string MSG_DUPLICATE_CENTER_CODE = "Center Code Already exist!";

        public const string MSG_SAVE_MOBILE_Duplicate = "इस मोबाइल नंबर का उपयोग पहले ही आवेदन जमा करने के लिए किया जा चुका है।(This mobile number has already been used to submit an application).";
        public const string MSG_SAVE_Itentity_Duplicate = "आवेदन प्रस्तुत करने के लिए आईडी प्रमाण संख्या का पहले ही उपयोग किया जा चुका है।(ID Proof number has already been used to submit an application.)";
        public const string MSG_SAVE_DETAILS_Duplicate = "चयनित कॉलेज के लिए आवेदन पहले से मौजूद है।।(The application already exists for selected college.)";

        public const string MSG_APPROVE_SUCCESS = "User Approved successfully.";
        public const string MSG_APPROVE_ERROR = "There was an error Approved User!";
        public const string MSG_UPDATE_SUCCESS = "Updated successfully.";
        public const string MSG_DELETE_SUCCESS = "Deleted successfully.";
        public const string MSG_ADD_ERROR = "There was an error adding data!";
        public const string MSG_UPDATE_ERROR = "There was an error updating data!";
        public const string MSG_DELETE_ERROR = "There was an error deleting data!";
        public const string MSG_NO_DATA_SAVE = "No Data to Save!";
        public const string MSG_NO_DATA_UPDATE = "No Data to Update!";
        public const string MSG_NO_DATA_DELETE = "No Data to Delete!";
        public const string MSG_ERROR_OCCURRED = "Error occurred!";
        public const string MSG_RECORD_ALREADY_EXISTS = "Record already exists!";
        public const string MSG_AGE_NOT_VALID = "Age not valid!";
        public const string MSG_COLLEGE_CODE_DUPLICATE = "The code has already been used with another college";
        public const string MSG_COLLEGE_DUPLICATE = "The College Name already Exist";
        public const string MSG_AlreadyAssigned = "Entered Roll number range is already allocated to another invigilator. Please enter a new range.";
        public const string MSG_SAVE_SUCCESS_EXCEPT_UNVERIFIED_STUDENTS = "Saved successfully. Except Unverified Students!";

        public const string Login_DefaultPassword = "KD@1230";


        // regex
        public const string RGX_MOBILE_NO = @"^\d{10}$";
        public const string RGX_FAX_NO = @"^\+?[0-9]{6}$";
        public const string RGX_WEBSITE = @"^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+(\.[a-z]{2,}){1,3}(#?\/?[a-zA-Z0-9#]+)*\/?(\?[a-zA-Z0-9-_]+=[a-zA-Z0-9-%]+&?)?$";
        public const string RGX_LANDLINE_STD_NO = @"^[0][1-9]{2,3}(-)[0-9]{8}";
        public const string RGX_PINCODE = @"^[1-9][0-9]{5}$";
        public const string RGX_AADHAR_NO = @"^[2-9]{1}[0-9]{11}$";
        public const string RGX_PAN_CARD = @"^[A-Z]{5}[0-9]{4}[A-Z]{1}$";


        // uploaded folder path in static folder 
        public const string ReportFolderBTER = "Report/BTER";
        public const string ReportFolderITI = "Report/ITI";

        //BTER FOLDER
        public const string RDLCFolderBTER = "Report/Files/BTER";
        public const string LogFolder = "Log";
        public const string ReportsFolder = "Reports";
        public const string PlacementCompanyFolder = "PlacementCompany";
        public const string StudentsFolder = "Students";
        public const string StaffMemberFolder = "Staff";
        public const string AllotmentReciept = "Reports/AllotmentReciept";
        public const string AadharCertificate = "/StaticFiles/Certificate";
        public const string EsignedPdfFolder = "/EsignedPdf";
        public const string JoiningRelivingLetterBTER = "Report/BTER";

        //ITI FOLDERS
        public const string RDLCFolderITI = "Report/Files/ITI";
        public const string AdmitCardFolder_ITI = "/AdmitCard";
        public const string TimeTableFolder_ITI = "/TimeTable";
        public const string CenterSuperintendentFolder_ITI = "/CenterSuperintendent";
        public const string ReportsFolder_ITI = $"{ReportsFolder}/ITI";

        public const string RemunerationFolder = "/Remuneration/Invigilator";

        public const string JoiningLetterITI = "Report/ITI";
        public const string GetITIStudent_MarksheetReport = "Report/ITI";
        public const string PassStudentRreport = "Report/ITI";

        public const string StateTradeCertificateITI = "Report/ITI";

        //un auth code
        public const string UN_AUTH_ACCESS = "UNAUTHACCESS";
        public const string UN_AUTH_ROLE = "UNAUTHROLE";


        //Aadhaar esign auth keys
        //public const string AADHAAR_ESIGN_AUTH_ENC_KEY = "O0HP3OOR1DP2KZY752TJDO9DRHURSOX1"; //ENCRYPTION KEY
        public const string AADHAAR_ESIGN_AUTH_ENC_KEY = "4MKHPUYXLXGQK0IBDBI33ICD7YQMC53Y"; //ENCRYPTION KEY
                                                                                             // public const string AADHAAR_ESIGN_AUTH_APP_CODE = "PRAVL22868ABFAP012";  //APP CODE
        public const string AADHAAR_ESIGN_AUTH_APP_CODE = "KAUSHAL_D_DCE_TEST_EM2GMO";  //APP CODE
        public const string AADHAAR_ESIGN_AUTH_URL = "https://aadhaarauthtest.rajasthan.gov.in/AadhaarAuth";  //Aadhaar Auth URL
        public const string AADHAAR_ESIGN_AUTH_RETURN_URL = "http://10.68.231.168/rajsahakarapp/AadhaarEsignAuth/AadharAuthLanding";  //Return URL
        public const string SEWADWAAR_CLIENT_ID = "02a79073-c2db-4c84-b810-885fdaa8c54a";  //Sewadwaar Client ID
        public const string AADHAAR_ESIGN_SINGLE_MODE_URL = $"https://apitest.sewadwaar.rajasthan.gov.in/app/live/rajesign/Staging/Service/all/webresources/generic/esign/Doc?client_id={SEWADWAAR_CLIENT_ID}";
        public const string AADHAAR_GENERIC_ESIGN_URL = $"https://apitest.sewadwaar.rajasthan.gov.in/app/live/RajeSign/UAT/webresources/generic/eSignedocument?client_id={SEWADWAAR_CLIENT_ID}";
        public const string AADHAAR_GENERIC_MULTIPLE_ESIGN_PAGE_URL = $"https://apitest.sewadwaar.rajasthan.gov.in/app/live/RajeSign/UAT/webresources/generic/multipleEsignPage?client_id={SEWADWAAR_CLIENT_ID}";
        public const string AADHAAR_PAGE_WISE_ESIGNED_DOCUMENT_URL = $"https://apitest.sewadwaar.rajasthan.gov.in/app/live/RajeSign/UAT/webresources/generic/pageWiseeSignedocument?client_id={SEWADWAAR_CLIENT_ID}";
        public const string AADHAAR_BULK_ESIGNED_DOCUMENT_URL = $"https://apitest.sewadwaar.rajasthan.gov.in/app/live/RajeSign/Staging/bulkesign/webresources/generic/bulk/document?client_id={SEWADWAAR_CLIENT_ID}";
        public const string AADHAAR_BULK_PAGE_WISE_DOCUMENT_URL = $"https://apitest.sewadwaar.rajasthan.gov.in/app/live/RajeSign/Staging/bulkesign/webresources/generic/multiple/page?client_id={SEWADWAAR_CLIENT_ID}";


        //readonly
        /// <summary>
        /// Here value(int) is TypeID (Configuration Type ID), ex: 1 for Admission
        /// </summary>
        public static readonly ReadOnlyCollection<RestrictedUrlModel> RESTRICTED_URLS =
            new ReadOnlyCollection<RestrictedUrlModel>(new List<RestrictedUrlModel>
            {
                new RestrictedUrlModel{Url = "Test/AdmissionDateTest", Message = "Sorry! Admission date is over on {0}.", TypeID = 1 },
                new RestrictedUrlModel{Url = "StudentJanAadharDetail/SendJanaadharOTP", Message = "", TypeID = 1 },
                new RestrictedUrlModel{Url = "StudentJanAadharDetail/SavePersonalData", Message = "Sorry! Your admission date is over on {0}.", TypeID = 1 },
                new RestrictedUrlModel{Url = "EmitraPaymentService/EmitraApplicationPayment", Message = "Sorry! Your fee payment date is over on {0}.", TypeID = 14 },
                new RestrictedUrlModel{Url = "EmitraPaymentService/EmitraApplicationPayment", Message = "Sorry! Your fee payment date is over on {0}.", TypeID = 15 }
            });

    }
    #endregion

    #region local/live dynamic urls
    public static class CommonDynamicUrls
    {
        #region esign
        public static string GetSignedXMLUrl
        {
            get
            {
                return ConfigurationHelper.IsLocal ? "https://apitest.sewadwaar.rajasthan.gov.in/app/live/rajEsign/rajApi/esignApi/GetSignedXML" : "https://api.sewadwaar.rajasthan.gov.in/app/live/rajesign/rajApi/esignApi/GetSignedXML";
            }
        }
        public static string GetSignedPDFUrl
        {
            get
            {
                return ConfigurationHelper.IsLocal ? "https://apitest.sewadwaar.rajasthan.gov.in/app/live/rajEsign/rajApi/esignApi/GetSignedPDF" : "https://api.sewadwaar.rajasthan.gov.in/app/live/rajesign/rajApi/esignApi/GetSignedPDF";
            }
        }
        #endregion

        #region sso
        public static string SSOIDGetSomeDetailsUrl
        {
            get
            {
                return ConfigurationHelper.IsLocal ? "https://ssotest.rajasthan.gov.in:4443/SSOREST/GetUserDetailJSON/" : "https://sso.rajasthan.gov.in:4443/SSOREST/GetUserDetailJSON/";
            }
        }
        #endregion
        
        #region JanAadhaar
        public static string JanAadhaarMembersUrl
        {
            get
            {
                return ConfigurationHelper.IsLocal ? "https://api.sewadwaar.rajasthan.gov.in/app/live/Janaadhaar/Prod/Service/action/fetchJayFamily/" : "https://api.sewadwaar.rajasthan.gov.in/app/live/Janaadhaar/Prod/Service/action/fetchJayFamily/";
            }
        }
        #endregion
    }
    #endregion

    #region local/live dynamic codes
    public static class CommonDynamicCodes
    {
        #region esign
        public static string EsignClientCode
        {
            get
            {
                return ConfigurationHelper.IsLocal ? "KAUSHAL_D_BTER_TEST_RLTBNJ" : "KAUSHAL_D_BTER_TEST_RLTBNJ";
            }
        }
        public static string EsignClientId
        {
            get
            {
                return ConfigurationHelper.IsLocal ? "0df7e4099e5fad031ff871400dc07152" : "4b555247b9579ec01679d8b73abe7021";
            }
        }
        #endregion

        #region JanAadhaar
        public static string JanAadhaarClientId
        {
            get
            {
                return ConfigurationHelper.IsLocal ? "f6de7747-60d3-4cf0-a0ae-71488abd6e95" : "f6de7747-60d3-4cf0-a0ae-71488abd6e95";
            }
        }
        #endregion
    }
    #endregion

    #region classe
    public class GenericPaginationSpecification
    {
        private int _pageNumber = 1;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value <= 0 ? _pageNumber : value;
            }

        }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value <= 0 ? _pageSize : value > Constants.MAX_PAGE_SIZE ? Constants.MAX_PAGE_SIZE : value;
            }
        }
        public string OrderBy { get; set; }
    }
    public class PagigantionList
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageRecord { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPagination { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
    public class ApiResult<T>
    {
        public EnumStatus State { get; set; }
        public string Message { get; set; }

        public string PDFURL { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }
        public T Data { get; set; }
    }
    public class NewException
    {
        public string PageName { get; set; }
        public string ActionName { get; set; }
        public Exception Ex { get; set; }
    }
    public class ErrorDescription
    {
        public string Message { get; set; }
        public string PageName { get; set; }
        public string ActionName { get; set; }
        public string SqlExecutableQuery { get; set; }
    }
    public class UploadFileModel
    {
        public string? FolderName { get; set; }
        public IFormFile? file { get; set; }
        public string? FileExtention { get; set; }
        public string? MinFileSize { get; set; }
        public string? MaxFileSize { get; set; }
        public string? Password { get; set; }

    }
    public class UploadBTERFileModel
    {
        public int ApplicationID { get; set; }
        public int DocumentMasterID { get; set; }
        public int AcademicYear { get; set; }
        public int DepartmentID { get; set; }
        public int EndTermID { get; set; }
        public int Eng_NonEng { get; set; }
        public string? FolderName { get; set; }
        public string? FileName { get; set; }
        public IFormFile? file { get; set; }
        public string? FileExtention { get; set; }
        public string? MinFileSize { get; set; }
        public string? MaxFileSize { get; set; }
        public string? Password { get; set; }
        public bool? IsCopy { get; set; }
    }

    public class GetBTEROriginalListModel
    {
        public List<int>? DocumentMasterID { get; set; }
    }

    public class BTEROriginalModel
    {
        public int DocumentMasterID { get; set; }
    }
    #endregion

    #region enums
    public enum EnumStatus
    {
        Success = 1,
        Error = 2,
        Warning = 3
    }
    public enum EnumRole
    {
        Guest = 0,
        Developer = 1,
        Admin = 2,//bter admin eng
        Admin_NonEng = 12,//bter admin
        AdminNodel = 5,
        TPO = 6,
        Principal = 7,
        Principal_NonEng = 13,
        ExaminationIncharge = 38,
        ExaminationIncharge_NonEng = 56,
        Student = 3,
        Invigilator = 11,
        DTETraing = 16,
        ITIPrincipal = 20,
        Emitra = 4,
        Teacher = 8,
        DTE_Eng = 17,
        DTE_NonEng = 18,
        ITIAdmin_SCVT = 22,
        ITIAdmin_NCVT = 42,
        Principal_NCVT = 43,
        Examiner_Eng = 15,
        AccountsSec_Eng = 51,
        JDConfidential_Eng = 50,
        Examiner_NonEng = 59,
        AccountsSec_NonEng = 58,
        JDConfidential_NonEng = 54,
        ACP = 193,
        ACP_NonEng = 202,
        Registrar = 40,
        Registrar_NonEng = 55,
        CenterSuperintendent_Eng = 11,
        CenterSuperintendent_NonEng = 57,

        ITCell_Eng = 206,
        ITCell_NonEng = 210,
    }
    public enum EnmPaymentGatway
    {
        RPP = 1
    }
    public enum EnmPaymetRequest
    {
        PaymentRequest = 1,
        RefundRequest = 2
    }
    public enum EnumExamStudentStatus
    {
        Enrolled = 8,
        New_Enrolled = 9,
        SelectedForExamination = 10,
        SubmittedExamStudentStatus = 11,
        EnrolledFeePaid = 12,
        EligibleForExamination = 13,
        NewEligibleForExamination = 14,
        RejectatBTER = 15,
        ExaminationFeesPaid = 32,
        BackFeePaid = 33,
        VerifiedForExamination = 34,
        SelectedForEnrollment = 35,
        Addimited = 36,
        VerifiedForEnrollment = 37,
        EligibleForEnrollment = 53,
        New_Addimited = 139,
        ApprovalReject = 161,
        Dropout = 4,
        RevokeDropout = 209,
        Detained = 230,
        DetainedRevoke = 233,
        ReturnByAcp = 316,
        ApproveByAcp = 317
    }


    public enum EnumPdfType
    {

        RollList = 1,
        AdmitCard = 2,
        TimeTable = 3,
        CenterSuperintendent = 4,
        PracticalExaminer = 5,
        ExamCoordinator = 6
    }
    public enum EnumRollAdmitStatus
    {
        Genrated = 11,
        Forward = 12,
        Verify = 13,
        Publish = 14,
        Revert = 15,
    }

    public enum EnumRenumerationExaminer
    {
        Pending = 0,
        SubmittedAndForwardedtoJD = 36,
        ApprovedAndSendtoAccounts = 42,
        ApprovedfromAccounts = 43
    }

    public enum EnumMessageType
    {
        [Description("OTP")]
        Iti_OTP,

        [Description("FormSubmit")]
        Iti_FormSubmit,

        [Description("FormFinalSubmit")]
        Iti_FormFinalSubmit,

        [Description("ApplicationMessageITI")]
        Iti_ApplicationMessage,

        [Description("ApplicationMessageBTER")]
        Bter_ApplicationMessage,

        [Description("Bter_OTP")]
        Bter_OTP,

        [Description("Bter_FormFinalSubmit")]
        Bter_FormFinalSubmit,

        [Description("Bter_FormSubmit")]
        Bter_FormSubmit,

        [Description("Bter_NotifyCandidateDeficiency")]
        Bter_NotifyCandidateDeficiency,

        [Description("Bter_NotifyCandidateApproveMerit")]
        Bter_NotifyCandidateApproveMerit,

        [Description("Bter_NotifyCandidateRejectMerit")]
        Bter_NotifyCandidateRejectMerit,

        [Description("Bter_EnrollmentForStudent")]
        Bter_EnrollmentForStudent,
    }

    #endregion

    #region extensions
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value) =>
            value.GetType()
                 .GetField(value.ToString())?
                 .GetCustomAttribute<DescriptionAttribute>()?
                 .Description ?? value.ToString();
    }
    #endregion

}
