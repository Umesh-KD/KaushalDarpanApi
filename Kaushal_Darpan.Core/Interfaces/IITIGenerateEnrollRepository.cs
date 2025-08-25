using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITIGenerateEnrollment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIGenerateEnrollRepository
    {
        Task<DataTable> GetGenerateEnrollData(ITIGenerateEnrollSearchModel model);
        Task<int> SaveEnrolledData(List<ITIGenerateEnrollMaster> model);
        Task<int> OnPublish(List<ITIGenerateEnrollMaster    > model);
    }
}
