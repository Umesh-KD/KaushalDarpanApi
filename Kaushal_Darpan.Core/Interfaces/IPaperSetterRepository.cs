using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.PaperSetter;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface IPaperSetterRepository
    {
        Task<DataTable> GetTeacherForExaminer(TeacherForPaperSetterSearchModel filterModel);
        Task<DataTable> GetExaminerData(TeacherForPaperSetterSearchModel filterModel);
        Task<int> SaveExaminerData(PaperSetterMaster productDetails);
        Task<bool> DeleteDataByID(PaperSetterMaster productDetails);
        Task<DataTable> GetExaminerByCode(ExaminerCodeLoginModel model);
        Task<PaperSetterMaster> GetById(int PK_ID, int StaffSubjectID, int DepartmentID);
        Task<AppointPaperSetterDataModel> GetPaperSetterStaffDetails(int PaperSetterID);
        Task<int> AppointPaperSetter(AppointPaperSetterDataModel request);
        Task<int> VerifyPaperSetter(List<VerifyPaperSetterDataModel> request);
        Task<DataSet> GeneratePaperSetterOrder(List<VerifyPaperSetterDataModel> model);
        Task<DataSet> GetStaffOrder(VerifyPaperSetterDataModel model);
        Task<int> UpdateOrder(List<VerifyPaperSetterDataModel> model);
    }
}
