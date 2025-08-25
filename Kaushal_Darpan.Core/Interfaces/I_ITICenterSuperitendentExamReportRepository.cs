
using Kaushal_Darpan.Models.ITICenterSuperitendentExamReport;
using Kaushal_Darpan.Models.ITICollegeMarksheetDownloadmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITICenterSuperitendentExamReportRepository
    {
        Task<int> SaveData(ITICenterSuperitendentExamReportModel productDetails);

        Task<DataTable> GetCenterSuperitendentReportById(int id);

        //Task<DataTable> GetCenterSuperitendentReportData(ITICollegeStudentMarksheetSearchModel model);

        Task<DataTable> GetCenterSuperitendentReportData();
    }
}
