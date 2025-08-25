using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.ITIMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBTERMasterRepository
    {
        Task<DataTable> GetBTER_CollegeLoginInfoMaster(BTERCollegeLoginInfoSearchModel request);
        Task<DataTable> BTERGetCollegeLoginInfoByCode(BTERCollegeLoginInfoSearchModel request);

        Task<int> BTERUpdate_CollegeLoginInfo(BTERCollegeLoginInfoSearchModel request);

    }
}
