using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.DateConfiguration;
using Kaushal_Darpan.Models.ItiExaminer;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface IAllotmentConfigurationRepository
    {
       
        Task<DataTable> GetAllData(DateConfigurationModel request);
        Task<int> SaveData(DateConfigurationModel productDetails);
        Task<int> AllSaveUpdateData(List<listDateConfigurationModel> model);
    }
}
