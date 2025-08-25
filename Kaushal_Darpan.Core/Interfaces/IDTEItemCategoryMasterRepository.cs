using Kaushal_Darpan.Models.DTEInventoryModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDTEItemCategoryMasterRepository
    {
        Task<DataTable> GetAllData();
        Task<DTEItemCategoryModel> GetById(int PK_ID);
        Task<int> SaveData(DTEItemCategoryModel productDetails);
        Task<bool> DeleteDataByID(DTEItemCategoryModel productDetails);
    }
}
