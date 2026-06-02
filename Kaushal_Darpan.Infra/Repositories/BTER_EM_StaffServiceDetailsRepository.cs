using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.StaffMaster;
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
    public class BTER_EM_StaffServiceDetailsRepository : IBTER_EM_StaffServiceDetailsRepository
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
                    command.Parameters.AddWithValue("@RoleID", body.RoleID);
                    command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
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
                    command.Parameters.AddWithValue("@RoleID", body.RoleID);
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
                null && body.TransferExtDetails.Any() ? System.Text.Json.JsonSerializer.Serialize(body.TransferExtDetails) : null;

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
                    command.Parameters.AddWithValue("@RoleID", body.RoleID);
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
                        command.Parameters.AddWithValue("@StatusID", filterModel.StatusID);
                        command.Parameters.AddWithValue("@CategoryID", filterModel.CategoryID);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@EmployeeType", filterModel.EmployeeType);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
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

        public async Task<DataTable> GetEM_RelievingTransferData(EM_TransferSystemSearchModel filterModel)
        {
            _actionName = "GetEM_RelievingTransferData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_EM_TransferSystemList";
                        command.Parameters.AddWithValue("@Action", "RelievingMechanismList");
                        command.Parameters.AddWithValue("@TransferSystemID", filterModel.TransferSystemID);
                        command.Parameters.AddWithValue("@StaffID", filterModel.StaffID);
                        command.Parameters.AddWithValue("@ActionBy", filterModel.ActionBy);
                        command.Parameters.AddWithValue("@StatusID", filterModel.StatusID);
                        command.Parameters.AddWithValue("@CategoryID", filterModel.CategoryID);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@EmployeeType", filterModel.EmployeeType);
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

        public async Task<DataTable> GetEM_TransferSystemEmployeeStatus(EM_TransferSystemSearchModel filterModel)
        {
            _actionName = "GetEM_TransferSystemEmployeeStatus()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_EM_TransferSystemList";
                        command.Parameters.AddWithValue("@Action", "EM_TransferSystemEmployeeStatus");
                        command.Parameters.AddWithValue("@TransferSystemID", filterModel.TransferSystemID);
                        command.Parameters.AddWithValue("@StaffID", filterModel.StaffID);
                        command.Parameters.AddWithValue("@ActionBy", filterModel.ActionBy);
                        command.Parameters.AddWithValue("@StatusID", filterModel.StatusID);
                        command.Parameters.AddWithValue("@CategoryID", filterModel.CategoryID);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@EmployeeType", filterModel.EmployeeType);
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
                    command.Parameters.AddWithValue("@RoleID", body.RoleID);
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

        public async Task<int> TransferSystemEXTStatusUpdate(TransferSystemUpdateDataModel body)
        {
            _actionName = "TransferSystemEXTStatusUpdate(TransferSystemUpdateDataModel body)";
            try
            {

                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_TransferSystemEXTStatusUpdate";
                    command.Parameters.AddWithValue("@TransferSystemID", body.TransferSystemID);
                    command.Parameters.AddWithValue("@Remark", body.Remark);
                    command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
                    command.Parameters.AddWithValue("@ID", body.ID);
                    command.Parameters.AddWithValue("@jsonData", body.jsonData);
                    command.Parameters.AddWithValue("@RoleID", body.RoleID);
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

        public async Task<int> TransferSystemGeneratorUpdate(TransferSystemUpdateDataModel body)
        {
            _actionName = "TransferSystemGeneratorUpdate(TransferSystemUpdateDataModel body)";
            try
            {

                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_TransferSystemGenerator";
                    command.Parameters.AddWithValue("@jsonData", body.jsonData);
                    command.Parameters.AddWithValue("@RoleID", body.RoleID);
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


        public async Task<int> AddTransferSystemManualRequest(BTERStaffManualRequestModel body)
        {
            _actionName = "AddTransferSystemManualRequest(TransferSystemUpdateDataModel body)";
            try
            {

                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_TransferSystemManualRequest";
                    command.Parameters.AddWithValue("@OfficeID", body.OfficeID ?? 0);
                    command.Parameters.AddWithValue("@PostID", body.PostID ?? 0);
                    command.Parameters.AddWithValue("@DistrictID", body.DistrictID ?? 0);
                    command.Parameters.AddWithValue("@InstituteID", body.InstituteID ?? 0);
                    command.Parameters.AddWithValue("@StaffID", body.StaffID ?? 0);
                    command.Parameters.AddWithValue("@NonGazettedID", body.NonGazettedID ?? 0);

                    command.Parameters.AddWithValue("@To_OfficeID", body.To_OfficeID ?? 0);
                    command.Parameters.AddWithValue("@To_PostID", body.To_PostID ?? 0);
                    command.Parameters.AddWithValue("@To_ddlDistrictID", body.To_ddlDistrictID ?? 0);
                    command.Parameters.AddWithValue("@To_ddlCollege", body.To_ddlCollege ?? 0);

                    command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy ?? 0);
                    command.Parameters.AddWithValue("@UserID", body.UserID ?? 0);
                    command.Parameters.AddWithValue("@SSOID", body.SSOID ?? "");
                    command.Parameters.AddWithValue("@TransferCategoryID", body.TransfercateID ?? 0);
                    command.Parameters.AddWithValue("@RoleID", body.RoleID ?? 0);
                    command.Parameters.AddWithValue("@BranchID", body.BranchID ?? 0);
                    command.Parameters.AddWithValue("@To_BranchID", body.To_BranchID ?? 0);
                    command.Parameters.AddWithValue("@ReasonDescription", body.ReasonDescription ?? "");
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

        #region GetRelievingLetter
        public async Task<DataSet> GetRelievingLetter(EM_TransferSystemSearchModel model)
        {
            _actionName = "GetJRelievingLetter(RelievingLetterSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetTransferSystemRelievingLetter";
                        command.Parameters.AddWithValue("@Action", "RelievingLetter");
                        command.Parameters.AddWithValue("@TransferSystemID", model.TransferSystemID);
                        command.Parameters.AddWithValue("@StaffID", model.StaffID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    return ds;
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
        #endregion


        public async Task<int> TransferSystemRetievingUpdateStatus(EM_TransferSystemSearchModel body)
        {
            _actionName = "TransferSystemRetievingUpdateStatus(TransferSystemUpdateDataModel body)";
            try
            {

                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_TransferSystemRetievingUpdateStatus";
                    command.Parameters.AddWithValue("@TransferSystemID", body.TransferSystemID);
                    command.Parameters.AddWithValue("@RelievingStatus", body.StatusID);
                    command.Parameters.AddWithValue("@RelievingDoc", body.RelievingDoc);
                    command.Parameters.AddWithValue("@RelievingDoc_Dis", body.RelievingDoc_Dis);
                    command.Parameters.AddWithValue("@StaffID", body.StaffID);
                    command.Parameters.AddWithValue("@Remark", body.Remark);
                    command.Parameters.AddWithValue("@CreatedBy", body.ActionBy);
                    command.Parameters.AddWithValue("@RelievingDate", body.RelievingDate);
                    command.Parameters.AddWithValue("@RoleID", body.RoleID);
                    command.Parameters.AddWithValue("@RelievingTimeID", body.RelievingTimeID);
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


        public async Task<int> DeleteStaffTrainingData(StaffTrainingDetailSearchData body)
        {


            _actionName = "TransferSystemRetievingUpdateStatus(TransferSystemUpdateDataModel body)";
            try
            {

                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_StaffTrainingDelete";
                    command.Parameters.AddWithValue("@StaffTrainingDetailID", body.StaffTrainingDetailID);
                    command.Parameters.AddWithValue("@ActionBy", body.UserID);
                    command.Parameters.AddWithValue("@StatusID", body.StatusID);
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

        public async Task<DataTable> GetTransferRequestReport(EM_TransferSystemSearchModel filterModel)
        {
            _actionName = "GetTransferRequestReport()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_EM_TransferRequestReport";
                        command.Parameters.AddWithValue("@Action", filterModel.Action);
                        command.Parameters.AddWithValue("@TransferSystemID", filterModel.TransferSystemID);
                        command.Parameters.AddWithValue("@StaffID", filterModel.StaffID);
                        command.Parameters.AddWithValue("@ActionBy", filterModel.ActionBy);
                        command.Parameters.AddWithValue("@StatusID", filterModel.StatusID);
                        command.Parameters.AddWithValue("@CategoryID", filterModel.CategoryID);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@EmployeeType", filterModel.EmployeeType);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);
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

        public async Task<DataTable> GetRelievingTransferRequestList(EM_TransferSystemSearchModel filterModel)
        {
            _actionName = "GetRelievingTransferRequestList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_EM_TransferRelievingReport";
                        command.Parameters.AddWithValue("@Action", "TransferRelievingList");
                        command.Parameters.AddWithValue("@TransferSystemID", filterModel.TransferSystemID);
                        command.Parameters.AddWithValue("@StaffID", filterModel.StaffID);
                        command.Parameters.AddWithValue("@ActionBy", filterModel.ActionBy);
                        command.Parameters.AddWithValue("@StatusID", filterModel.StatusID);
                        command.Parameters.AddWithValue("@CategoryID", filterModel.CategoryID);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@EmployeeType", filterModel.EmployeeType);
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


        public async Task<DataTable> GetTransferSystem_PostWiseBranchCheck(EM_TransferSystemSearchModel filterModel)
        {
            _actionName = "GetTransferSystem_PostWiseBranchCheck()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_TransferSystem_PostWiseBranchCheck";
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
