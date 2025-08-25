
using Kaushal_Darpan.Models.ITI_IIP_TrimashQuaterlyReportModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITI_IIP_TrimashQuaterlyReportRepository
    {
        Task<int> SaveIIPmasterData(ITI_IIP_TrimashQuaterlyReportModel model);

        Task<DataTable> GetIIPmasterDataByID(int id);
        Task<int> deleteIIPDataByID(int id);

        //Task<DataTable> GetCenterSuperitendentReportData(ITICollegeStudentMarksheetSearchModel model);

        Task<DataTable> GetIIPmasterData();
    }
}
