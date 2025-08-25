using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.Allotment;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.ITIApplication;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface IBTERAllotmentRepository
    {
        Task<DataTable> GetGenerateAllotment(BTERAllotmentdataModel filterModel);
        Task<DataTable> GetPublishAllotment(BTERAllotmentdataModel filterModel);

        Task<DataTable> AllotmentCounter(BTERSearchModelCounter body);
        Task<DataTable> GetShowSeatMetrix(BTERSearchModel filterModel);
        Task<DataTable> GetStudentSeatAllotment(BTERSearchModel filterModel);
        Task<List<OptionDetailsDataModel>> GetOptionDetailsbyID(BTERSearchModel request);
        Task<DataTable> GetAllotmentData(BTERAllotmentModel body);
        Task<DataTable> GetAllotmentReport(BTERAllotmentModel body);


        Task<DataTable> UploadAllotmentData(BterUploadAllotmentDataModel model);
        Task<DataTable> AllotmnetFormateData(BterUploadAllotmentDataModel model);
    }
}
