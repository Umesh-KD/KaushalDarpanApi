using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.ViewStudentDetailsModel;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPreExamStudentRepository
    {
        Task<DataTable> GetPreExamStudent(PreExamStudentModel model);
        Task<DataTable> GetEnrollmentCancelStudent(PreExamStudentModel model);
        Task<PreExam_UpdateEnrollmentNoModel> GetStudentupdateEnrollData(int StudentID, int statusId, int DepartmentID, int Eng_NonEng, int status, int EndtermID, int StudentExamID);
        Task<DataTable> GetStudentAdmitted(PreExamStudentModel model);
        Task<DataTable> GetAnnextureListPreExamStudent(PreExamStudentModel model);
        Task<bool> EditStudentData_PreExam(StudentMasterModel productDetails);
        Task<bool> PreExam_UpdateEnrollmentNo(PreExam_UpdateEnrollmentNoModel productDetails);
        Task<int> Save_Student_Exam_Status(List<Student_DataModel> productDetails);
        Task<bool> PreExamStudentSubject(PreExamStudentSubjectRequestModel productDetails);
        //Task<List<Student_DataModel>> Save_Student_Exam_Status_Update();
        Task<int> Save_Student_Exam_Status_Update(List<Student_DataModel> productDetails);
        Task<int> SaveAdmittedStudentData(List<StudentMarkedModel> model);
        Task<int> UndoRejectAtbter(List<RejectMarkModel> model);
        Task<int> SaveAdmittedFinalStudentData(List<StudentMarkedModelForJoined> model);
        Task<PreExamSubjectModel> GetStudentSubject_ByID(int PK_ID, int DepartmentID);
        Task<int> SaveEligibleForEnrollment(List<StudentMarkedModel> model);
        Task<int> SaveSelectedForExamination(List<StudentMarkedModel> model);
        Task<int> SaveEligibleForExamination(List<StudentMarkedModel> model);
        Task<int> SaveRejectAtBTER(List<StudentMarkedModel> model);
        Task<int> SaveDropout(List<StudentMarkedModel> model);
        Task<int> SaveRevokeDropout(List<StudentMarkedModel> model);
        Task<int> Save_Student_Optional_Subject(OptionalSubjectModel optionalSubject);
        Task<DataTable> GetStudentOptionalSubject_ByStudentID(int StudentID, int EndTermID);

        Task<DataTable> GetStudentEnrollmentApprovalReject(PreExamStudentModel model);

        Task<int> SaveRejectAtBTERApprovalReject(List<StudentMarkedModel> model);
        Task<int> SaveEnrolledStudentExam(List<StudentMarkedModel> model);
        Task<ViewStudentDetailsModel> ViewStudentDetails(ViewStudentDetailsRequestModel model);
        Task<StudentMasterModel> PreExam_StudentMaster(int StudentID, int statusId, int DepartmentID, int Eng_NonEng, int status, int EndtermID, int StudentExamID);

        Task<DataTable> GetRejectBTERExcelData(PreExamStudentModel model);
        Task<DataTable> GetMainAnnexure(AnnexureDataModel model);
        Task<DataTable> GetSpecialAnnexure(AnnexureDataModel model);
        Task<int> SaveDetained(List<StudentMarkedModel> model);
        Task<int> SaveRevokeDetained(List<StudentMarkedModel> model);


        Task<int> SaveITIEnrolledStudentExam(List<StudentMarkedModel> model);

        Task<DataTable> GetPreExamStudentForVerify(PreExamStudentModel model);
        Task<int> VerifyByExaminationIncharge(List<StudentMarkedModel> model);
        Task<int> VerifyStudent_Registrar(List<StudentMarkedModel> model);
    }
}
