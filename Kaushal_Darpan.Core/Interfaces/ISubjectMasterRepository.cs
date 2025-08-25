using Kaushal_Darpan.Models.SubjectMaster;
using System.Data;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ISubjectMasterRepository
    {

        Task<DataTable> GetAllData(SubjectSearchModel model);
        Task<SubjectMaster> GetById(int PK_ID, int DepartmentID);
        Task<ParentSubjectMap> GetChildSubject(int PK_ID);
        Task<bool> SaveData(SubjectMaster productDetails);
        Task<bool> SaveParentData(ParentSubjectMap productDetails);
        Task<bool> DeleteDataByID(SubjectMaster productDetails);



    }
}
