using Kaushal_Darpan.Models.ItemUnitModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ItemUnitMasterRepository
    {
        Task<DataTable> GetAllData();
        Task<ItemUnitModel> GetById(int PK_ID);
        Task<bool> SaveData(ItemUnitModel productDetails);
        Task<bool> DeleteDataByID(ItemUnitModel productDetails);
    }
}
