using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using System.Data;
using static Kaushal_Darpan.Core.Helper.CommonFuncationHelper;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICampusPostMasterRepository
    {
        Task<DataTable> GetAllData(string SSOID, int DepartmentID);
        Task<CampusPostMasterModel> GetById(int PK_ID);
        Task<List<CampusPostMasterModel>> GetNameWiseData(int ID, int DepartmentID);
        Task<DataTable> SaveData(CampusPostMasterModel productDetails);
        Task<bool> Save_CampusValidation_NodalAction(CampusPostMaster_Action model);
        Task<bool> UpdateData(CampusPostMasterModel productDetails);
        Task<bool> DeleteDataByID(CampusPostMasterModel productDetails);
        Task<DataTable> CampusValidationList(int CompanyID, int CollegeID, string Status, int DepartmentID, int CompanyTypeID = 0, string Flag="");
        Task<DataTable> GetCampusSMSDataByID(SmsDataModel reuqest);
        Task<DataTable> CampusValidationList(int CompanyID, int CollegeID, string Status, int DepartmentID, int CompanyTypeID = 0, string Flag = "", int FinancialYearID = 0, int postId = 0);
        Task<DataTable> CampusHistoryList(int CompanyID, int CollegeID, string Status, int DepartmentID);
        Task<DataTable> GetAllSignedCopyData(SignedCopyOfResultSearchModel signedCopy);
        Task<SignedCopyOfResultModel> GetSignedCopyById(int PK_ID);
        Task<int> SaveSignedCopyData(SignedCopyOfResultModel productDetails);
        Task<bool> DeleteSignedCopyDataByID(SignedCopyOfResultSearchModel signedCopy);
        Task<int> CampusPost_UpdateStatus(CampusPost_UpdateStatus_Model request);



    }
}
