using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.HostelManagementModel;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IHostelManagementRepository
    {

        Task<int> SaveData(HostelManagementDataModel  hostelManagement);
        Task<DataTable> GetAllHostelList(HostelManagementSearchModel filterModel);
        

        Task<HostelManagementDataModel> GetByHostelId(int PK_ID);

        Task<bool> DeleteDataByID(StatusChangeModel request);

        Task<DataTable> GetStudentDetailsForHostelApply(HostelStudentSearchModel filterModel);


        Task<DataTable> GetHostelNameList(HostelRoomSeatSearchModel filterModel);

        Task<int> SaveRoomSeatData(HostelRoomSeatDataModel hostelManagement);
       

        Task<DataTable> GetAllRoomSeatList(HostelRoomSeatSearchModel filterModel);

        Task<HostelRoomSeatDataModel> GetByHRSMasterID(int PK_ID);

        Task<int> DeleteDataByHRSMasterID(StatusChangeModel request);

        Task<int> StudentApplyHostel(StudentApplyHostelData hostelManagement);
        Task<int> EditStudentApplyHostel(StudentApplyHostelData hostelManagement);
        Task<int> HostelWardenupdateData(StudentApplyHostelData hostelManagement);
        Task<int> SaveFacilities(HostelFacilitiesDataModel hostelManagement);
        Task<DataTable> HostelFacilityList(HostelFacilitiesSearchModel filterModel);
        Task<DataTable> GetByHFID(int PK_ID);
        Task<bool> DeleteDataByHFID(StatusChangeModel request);
        Task<DataSet> CollegeHostelDetailsList(CollegeHostelDetailsSearchModel filterModel);
        Task<bool> IsFacilitiesStatusByID(StatusChangeModel request);
        Task<DataSet> GetLastFYEndTerm(HostelStudentSearchModel request);
        Task<DataTable> GetAllotedHostelDetails(HostelStudentSearchModel filterModel);
        Task<DataTable> GetStudentDetailsForHostel_Principle(HostelStudentSearchModel body);
        Task<int> HostelInstituteMapping(HostelInstituteMappingModel hostelManagement);
        Task<DataTable> GetAllddlHostelList(HostelManagementSearchModel filterModel);
        Task<DataTable> GetAllHostelInstituteMappingList(HostelInstituteMappingModel filterModel);
        Task<int> UnmapHostelInstitute(UnmapHostelDataModel model);
        Task<DataTable> GetHostelInstituteMappingByID(int PK_ID);
        Task<int> HostelInstituteMappingSaveData(List<InstituteMappingListModel> productDetails);
        Task<bool> SaveHostelFee(HostelFeeModel request);
        Task<DataTable> getHostelFeeList();
        Task<DataTable> getHostelFeeByID(int id);
        Task<DataTable> GetRoomAllotmentCancelHistory(int reqId);


    }
}
