
using Kaushal_Darpan.Models.ITIIMCAllocation;
using Kaushal_Darpan.Models.ITINCVT;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITINCVTRepository
    {
        Task<DataTable> GetAllData(ITINCVTDataModel body);
        Task<string> UpdatePushStatus(ITINCVTDataModel body);
        Task<DataTable> GetNCVTExamDataFormat(ITINCVTDataModel body);
        Task<DataTable> SaveExamData(List<ITINCVTImportDataModel> model);
    }
}
