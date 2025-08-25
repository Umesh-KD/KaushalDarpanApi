using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ITIMaster;
using Kaushal_Darpan.Models.StudentDataVerification;
using Kaushal_Darpan.Models.studentve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IItiDocumentScrutinyRepository
    {
        Task<int> Save_Documentscrutiny(ItiDocumentScrutinyDataModel productDetails);
        Task<ItiDocumentScrutinyDataModel> DocumentScrunityData(BterSearchModel searchRequest);
    }
}
