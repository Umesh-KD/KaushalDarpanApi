using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.GenerateAdmitCard;
using Kaushal_Darpan.Models.ITICenterObserver;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.TimeTable;
using Kaushal_Darpan.Models.UserMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICenterObserverRepository
    {
        Task<int> SaveData(CenterObserverDataModel productDetails);
        Task<DataTable> GetAllData(CenterObserverSearchModel body);
        Task<DataTable> GetAllDataForVerify(CenterObserverSearchModel body);
        Task<DataTable> GetAllDataForGenerateOrder(CenterObserverSearchModel body);
        Task<DataTable> GetObserverDataByID_Status(CenterObserverSearchModel body);
        Task<DataTable> GetObserverDataByID_Status_ForWeb(CenterObserverSearchModel body);
        Task<DataTable> GetDeploymentDetailsByID(CenterObserverSearchModel body);
        Task<DataTable> GetAllTimeTableData(TimeTableSearchModel body);
        Task<DataTable> GetTimeTableDates(CenterObserverSearchModel body);
        
        Task<CenterObserverDataModel> GetById(int ID, int DepartmentID);
        Task<CenterObserverDataModel> GetById_Attendance(int ID, int DepartmentID);
        Task<bool> DeleteDataByID(CenterObserverDataModel model);
        Task<bool> DeleteDeploymentDataByID(CenterObserverDataModel model);
        Task<bool> UpdateAttendance(UpdateAttendance model);
        Task<SaveDeploymentResult> SaveDeploymentData(List<DeploymentDataModel> productDetails);
        Task<int> SaveDeploymentVerifiedData(List<CODeploymentDataModel> productDetails);
        Task<int> ForwardToVerify(List<CODeploymentDataModel> productDetails);
        Task<DataSet> GenerateCenterObserverDutyOrder(List<CODeploymentDataModel> model);
        Task<int> UpdateDutyOrder(List<CODeploymentDataModel> model);
        Task<DataSet> GenerateCOAnsweredReport(CenterObserverSearchModel model);
        Task<DataTable> GetCOAttendanceData(int DeploymentID);
        Task<DataTable> GetTeamCount(CenterObserverSearchModel body);
        Task<int> IsRequestCenterObserver(PostIsRequestCenterObserver model);
        Task<int> IsRequestHistoryCenterObserver(PostIsRequestCenterObserver model);
    }
}
