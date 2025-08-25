using Kaushal_Darpan.Models.NodalOfficer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface INodalRepository
    {
        Task<DataTable> GetAllData(SearchNodalModel filterModel);
        Task<int> SaveNodalData(List<NodalModel> entity);
        //Task<NodalModel> GetById(int NodalId);
        Task<DataTable> GetById(int NodalId);
        Task<bool> DeleteDataByID(NodalModel request);
    }
}
