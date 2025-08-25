using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIFeeModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIFeeRepository
    {
        Task<bool> SaveITIFeeData(ITIFeeModel request);
        Task<ITIFeeModel> UpdateData(int Id);
        Task<ITIFeeModel> GetById(int id);
      

    }
}
