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
        Task<int> SaveData(DTELaboratoryDataModel productDetails);
        Task<bool> DeleteDataByID(DTELaboratoryDataModel productDetails);
        Task<DataTable> GetLabDetailsByUserID(LabDetailsSearchModel model);
    }
}
