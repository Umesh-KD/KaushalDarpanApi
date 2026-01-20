using Kaushal_Darpan.Models.BhandarFormDataModel;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IBhandarFormMasterRepository
    {
        Task<AddBhandarFormDataModel> GetExamStudentData(AddBhandarFormDataModel filterModel);
        Task<bool> SaveData(AddBhandarFormDataModel productDetails);
    }
}
