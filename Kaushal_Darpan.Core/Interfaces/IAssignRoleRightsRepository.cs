using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Models.AssignRoleRight;
using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IAssignRoleRightsRepository
    {
        //Task<int> SaveData(AssignRoleRightsModel entity);
        //Task<bool> SaveDataChild(List<AssignRoleRightsDetailModel> entity);

        Task<bool> SaveData(List<AssignRoleRightsModel> productDetails);
        Task<List<AssignRoleRightsModel>> GetAssignedRoleById(int UserID);
    }
}
