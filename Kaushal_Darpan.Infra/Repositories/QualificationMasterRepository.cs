using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class QualificationMasterRepository : IQualificationMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public QualificationMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "QualificationMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> QualificationMaster_GetData(QualificationMasterSearchModel body)
        {
            _actionName = "GetBudgetHeadMasterData_EM(EM_BudgetHeadMasterDataModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_QualificationMaster_GetData";

                    command.Parameters.AddWithValue("@Action", body.Action);
                    command.Parameters.AddWithValue("@QualificationID", body.QualificationID);

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
        }

        public async Task<int> Save_QualificationMasterData(QualificationMasterDataModel body)
        {
            _actionName = "Save_QualificationMasterData(StaffTrainingDetailDataModel body)";
            try
            {
                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_QualificationMaster_IU_master";

                    command.Parameters.AddWithValue("@Action", body.Action);
                    command.Parameters.AddWithValue("@QualificationID", body.QualificationID);
                    command.Parameters.AddWithValue("@QualificationLevel", body.QualificationLevel);
                    command.Parameters.AddWithValue("@QualificationName", body.QualificationName);
                    command.Parameters.AddWithValue("@Remark", body.Remarks);
                    command.Parameters.AddWithValue("@UserID", body.UserID);
                    command.Parameters.AddWithValue("@DeparmentID", body.DepartmentID);

                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    command.Parameters.Add("@Return", SqlDbType.Int);
                    command.Parameters["@Return"].Direction = ParameterDirection.Output;
                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(command.Parameters["@Return"].Value);
                }

                return result;
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
        }

        public async Task<bool> Qualification_DeleteById(QualificationMasterSearchModel request)
        {
            _actionName = "BTER_EM_UnlockProfile(BTER_EM_UnlockProfileDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_QualificationMaster_GetData";

                    command.Parameters.AddWithValue("@Action", request.Action);
                    command.Parameters.AddWithValue("@UserID", request.UserID);
                    command.Parameters.AddWithValue("@QualificationID", request.QualificationID);
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();
                }
                if (result > 0)
                    return true;
                else
                    return false;
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
        }
    }
}
