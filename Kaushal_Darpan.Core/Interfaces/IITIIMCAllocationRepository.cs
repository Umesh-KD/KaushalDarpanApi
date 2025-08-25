
using Kaushal_Darpan.Models.ITIIMCAllocation;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIIMCAllocationRepository
    {



        Task<DataTable> GetAllData(ITIIMCAllocationSearchModel filterModel);
        
        Task<DataTable> GetAllDataPhoneVerify(ITIIMCAllocationSearchModel filterModel);
        Task<DataTable> StudentDetailsList(ITIIMCAllocationSearchModel filterModel);

        Task<int> UpdateAllotments(ITIIMCAllocationDataModel productDetails);
        Task<int> RevertAllotments(ITIIMCAllocationDataModel productDetails);

        Task<DataTable> GetTradeListByCollege(ITIIMCAllocationSearchModel filterModel);
        Task<DataTable> ShiftUnitList(ITIIMCAllocationSearchModel filterModel);

        Task<DataSet> GetIMCStudentDetails(ITIIMCAllocationSearchModel body);
        Task<DataTable> UpdateMobile(ITIIMCAllocationSearchModel body);
    }
}
