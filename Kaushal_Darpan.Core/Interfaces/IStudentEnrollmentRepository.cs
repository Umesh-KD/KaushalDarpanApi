using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StudentMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStudentEnrollmentRepository
    {
        Task<bool> EditStudentData_PreExam(StudentMasterModel request);
        Task<DataTable> GetAnnextureListPreExamStudent(PreExamStudentModel model);
        Task<DataTable> GetPreExamStudent(PreExamStudentModel model);
        Task<DataTable> GetStudentAdmitted(PreExamStudentModel model);
        Task<bool> PreExam_UpdateEnrollmentNo(PreExam_UpdateEnrollmentNoModel request);
        Task<int> SaveAdmittedFinalStudentData(List<StudentMarkedModelForJoined> model);
        Task<int> SaveAdmittedStudentData(List<StudentMarkedModel> model);
        Task<int> SaveDropout(List<StudentMarkedModel> model);
        Task<int> SaveRevokeDropout(List<StudentMarkedModel> model);
        Task<int> SaveEligibleForEnrollment(List<StudentMarkedModel> model);
        Task<int> SaveEligibleForExamination(List<StudentMarkedModel> model);
        Task<int> SaveRejectAtBTER(List<StudentMarkedModel> model);
        Task<int> SaveSelectedForExamination(List<StudentMarkedModel> model);
        Task<int> UndoRejectAtbter(List<RejectMarkModel> model);
    }
}
