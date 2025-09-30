using Kaushal_Darpan.Models.CollegeWiseScholarship;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITICollegeWiseScholarship;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITICollegeWiseScholarshipRepository
    { 
        Task<DataTable> GetCollegeWiseScholarshipList(ITICollegeWiseScholarshipSearchModel filterModel);
        Task<DataTable> GetSchemeList();
        Task<DataTable> GetTypeList(); 
        Task<bool> SaveCollegeWiseScholarshipDetails(List<SaveITICollegeWiseScholershipDetails> model); 
        Task<DataTable> GetDetailList(int id); 
    }
}
