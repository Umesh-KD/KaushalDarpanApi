using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.ScholarshipMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public  interface IScholarshipRepository
    {
        Task<DataTable> GetAllData(ScholarshipSearchModel model);
        Task<bool> SaveData(ScholarshipMaster productDetails);
        Task<ScholarshipMaster> GetById(int PK_ID);
        Task<bool> DeleteDataByID(ScholarshipMaster productDetails);
    }
}
