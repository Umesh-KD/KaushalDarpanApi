using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.TheoryMarks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ITheoryMarksRepository
    {
        Task<DataTable> GetTheoryMarksDetailList(TheorySearchModel filterModel);
        Task<DataTable> GetTheoryMarksRptData(TheorySearchModel filterModel);
        Task<int> UpdateSaveData(List<TheoryMarksModel> productDetails);
        Task<int> FeedbackSubmit(ExaminerFeedbackDataModel entity);
        Task<DataTable> GetTheoryMarks_Admin(TheorySearchModel filterModel);
        Task<int> UpdateTheoryMarks_Admin(List<TheoryMarksModel> entity);
    }
}
