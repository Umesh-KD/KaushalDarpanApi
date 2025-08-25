using Kaushal_Darpan.Models.ApplicationStatus;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.studentve;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IApplicationStatusRepository
    {
        Task<DataTable> GetAllData(StudentSearchModel filterModel);
        Task<List<DocumentDetailsModel>> GetByID(int ApplicationID);
        Task<int> SaveRevertData(List<DocumentDetailsModel> productDetails);
    }
}
