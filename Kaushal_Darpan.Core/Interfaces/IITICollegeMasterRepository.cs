
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.ITIPlanning;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITICollegeMasterRepository
    {
        Task<DataTable> GetAllData(ITIsSearchModel model);
        Task<DataTable> GetPlanningList(int CollegeID ,int Status);
        Task<DataTable> ViewWorkflow(int CollegeID);
        Task<ITICollegeMasterModel> Get_ITIsData_ByID(int Id);
        Task<ITI_PlanningColleges> Get_ITIsPlanningData_ByID(int Id);
        Task<DataSet> Get_ITIsPlanningData_ByIDReport(int Id);
        Task<ItiReportDataModel> Get_ITIsReportData_ByID(int Id);
        Task<int> SaveData(ITICollegeMasterModel productDetails);
        Task<bool> SaveDataReport(ItiReportDataModel productDetails);
        Task<bool> SaveDataPlanning(ITI_PlanningColleges productDetails);
        Task<bool> SaveItiworkflow(ItiVerificationModel productDetails);
        Task<bool> UpdateActiveStatusByID(ITICollegeMasterModel productDetails);
        Task<bool> ResetSSOID(int id, int ModifyBy,string remarks,string ssoid);
        Task<bool> unlockfee(int id, int ModifyBy,string remarks);
      
        Task<bool> DeleteDataById(int id, int ModifyBy,string remark);
        Task<DataTable> GetItiTradeData_ByID(int Id);
        Task<DataSet> Get_ITIsPlanningDataByID(int Id);
        Task<DataTable> ItiSearchCollege(ItiSearchCollegeModel model);
        Task<DataTable> AllNCVTInstituteList(ITIsSearchModel model);
    }
}
