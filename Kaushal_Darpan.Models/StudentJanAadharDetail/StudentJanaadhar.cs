namespace Kaushal_Darpan.Models.StudentJanAadharDetail
{
    //public class StudentJanaadharDetail
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }

    //}
    public class JanAaadharMemberListEntity
    {

        public string? ENR_ID { get; set; }
        public string? AADHAR_ID { get; set; }
        public string? NAME { get; set; }
        public string? MOBILE_NO { get; set; }
        public string? SRDR_MID { get; set; }
        public string? JAN_MEMBER_ID { get; set; }
        public string? JAN_AADHAR { get; set; }
        public string? BhamashaCardNo { get; set; }
        public string?  NAME_HINDI { get; set; }
        public string? SRDR_ID { get; set; }

        public string? MaskedMid { get; set; }
        public string? AADHAR_REF_NO { get; set; }
        public string? Status { get; set; }
        public string? Tid { get; set; }
        public string? OTP { get; set; }
        public int SendOTPSource { get; set; } = 1;

    }

    public class JanAaadharOTPRequest
    {
        public string? JAN_MEMBER_ID { get; set; }
        public string? AADHAR_REF_NO { get; set; }
    }

    public class OTPResponse
    {
        public string? Status { get; set; }
        public string? Tid { get; set; }
    }

    public class VerifyOTP
    {
        public string? Status { get; set; }
        public string? Tid { get; set; }
        public string? IsSuccess { get; set; }
        public string? JanAadharid { get; set; }
        public string? enrId { get; set; }
        public string? aadharid { get; set; }
        public string? janmemidselected { get; set; }
        public string? OTP { get; set; }
        public string? AADHAR_ID { get; set; }
        public bool IsVerified { get; set; }
        public string? SSOUserId { get; set; }
        public JanAadharMemberDetails Janaadhardetail { get; set; }
    }

    //public class JanAadharMemberDetails
    //{
    //    public string? mobile { get; set; }
    //    public string? gender { get; set; }
    //    public string? dob { get; set; }
    //    public string? IsVerified { get; set; }
    //    public string? nameEng { get; set; }
    //    public string? nameHnd { get; set; }
    //    public string? fnameEng { get; set; }
    //    public string? fnameHnd { get; set; }
    //    public string? ssoid { get; set; }
    //    public string? aadhar { get; set; }
    //    public string? passportphotoBase64 { get; set; }
    //    public string? IsRajasthanResident { get; set; }
    //    public string? Janaadhardetail { get; set; }
    //}


    public class BoardDataResult
    {
        public string ROLL { get; set; }
        public string DISTT { get; set; }
        public string CASTE { get; set; }
        public string SEX { get; set; }
        public string GRAND_TOT { get; set; }
        public string RESREM { get; set; }
        public string NAME { get; set; }
        public string F_NAME { get; set; }
        public string M_NAME { get; set; }
        public string YEAR { get; set; }
        public string MAX { get; set; }
        public string MAXNO { get; set; }
        public string Faculty { get; set; }
    }

    public class APIResponce
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public AadharResponce Data { get; set; }
    }

    public class AadharResponce
    {
        public AadharUserInfoResponce UserInfo { get; set; }
        public AadharAddressResponce UserAddress { get; set; }
        public AadharLocationResponce UserLocation { get; set; }

    }

    public class AadharLocationResponce
    {
        public string co { get; set; }
        public string country { get; set; }
        public string dist { get; set; }
        public string house { get; set; }
        public string lang { get; set; }
        public string lm { get; set; }
        public string loc { get; set; }
        public string name { get; set; }
        public string pc { get; set; }
        public string po { get; set; }
        public string state { get; set; }
        public string street { get; set; }
        public string subdist { get; set; }
        public string vtc { get; set; }
    }

    public class AadharAddressResponce
    {

        public string co { get; set; }
        public string dist { get; set; }
        public string house { get; set; }
        public string lm { get; set; }
        public string loc { get; set; }
        public string ms { get; set; }
        public string pc { get; set; }
        public string state { get; set; }
        public string street { get; set; }
        public string vtc { get; set; }



    }

    public class AadharUserInfoResponce
    {
        public string dob { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string ms { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string photo { get; set; }
    }

    public class JanAadharDetailsEntity
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public JanAadharMemberDetails UserDetails { get; set; }

    }
    public class JanAadharMemberDetails
    {
        public string janmemid { get; set; }
        public string enrid { get; set; }
        public string janaadhaarId { get; set; }
        public string aadhar { get; set; }
        public string nameEng { get; set; }
        public string nameHnd { get; set; }
        public string fnameEng { get; set; }
        public string caste { get; set; }
        public string category { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string mobile { get; set; }
        public string acc { get; set; }
        public string bankName { get; set; }
        public string ifsc { get; set; }
        public string age { get; set; }
        public string bankBranch { get; set; }
        public string fnameHnd { get; set; }
        public string snameEng { get; set; }
        public string snameHnd { get; set; }
        public string maritalStatus { get; set; }
        public string relationTyp { get; set; }
        public string mnameEng { get; set; }
        public string mnameHnd { get; set; }
        public string voterId { get; set; }
        public string micr { get; set; }
        public string income { get; set; }
        public string occupation { get; set; }
        public string qualification { get; set; }
        public string panNo { get; set; }
        public string passport { get; set; }
        public string dlNo { get; set; }
        public string email { get; set; }
        public string eid { get; set; }
        public string passportphotoBase64 { get; set; }
        public JanAadharMemberAddressDetails Address { get; set; }
        public bool IsRajasthanResident { get; set; }
        public string ssoid { get; set; }
    }
    public class JanAadharMemberAddressDetails
    {
        public string addressEng { get; set; }
        public string districtName { get; set; }
        public string block_city { get; set; }
        public string gp { get; set; }
        public string village { get; set; }
        public string pin { get; set; }
        public string age { get; set; }
        public string addressHnd { get; set; }
    }
    public enum SendOTPSource
    { 
        JanaadharOTP=1,
        AadharOTP=2
    
    }


    public class ApplicationStudentDatamodel
    {
        public int ApplicationID { get; set; }
        public string SSOID { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string CertificateGeneratDate { get; set; }
        public string CasteCertificateNo { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public int CategoryA { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public string JanAadharMemberID { get; set; }
        public string JanAadharNo { get; set; }
        public string ENR_ID { get; set; }
        public int coursetype {  get; set; }
        public bool IsRajasthani { get; set; }
        public int IsEws { get; set; }
        public string? AadharNo { get; set; }

        public int TradeLevel { get; set; }
        public int TradeID { get; set; }
        public int InstituteID { get; set; }
        public int DirectAdmissionTypeID { get; set; }
        public int BranchID { get; set; }
        public string? Apaarid { get; set; }
        public string? DepartmentName { get; set; }

        public int RoleID { get; set; }

        public string? adds_addressEng { get; set; }
        public string? adds_districtName { get; set; }
        public string? adds_block_city { get; set; }
        public string? adds_gp { get; set; }
        public string? adds_village { get; set; }
        public string? adds_pin { get; set; }
        public string? adds_addressHnd { get; set; }

    }

    public class ApplicationDTEStudentDatamodel
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
        public string CertificateGeneratDate { get; set; }
        public string CasteCertificateNo { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string CategoryA { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public string JanAadharMemberID { get; set; }
        public string JanAadharNo { get; set; }
        public string ENR_ID { get; set; }
        public int coursetype { get; set; }
        public bool IsRajasthani { get; set; }
        public int IsEws { get; set; }
        public string? AadharNo { get; set; }

        public int? TradeLevel { get; set; }
        public int? TradeID { get; set; }
        public int? InstituteID { get; set; }
        public int? DirectAdmissionTypeID { get; set; }
        public int? BranchID { get; set; }
        public string PrefentialCategoryType { get; set; } 
        public string? Apaarid { get; set; }
        public string? DepartmentName { get; set; }
        public string? WhatsNumber { get; set; }
        public string? LandlineNumber { get; set; }
        public string IndentyProff { get; set; }
        public string DetailID { get; set; }
        public string Maritial { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
     
        public string? IsPH { get; set; }
        public string? IsKM { get; set; }
        public int CategoryB { get; set; }
        public int CategoryC { get; set; }
        public int CategoryE { get; set; }
        public int Prefential { get; set; }
        public string? Dis_SignaturePhoto { get; set; }
        public string? Dis_StudentPhoto { get; set; }
        public string? SignaturePhoto { get; set; }
        public string? StudentPhoto { get; set; }
        public bool IsMinority { get; set; }
        public int IsFinalSubmit { get; set; }
        public int CourseType { get; set; }
        public string? CourseTypeName { get; set; }
        public bool IsTSP { get; set; }
        public bool IsSaharia { get; set; }
        public int TspDistrictID { get; set; }
        public int IsDevnarayan { get; set; }
        public int DevnarayanTehsilID { get; set; }
        public int DevnarayanDistrictID { get; set; }
        public int TSPTehsilID { get; set; }
        public int subCategory { get; set; }
        public int EWS { get; set; }
        public int CategoryD { get; set; }
        public int DirectAdmissionType { get; set; }
        public int RoleID { get; set; }
        public int IsMBCCertificate { get; set; }
        public bool? isCorrectMerit { get; set; }
    }

    public class SearchApplicationStudentDatamodel
    {
        public int ApplicationID { get; set; }

        public string SSOID { get; set; }
        public string Action { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public int DepartmentID { get; set; }
        public int ModifyBy { get; set; }
        public int RoleID { get; set; }
        public int CourseTypeID { get; set; }
        public string? JanAadharMemberId { get; set; }
    }


    public class BoardDat
    {
        public string Class { get; set; }
        public string rollno { get; set; }
        public string year { get; set; }
        public string board { get; set; }
        
    }

}
