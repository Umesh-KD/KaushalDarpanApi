using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.GuestRoomManagementModel;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IGuestRoomManagementRepository
    {

        Task<int> SaveData(GuestRoomManagementDataModel guestRoomManagement);
        Task<DataTable> GetAllGuestRoomList(GuestRoomManagementSearchModel filterModel);


        Task<GuestRoomManagementDataModel> GetByGuestHouseID(int PK_ID);

        Task<bool> DeleteDataByID(StatusChangeGuestModel request);

        // Task<DataTable> GetStudentDetailsForHostelApply(HostelStudentSearchModel filterModel);


        Task<DataTable> GetGuestHouseNameList(GuestRoomSeatSearchModel filterModel);




        Task<DataTable> GetAllRoomSeatList(GuestRoomSeatSearchModel filterModel);

        Task<GuestRoomSeatDataModel> GetByGRSMasterID(int PK_ID);

        Task<bool> DeleteDataByGRSMasterID(StatusChangeGuestModel request);

        // Task<int> StudentApplyHostel(StudentApplyHostelData hostelManagement);


        Task<int> SaveFacilities(GuestRoomFacilitiesDataModel guestRoomManagement);
        Task<DataTable> GuestRoomFacilityList(GuestRoomFacilitiesSearchModel filterModel);
        Task<DataTable> GetByGFID(int PK_ID);
        Task<bool> DeleteDataByGFID(StatusChangeGuestModel request);

        //Task<DataSet> GuestRoomDetailsList(GuestRoomDetailsSearchModel filterModel);

        Task<bool> IsFacilitiesStatusByID(int PK_ID, int ModifyBy);

        // Task<DataSet> GetLastFYEndTerm(HostelStudentSearchModel request);
        // Task<DataTable> GetAllotedHostelDetails(HostelStudentSearchModel filterModel);


        Task<int> GuestRoomSeatMasterSaveData(GuestRoomSeatDataModel guestRoomSeatData);



        Task<DataTable> GetAllGuestApplyForGuestRoomList(GuestApplyForGuestRoomSearchModel filterModel);
        Task<int> GuestApplyForGuestRoomSaveData(GuestApplyForGuestRoomDataModel guestRoomManagement);
        Task<GuestApplyForGuestRoomDataModel> GetByGuestApplyForGuestRoomByID(int PK_ID);
        Task<bool> DeleteGuestApplyForGuestRoomDataByID(GuestApplyForGuestRoomSearchModel productDetails);


        Task<DataTable> GuestRequestList(GuestApplyForGuestRoomSearchModel filterModel);
        

        Task<int> updateReqStatusGuestRoom(GuestApplyForGuestRoomDataModel guestRoomManagement);
        Task<int> updateReqStatusCheckInOut(GuestApplyForGuestRoomDataModel guestRoomManagement);

        Task<DataTable> GuestRequestReportList(GuestApplyForGuestRoomSearchModel filterModel);

        Task<DataTable> GuestStaffProfile(GuestStaffProfileSearchModel body);

        Task<int> SaveGuestRoomDetails(RoomDetailsDataModel request);
        Task<DataTable> GetAllGuestRoomDetails(RoomDetailsDataModel request);
        Task<RoomDetailsDataModel> GetByIDGuestRoomDetails(int id);
        Task<bool> DeleteGuestRoomDetails(StatusChangeGuestModel request);
    }
}
