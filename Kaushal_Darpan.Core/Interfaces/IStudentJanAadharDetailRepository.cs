using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.StudentJanAadharDetail;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStudentJanAadharDetailRepository
    {
        Task<int> SaveData(ApplicationStudentDatamodel productDetails);
        Task<int> SaveDTEApplicationData(ApplicationDTEStudentDatamodel productDetails);
        Task<DataTable> GetApplicationId(SearchApplicationStudentDatamodel filterModel);
        Task<int> SaveDTEDirectApplicationData(ApplicationDTEStudentDatamodel productDetails);
    }
}
