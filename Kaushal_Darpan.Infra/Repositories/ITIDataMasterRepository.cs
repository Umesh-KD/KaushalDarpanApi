using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.MenuMaster;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIDataMasterRepository : IITIDataMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIDataMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "IITIDataMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

     
        public async Task<DataTable> GetAllData(SeatIntakesDataListSearchModel request)
        {
            _actionName = "GetAllData(SeatIntakeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetDataMaster";
                        command.Parameters.AddWithValue("@AcademicYearID", request.AcademicYearID);
                        //command.Parameters.AddWithValue("@RequestType", request.RequestType);
                        command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
                        command.Parameters.AddWithValue("@action",request.action);

                        //command.Parameters.AddWithValue("@action", "_getAllData");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    //var data = new List<SeatIntakesDataListModel>();
                    //if (dataTable != null)
                    //{
                    //    data = CommonFuncationHelper.ConvertDataTable<List<SeatIntakesDataListModel>>(dataTable);
                    //}
                    return dataTable;
                }
                catch (Exception ex)
                {
                    var errorDesc = new ErrorDescription
                    {
                        Message = ex.Message,
                        PageName = _pageName,
                        ActionName = _actionName,
                        SqlExecutableQuery = _sqlQuery
                    };
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        
  
    
    }
}
