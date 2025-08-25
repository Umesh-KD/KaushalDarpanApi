using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.Allotment;
using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.DateConfiguration;
using Kaushal_Darpan.Models.ItiExaminer;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface IJanaadharRepository
    {

        Task<DataTable> GetJanaadharListData(GETJanaadharListDataModel body);
        Task<DataTable> GetStudentJanaadharData(GETStudentJanaadharDataModel body);

        Task<DataTable> GetInstituteJanaadharListData(GETJanaadharListDataModel body);
        Task<int> IsDroppedChange(GETJanaadharListDataModel body);

        Task<int> PostStudentJanaadharForm(PostJanaadharDataModel body);

        Task<int> PostStudentAdmittedForm(GETJanaadharListDataModel body);
    }
}
