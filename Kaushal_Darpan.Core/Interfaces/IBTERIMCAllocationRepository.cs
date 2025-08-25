
using Kaushal_Darpan.Models.BTERIMCAllocationModel;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBTERIIMCAllocationRepository
    {



        Task<DataTable> GetAllData(BTERIMCAllocationSearchModel filterModel);
        
        Task<DataTable> GetAllDataPhoneVerify(BTERIMCAllocationSearchModel filterModel);
        Task<DataTable> StudentDetailsList(BTERIMCAllocationSearchModel filterModel);

        Task<int> UpdateAllotments(BTERIMCAllocationDataModel productDetails);
        Task<int> RevertAllotments(BTERIMCAllocationDataModel productDetails);

        Task<DataTable> GetBranchListByCollege(BTERIMCAllocationSearchModel filterModel);
        Task<DataTable> ShiftUnitList(BTERIMCAllocationSearchModel filterModel);

      
    }
}
