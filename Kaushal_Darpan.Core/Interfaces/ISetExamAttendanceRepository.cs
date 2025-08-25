using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ISetExamAttendanceRepository
    {
        Task<DataTable> GetExamStudentData(SetExamAttendanceSearchModel filterModel);
        Task<DataTable> GetExamAttendence(SetExamAttendanceSearchModel filterModel);
        Task<int> SaveData(List<SetExamAttendanceModel> productDetails,int InvigilatorAppointmentID=0 );
        Task<DataTable> GetExamStudentData_Internal(SetExamAttendanceSearchModel filterModel);
    }
}
