using Kaushal_Darpan.Models.HostelManagement;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IHostelRoomDetailsRepository
    {
        Task<int> SaveData(RoomDetailsModel request);
        Task<int> SaveExcelData(List<RoomExcelDetailsModel> request);
        Task<DataTable> GetAllData(int HostelID, int RoomTypeID);
        Task<DataTable> GetRoomDDLList(int HostelID, int RoomTypeID, int EndTermID);
        Task<bool> DeleteDataByID(StatusChangeModelNew request);
        Task<RoomDetailsModel> GetById(int PK_ID);

    }
}
