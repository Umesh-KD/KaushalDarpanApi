using Kaushal_Darpan.Models.CreateTpoMaster;
using Kaushal_Darpan.Models.SsoidUpdate;
using Kaushal_Darpan.Models.StaffDashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ISsoidUpdateRepository
    {
        Task<DataTable> GetAllData(SsoidUpdateSearchModel model);
        Task<int> SaveAllData(List<SsoidUpdateDataModel> productDetails);
    }
}
