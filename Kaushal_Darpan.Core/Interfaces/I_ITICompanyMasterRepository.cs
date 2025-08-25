using Kaushal_Darpan.Models.ItiCompanyMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITICompanyMasterRepository
    {
        Task<DataTable> GetAllData(ItiCompanyMasterSearchModel filterModel);
        Task<ItiCompanyMasterResponsiveModel> GetById(int ID);
        Task<bool> SaveData(ItiCompanyMasterModels productDetails);
        Task<bool> Save_CompanyValidation_NodalAction(ItiCompanyMaster_Action model);
        Task<bool> DeleteDataByID(ItiCompanyMasterModels productDetails);
        Task<DataTable> CompanyValidationList(ItiCompanyMasterSearchModel filterModel);
    }
}
