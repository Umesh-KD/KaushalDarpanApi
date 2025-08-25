using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.ITICollegeMarksheetDownloadmodel;
using Kaushal_Darpan.Models.Report;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITICollegeMarksheetDownloadRepository
    {
        //Task<DataSet> GetITICollegeStudent_Marksheet(ITICollegeStudentMarksheetSearchModel Model);

        Task<DataTable> GetRollNumberOfStudentOfCollege(ITICollegeStudentMarksheetSearchModel model);

        Task<DataSet> GetITICollegeStudent_Marksheet(ITICollegeStudentMarksheetSearchModel model);

        Task<DataSet> ITIStateTradeCertificateReport(ITICollegeStudentMarksheetSearchModel model);
        Task<DataTable> GetITICollegeList(ITICollegeStudentMarksheetSearchModel model);

        Task<DataSet> GetITIConsolidateCertificate(ITICollegeStudentMarksheetSearchModel model);
    }
}
