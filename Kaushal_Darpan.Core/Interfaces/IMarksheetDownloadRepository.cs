using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IMarksheetDownloadRepository
    {
        Task<DataTable> GetStudents(MarksheetDownloadSearchModel filterModel);
        Task<DataSet> MarksheetLetterDownload(MarksheetDownloadSearchModel model);
        Task<DataTable> Get5thSemBackPaperReport(BackPaperReportDataModel body);
    }
}
