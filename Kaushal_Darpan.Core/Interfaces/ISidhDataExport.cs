using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Models.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{    
    public interface ISidhDataExport
    {
        Task<ApiResult<DataTable>> ProcessSidhData();
    }
}
