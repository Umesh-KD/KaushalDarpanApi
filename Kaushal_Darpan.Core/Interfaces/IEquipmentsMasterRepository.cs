using Kaushal_Darpan.Models.EquipmentsMaster;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IEquipmentsMasterRepository
    {
        Task<DataTable> GetAllData();
        Task<EquipmentsModel> GetById(int PK_ID);
        Task<bool> SaveData(EquipmentsModel productDetails);
        Task<bool> DeleteDataByID(EquipmentsModel productDetails);
    }
}
