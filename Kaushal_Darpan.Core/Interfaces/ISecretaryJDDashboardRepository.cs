using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.SecretaryJDDashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ISecretaryJDDashboardRepository
    {
        Task<DataTable> GetDashboardCount(SecretaryJDDashboardDataModel body);
        Task<DataTable> GetJDConfidentialDashboardCount(SecretaryJDDashboardDataModel body);
    }
}
