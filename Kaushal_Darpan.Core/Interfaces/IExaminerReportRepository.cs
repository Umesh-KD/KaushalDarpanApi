using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.PolytechnicReport;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface IExaminerReportRepository
    {
        Task<DataTable> GetAllData(ExaminerReportDataSearchModel model);
        
    }
}
