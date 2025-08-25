using System.Data;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.DispatchFormDataModel;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.UserMaster;
using Newtonsoft.Json;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IUsersRequestRepository
    {
        Task<DataTable> UserRequest(RequestSearchModel Model);

        Task<bool> UserRequestUpdateStatus(RequestUpdateStatus productDetails);
        Task<DataTable> UserRequestHistory(RequestUserRequestHistory Model);

        Task<int> StafffJoiningRequestUpdateAndPromotions(RequestUpdateStatus productDetails);



       // -----------------Bter Em User Reqest-----------
        Task<DataTable> BterEmUserRequest(BterRequestSearchModel Model);

        Task<bool> BterEmUserRequestUpdateStatus(BterRequestUpdateStatus productDetails);
        Task<DataTable> BterEmUserRequestHistory(BterRequestUserRequestHistory Model);

        Task<int> BterEmStafffJoiningRequestUpdateAndPromotions(BterRequestUpdateStatus productDetails);

        Task<DataSet> GetJoiningLetter(JoiningLetterSearchModel model);
        Task<DataSet> GetRelievingLetter(RelievingLetterSearchModel model);

        Task<DataTable> BterGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData(Bter_Govt_EM_ZonalOFFICERSSearchDataModel body);
        Task<DataTable> BterGovtEM_Govt_EstablishUserRequestReportRelievingAndJoing(BterStaffUserRequestReportSearchModel body);
        Task<DataTable> GetBter_GetStaffDetailsVRS(BTER_EM_UnlockProfileDataModel Model);

    }
}
