using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CollegeAdmissionSeatAllotment;
using Kaushal_Darpan.Models.ITIIMCAllocation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICollegeAdmissionSeatAllotmentRepository
    {
        Task<DataTable> GetApplicationDatabyID(ApplicationSearchDataModel searchRequest);
        Task<DataTable> GetTradeListByCollege(SeatMatrixSearchModel filterModel);
        Task<DataTable> GetBranchListByCollege(SeatMatrixSearchModel filterModel);
        Task<DataTable> ShiftUnitList(SeatMatrixSearchModel filterModel);
        Task<int> UpdateAllotments(SeatAllocationDataModel productDetails);
    }
}
