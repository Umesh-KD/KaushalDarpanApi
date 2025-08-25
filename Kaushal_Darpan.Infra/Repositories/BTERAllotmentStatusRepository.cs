using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Allotment;
using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.BTERInternalSliding;
using Kaushal_Darpan.Models.BterMeritMaster;
using Kaushal_Darpan.Models.ITIApplication;
using Newtonsoft.Json;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class BTERAllotmentStatusRepository : IBTERAllotmentStatusRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public BTERAllotmentStatusRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "BTERAllotmentStatusRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        public async Task<DataTable> GetAllotmentStatusList(AllotmentStatusSearchModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_AllotmentStatus_List";
                        command.Parameters.AddWithValue("@DOB", body.DOB);
                        command.Parameters.AddWithValue("@ApplicationNo", body.ApplicationNo);
                        command.Parameters.AddWithValue("@DepartmentId", body.DepartmentId);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

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

        public async Task<DataTable> GetAllotmentUpwardList(AllotmentStatusSearchModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_AllotmentUpward_List";
                        command.Parameters.AddWithValue("@DOB", body.DOB);
                        command.Parameters.AddWithValue("@ApplicationNo", body.ApplicationNo);
                        command.Parameters.AddWithValue("@DepartmentId", body.DepartmentId);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

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
        public async Task<DataTable> GetITIAllotmentUpwardList(AllotmentStatusSearchModel body)
        {
            _actionName = "GetGenerateAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_AllotmentUpward_List";
                        command.Parameters.AddWithValue("@DOB", body.DOB);
                        command.Parameters.AddWithValue("@ApplicationNo", body.ApplicationNo);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

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
