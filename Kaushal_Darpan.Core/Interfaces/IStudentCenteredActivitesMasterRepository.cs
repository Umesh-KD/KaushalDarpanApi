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
    public interface IStudentCenteredActivitesMasterRepository
    {
        Task<DataTable> GetAllData(StudentCenteredActivitesMasterSearchModel filterModel);
        Task<int> UpdateSaveData(List<StudentCenteredActivitesMasterModel> productDetails);
        Task<int> UpdateSaveDataSCA_Admin(List<StudentCenteredActivitesMasterModel> productDetails);
        Task<DataTable> GetAllDataSCA_Admin(StudentCenteredActivitesMasterSearchModel filterModel);
    }
}
