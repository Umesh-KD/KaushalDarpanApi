using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.Allotment;
using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.ITIApplication;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface IITIAllotmentRepository
    {
        Task<DataTable> GetGenerateAllotment(AllotmentdataModel filterModel);

        Task<DataTable> AllotmentCounter(SearchModel body);
        Task<DataTable> GetShowSeatMetrix(SearchModel filterModel);
        Task<DataTable> GetStudentSeatAllotment(SearchModel filterModel);
        Task<List<OptionDetailsDataModel>> GetOptionDetailsbyID(SearchModel request);
        Task<DataTable> GetAllotmentData(SearchModel body);
        Task<DataTable> GetAllotmentStatusList(AllotmentStatusSearchModel filterModel);

        Task<DataTable> GetPublishAllotment(AllotmentdataModel body);

        Task<DataTable> GetAllotmentReport(SearchModel body);

        Task<DataTable> GetAllData(ITIDirectAllocationSearchModel body);
        Task<DataTable> StudentDetailsList(ITIDirectAllocationSearchModel body);
        Task<DataTable> GetAllDataPhoneVerify(ITIDirectAllocationSearchModel body);
        Task<DataSet> GetStudentDetails(ITIDirectAllocationSearchModel body);
        Task<int> UpdateAllotments(ITIDirectAllocationDataModel request);
        Task<int> RevertAllotments(ITIDirectAllocationDataModel request);
        Task<DataTable> GetTradeListByCollege(ITIDirectAllocationSearchModel body);
        Task<DataTable> ShiftUnitList(ITIDirectAllocationSearchModel body);

    }
}
