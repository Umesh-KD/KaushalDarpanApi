using Kaushal_Darpan.Models.CreateTpoMaster;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.MenuMaster;
using Kaushal_Darpan.Models.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDTEApplicationDashboardRepository
    {
        Task<DataTable> GetDTEDashboard(DTEApplicationDashboardModel request);       
        Task<DataTable> GetAllApplication(DTEApplicationDashboardModel request);       
        Task<DataTable> GetDTEDashboardReports(DTEApplicationDashboardModel request);       
        Task<DataTable> GetDTEDashboardReportsNew(DTEAdminDashApplicationSearchModel request);       


    }
}
