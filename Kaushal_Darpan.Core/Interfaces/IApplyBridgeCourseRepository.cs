using Kaushal_Darpan.Models.BridgeCourse;
using Kaushal_Darpan.Models.PrometedStudentMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IApplyBridgeCourseRepository
    {
        Task<List<BridgeCourseStudentMasterModel>> GetAllStudent(BridgeCourseStudentSearchModel model);
        Task<int> SaveStudent(List<BridgeCourseStudentMarkedModel> model);
    }
}
