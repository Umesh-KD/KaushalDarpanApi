using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.ITICenterObserver;
using Kaushal_Darpan.Models.ITITimeTable;
using Kaushal_Darpan.Models.TimeTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITICenterObserverRepository
    {
        Task<int> SaveData(ITICenterObserverDataModel productDetails);
        Task<DataTable> GetAllData(ITICenterObserverSearchModel body);
        Task<DataTable> GetAllInspectedData(ITICenterObserverSearchModel body);
        Task<DataTable> GetAllDataForVerify(ITICenterObserverSearchModel body);
        Task<DataTable> GetAllDataForGenerateOrder(ITICenterObserverSearchModel body);
        Task<DataTable> GetObserverDataByID_Status(ITICenterObserverSearchModel body);
        Task<DataTable> GetDeploymentDetailsByID(ITICenterObserverSearchModel body);
        Task<DataTable> GetAllTimeTableData(ITITimeTableSearchModel body);
    
        Task<ITICenterObserverDataModel> GetById(int ID, int DepartmentID);
        Task<bool> DeleteDataByID(ITICenterObserverDataModel model);
        Task<bool> DeleteDeploymentDataByID(ITICenterObserverDataModel model);
        Task<SaveDeploymentResult> SaveDeploymentData(List<ITIDeploymentDataModel> productDetails);
        Task<int> SaveDeploymentVerifiedData(List<ITICODeploymentDataModel> productDetails);
        //Task<int> GenerateCenterObserverDutyOrder(List<GenerateDutyOrder> productDetails);
        Task<DataSet> GenerateCenterObserverDutyOrder(List<ITICODeploymentDataModel> model);
        Task<int> UpdateDutyOrder(List<ITICODeploymentDataModel> model);
        Task<DataSet> GenerateCOAnsweredReport(ITICenterObserverSearchModel model);

        Task<DataTable> GetCenter_DistrictWise(CenterMasterDDLDataModel body);
        //Task<DataTable> GetTimeTableDates(CenterObserverSearchModel body);
        Task<DataTable> GetTimeTableDates(ITICenterObserverSearchModel body);
        Task<DataTable> GetExamShift(int DepartmentID);
        Task<int> ForwardToVerify(List<CODeploymentDataModel> productDetails);
        Task<DataTable> GetCOAttendanceData(int DeploymentID, int TypeID);

        Task<bool> UpdateAttendance(UpdateAttendance model);
        Task<CenterObserverDataModel> GetById_Attendance(int ID, int DepartmentID);
        Task<int> IsRequestCenterObserver(PostIsRequestCenterObserver model);
        Task<int> IsRequestHistoryCenterObserver(PostIsRequestCenterObserver model);
        Task<int> ApproveRequest(int DeployedID);

    }
}
