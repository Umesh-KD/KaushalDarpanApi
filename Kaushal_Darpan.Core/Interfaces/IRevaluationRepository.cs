using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.RevaluationDataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IRevaluationRepository
    {
        Task<DataTable> GetDetails(RevaluationDataModel filterModel);
        Task<DataTable> GetAllRevalation(StudentDetailsByRollNoModel filterModel);
    }
}
