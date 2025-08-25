using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.CheckListModel;
using Kaushal_Darpan.Models.studentve;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface ICheckListRepository
    {
        Task<DataTable> GetCheckListQuestion(CheckListSearchModel filterModel);


        Task<int> SaveChecklistAnswers(ChecklistAnswerRequest request);
        Task<int> SaveChecklistAnswers_ITI(ChecklistAnswerRequest request);

        //Task<int> Save_MeritDocumentscrutiny(MeritDocumentScrutinyModel productDetails);
        //Task<int> Reject_Document(RejectModel productDetails);
    }
}
