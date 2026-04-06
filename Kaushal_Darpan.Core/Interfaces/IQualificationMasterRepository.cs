using Kaushal_Darpan.Models.BTER_EstablishManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IQualificationMasterRepository
    {
        Task<DataTable> QualificationMaster_GetData(QualificationMasterSearchModel body);
        Task<int> Save_QualificationMasterData(QualificationMasterDataModel body);
        Task<bool> Qualification_DeleteById(QualificationMasterSearchModel request);
    }
}
