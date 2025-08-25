using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.ITI_Apprenticeship;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIApprenticeshipRepository
    {
        Task<DataTable> GetAllData(ITI_ApprenticeshipSearchModel body);
        Task<DataTable> GetAllInspectedData(ITI_ApprenticeshipSearchModel body);
        Task<DataTable> GetAllInspectedDataByID(ITI_ApprenticeshipSearchModel body);
        Task<DataTable> GetAllData_GenerateOrder(ITI_ApprenticeshipSearchModel body);
        Task<DataTable> GetITIApprenticeshipDropdown(ITI_ApprenticeshipDropdownModel body);
        Task<int> SaveData(ITI_ApprenticeshipDataModel productDetails);
        Task<int> SaveCheckSSODataModel(Apprenticeship_SaveCheckSSODataModel SSODetails);
        Task<Boolean> check_Engagement(ApprenticeshipMemberDetailsDataModel model);
        Task<int> SaveApprenticeshipDeploymentData(List<ApprenticeshipDeploymentDataModel> request);
        Task<ITI_ApprenticeshipDataModel> GetById_Team(int ID);
        Task<DataTable> GetById_Deployment(int ID);
        Task<int> UpdateDeployment(int ID);
        Task<DataTable> GetApprenticeshipDataByID_Status(ITI_ApprenticeshipSearchModel body);
        Task<DataSet> GenerateApprenticeshipDeploymentOrder(int id);
        Task<DataSet> GenerateApprenticeshipDutyOrder(List<CODeploymentDataModel> model);
        Task<DataTable> GetITIApprenticeshipDashData(ApprenticeshipDeploymentDataModel model);
        Task<DataTable> GetITIApprenticeshipMemeberTeamList(ApprenticeshipDeploymentDataModel model);
        Task<DataTable> GetITIApprenticeshipIndustryList(ApprenticeshipDeploymentDataModel model);
        Task<DataSet> GetITIApprenticeshipQuestionData(ApprenticeshipDeploymentDataModel model);
        //Task<DataSet> GetITIApprenticeshipQuestionData_Completed(ApprenticeshipDeploymentDataModel model);
        Task<int> SaveApprenticeshipAnswersByIndustry(ITI_ApprenticeshipAnswerModel request);
        Task<DataSet> GenerateCOAnsweredReport( int DeploymentID);
        Task<int> UpdateReport(string filename, int DeploymentID);
        Task<int> UpdateDutyOrder(int id);
        Task<bool> UpdateAttendance(UpdateAttendance model);
        Task<int> IsRequestApprenticeship(PostIsRequestCenterObserver model);
        //Task<int> IsRequestHistoryCenterObserver(PostIsRequestCenterObserver model);
        Task<int> ApproveRequest(int DeployedID);
    }
}
