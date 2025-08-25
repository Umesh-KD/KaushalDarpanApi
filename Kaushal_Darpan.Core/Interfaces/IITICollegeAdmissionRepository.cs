using Kaushal_Darpan.Models.ITICollegeAdmissionModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITICollegeAdmissionRepository
    {
        Task<DataTable> GetAllData(ITICollegeAdmissionSearch searchModel);
        Task<bool> SaveData(ITICollegeAdmissionModel request);
        Task<bool> SaveBterData(ITICollegeAdmissionModel request);
        Task<ITICollegeAdmissionModel> GetById(int ApplicationID);
    }
}
