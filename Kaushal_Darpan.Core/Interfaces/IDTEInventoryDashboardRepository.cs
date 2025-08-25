using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.ITIInventoryDashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDTEInventoryDashboardRepository
    {
        Task<DataTable> GetInventoryDashboard(DTEInventoryDashboard filterModel);
    }
}
