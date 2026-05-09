using Kaushal_Darpan.Models.TheoryMarks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ITheoryMarksRevalRepository
    {
        Task<DataTable> GetTheoryMarksDetailList_Reval(TheorySearchModel filterModel);
        Task<DataTable> GetTheoryMarksRptData_Reval(TheorySearchModel filterModel);
        Task<DataTable> GetTheoryMarks_Admin(TheorySearchModel body);
        Task<int> UpdateSaveData_Reval(List<TheoryMarksModel> productDetails);
        Task<int> UpdateTheoryMarks_Admin(List<TheoryMarksModel> entity);
    }
}
