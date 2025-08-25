using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.ApplicationMessageModel;
using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.studentve;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface ICorrectMeritRepository
    {

        Task<DataTable> CorrectMeritList(CorrectMeritSearchModel filterModel);
        Task<DataTable> GetApplicationIDByMeritID(CorrectMerit_ApplicationSearchModel filterModel);
        Task<MeritDocumentScrutinyModel> MeritDocumentScrunityData(BterSearchModel searchRequest);
        Task<int> Save_MeritDocumentscrutiny(MeritDocumentScrutinyModel productDetails);
        Task<int> Reject_Document(RejectModel productDetails);
        Task<bool> ApproveMerit(CorrectMeritApproveDataModel productDetails);
        Task<DataTable> GetApplicationDetails_ByMeritId(int MeritId);
    }
}
