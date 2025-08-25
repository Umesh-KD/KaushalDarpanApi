using Kaushal_Darpan.Models.StudentMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IStudentJanaadharRepository
    {
        Task<DataTable> GetAllData(StudentJanaadhar filterModel);
        Task<DataTable> GetStudentsJanAadharData(StudentJanaadhar filterModel);
    }
}
