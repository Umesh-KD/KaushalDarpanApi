using Kaushal_Darpan.Models.CollegeMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICollegeMasterRepository
    {
        Task<DataTable> GetCollegeNodalDashboardData(CollageDashboardSearchModel model);
        Task<DataTable> GetAllData(CollegeMasterSearchModel model);
        Task<DataTable> StatusChangeByID(int InstituteID, int ActiveStatus, int UserID);
        Task<CollegeMasterModel> GetById(CollegeMasterRequestModel model);
        Task<bool> SaveData(CollegeMasterModel productDetails);
        Task<bool> DeleteDataByID(CollegeMasterModel productDetails);
        Task<bool> UpdateActiveStatusByID(CollegeMasterModel productDetails);
        Task<DataTable> GetInstituteProfileStatus(int InstituteID);
        Task<DataTable> GetCollegeList(CollegeListSearchModel model);
    }
}
