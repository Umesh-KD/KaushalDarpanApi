using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IRoomsMasterRepository
    {
        Task<DataTable> GetAllData(RoomsMasterDataModel filterModel);
        Task<RoomsMasterDataModel> Get_RoomsMasterData_ByID(int RoomMasterID, int DepartmentID);
        Task<bool> SaveData(RoomsMasterDataModel exammaster);
        Task<bool> DeleteDataByID(RoomsMasterDataModel productDetails);

    }
}
