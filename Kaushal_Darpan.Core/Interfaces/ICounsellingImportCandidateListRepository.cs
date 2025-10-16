using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.ITITimeTable;
using Kaushal_Darpan.Models.TimeTable;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICounsellingImportCandidateListRepository
    {
        Task<DataTable> GetSampleExcelFile();
        Task<DataTable> GetCandidateList(CounsellingAllotmentListModel filterModel);
        Task<List<CounsellingImportExcelModel>> ImportExcelFile(List<CounsellingImportExcelModel> model);
        Task<int> SaveImportExcelData(List<CounsellingImportExcelModel> request);
        Task<int> EditCandidateExcelDataById(CounsellingImportExcelModel request);
    }
}
