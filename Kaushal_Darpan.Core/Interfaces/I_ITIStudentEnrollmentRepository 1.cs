using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;
using ResponseData = Kaushal_Darpan.Models.Student.ResponseData;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITIStudentEnrollmentRepository
    {
        Task<DataTable> GetStudentAdmitted(PreExamStudentModel model);
        Task<int> SaveAdmittedFinalStudentData(List<StudentMarkedModelForJoined> model);
        Task<int> updateOnResponseData(List<ResponseData> model);
        Task<List<ITITraineeUploadModel>> GetNCVTStudentData(int pageNumber, int pageSize);
        Task<DataTable> GetNcvtStudentData_Chunks(ChunksSearchModel model);
    }


}
