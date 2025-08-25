using Kaushal_Darpan.Models.ItiStudentActivities;
using Kaushal_Darpan.Models.TheoryMarks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IItiStudentCenterActivityRepository
    {
        Task<DataTable> GetAllData(ITIStudentCenterActiviteSearchModel filterModel);
        Task<int> UpdateSaveData(List<ITIStudentCenterActivities> productDetails);
    }
}
