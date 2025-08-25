using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.ExamShiftMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IExamShiftMasterRepository
    {
        Task<DataTable> GetAllData(ExamShiftSearchModel filterModel);
        //Task<int> SaveData(List<CenterCreationAddEditModel> productDetails, int StartValue);
        //Task<CenterCreationAddEditModel> GetById(CenterCreationSearchModel request);
    }
}
