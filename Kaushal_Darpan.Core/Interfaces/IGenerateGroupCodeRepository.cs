using Kaushal_Darpan.Models.GenerateAdmitCard;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.GenerateGroupCode;
using Kaushal_Darpan.Models.PreExamStudent;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IGenerateGroupCodeRepository
    {
        Task<DataTable> GetGenerateGroupCodeList(GenerateGroupCodeSearchModel model);
        Task<GenerateGroupCodeModel> GetGenerateGroupCodeData(GenerateGroupCodeModel model);
        Task<int> UpdateGroupCode(GenerateGroupCodeSearchModel model);
    }
}
