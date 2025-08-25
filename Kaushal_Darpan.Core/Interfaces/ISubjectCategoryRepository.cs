using Kaushal_Darpan.Models.SubjectCategory;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ISubjectCategoryRepository
    {
        Task<DataTable> GetAllData();
        Task<SubjectCategory> GetById(int PK_ID);
        Task<bool> SaveData(SubjectCategory productDetails);
        Task<bool> DeleteDataByID(SubjectCategory productDetails);
    }
}
