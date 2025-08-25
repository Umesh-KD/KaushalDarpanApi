using Kaushal_Darpan.Models.FlyingSquad;
using Kaushal_Darpan.Models.ITIFlyingSquad;
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
    public interface I_ITIFlyingSquadManageRepository
    {
        Task<int> SaveData(ITIFlyingSquadDataModel productDetails);
        Task<DataTable> GetAllData(ITIFlyingSquadSearchModel body);
        Task<DataTable> GetAllInspectedData(ITIFlyingSquadSearchModel body);
        Task<DataTable> GetAllDataForVerify(ITIFlyingSquadSearchModel body);
        Task<DataTable> GetAllDataForGenerateOrder(ITIFlyingSquadSearchModel body);
        Task<DataTable> GetObserverDataByID_Status(ITIFlyingSquadSearchModel body);
        Task<DataTable> GetDeploymentDetailsByID(ITIFlyingSquadSearchModel body);
        Task<DataTable> GetAllTimeTableData(ITITimeTableSearchModel body);
    
        Task<ITIFlyingSquadDataModel> GetById(int ID, int DepartmentID);
        Task<bool> DeleteDataByID(ITIFlyingSquadDataModel model);
        Task<bool> DeleteDeploymentDataByID(ITIFlyingSquadDataModel model);
        Task<int> SaveDeploymentData(List<ITIFlyingDeploymentDataModel> productDetails);
        Task<int> SaveDeploymentVerifiedData(List<ITICOFlyinDeploymentDataModel> productDetails);
        //Task<int> GenerateFlyingSquadDutyOrder(List<GenerateDutyOrder> productDetails);
        Task<DataSet> GenerateFlyingSquadDutyOrder(List<ITICOFlyinDeploymentDataModel> model);
        Task<int> UpdateDutyOrder(List<ITICOFlyinDeploymentDataModel> model);
        Task<DataSet> GenerateCOAnsweredReport(ITIFlyingSquadSearchModel model);

        Task<DataTable> GetCenter_DistrictWise(CenterMasterDDLDataModel body);
        //Task<DataTable> GetTimeTableDates(FlyingSquadSearchModel body);
        Task<DataTable> GetTimeTableDates(ITIFlyingSquadSearchModel body);
        Task<DataTable> GetExamShift(int DepartmentID);
        //Task<int> ForwardToVerify(List<CODeploymentDataModel> productDetails);

        Task<int> IsRequestFlyingSquad(PostIsRequestFlyingSquadModal model);
        Task<int> IsRequestHistoryFlyingSquad(PostIsRequestFlyingSquadModal model);

        Task<int> ApproveRequest(int DeplomentId);

        Task<bool> UpdateAttendance(UpdateAttendance model);
    }
}
