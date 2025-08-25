using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.AdminDashboardIssueTrackerSearchModel;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IAdminDashboardIssueTrackerRepository
 
    {
         Task<DataTable> GetAdminDashData(AdminDashboardIssueTrackerSearchModel model);
        
    }
}
