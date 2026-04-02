using Kaushal_Darpan.Models.BTER_EstablishManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBTER_EM_StaffServiceDetailsRepository
    {
        Task<int> Save_StaffTrainingDetails(StaffTrainingDetailDataModel body);
        Task<DataTable> StaffTrainingDetails_GetData(StaffTrainingDetailSearchData body);
    }
}
