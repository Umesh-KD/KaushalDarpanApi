using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.TheoryMarks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IInternalPracticalStudentRepository
    {
        Task<DataTable> GetAllData(TheorySearchModel filterModel);
        Task<int> UpdateSaveData(List<TheoryMarksModel> productDetails, int InternalPracticalID);
        Task<DataTable> GetAllDataInternal_Admin(TheorySearchModel filterModel);
        Task<int> UpdateSaveDataInternal_Admin(List<TheoryMarksModel> productDetails, int InternalPracticalID);       

    }
}
