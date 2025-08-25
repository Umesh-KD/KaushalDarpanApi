using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.ITIAllotment;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ITISeatMatrix;

namespace Kaushal_Darpan.Core.Interfaces
{

    public interface IITISeatMatrixRepository
    {

        Task<DataTable> GetShowSeatMetrix(SeatSearchModel filterModel);

        Task<DataTable> SaveData(SeatSearchModel request);

    }
}
