using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StudentMaster;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIExaminationRepository
    {

        Task<DataTable> GetPreExamStudent(PreExamStudentModel model);
        Task<DataTable> GetPreEnrollStudent(PreExamStudentModel model);
        Task<DataTable> GetAnnextureListPreExamStudent(PreExamStudentModel model);
        Task<bool> EditStudentData_PreExam(StudentMasterModel productDetails);
        Task<bool> PreExam_UpdateEnrollmentNo(PreExam_UpdateEnrollmentNoModel productDetails);
        Task<int> Save_Student_Exam_Status(List<Student_DataModel> productDetails);
        Task<bool> PreExamStudentSubject(PreExamStudentSubjectRequestModel productDetails);
        //Task<List<Student_DataModel>> Save_Student_Exam_Status_Update();
        Task<int> Save_Student_Exam_Status_Update(List<Student_DataModel> productDetails);
        Task<int> SaveAdmittedStudentData(List<StudentMarkedModel> model);
        Task<PreExamSubjectModel> GetStudentSubject_ByID(int PK_ID, int DepartmentID);
        Task<ITIExamination_UpdateEnrollmentNoModel> GetStudentDropoutStudent(int PK_ID, int StudentExamID);
        Task<int> SaveEligibleForEnrollment(List<StudentMarkedModel> model);
        Task<int> SaveSelectedForExamination(List<StudentMarkedModel> model);
        Task<int> SaveEligibleForExamination(List<StudentMarkedModel> model);
        Task<int> SaveRejectAtBTER(List<StudentMarkedModel> model);
        Task<int> UpdateStudentEligibility(StudentAttendenceModel model);
        Task<int> UpdateDropout(ITIExamination_UpdateEnrollmentNoModel model);
        Task<int> ReturnToAdmitted(int StudentID);
        Task<bool> RevertStatus(RevertDataModel productDetails);



    }
}
