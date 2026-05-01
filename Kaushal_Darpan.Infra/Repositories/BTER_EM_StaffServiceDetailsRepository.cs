using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
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

        //// BTER Staff Transfer System

        public async Task<DataTable> GetStaffPersonalDetails(BTER_GetStaffPersonalDetailsModel filterModel)
        {
            _actionName = "GetStaffPersonalDetails()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStaffPersonalDetails";
                        command.Parameters.AddWithValue("@StaffID", filterModel.StaffID);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@StaffUserID", filterModel.StaffUserID);
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

        public async Task<int> BTER_EM_TransferSystem_IU(BTER_EM_TransferSystemModule body)
        {
            _actionName = "BTER_EM_TransferSystem_IU(BTER_EM_TransferSystemModule body)";
            try
            {
                int result = 0;

                    var json = body.TransferExtDetails != 
                    null && body.TransferExtDetails.Any()? System.Text.Json.JsonSerializer.Serialize(body.TransferExtDetails): null;

                var jsonParam = new SqlParameter("@TransferExtJson", SqlDbType.NVarChar, -1)
                {
                    Value = string.IsNullOrEmpty(json) ? DBNull.Value : json
                };

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_BTER_EM_TransferSystem_IU";
                    command.Parameters.AddWithValue("@TransferSystemID", body.TransferSystemID);
                    command.Parameters.AddWithValue("@UserID", body.UserID);
                    command.Parameters.AddWithValue("@StaffID", body.StaffID);
                    command.Parameters.AddWithValue("@SSOID", body.SSOID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TransferCategoryID", body.TransferCategoryID);
                    command.Parameters.AddWithValue("@ReasonDescription", body.ReasonDescription ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SupportingDocuments", body.SupportingDocuments ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SupportingDocumentsDis", body.SupportingDocumentsDis ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
                    command.Parameters.AddWithValue("@UpdatedBy", body.UpdatedBy);
                    command.Parameters.AddWithValue("@TransferStatus", body.TransferStatus);
                    //command.Parameters.AddWithValue("@TransferExtJson", jsonParam ?? (object)DBNull.Value);
                    command.Parameters.Add(jsonParam);
                    var returnParam = new SqlParameter("@Return", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(returnParam);
                    _sqlQuery = command.GetSqlExecutableQuery();
                    await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(returnParam.Value);
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

        public async Task<DataTable> GetEM_TransferSystemData(EM_TransferSystemSearchModel filterModel)
        {
            _actionName = "GetEM_TransferSystemData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_EM_TransferSystemList";
                        command.Parameters.AddWithValue("@Action", filterModel.Action);
                        command.Parameters.AddWithValue("@TransferSystemID", filterModel.TransferSystemID);
                        command.Parameters.AddWithValue("@StaffID", filterModel.StaffID);
                        command.Parameters.AddWithValue("@ActionBy", filterModel.ActionBy);
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

        public async Task<bool> EM_TransferSystemUpdatePocessManage(EM_TransferSystemSearchModel request)
        {
            _actionName = "EM_TransferSystemUpdatePocessManage(EM_TransferSystemSearchModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_EM_TransferSystemUpdatePocessManage";

                    command.Parameters.AddWithValue("@Action", request.Action);
                    command.Parameters.AddWithValue("@TransferSystemID", request.TransferSystemID);
                    command.Parameters.AddWithValue("@ActionBy", request.ActionBy);
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

        public async Task<int> EM_TransferSystemUpdateStatus(TransferSystemUpdateDataModel body)
        {
            _actionName = "EM_TransferSystemUpdateStatus(TransferSystemUpdateDataModel body)";
            try
            {

                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_TransferSystemStatusUpdate";
                    command.Parameters.AddWithValue("@TransferSystemID", body.TransferSystemID);
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
    }
}
