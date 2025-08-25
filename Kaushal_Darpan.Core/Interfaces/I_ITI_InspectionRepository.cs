using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.ITI_Inspection;
using Kaushal_Darpan.Models.ITICenterObserver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITI_InspectionRepository
    {
        Task<DataTable> GetAllData(ITI_InspectionSearchModel body);
        Task<DataTable> GetAllInspectedData(ITI_InspectionSearchModel body);
        Task<DataTable> GetAllData_GenerateOrder(ITI_InspectionSearchModel body);
        Task<DataTable> GetITIInspectionDropdown(ITI_InspectionDropdownModel body);
        Task<int> SaveData(ITI_InspectionDataModel productDetails);
        Task<int> SaveCheckSSODataModel(SaveCheckSSODataModel SSODetails);
        Task<Boolean> check_Engagement(InspectionMemberDetailsDataModel model);
        Task<int> SaveInspectionDeploymentData(List<InspectionDeploymentDataModel> request);
        Task<ITI_InspectionDataModel> GetById_Team(int ID);
        Task<DataTable> GetById_Deployment(int ID);
        Task<int> UpdateDeployment(int ID);
        Task<DataTable> GetInspectionDataByID_Status(ITI_InspectionSearchModel body);
        Task<DataSet> GenerateInspectionDeploymentOrder(int id);
        Task<DataSet> GenerateInspectionDutyOrder(List<CODeploymentDataModel> model);
        Task<DataTable> GetITIInspectionDashData(InspectionDeploymentDataModel model);
        Task<DataTable> GetITIInspectionMemeberTeamList(InspectionDeploymentDataModel model);
        Task<DataTable> GetITIInspectionInstituteList(InspectionDeploymentDataModel model);
        Task<DataTable> GetITIInspectionQuestionData(InspectionDeploymentDataModel model);
        Task<int> SaveInspectionAnswersByInstitute(ITI_InspectionAnswerModel request);
        Task<DataSet> GenerateCOAnsweredReport(int id);
        Task<int> UpdateDutyOrder(CODeploymentDataModel model);

        Task<bool> UpdateAttendance(UpdateAttendance model);
        //Task<ITI_InspectionDataModel> GetById_Attendance(int ID, int DepartmentID);
        Task<int> IsRequestInspection(PostIsRequestCenterObserver model);
        //Task<int> IsRequestHistoryCenterObserver(PostIsRequestCenterObserver model);
        Task<int> ApproveRequest(int DeployedID);

    }
}
