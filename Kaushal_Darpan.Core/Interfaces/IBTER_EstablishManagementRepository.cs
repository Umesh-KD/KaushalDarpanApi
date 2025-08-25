using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.StaffDashboard;
using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBTER_EstablishManagementRepository
    {
        Task<int> BTER_EM_AddStaffInitialDetails(BTER_EM_AddStaffInitialDetailsDataModel model);
        Task<DataTable> BTER_EM_GetStaffList(BTER_EM_GetStaffListDataModel body);
        Task<int> BTER_EM_AddStaffPrinciple(BTER_EM_AddStaffPrincipleDataModel request);
        Task<DataTable> BTER_EM_GetPrincipleStaff(BTER_EM_StaffMasterSearchModel filterModel);
        Task<DataTable> BTER_EM_GetPersonalDetailByUserID(BTER_EM_GetPersonalDetailByUserID body);
        Task<int> BTER_EM_AddStaffDetails(BTER_EM_AddStaffDetailsDataModel request);
        Task<int> BTER_EM_DeleteStaff(ITIGovtEM_OfficeDeleteModel body);
        Task<int> BTER_EM_ApproveStaffProfile(BTER_EM_ApproveStaffDataModel request);
        Task<bool> BTER_EM_UnlockProfile(BTER_EM_UnlockProfileDataModel productDetails);
        Task<DataTable> BTER_EM_InstituteDDL(int DepartmentID, int InsType, int DistrictId);

        Task<bool> FinalSubmitUpdateStaffProfileStatus(RequestUpdateStatus productDetails);

        Task<int> BTERGovtEM_Govt_StaffProfileStaffPosting(List<StaffPostingData> model);

        Task<BTERPersonalDetailByUserIDSearchModel> BTERGovtEM_BTER_Govt_Em_PersonalDetailByUserID(BTERPersonalDetailByUserIDSearchModel body);
        Task<BTERPersonalDetailByUserIDSearchModel> BTERGovtEM_BTER_Govt_Em_PersonalDetailList(BTERPersonalDetailByUserIDSearchModel body);

        Task<DataTable> GetBTER_Govt_EM_GetUserProfileStatus(int ID);
        Task<int> BTER_Govt_EM_ServiceDetailsOfPersonnelDeleteByID(BTERGovtEM_DeleteByIdStaffServiceDelete body);

        Task<DataTable> GetBter_Govt_EM_UserProfileStatusHt(Bter_Govt_EM_UserRequestHistoryListSearchDataModel Model);

        Task<bool> Bter_GOVT_EM_ApproveRejectStaff(RequestUpdateStatus productDetails);

        Task<BTER_EM_AddStaffDetailsDataModel> BTER_EM_GetBterStaffSubjectListModelStaffID(int PK_ID, int DepartmentID);


        //-----HOD_dashboard---
        Task<DataTable> GetHODDash(HODDashboardSearchModel model);



    }
}
