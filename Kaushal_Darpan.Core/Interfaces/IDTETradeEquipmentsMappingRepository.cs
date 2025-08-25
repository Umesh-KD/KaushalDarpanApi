using Kaushal_Darpan.Models.DTEInventoryModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDTETradeEquipmentsMappingRepository
    {
        Task<DataTable> GetAllData(DTESearchTradeEquipmentsMapping SearchReq);
        Task<DTETradeEquipmentsMapping> GetById(int PK_ID);
        Task<bool> SaveData(DTETradeEquipmentsMapping productDetails);
        Task<bool> SaveEquipmentsMappingRequestData(DTERequestTradeEquipmentsMapping request);
        Task<bool> UpdateStatusData(DTEUpdateStatusMapping productDetails);
        Task<bool> DeleteDataByID(DTETradeEquipmentsMapping productDetails);
        Task<bool> HOD_EquipmentVerifications(DTETradeEquipmentsMapping productDetails);
        Task<DataTable> GetAllRequestData(DTESearchTradeEquipmentsMapping SearchReq);
        Task<bool> SaveRequestData(DTETEquipmentsRequestMapping request);



    }
}
