
using Kaushal_Darpan.Models.IDfFundDetailsModel;
using Kaushal_Darpan.Models.ITIIIPManageDataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IITIIIPManageRepository
    {
        Task<DataSet> GetAllData(ITIIIPManageDataModel body);
        Task<int> SaveIMCReg(ITIIIPManageDataModel productDetails);  
        Task<DataSet> GetAllIMCFundData(IIPManageFundSearchModel body);
        Task<int> SaveIMCFund(IIPManageFundSearchModel productDetails);
        Task<ITIIIPManageDataModel> GetById_IMC(int ID);
        Task<DataTable> GetIMCHistory_ById(int RegID);
        Task<int> SaveFundDetails(IDfFundDetailsModel FundDeatils);
        Task<DataTable> GetFundDetailsData(IDfFundSearchDetailsModel body);
        Task<IDfFundDetailsModel> GetById_FundDetails(int ID);


        Task<IIPManageFundSearchModel> GetById_IMCFund(int ID);

        Task<DataTable> GetQuaterlyProgressData(int ID);
        Task<int> SaveQuaterlyProgressData(IMCFundRevenue FundDeatils);
        Task<int> FinalSubmitUpdate(int ID);

        Task<DataSet> GetIIPQuaterlyFundReport(int id);
    }
}
