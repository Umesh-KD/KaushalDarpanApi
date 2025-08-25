using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.studentve;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStudentVerificationRepository
    {
        Task<DataTable> GetAllStudentData(StudentVerificationSearchModel filterModel);
        Task<StudentVerificationDocumentsDataModel> GetById(int ID);
        Task<int> Save_Documentscrutiny(DocumentScrutinyModel productDetails);
        Task<int> Reject_Document(RejectModel productDetails);

        Task<DocumentScrutinyModel> DocumentScrunityData(BterSearchModel searchRequest);
        Task<int> NotifyStudent(NotifyStudentModel model);

    }
}
