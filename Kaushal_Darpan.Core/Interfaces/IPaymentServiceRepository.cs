using Kaushal_Darpan.Models.PaymentServiceMaster;
using Kaushal_Darpan.Models.RoleMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IPaymentServiceRepository
    {
        Task<int> SaveData(PaymentServiceDataModel productDetails);
        Task<DataTable> GetAllData(PaymentServiceSearchModel request);
    }
}
