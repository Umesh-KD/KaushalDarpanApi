using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITIGenerateEnrollment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIGenerateRollRepository
    {
        Task<DataTable> GetGenerateRollData(GenerateRollSearchModel model);
        Task<int> SaveRolledData(List<GenerateRollMaster> model);
        Task<int> OnPublish(List<GenerateRollMaster> model);
        Task<List<DownloadnRollNoModel>> GetGenerateRollDataForPrint(DownloadnRollNoModel model);
        Task<DataTable> GetPublishedRollDataITI(ITIGenerateRollSearchModel model);

    }
}
