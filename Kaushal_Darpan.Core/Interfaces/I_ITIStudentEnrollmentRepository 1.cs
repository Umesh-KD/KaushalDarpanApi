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
        //Task<bool> EditStudentData_PreExam(StudentMasterModel request);
        //Task<DataTable> GetAnnextureListPreExamStudent(PreExamStudentModel model);
        //Task<DataTable> GetPreExamStudent(PreExamStudentModel model);
        Task<DataTable> GetStudentAdmitted(PreExamStudentModel model);
        //Task<bool> PreExam_UpdateEnrollmentNo(PreExam_UpdateEnrollmentNoModel request);
        Task<int> SaveAdmittedFinalStudentData(List<StudentMarkedModelForJoined> model);
        Task<int> updateOnResponseData(List<ResponseData> model);
        //Task<int> SaveAdmittedStudentData(List<StudentMarkedModel> model);
        //Task<int> SaveDropout(List<StudentMarkedModel> model);
        //Task<int> SaveRevokeDropout(List<StudentMarkedModel> model);
        //Task<int> SaveEligibleForEnrollment(List<StudentMarkedModel> model);
        //Task<int> SaveEligibleForExamination(List<StudentMarkedModel> model);
        //Task<int> SaveRejectAtBTER(List<StudentMarkedModel> model);
        //Task<int> SaveSelectedForExamination(List<StudentMarkedModel> model);
        //Task<int> UndoRejectAtbter(List<RejectMarkModel> model);

        Task<List<ITITraineeUploadModel>> GetNCVTStudentData(int pageNumber, int pageSize);
    }
}
