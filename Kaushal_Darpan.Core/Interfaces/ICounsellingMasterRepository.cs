using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CollegeWiseScholarship;
using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.CounsellingMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICounsellingMasterRepository
    {
        Task<int> SaveData(ApplicationDataModel productDetails);
        Task<DataTable> MapCandidateSSO(CounsellingApplicationSearchModel filterModel);
        Task<DataTable> GetCounsellingAllotmentList(CounsellingAllotmentListModel filterModel);
        Task<DataTable> GetCounsellingAllotmentListExcel(CounsellingAllotmentListModel filterModel);
        Task<DataTable> GetCandidateList(CounsellingAllotmentListModel filterModel);
        Task<int> SaveCandidateAllotment_Counselling(int TradeID, List<Counselling_AllotmentDataModel> model);
        Task<DataTable> GetAllottedCandidateList_Counselling(CounsellingAllottedListSearchModel body);
        Task<bool> SaveFinalInstituteAllotment_Counselling(EditInstituteDataModel_Counselling model);
        Task<DataSet> GenerateAllotmentOrder_Counselling(List<EditInstituteDataModel_Counselling> model);
        Task<bool> UpdateAllotmentOrder_Counselling(List<EditInstituteDataModel_Counselling> model);
        Task<DataTable> GetSampleExcelFile_CounsellingVacant();
        Task<List<ImportCounsellingVacancyDataModel>> ImportExcelFile_CounsellingVacant(List<ImportCounsellingVacancyDataModel> model);
        Task<int> SaveExcelData_CounsellingVacant(List<ImportCounsellingVacancyDataModel> request);
        Task<DataTable> GetCounsellingVacancyData(CounsellingVacancySearchModel body);
        Task<int> EditVacancyData_Counselling(EditVacancyDataModel request);
        Task<EditVacancyDataModel> GetVacancyDetailsById_Counselling(int TradeInstituteID);
        Task<DataTable> GetAllottedCandidateList_CounsellingReport(CounsellingAllottedListSearchModel body);
        Task<DataTable> Counselling_GetCandidateDetailsRPT(CounsellingReportListSearchModel filterModel);
        Task<DataSet> GenerateCounsellingAppointmentOrder(CounsellingAppointmentOrder model);
    }
}
