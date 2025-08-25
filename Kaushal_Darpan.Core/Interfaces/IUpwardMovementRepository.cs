using Kaushal_Darpan.Models.ApplicationStatus;
using Kaushal_Darpan.Models.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IUpwardMovementRepository
    {
        Task<DataTable> GetDataItiStudentApplication(ItiStuAppSearchModelUpward filterModel);
        Task<DataTable> GetDataItiUpwardMoment(ItiStuAppSearchModelUpward filterModel);
        Task<bool> UpwardMomentUpdate(UpwardMoment model);
        Task<bool> ITIUpwardMomentUpdate(UpwardMoment model);
    }
}
