using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.RoleMaster;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IHiringRoleMasterRepository
    {
        Task<DataTable> GetAllData();
        Task<DataTable> GetsanctionOrder(OrderDetailsList body);
        Task<DataTable> GetsanctionOrderNotAssign(OrderDetailsList body);
        Task<DataTable> GetAllSanction();
        Task<HiringRoleMasterModel> GetById(int PK_ID);
        Task<SanctionOrderMasterModel> GetByIDSanction(int PK_ID);
        Task<OrderDetailsList> GetByIDSanctionOrder(int PK_ID);
        Task<bool> SaveData(HiringRoleMasterModel productDetails);
        Task<bool> SaveDataSanction(SanctionOrderMasterModel productDetails);
        Task<bool> SaveSanctionOrder(OrderDetailsList productDetails);
        Task<bool> UpdateData(HiringRoleMasterModel productDetails);
        Task<bool> DeleteDataByID(HiringRoleMasterModel productDetails);
        Task<bool> DeleteSanctionOrder(HiringRoleMasterModel productDetails);
        Task<bool> DeleteDataBySanctionID(HiringRoleMasterModel productDetails);
    }
}
