using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITICenterAllocaqtion;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ITIPracticalExaminer;
using Kaushal_Darpan.Models.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIPracticalExaminerRepository
    {
        Task<DataTable> GetPracticalExamCenter(ITIPracticalExaminerSearchFilter filterModel);
        Task<DataTable> GetPracticalExamCenter_Report(ITIPracticalExaminerSearchFilter filterModel);
        Task<DataTable> GetCenterPracticalexaminer(ITIPracticalExaminerSearchFilter filterModel);
        Task<DataTable> GetCenterPracticalexaminerReliving(ITIPracticalExaminerSearchFilter filterModel);
        Task<DataSet> DownloadItiPracticalExaminer(ITIPracticalExaminerSearchFilter filterModel);

        Task<DataTable> GetParcticalStudentCenterWise(ITIPracticalExaminerSearchFilter productDetails);
        Task<DataTable> ParcticalExaminerDashboard(ITIPracticalExaminerSearchFilter filterModel);
        Task<int> AssignPracticalExaminer(PracticalExaminerDetailsModel model);
        Task<DataTable> Getstaffpractical(ItiPracticalExaminerDDLDataModel body);
        Task<DataTable> GetUndertakingExaminerDetailsByIdAsync(int id);

        Task<DataTable> GetStudentExamReportAsync(ITIPracticalExaminerSearchFilter filterModel, string subjectCode);
        Task<DataTable> GetStudentExamReportGetStudentExamReportForITIAsync(ITIExaminerDataModel filterModel);
        Task<DataTable> GetAssignedCentersAndTimetableAsync(PracticalExaminerDetailsModel model);
        Task<int> UpdateStudentExamMarks(StudentExamMarksUpdateModel model);

        Task<int> UpdateStudentExamMarksData(List<StudentExamMarksUpdateModel> entityList);
        Task<int> NcvtUpdateStudentExamMarksData(List<StudentExamMarksUpdateModel> entityList);

        Task<DataTable> GetPracticalExaminerRelivingByUserId(ITIPracticalExaminerSearchFilter filterModel);

        Task<DataTable> GetItiRemunerationExaminerDetails(ITI_AppointExaminerDetailsModel filterModel);
        Task<DataTable> GetItiRemunerationAdminDetails(ITI_AppointExaminerDetailsModel filterModel);

        Task<DataTable> Iti_RemunerationGenerateAndViewPdf(ITI_AppointExaminerDetailsModel filterModel);
        Task<int> SaveDataSubmitAndForwardToAdmin(ITI_AppointExaminerDetailsModel filterModel);
        Task<int> UpdateToApprove(ITI_AppointExaminerDetailsModel filterModel);
    }
}
