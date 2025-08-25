using Kaushal_Darpan.Models.ItiMerit;
using Kaushal_Darpan.Models.TimeTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IItiMeritMasterRepository
    {
        Task<DataTable> GetAllData(ItiMeritSearchModel model);
        Task<DataTable> GenerateMerit(ItiMeritSearchModel model);
        Task<DataTable> PublishMerit(ItiMeritSearchModel model);

    }
}
