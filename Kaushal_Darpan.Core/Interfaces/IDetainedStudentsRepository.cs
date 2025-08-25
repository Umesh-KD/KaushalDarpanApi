using Kaushal_Darpan.Models.DetainedStudents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IDetainedStudentsRepository
    {
        Task<DataTable> GetAllData(DetainedStudentsSearchModel filterModel);
        Task<int> RevokeDetain(DispatchMasterModel filterModel);
    }
}
