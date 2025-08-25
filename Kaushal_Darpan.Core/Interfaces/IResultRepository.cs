using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.Results;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IResultRepository
    {
        Task<DataTable> GetStudentResult(StudentResultSearchModal model);
    }
}
