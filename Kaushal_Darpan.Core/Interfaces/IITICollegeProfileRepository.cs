using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.CollegeMaster;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface IITICollegeProfileRepository
    {
        Task<ITICollegeProfileDataModel> GetById(int PK_ID);
        Task<bool> SaveData(ITICollegeProfileDataModel productDetails);
    }
}
