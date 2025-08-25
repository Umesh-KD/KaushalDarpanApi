using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.SubjectMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public  interface IAppointExaminerRepository
    {
        Task<DataTable> GetAllData(AppointExaminerSearchModel model);
        Task<AppointExaminerModel> GetById(int PK_ID, int DepartmentID);
        Task<bool> SaveData(AppointExaminerModel productDetails);
        //Task<bool> DeleteDataByID(HRMaster productDetails);
        Task<bool> DeleteDataByID(AppointExaminerModel productDetails);
    }
}
