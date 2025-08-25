using Kaushal_Darpan.Models.BridgeCourse;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.ItiExaminer;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IItiExaminerRepository
    {
        Task<DataTable> GetAllData(ItiExaminerSearchModel filterModel);
        Task<DataTable> GetStudentTheory(ITITeacherForExaminerSearchModel filterModel);
        Task<DataTable> GetITIExaminer(ItiExaminerSearchModel filterModel);
        Task<ITIExaminerModel> GetById(int PK_ID, int StaffSubjectID, int DepartmentID);
        Task<int> SaveData(ITIExaminerModel productDetails);
        Task<bool> DeleteDataByID(int StaffID, int ModifyBy);
        Task<DataTable> GetTeacherForExaminer(ITITeacherForExaminerSearchModel filterModel);
        Task<DataTable> GetTeacherForExaminerById(ITITeacherForExaminerSearchModel filterModel);
       // Task<int> SaveExaminerData(ITIExaminerMaster productDetails);
        Task<DataTable> GetItiExaminerDashboardTiles(ITI_ExaminerDashboardModel filterModel);
        Task<DataTable> GetItiAppointExaminerDetails(ITI_AppointExaminerDetailsModel filterModel);
        Task<DataTable> GetItiRemunerationExaminerDetails(ITI_AppointExaminerDetailsModel filterModel);
        Task<DataTable> GetItiRemunerationAdminDetails(ITI_AppointExaminerDetailsModel filterModel);

        Task<int> SaveStudent(List<ItiAssignStudentExaminer> model);
        Task<DataTable> DeleteAssignStudentByExaminerID(int examinerId);
        Task<int> SaveExaminerData(ITITheoryExaminerModel productDetails);

        Task<DataTable> ITIAssignedExaminerInstituteDetailbyID(int BundelID);
        Task<DataTable> Iti_RemunerationGenerateAndViewPdf(ITI_AppointExaminerDetailsModel filterModel);
        Task<int> SaveDataSubmitAndForwardToAdmin(ITI_AppointExaminerDetailsModel filterModel);
        Task<int> UpdateToApprove(ITI_AppointExaminerDetailsModel filterModel);
    }
}
