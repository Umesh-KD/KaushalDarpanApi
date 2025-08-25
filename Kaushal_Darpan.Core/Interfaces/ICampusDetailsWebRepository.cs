using Kaushal_Darpan.Models.CampusDetailsWeb;
using System.Data;


namespace Kaushal_Darpan.Core.Interfaces
{
    public interface ICampusDetailsWebRepository
    {

        Task<DataTable> GetAllPost( int postId,int DepartmentID,int StreamID, int FinancialYearID, int InstituteID,  string CampusFromDate,string CampusToDate);
        Task<DataTable> GetAllPlacementCompany(CampusDetailsWebSearchModel model);

        Task<DataTable> GetAllPostExNonList(int postId,int DepartmentID,int StreamID,int FinancialYearID, int InstituteID, string  CampusFromDate,string CampusToDate);
    }
}
