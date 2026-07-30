using Kaushal_Darpan.Models.BTER_EstablishManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPayLevelMasterRepository
    {
        Task<int> SavePayLevelMasterData(PayLevelMasterDataModel request);
        Task<DataTable> GetPayLevelMasterData(PayLevelMasterDataModel body);
        Task<bool> DeletePayLevel_ByID(PayLevelMasterDataModel request);
    }
}
