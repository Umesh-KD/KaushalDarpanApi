using Kaushal_Darpan.Models.DTEInventoryModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDTELaboratoryMasterRepository
    {
        Task<DataTable> GetAllData(DTELaboratoryDataModel modal);
        Task<DTELaboratoryDataModel> GetById(int PK_ID);
        Task<bool> SaveData(DTELaboratoryDataModel productDetails);
        Task<bool> DeleteDataByID(DTELaboratoryDataModel productDetails);
    }
}
