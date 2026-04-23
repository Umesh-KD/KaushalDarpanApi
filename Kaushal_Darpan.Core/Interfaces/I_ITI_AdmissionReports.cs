using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface I_ITI_AdmissionReports
    {
        Task<DataSet> GetITISeatOffered();
        Task<DataSet> GetITIStatistics();
    }
}
