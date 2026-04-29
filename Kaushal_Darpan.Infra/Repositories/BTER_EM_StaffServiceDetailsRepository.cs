using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class BTER_EM_StaffServiceDetailsRepository: IBTER_EM_StaffServiceDetailsRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public BTER_EM_StaffServiceDetailsRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "BTER_EM_StaffServiceDetailsRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> Save_StaffTrainingDetails(StaffTrainingDetailDataModel body)
        {
            _actionName = "Save_StaffTrainingDetails(StaffTrainingDetailDataModel body)";
            try
            {
                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_BTER_EM_StaffTrainingDetails_IU";

                    command.Parameters.AddWithValue("@StaffTrainingDetailID", body.StaffTrainingDetailID);
                    command.Parameters.AddWithValue("@OrganizinglnstituteName", body.OrganizinglnstituteName);
                    command.Parameters.AddWithValue("@CourseType", body.CourseType);
                    command.Parameters.AddWithValue("@CourseName", body.CourseName);
                    command.Parameters.AddWithValue("@DurationUnit", body.DurationUnit);
                    command.Parameters.AddWithValue("@Duration", body.Duration);
                    command.Parameters.AddWithValue("@StartDate", body.StartDate);
                    command.Parameters.AddWithValue("@EndDate", body.EndDate);
                    command.Parameters.AddWithValue("@ModeOfTraining", body.ModeOfTraining);
                    command.Parameters.AddWithValue("@Venue", body.Venue);
                    command.Parameters.AddWithValue("@UserID", body.UserID);
                    command.Parameters.AddWithValue("@StaffID", body.StaffID);
                    command.Parameters.AddWithValue("@TrainingDoc", body.TrainingDoc);
                    command.Parameters.AddWithValue("@Dis_TrainingDoc", body.Dis_TrainingDoc);
                    command.Parameters.AddWithValue("@TrainingTypeID", body.TrainingTypeID);
                    command.Parameters.AddWithValue("@ComplitionTrainingDoc", body.ComplitionTrainingDoc);
                    command.Parameters.AddWithValue("@Dis_complitionTrainingDoc", body.Dis_complitionTrainingDoc);
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

        public async Task<DataTable> StaffTrainingDetails_GetData(StaffTrainingDetailSearchData body)
        {
            _actionName = "GetBudgetHeadMasterData_EM(EM_BudgetHeadMasterDataModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_BTER_EM_StaffTrainingDetails_GetData";

                    command.Parameters.AddWithValue("@Action", body.Action);
                    command.Parameters.AddWithValue("@UserID", body.UserID);
                    command.Parameters.AddWithValue("@StaffID", body.StaffID);
                    command.Parameters.AddWithValue("@StaffTrainingDetailID", body.StaffTrainingDetailID);
                    command.Parameters.AddWithValue("@StatusID", body.StatusID);
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

        public async Task<bool> StaffTrainingDetails_DeleteById(StaffTrainingDetailSearchData request)
        {
            _actionName = "BTER_EM_UnlockProfile(BTER_EM_UnlockProfileDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_BTER_EM_StaffTrainingDetails_GetData";

                    command.Parameters.AddWithValue("@Action", request.Action);
                    command.Parameters.AddWithValue("@UserID", request.UserID);
                    command.Parameters.AddWithValue("@StaffID", request.StaffID);
                    command.Parameters.AddWithValue("@StaffTrainingDetailID", request.StaffTrainingDetailID);
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

        public async Task<int> StaffTrainingStatusUpdate(StaffTrainingStatusUpdateDataModel body)
        {
            _actionName = "StaffTrainingStatusUpdate(StaffTrainingStatusUpdateDataModel body)";
            try
            {
                
                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_StaffTrainingStatusUpdate";
                    command.Parameters.AddWithValue("@TrainingStatus", body.TrainingStatus);
                    command.Parameters.AddWithValue("@Remark", body.Remark);
                    command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
                    command.Parameters.AddWithValue("@jsonData", body.jsonData);
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

        public async Task<DataTable> StaffTrainingHTS_GetData(StaffTrainingDetailSearchData body)
        {
            _actionName = "StaffTrainingHTS_GetData(StaffTrainingDetailSearchData body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_StaffTrainingHTS";
                    command.Parameters.AddWithValue("@StaffTrainingDetailID", body.StaffTrainingDetailID);
                    command.Parameters.AddWithValue("@ActionBy", body.UserID);
                    command.Parameters.AddWithValue("@StatusID", body.StatusID);
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

        public async Task<int> StaffTrainingDocUpdate(StaffTrainingDetailDataModel body)
        {
            _actionName = "StaffTrainingDocUpdate(StaffTrainingDetailDataModel body)";
            try
            {
                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_StaffTrainingDocUpdate";

                    command.Parameters.AddWithValue("@StaffTrainingDetailId", body.StaffTrainingDetailID);
                    command.Parameters.AddWithValue("@ComplitionTrainingDoc", body.ComplitionTrainingDoc);
                    command.Parameters.AddWithValue("@Dis_complitionTrainingDoc", body.Dis_complitionTrainingDoc);
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
    }
}
