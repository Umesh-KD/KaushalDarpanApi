using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.GenerateEnroll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IGenerateRollRepository
    {
        Task<DataTable> GetGenerateRollData(GenerateRollSearchModel model);
        Task<DataTable> GetGenerateRevelData(GenerateRollSearchModel model);
        Task<int> SaveRolledData(List<GenerateRollMaster> model);
        Task<int> SaveAllRevelData(List<GenerateRollMaster> model);
        Task<int> OnPublish(List<GenerateRollMaster> model);
        Task<int> OnPublishRevelData(List<GenerateRollMaster> model);
        Task<int> SaveWorkflow(List<VerifyRollNumberList> model);
        Task<List<DownloadnRollNoModel>> GetGenerateRollDataForPrint(DownloadnRollNoModel model);
        Task<DataTable> GetPublishedRollData(GenerateRollSearchModel model);
        Task<DataTable> GetPublishedEnrollollData(GenerateRollSearchModel model);
        Task<DataTable> GetVerifyRollList(GenerateRollSearchModel model);
        Task<DataTable> GetVerifyRollListPdf(GenerateRollSearchModel model);
        Task<DataTable> GetITIAdmit_RollListPdf(GenerateRollSearchModel model);
        Task<int> ChangeRollNoStatus(GenerateRollSearchModel model);
        Task<DataTable> GetRollNumberDetails_History(GenerateRollSearchModel model);
        Task<DataTable> GenerateRollNoValidate(GenerateRollSearchModel model);
        Task<List<DownloadnRollNoModel>> GetGenerateRollDataForPrint_Insitute(DownloadnRollNoModel model);

        Task<List<DownloadnRollNoModel>> GetGenerateRollData_Collegewise(DownloadnRollNoModel model);
        Task<DataTable> ViewGenerateRollData(GenerateRollSearchModel model);
        Task<DataTable> GetRevalRollNoData_Verify(GenerateRollSearchModel model);
        Task<int> ChangeRevalRollNoStatus(GenerateRollSearchModel model);
        Task<DataTable> GetCollegeRollListAndAdmitCard(CollegeMasterSearchModel model);
    }
}
