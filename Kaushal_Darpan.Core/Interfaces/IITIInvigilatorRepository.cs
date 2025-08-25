using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ItiInvigilator;
using Kaushal_Darpan.Models.ITIPracticalExaminer;
using Kaushal_Darpan.Models.ITITheoryMarks;
using Kaushal_Darpan.Models.ITITimeTable;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.TimeTable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIInvigilatorRepository
    {
        Task<DataTable> GetAllData(TimeTableSearchModel model);
        Task<DataTable> GetAllInvigilator(ItiInvigilatorSearchModel model);
        Task<DataTable> GetAllTheoryStudents(ItiTheoryStudentMaster model);
        Task<int> SaveInvigilator(ItiInvigilatorDataModel model);
        Task<DataTable> GetInvigilatorData_UserWise(int departmentId, int semesterId, int instituteId, int endTermId, int shiftId, int engNonEng, int userId);
        Task<DataTable> GetTheoryStudentsByRollRangeAsync(ItiInvigilatorDataModel model);
        Task<DataTable> ITIInvigilatorDashboard(ItiInvigilatorSearchModel filterModel);
        Task<int> SaveIsPresentData(List<StudentExamMarksUpdateModel> entityList);

        Task<DataTable> Iti_InvigilatorPaymentGenerateAndViewPdf(ITI_InvigilatorPDFViewModal filterModel);

        Task<DataTable> Iti_InvigilatorSubmitandForwardToAdmin(ITI_InvigilatorPDFForwardModal filterModel);

        Task<DataTable> GetItiRemunerationInvigilatorAdminDetails(ITI_AdminInvigilatorRemunerationDetailModal filterModel);

        Task<int> UpdateToApprove(ITI_AdminInvigilatorRemunerationDetailModal Model);

        Task<DataTable> GetinvigilatorDetailbyRemunerationID(int RemunerationID);

        Task<DataTable> GetRemunerationApproveList(ITI_AdminInvigilatorRemunerationDetailModal filterModel);
    }
}
