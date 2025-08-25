using Kaushal_Darpan.Models.BterMeritMaster;
using Kaushal_Darpan.Models.ItiMerit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBterMeritMasterRepository
    {
        Task<DataTable> GetAllData(BterMeritSearchModel model);
        Task<DataTable> GenerateMerit(BterMeritSearchModel model);
        Task<DataTable> PublishMerit(BterMeritSearchModel model);

        Task<DataTable> UploadMeritdata(BterUploadMeritDataModel model);
        Task<DataTable> MeritFormateData(BterMeritSearchModel model);
        Task<DataTable> MeritReport(BterMeritSearchModel model);
    }
}
