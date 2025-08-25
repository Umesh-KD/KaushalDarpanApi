using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.GuestRoomManagementModel;
using Kaushal_Darpan.Models.HostelManagementModel;

namespace Kaushal_Darpan.Core.Interfaces
{
    

    public interface IGuestHouseRepository
    {

        Task<int> SaveData(GuestHouseDataModel hostelManagement);
        Task<DataTable> GetAllHostelList(GuestHouseSearchModel filterModel);


        Task<GuestHouseDataModel> GetByHostelId(int PK_ID);

        Task<bool> DeleteDataByID(GuestHouseDataModel productDetails);

    }
}