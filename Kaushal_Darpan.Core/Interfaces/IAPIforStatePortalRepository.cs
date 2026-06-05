using Kaushal_Darpan.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IAPIforStatePortalRepository
    {
        Task<DataTable> GetAPIforStatePortal(APIforStatePortalModel SearchReq);
    }
}
