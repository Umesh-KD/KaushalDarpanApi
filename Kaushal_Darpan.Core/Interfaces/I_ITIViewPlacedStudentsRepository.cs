using Kaushal_Darpan.Models.ViewPlacedStudents;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITIViewPlacedStudentsRepository
    {
        Task<DataTable> GetAllData(ITIViewPlacedStudents filterModel);
        Task<DataTable> GetAllPlacementData();
    }
}
