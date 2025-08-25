using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.InvigilatorAppointmentMaster;
using Kaushal_Darpan.Models.StaffMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IInvigilatorAppointmentMasterRepository
    {
        Task<DataTable> GetAllData(InvigilatorAppointmentMasterSearchModel searchRequest);
        Task<bool> SaveData(InvigilatorAppointmentMasterModel productDetails);
        Task<InvigilatorAppointmentMasterModel> GetById(int PK_ID, int DepartmentID);
        Task<bool> DeleteDataByID(InvigilatorAppointmentMasterModel productDetails);
        Task<DataTable> UnlockExamAttendance_GetCSData(InvigilatorAppointmentMasterSearchModel searchRequest);
        Task<bool> UnlockExamAttendance(UnlockExamAttendanceDataModel productDetails);
    }
}
