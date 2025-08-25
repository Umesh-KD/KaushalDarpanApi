using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.ITITheoryMarks;
using Kaushal_Darpan.Models.TheoryMarks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIPracticalAssesmentRepository
    {
        Task<DataTable> GetAllData(ITITheorySearchModel filterModel);
        Task<int> UpdateSaveData(List<ITITheoryMarksModel> productDetails, int InternalPracticalID);
    }

}
