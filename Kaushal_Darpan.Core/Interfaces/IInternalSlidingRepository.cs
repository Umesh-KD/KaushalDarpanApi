using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.ITIApplication;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IInternalSlidingRepository
    {
        Task<DataTable> GetInternalSliding(SearchSlidingModel filterModel);
        Task<DataTable> GetGenerateAllotment(SearchSlidingModel filterModel);
        Task<DataTable> GetDDLInternalSlidingUnitList(SearchSlidingModel filterModel);
        Task<DataTable> SaveData(SearchSlidingModel productDetails);
        Task<DataTable> SaveSawpData(SearchSlidingModel productDetails);
    }
}
