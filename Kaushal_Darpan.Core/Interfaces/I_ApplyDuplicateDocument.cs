using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.CollegeWiseScholarship;
using Kaushal_Darpan.Models.CompanyMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ApplyDuplicateDocument
    {
        Task<DataTable> GetApplyDuplicateDocumentTypeList();
        Task<DataTable> GetApplyDuplicateDocumentList(ApplyDuplicateDocumentDataModel filterModel);
        Task<DataTable> GetDuplicateDocFeeAmount(ApplyDuplicateDocumentDataModel filtermodel);

        Task<DataTable> GetDuplicateDocInstituteWise(DuplicateDocumentSearchModel filterModel);
        Task<bool> SaveDuplicateDocumentDetails(ApplyDuplicateDocumentDataModel model);

        Task<bool> Save_DuplicateDocumentAction(DuplicateDoc_Action model);

        Task<DataTable> GetStudentDMarshkeetSession(int SemesterID, int StudentID, int DepartmentID = 0);
    }
}
