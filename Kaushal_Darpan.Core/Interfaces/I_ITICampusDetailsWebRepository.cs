using Kaushal_Darpan.Models.ITICampusDetailsWeb;
using System.Data;


namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITICampusDetailsWebRepository
    {

        Task<DataTable> GetITIAllPost(int postId, int DepartmentID);
        Task<DataTable> GetITIAllTrade();
        Task<DataTable> GetITIAllPost(int postId, int DepartmentID, ITIAllPostSearchModel model);
        Task<DataTable> GetAllPlacementCompany(ITICampusDetailsWebSearchModel model);

        Task<DataTable> GetAllPostExNonList(int postId, int DepartmentID);
    }
}
