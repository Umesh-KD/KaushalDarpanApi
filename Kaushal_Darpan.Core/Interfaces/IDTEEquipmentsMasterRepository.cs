using Kaushal_Darpan.Models.DTEInventoryModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDTEEquipmentsMasterRepository
    {
        Task<DataTable> GetAllData(CommonSearchModal modal);
        Task<DTEEquipmentsModel> GetById(int PK_ID);
        Task<bool> SaveData(DTEEquipmentsModel productDetails);
        Task<bool> DeleteDataByID(DTEEquipmentsModel productDetails);
    }
}
