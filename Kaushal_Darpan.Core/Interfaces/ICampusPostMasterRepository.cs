using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICampusPostMasterRepository
    {
        Task<DataTable> GetAllData(string SSOID, int DepartmentID);
        Task<CampusPostMasterModel> GetById(int PK_ID);
        Task<List<CampusPostMasterModel>> GetNameWiseData(int ID, int DepartmentID);
        Task<bool> SaveData(CampusPostMasterModel productDetails);
        Task<bool> Save_CampusValidation_NodalAction(CampusPostMaster_Action model);
        Task<bool> UpdateData(CampusPostMasterModel productDetails);
        Task<bool> DeleteDataByID(CampusPostMasterModel productDetails);
        Task<DataTable> CampusValidationList(int CompanyID, int CollegeID, string Status, int DepartmentID);
        Task<DataTable> CampusHistoryList(int CompanyID, int CollegeID, string Status, int DepartmentID);


        Task<DataTable> GetAllSignedCopyData(SignedCopyOfResultSearchModel signedCopy);
        Task<SignedCopyOfResultModel> GetSignedCopyById(int PK_ID);
        Task<int> SaveSignedCopyData(SignedCopyOfResultModel productDetails);
        Task<bool> DeleteSignedCopyDataByID(SignedCopyOfResultSearchModel signedCopy);



    }
}
