using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.Allotment;
using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.BTERInternalSliding;
using Kaushal_Darpan.Models.ITIApplication;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface IBTERAllotmentStatusRepository
    {
        Task<DataTable> GetAllotmentStatusList(AllotmentStatusSearchModel filterModel);
        Task<DataTable> GetAllotmentUpwardList(AllotmentStatusSearchModel body);
        Task<DataTable> GetITIAllotmentUpwardList(AllotmentStatusSearchModel body);
		
    }
	
}
