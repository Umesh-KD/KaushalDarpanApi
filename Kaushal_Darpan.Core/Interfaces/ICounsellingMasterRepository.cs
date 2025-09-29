using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CounsellingMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICounsellingMasterRepository
    {
        Task<int> SaveData(ApplicationDataModel productDetails);
        Task<DataTable> MapCandidateSSO(CounsellingApplicationSearchModel filterModel);


    }
}
