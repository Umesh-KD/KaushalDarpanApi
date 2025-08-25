using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.CampusDetailsWeb;
using Kaushal_Darpan.Models.DateConfiguration;
using Kaushal_Darpan.Models.InvigilatorAppointmentMaster;
using Kaushal_Darpan.Models.UserMaster;
using Kaushal_Darpan.Models.WebsiteSettings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IWebsiteSettingsRepository
    {
        Task<int> SaveData(WebsiteSettingDataModel request);
        Task<DataTable> GetAllData(WebsiteSettingDataModel request);
        Task<DataTable> GetDynamicUploadTypeDDL(RequestBaseModel body);
        Task<bool> DeleteDataByID(WebsiteSettingDataModel request);
        Task<WebsiteSettingDataModel> GetById(WebsiteSettingDataModel body);

        Task<DataTable> GetDynamicUploadContent(DynamicUploadContentListsModal model);
        Task<bool> ActiveStatusChange(WebsiteSettingDataModel request);
    }
}
