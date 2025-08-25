using Kaushal_Darpan.Models.EquipmentsMaster;
using Kaushal_Darpan.Models.ItemsMaster;
using Kaushal_Darpan.Models.TradeEquipmentsMapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ITradeEquipmentsMappingRepository
    {
        Task<DataTable> GetAllData(SearchTradeEquipmentsMapping SearchReq);
        Task<TradeEquipmentsMapping> GetById(int PK_ID);
        Task<bool> SaveData(TradeEquipmentsMapping productDetails);
        Task<bool> DeleteDataByID(TradeEquipmentsMapping productDetails);
    }
}
