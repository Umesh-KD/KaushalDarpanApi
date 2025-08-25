using Kaushal_Darpan.Models.ITICenterObserver;
using Kaushal_Darpan.Models.ITIResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITIResultRepository
    {
        Task<DataTable> GenerateResult(ITIResultsModel model);
        Task<DataTable> GetResultData(ITIResultsModel model);
        Task<DataTable> PublishResult(ITIResultsModel model);

        Task<DataTable> GetCurrentStatusOfResult(ITIResultsModel model);
        Task<DataSet> GetCFormReport(ITIResultsModel model);
        Task<DataTable> GetStudentPassFailResultData(ITIStudentPassFailResultsModel model);
        //Task<DataTable> GetCurrentPassFailResultStatus(ITIStudentPassFailResultsModel model);
        Task<DataTable> GetITITradeList(ITIStudentPassFailResultsModel model);
    }
}
