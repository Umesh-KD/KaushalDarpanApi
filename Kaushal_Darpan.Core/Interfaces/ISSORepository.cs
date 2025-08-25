using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.RoleMaster;
using Kaushal_Darpan.Models.SSOUserDetails;
using Kaushal_Darpan.Models.UserMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ISSORepository
    {
        Task<SSOUserDetailsModel> GetSSOUserDetails(string SearchRecordID);
        Task<SSOUserDetailsModel> Login(string SSOID, string Password);
        Task<SSOUserDetailsModel> StudentLogin(string SSOID);
        Task<SSOUserDetailsModel> MobileLogin(string SSOID, int CourseType);
        Task<bool> SaveData(UserRequestModel productDetails);
        Task<DataTable> GetAcedmicYearList(int RoleID=0, int DepartmentID=0 , int SessionTypeID =0);
        Task<string> AddSSOUserProfileDetails(SSO_UserProfileDetailModel model);
        Task<DataTable> GetUserRequestList(UserSearchModel model);
        Task<int> UpdateStudentUserType(UpdateStudentDetailsModel request);
        Task<ITICollegeSSoMAP> ItiCollegeMap(string CollegeCode, string Password);
        Task<BTERCollegeSSoMAP> BTERCollegeMap(string CollegeCode, string Password);
        Task<int> CreateCollegePrincipal(CreatePrincipalModel Model);
        Task<int> CreateBTERCollegePrincipal(CreatePrincipalModel Model);
        Task<DataTable> GetUserRoleList(RoleListRequestModel request);

        Task<DataTable> GetAcedmicYearListbyTypeID(RequestBaseModel model);
    }
}
