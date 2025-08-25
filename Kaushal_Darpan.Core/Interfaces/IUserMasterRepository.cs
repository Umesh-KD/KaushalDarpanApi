using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.UserMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IUserMasterRepository
    {
        Task<DataTable> GetAllData(StudentSearchModel body);
        Task<UserMasterModel> GetById(int PK_ID);
        Task<bool> SaveData(UserMasterModel productDetails);
        Task<bool> UpdateData(UserMasterModel productDetails);
        Task<bool> DeleteDataByID(UserMasterModel productDetails);

        Task<UserMasterModel> GetUserMobileNoForOTP(int RoleID,int DepartmentID);

        Task<DataTable> GetPrincipleList(StudentSearchModel body);


        Task<bool> UpdatePrincipleData(PrincipleUpdateInstituteIDModel principle);

    }
}
