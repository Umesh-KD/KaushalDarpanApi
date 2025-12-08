using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Student;
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
        Task<DataTable> GetAdmittedStudentToVerify(StudentApplicationModel model);
        Task<int> SaveAdmittedStudentForApproveByAcp(List<StudentApplicationSaveModel> model);
        Task<int> SaveAdmittedStudentForReturnByAcp(List<StudentApplicationSaveModel> model);
        Task<int> SaveEnrolledStudentVerify_VerifyandForwardtoExamIncharge(List<EnrolledPromotedStudentSaveModel> model);
        Task<int> SaveEnrolledStudentVerify_VerifyandForwardtoRegistrar(List<EnrolledPromotedStudentSaveModel> model);
        Task<int> SaveEnrolledStudentVerify_ApprovebyRegistrar(List<EnrolledPromotedStudentSaveModel> model);
        Task<int> SaveEnrolledStudentVerify_ReturnbyRegistrar(List<EnrolledPromotedStudentSaveModel> model);
        Task<int> SaveEnrolledStudentVerify_SelectedforExamination(List<EnrolledPromotedStudentSaveModel> model);
        Task<DataTable> GetEnrolledStudent_Promoted(EnrolledPromotedStudentModel model);
        Task<DataTable> GetEnrolledStudent_VerifyandForwardtoExamIncharge(EnrolledPromotedStudentModel model);
        Task<DataTable> GetEnrolledStudent_VerifyandForwardtoRegistrar(EnrolledPromotedStudentModel model);
        Task<DataTable> GetEnrolledStudent_ApprovebyRegistrar(EnrolledPromotedStudentModel model);
        Task<DataTable> GetEnrolledStudent_ReturnbyRegistrar(EnrolledPromotedStudentModel model);
        Task<int> SaveEnrolledStudentVerify_ReturnbyExamIncharge(List<EnrolledPromotedStudentSaveModel> model);
        Task<DataTable> GetEnrolledStudent_ReturnbyExamIncharge(EnrolledPromotedStudentModel model);
        Task<DataTable> GetPreExamStudentReport(PreExamStudentModel model);
        Task<DataTable> GetRejectAtBter_StudentDetails_Enrollment(RejectAtBterStudentDataModel model);
    }
}
