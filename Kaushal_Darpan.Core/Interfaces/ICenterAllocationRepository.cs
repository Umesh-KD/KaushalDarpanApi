using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICenterAllocationRepository
    {
        Task<DataTable> GetAllData(CenterAllocationSearchFilter filterModel);
        Task<DataTable> CenterSuperintendent(CenterAllocationSearchFilter filterModel);
        Task<DataSet> DownloadCenterSuperintendent(CenterAllocationSearchFilter filterModel);
        Task<int> SaveRollNumbePDFData(DownloadnRollNoModel request);
        Task<DataTable> GetRollCenterSuperintendentOrder(int status,int coursetype);
        Task<CenterAllocationtDataModel> GetById(int PK_ID);
        Task<int> SaveData(List<CenterAllocationtDataModel> productDetails);
        Task<bool> DeleteDataByID(CenterAllocationtDataModel productDetails);
        Task<DataTable> GetInstituteByCenterID(CenterAllocationSearchFilter productDetails);

    }
}
