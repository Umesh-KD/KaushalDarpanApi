using Kaushal_Darpan.Models.BTERInternalSliding;
using Kaushal_Darpan.Models.ITIAllotment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBTERInternalSlidingRepository
    {
        Task<DataTable> GetInternalSliding(BTERInternalSlidingSearchModel filterModel);
        Task<DataTable> SaveData(BTERInternalSlidingSearchModel productDetails);
        Task<DataTable> SaveSwapData(BTERInternalSlidingSearchModel productDetails);
        Task<DataTable> GetGenerateAllotment(BTERInternalSlidingSearchModel filterModel);
    }
}
