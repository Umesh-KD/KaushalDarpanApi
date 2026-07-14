using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.RevaluationDataModel;
using Kaushal_Darpan.Models.UserMaster;
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
        Task<DataTable> GetDetailsWhatsApp(RevaluationDataModel filterModel);
        Task<DataTable> GetAllRevalation(StudentDetailsByRollNoModel filterModel);
        Task<DataTable> GetAllRevalationWhatsApp(StudentDetailsByRollNoModel filterModel);
        Task<FeeAmountResponseModelWhatsApp> GetFeeAmountRevalSubject(FeeAmountModel_WhatsApp body);
        Task<DataTable> GetAllRevalationReportList(RevalationReportsearchModel body);
    }
}
