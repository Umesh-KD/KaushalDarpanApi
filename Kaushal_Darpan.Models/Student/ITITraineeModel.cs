using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaushal_Darpan.Models.Student
{
    public class ITITraineeUploadModel
    {
        public string? StateRegNumber { get; set; }
        public string? TraineeName { get; set; }
        public string? UIDNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Category { get; set; }
        public string? FatherGuardianName { get; set; }
        public string? MotherName { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailID { get; set; }
        public string? Session { get; set; }
        public string? AdmissionDate { get; set; }
        public string? HighestQualification { get; set; }
        public string? Trade { get; set; }
        public string? Shift { get; set; }
        public string? Unit { get; set; }
        public string? IsTraineeDualMode { get; set; }
        public string? MISITICode { get; set; }
        public string? PersonwithDisability { get; set; }
        public string? PWDcategory { get; set; }
        public string? EconomicWeakerSection { get; set; }
        public string? TraineeType { get; set; }
    }
    public class TraineeUploadResponse
    {
        public int? SucessRec { get; set; }
        public int? ErrorRec { get; set; }
        public int? TotalRec { get; set; }
        public List<ResponseData>? ResponseData { get; set; }
    }

    public class ResponseData
    {
        public string? StateRegNumber { get; set; }
        public string? TraineeName { get; set; }
        public string? ErrorDescription { get; set; }
        public string? RecordStatus { get; set; }
        public string? MobileNumber { get; set; }
    }

    public class RootDataModel
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public TraineeUploadResponse? Data { get; set; }
        public object? MetaData { get; set; }
    }




  









    public class TokenResponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public TokenData? Data { get; set; }
    }

    public class TokenData
    {
        public string? accessToken { get; set; }
        public string? refreshToken { get; set; }
        public string? tokenType { get; set; }
        public int? expiresIn { get; set; }
        public int? refreshExpiresIn { get; set; }
        public string[]? roles { get; set; }
        public string? sessionId { get; set; }
    }

    public class ChunksSearchModel
    {
        public int pageSize { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int AcedmicYearID { get; set; }
        public string? Action { get; set; }

    }

    public class NCVTChunkInfoDataModel
    {
        public int PageNumber { get; set; }
        public int RowCount { get; set; }
        public int MinAID { get; set; }
        public int MaxAID { get; set; }
        public string? AIDS { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
        public string? SessionID { get; set; }

    }

    public class UploadTrainee_LogsModel
    {
        public string LogID { get; set; }
        public string RequestID { get; set; }
        public string Response { get; set; }
    }

}
