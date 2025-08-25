using Kaushal_Darpan.Models.ITITheoryMarks;
using Kaushal_Darpan.Models.TheoryMarks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IItiTheoryMarksRepository
    {

        Task<DataTable> GetTheoryMarksDetailList(ITITheorySearchModel filterModel);
        Task<int> UpdateSaveData(List<ITITheoryMarksModel> productDetails);
        Task<DataTable> GetTheoryMarksRptData(TheorySearchModel filterModel);
        Task<DataTable> GetCenterStudents(CenterStudentSearchModel filterModel);
        Task<DataTable> GetNcvtPracticalstudent(CenterStudentSearchModel filterModel);
    }
}
