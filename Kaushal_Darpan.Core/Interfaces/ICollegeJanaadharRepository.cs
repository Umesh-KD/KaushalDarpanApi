using Kaushal_Darpan.Models.CollegeMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICollegeJanaadharRepository
    {
        Task<DataTable> GetAllData(CollegeJanaadhar filterModel);
    }
}
