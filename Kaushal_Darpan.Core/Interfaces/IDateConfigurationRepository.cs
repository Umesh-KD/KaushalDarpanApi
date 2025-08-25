using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.DateConfiguration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDateConfigurationRepository
    {
        Task<int> DeleteByID(int PK_ID);
        Task<DataTable> GetDateDataList(DateConfigurationModel dateConfiguration);
        Task<List<DateConfigurationModel>> GetDateDataForMiddleware(DateConfigurationModel dateConfiguration);
        Task<DataTable> GetAllData(DateConfigurationModel request);
        Task<DateConfigurationModel> GetById(int PK_ID);
        Task<int> SaveData(DateConfigurationModel productDetails);
    }
}
