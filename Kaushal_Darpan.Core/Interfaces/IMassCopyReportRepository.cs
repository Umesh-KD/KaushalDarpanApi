using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.ITINCVT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IMassCopyReportRepository
    {
        Task<DataTable> MassCopyReport_GetListData(MassCopyReportDataModel body);
    }
}
