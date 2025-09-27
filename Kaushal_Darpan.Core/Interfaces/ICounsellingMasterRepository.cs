using Kaushal_Darpan.Models.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICounsellingMasterRepository
    {
        Task<int> SaveData(ApplicationDataModel productDetails);
    }
}
