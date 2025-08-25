using Kaushal_Darpan.Models.ITIRelievingExam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIRelievingExamRepository
    {
        // Define methods for the ITI Relieving Exam repository
        Task<DataTable> SaveRelievingExaminerDataAsync(ITIExaminerRelievingModel model);

        Task<bool> SaveRelievingCoOrdinatorData(ITICoordinatorRelievingModel model);
        // Add other methods as needed, e.g., GetTradesAsync, etc.

        Task<DataTable> GetRelievingExaminerByIdAsync(int id);

        Task<DataTable> GetRelievingByExamCoordinatorByIdAsync(int id);

        Task<DataTable> GetDataBySSOId(string SSOId);

        Task<bool> SaveUndertakingExaminerData(UndertakingExaminerFormModel model);

        Task<DataTable> GetUndertakingExaminerDetailsByIdAsync(int id);
        Task<DataTable> Get_CenterListByUserid(int id);
    }
}
