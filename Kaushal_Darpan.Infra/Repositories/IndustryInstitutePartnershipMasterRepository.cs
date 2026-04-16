using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.UserMaster;
using Newtonsoft.Json;
using System.Data;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class IndustryInstitutePartnershipMasterRepository :IIndustryInstitutePartnershipMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public IndustryInstitutePartnershipMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "IndustryInstitutePartnershipMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        #region  Previous IIP 

        public async Task<DataTable> GetAllData(IndustryInstitutePartnershipMasterSearchModel body)
        {
            _actionName = "GetAllData(IndustryInstitutePartnershipMasterSearchModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetIndustryInstitutePartnershipMaster";
                    //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter
                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                    command.Parameters.AddWithValue("@CompanyStatus", body.CompanyStatus);
                    command.Parameters.AddWithValue("@Action", "GetAllData");

                    if (body.Name != null)
                    {
                        command.Parameters.AddWithValue("@Name", body.Name);
                        command.Parameters.AddWithValue("@Status", body.Status);
                    }
                    command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                    command.Parameters.AddWithValue("@RoleID", body.RoleID);
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

        public async Task<int> SaveData(IndustryInstitutePartnershipMasterModels request)
        {
            _actionName = "SaveData(IndustryInstitutePartnershipMasterModels request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_IndustryInstitutePartnershipMaster_IU";


                    // Add parameters with appropriate null handling
                    command.Parameters.AddWithValue("@ID", request.ID);
                    command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                    command.Parameters.AddWithValue("@StateID", request.StateID);
                    command.Parameters.AddWithValue("@Website", request.Website);
                    command.Parameters.AddWithValue("@Address", request.Address);

                    command.Parameters.AddWithValue("@Logo", request.CompanyPhoto);
                    command.Parameters.AddWithValue("@Dis_Name", request.Dis_CompanyName);
                    command.Parameters.AddWithValue("@CompanyDocument", request.CompanyDocument);
                    command.Parameters.AddWithValue("@Dis_DocName", request.Dis_DocName);
                    command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                    command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    //command.Parameters.AddWithValue("@EventTypeID", request.EventTypeID);

                    command.Parameters.Add("@Return", SqlDbType.Int); // out
                    command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                    _sqlQuery = command.GetSqlExecutableQuery();

                    // Execute the command
                    result = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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
        public async Task<IndustryInstitutePartnershipMasterResponsiveModel> GetById(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandText = " select * from IIP_IndustryInstitutePartnershipMaster Where ID='" + PK_ID + "' ";

                    _sqlQuery = command.GetSqlExecutableQuery();
                    dataTable = await command.FillAsync_DataTable();
                }
                var data = new IndustryInstitutePartnershipMasterResponsiveModel();
                if (dataTable != null)
                {
                    data = CommonFuncationHelper.ConvertDataTable<IndustryInstitutePartnershipMasterResponsiveModel>(dataTable);
                }
                return data;
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

        public async Task<bool> DeleteDataByID(IndustryInstitutePartnershipMasterModels request)
        {
            _actionName = "DeleteDataByID(IndustryInstitutePartnershipMasterModels request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $" update [IIP_IndustryInstitutePartnershipMaster] set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ID={request.ID}";

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

        public async Task<bool> Save_IndustryInstitutePartnershipValidation_NodalAction(IndustryInstitutePartnershipMaster_Action request)
        {
            _actionName = "Save_IndustryInstitutePartnershipValidation_NodalAction(IndustryInstitutePartnershipMaster_Action request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_IndustryInstitutePartnershipValidation_NodalAction";
                    command.Parameters.AddWithValue("@ID", request.ID);
                    command.Parameters.AddWithValue("@Action", request.Action);
                    command.Parameters.AddWithValue("@ActionRemarks", request.ActionRemarks);
                    command.Parameters.AddWithValue("@ActionBy", request.ActionBy);
                    command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

                    _sqlQuery = command.GetSqlExecutableQuery();// sql query
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

        public async Task<DataTable> IndustryInstitutePartnershipValidationList(IndustryInstitutePartnershipMasterSearchModel body)
        {
            _actionName = "IndustryInstitutePartnershipValidationList(IndustryInstitutePartnershipMasterSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_IndustryInstitutePartnershipValidationList";
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null)
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                });
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

        public async Task<int> SaveIndustryTrainingData(IndustryTrainingMaster request)
        {
            _actionName = "SaveIndustryTrainingData(IndustryTrainingMaster request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UPS_IndustryTraining_IU";


                    // Add parameters with appropriate null handling
                    command.Parameters.AddWithValue("@Action", "Insert");
                    command.Parameters.AddWithValue("@IndustryTRID", request.IndustryTRID);
                    command.Parameters.AddWithValue("@IndustryID", request.IndustryID);
                    command.Parameters.AddWithValue("@EventTypeID", request.EventTypeID);
                    command.Parameters.AddWithValue("@EventDate", request.EventDate);
                    command.Parameters.AddWithValue("@SemesterID", request.SemesterID);

                    command.Parameters.AddWithValue("@Purpose", request.Purpose);
                    command.Parameters.AddWithValue("@TradeID", request.TradeID);
                    command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                    command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                    command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);

                    command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                    command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);

                    command.Parameters.AddWithValue("@IPAddress", request.IPAddress);


                    //command.Parameters.Add("@Return", SqlDbType.Int); // out
                    //command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                    _sqlQuery = command.GetSqlExecutableQuery();

                    // Execute the command
                    result = await command.ExecuteNonQueryAsync();
                    //result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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


        public async Task<DataTable> GetAllIndustryTrainingData(IndustryTrainingSearch body)
        {
            _actionName = "GetAllIndustryTrainingData(IndustryTrainingSearch body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UPS_IndustryTraining";
                    command.Parameters.AddWithValue("@Action", "List");
                    command.Parameters.AddWithValue("@IndustryTRID", body.IndustryTRID);
                    command.Parameters.AddWithValue("@IndustryID", body.IndustryID);
                    command.Parameters.AddWithValue("@EventTypeID", body.EventTypeID);
                    command.Parameters.AddWithValue("@EventDate", body.EventDate);
                    command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                    command.Parameters.AddWithValue("@TradeID", body.TradeID);
                    command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
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
        #endregion

        // ---------------------------------------------------------- BTER IIP by Ramesh ----------------------------------------------------------------------------

        #region  BTER IIP 
        public async Task<int> SaveData_IIP_Company(IndustryInstitutePartnershipMasterModels request)
        {
            _actionName = "SaveData_IIP_Company(IndustryInstitutePartnershipMasterModels request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_IIP_CompanyDetails_IU";


                    // Add parameters with appropriate null handling
                    command.Parameters.AddWithValue("@ID", request.ID);
                    command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                    command.Parameters.AddWithValue("@StateID", request.StateID);
                    command.Parameters.AddWithValue("@Website", request.Website);
                    command.Parameters.AddWithValue("@Address", request.Address);

                    command.Parameters.AddWithValue("@Logo", request.Logo);
                    command.Parameters.AddWithValue("@Dis_Name", request.Dis_Logo);
                    command.Parameters.AddWithValue("@CompanyDocument", request.CompanyDocument);
                    command.Parameters.AddWithValue("@Dis_DocName", request.Dis_DocName);
                    command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                    command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

                    command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                    command.Parameters.AddWithValue("@PlacementCompanyID", request.PlacementCompanyID);

                    command.Parameters.AddWithValue("@ConcernPersonDetails", JsonConvert.SerializeObject(request.ConcernPersonDetails));
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    //command.Parameters.AddWithValue("@EventTypeID", request.EventTypeID);

                    command.Parameters.Add("@Return", SqlDbType.Int); // out
                    command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                    _sqlQuery = command.GetSqlExecutableQuery();

                    // Execute the command
                    result = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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

        public async Task<IndustryInstitutePartnershipMasterModels> GetById_IIP_CompanyDetails(IIP_SearchModel request)
        {
            _actionName = "GetById_IIP_CompanyDetails(IIP_SearchModel request)";
            try
            {
                //DataTable dataTable = new DataTable();
                DataSet ds = new DataSet();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetIndustryInstitutePartnershipMaster";
                    command.Parameters.AddWithValue("@Action", "GetById");

                    command.Parameters.AddWithValue("@CompanyID", request.CompanyID);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    ds = await command.FillAsync();
                }
                var data = new IndustryInstitutePartnershipMasterModels();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<IndustryInstitutePartnershipMasterModels>(ds.Tables[0]);
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            data.ConcernPersonDetails = CommonFuncationHelper.ConvertDataTable<List<ConcernPersonDetailsDataModel>>(ds.Tables[1]);
                        }
                    }

                }
                return data;
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

        public async Task<bool> DeleteCompanyById_IIP(IndustryInstitutePartnershipMasterModels request)
        {
            _actionName = "DeleteCompanyById_IIP(IndustryInstitutePartnershipMasterModels request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetIndustryInstitutePartnershipMaster";
                    command.Parameters.AddWithValue("@Action", "Delete_Company");

                    command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                    command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
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
        public async Task<bool> Delete_Hr(ConcernPersonDetailsDataModel request)
        {
            _actionName = "Delete_Hr(ConcernPersonDetailsDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetIndustryInstitutePartnershipMaster";
                    command.Parameters.AddWithValue("@Action", "Delete_Hr");

                    command.Parameters.AddWithValue("@HRManagerID", request.HRManagerID);
                    command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
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

        public async Task<int> SaveData_IIP_Events(IIP_EventDataModel request)
        {
            _actionName = "SaveData_IIP_Events(IIP_EventDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_IIP_EventDetails_IU";

                    command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                    command.Parameters.AddWithValue("@EventID", request.EventID);
                    command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                    command.Parameters.AddWithValue("@EventTypeID", request.EventTypeID);
                    command.Parameters.AddWithValue("@Event", request.Event);
                    command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                    command.Parameters.AddWithValue("@EventForID", request.EventForID);
                    command.Parameters.AddWithValue("@EventStartDate", Convert.ToDateTime(request.EventStartDate));
                    command.Parameters.AddWithValue("@EventEndDate", Convert.ToDateTime(request.EventEndDate));
                    command.Parameters.AddWithValue("@FileUpload", request.FileUpload);
                    command.Parameters.AddWithValue("@Dis_FileUpload", request.Dis_FileUpload);
                    command.Parameters.AddWithValue("@EventLevelID", request.EventLevelID);
                    command.Parameters.AddWithValue("@Remark", request.Remark);
                    command.Parameters.AddWithValue("@Semesterlist", JsonConvert.SerializeObject(request.Semesterlist));
                    command.Parameters.AddWithValue("@Branchlist", JsonConvert.SerializeObject(request.Branchlist));
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    command.Parameters.AddWithValue("@SSOID", request.SSOID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MobileNo", request.MobileNo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", request.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Designation", request.Designation ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TrainingDuration", request.TrainingDuration ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AreaOfDomain", request.AreaOfDomain ?? (object)DBNull.Value);

                    command.Parameters.Add("@Return", SqlDbType.Int); // out
                    command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                    _sqlQuery = command.GetSqlExecutableQuery();

                    // Execute the command
                    result = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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

        public async Task<DataTable> GetCompanyEvents(CompanyEventSearchModel body)
        {
            _actionName = "GetCompanyEvents(CompanyEventSearchModel body)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_IIP_GetCompanyEventsData";
                    command.Parameters.AddWithValue("@Action", "GetByCompanyID");

                    command.Parameters.AddWithValue("@CompanyID", body.CompanyID);

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

        public async Task<bool> DeleteEvent_ById(IIP_EventDataModel request)
        {
            _actionName = "DeleteEvent_ById(IIP_EventDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_IIP_GetCompanyEventsData";
                    command.Parameters.AddWithValue("@Action", "Delete_Event");

                    command.Parameters.AddWithValue("@EventID", request.EventID);
                    command.Parameters.AddWithValue("@UserID", request.UserID);
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

        public async Task<IIP_EventDataModel> GetEvent_ById(CompanyEventSearchModel request)
        {
            _actionName = "GetEvent_ById(CompanyEventSearchModel request)";
            try
            {
                //DataTable dataTable = new DataTable();
                DataSet ds = new DataSet();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_IIP_GetCompanyEventsData";
                    command.Parameters.AddWithValue("@Action", "GetEvent_ById");

                    command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                    command.Parameters.AddWithValue("@EventID", request.EventID);

                    _sqlQuery = command.GetSqlExecutableQuery();
                    ds = await command.FillAsync();
                }
                var data = new IIP_EventDataModel();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<IIP_EventDataModel>(ds.Tables[0]);
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            data.Branchlist = CommonFuncationHelper.ConvertDataTable<List<BranchList>>(ds.Tables[1]);
                        }
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            data.Semesterlist = CommonFuncationHelper.ConvertDataTable<List<Semesterlist>>(ds.Tables[2]);
                        }
                    }

                }
                return data;
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

        public async Task<int> ApproveCompanyEvents(IIP_EventDataModel request)
        {
            _actionName = "ApproveCompanyEvents(IIP_EventDataModel request)";
            try
            {
                int result = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_IIP_EventDetails_IU";

                    command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                    command.Parameters.AddWithValue("@EventID", request.EventID);
                    command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                    command.Parameters.AddWithValue("@EventTypeID", request.EventTypeID);
                    command.Parameters.AddWithValue("@Event", request.Event);
                    command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                    command.Parameters.AddWithValue("@EventForID", request.EventForID);
                    command.Parameters.AddWithValue("@EventStartDate", Convert.ToDateTime(request.EventStartDate));
                    command.Parameters.AddWithValue("@EventEndDate", Convert.ToDateTime(request.EventEndDate));

                    command.Parameters.AddWithValue("@Semesterlist", JsonConvert.SerializeObject(request.Semesterlist));
                    command.Parameters.AddWithValue("@Branchlist", JsonConvert.SerializeObject(request.Branchlist));
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                    command.Parameters.Add("@Return", SqlDbType.Int); // out
                    command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                    _sqlQuery = command.GetSqlExecutableQuery();

                    // Execute the command
                    result = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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

        public async Task<int> ApproveCompanyEvents(List<IndustryInstitutePartnershipMasterModels> model)
        {
            _actionName = "ApproveCompanyEvents(List<IndustryInstitutePartnershipMasterModels> model)";
            try
            {
                int result = 0;
                int retval = 0;
                using (var command = await _dbContext.CreateCommandAsync(true))
                {
                    // Set the stored procedure name and type
                    command.CommandText = "USP_IIP_ApproveCompanyEvents";
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate null handling
                    command.Parameters.AddWithValue("@action", "ApproveByAdmin");
                    command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                    command.Parameters.Add("@Return", SqlDbType.Int);// out
                    command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                    _sqlQuery = command.GetSqlExecutableQuery();
                    result = await command.ExecuteNonQueryAsync();

                    retval = Convert.ToInt32(command.Parameters["@Return"].Value);// out
                }
                return retval;
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

        #endregion
    }
}
