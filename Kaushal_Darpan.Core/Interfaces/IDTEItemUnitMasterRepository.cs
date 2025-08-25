using Kaushal_Darpan.Models.DTEInventoryModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDTEItemUnitMasterRepository
    {
        Task<DataTable> GetAllData();
        Task<DTEItemUnitModel> GetById(int PK_ID);
        Task<bool> SaveData(DTEItemUnitModel productDetails);
        Task<bool> DeleteDataByID(DTEItemUnitModel productDetails);
    }
}
