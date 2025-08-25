using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.UserMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIAdminUserRepository
    {
        Task<DataTable> GetAllData(ITIAdminUserSearchModel body);
        Task<ITIAdminUserDetailModel> GetById(int UserID, int UserAdditionID, int ProfileID);
        Task<int> SaveData(ITIAdminUserDetailModel productDetails);
        Task<bool> UpdateData(ITIAdminUserDetailModel productDetails);
        Task<bool> DeleteDataByID(ITIAdminUserDetailModel productDetails);
    }
}
