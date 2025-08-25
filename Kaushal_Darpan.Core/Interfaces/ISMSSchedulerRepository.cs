using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.SMSService;
//using Kaushal_Darpan.Models.ITITimeTable;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ISMSSchedulerRepository
    {
        Task<DataTable> GetAllUnsentMsgs();
        Task<int> MarkAsSentAsync(int id);

    }
}
