using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CampusPostMasterRepository : ICampusPostMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CampusPostMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CampusPostMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetAllData(string SSOID, int DepartmentID)
        {
            _actionName = "GetAllData(string SSOID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CampusPostMasterList";
                        command.Parameters.AddWithValue("@RoleID", SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
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

        public async Task<List<CampusPostMasterModel>> GetNameWiseData(int PK_ID, int DepartmentID)
        {
            _actionName = "GetNameWiseData(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetDataCampusPost";
                        command.Parameters.AddWithValue("@ID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CampusPostMasterModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CampusPostMasterModel>>(dataTable);
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
            });
        }

        public async Task<CampusPostMasterModel> GetById(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CampusPostMaster_Edit";
                        command.Parameters.AddWithValue("@PostID", PK_ID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new CampusPostMasterModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<CampusPostMasterModel>(dataSet.Tables[0]);
                            data.EligibilityCriteriaModel = CommonFuncationHelper.ConvertDataTable<List<CampusPostMaster_EligibilityCriteria>>(dataSet.Tables[1]);
                        }
                    }
                    return data;
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
        public async Task<bool> SaveData(CampusPostMasterModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveData(CampusPostMasterModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CampusPostMaster_IU";

                        command.Parameters.AddWithValue("@PostID", request.PostID);
                        command.Parameters.AddWithValue("@PostNo", request.PostNo);
                        command.Parameters.AddWithValue("@PostCollegeID", request.PostCollegeID);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@PostSSOID", request.PostSSOID);
                        command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                        command.Parameters.AddWithValue("@Website", request.Website);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@Address", request.Address);
                        command.Parameters.AddWithValue("@HR_Name", request.HR_Name);
                        command.Parameters.AddWithValue("@HR_MobileNo", request.HR_MobileNo);
                        command.Parameters.AddWithValue("@HR_Email", request.HR_Email);
                        command.Parameters.AddWithValue("@HR_SSOID", request.HR_SSOID);
                        command.Parameters.AddWithValue("@CampusVenue", request.CampusVenue);
                        command.Parameters.AddWithValue("@CampusVenueLocation", request.CampusVenueLocation);
                        command.Parameters.AddWithValue("@JobDiscription", request.JobDiscription);
                        command.Parameters.AddWithValue("@Dis_JobDiscription", request.Dis_JobDiscription);
                        command.Parameters.AddWithValue("@CampusFromDate", request.CampusFromDate);
                        command.Parameters.AddWithValue("@CampusFromTime", request.CampusFromTime);
                        command.Parameters.AddWithValue("@CampusToDate", request.CampusToDate);
                        command.Parameters.AddWithValue("@CampusToTime", request.CampusToTime);
                        command.Parameters.AddWithValue("@CampusAddress", request.CampusAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@CampusPostType", request.CampusPostType);
                        command.Parameters.AddWithValue("@EligibilityCriteria_Str", JsonConvert.SerializeObject(request.EligibilityCriteriaModel));


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
            });
        }

        public async Task<bool> Save_CampusValidation_NodalAction(CampusPostMaster_Action request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "Save_CampusValidation_NodalAction(CampusPostMaster_Action request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CampusValidation_NodalAction";
                        command.Parameters.AddWithValue("@PostID", request.PostID);
                        command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                        command.Parameters.AddWithValue("@PostCollegeID", request.PostCollegeID);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@ActionRemarks", request.ActionRemarks);
                        command.Parameters.AddWithValue("@SuspendDocumnet", request.SuspendDocumnet);
                        command.Parameters.AddWithValue("@Dis_SuspendDoc", request.Dis_SuspendDoc);
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
            });
        }
        public async Task<bool> UpdateData(CampusPostMasterModel request)
        {

            return await Task.Run(async () =>
            {
                _actionName = "UpdateData(CampusPostMasterModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CampusPostMaster_IU";

                        command.Parameters.AddWithValue("@PostID", request.PostID);
                        command.Parameters.AddWithValue("@PostNo", request.PostNo);
                        command.Parameters.AddWithValue("@PostCollegeID", request.PostCollegeID);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@PostSSOID", request.PostSSOID);
                        command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                        command.Parameters.AddWithValue("@Website", request.Website);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@Address", request.Address);
                        command.Parameters.AddWithValue("@HR_Name", request.HR_Name);
                        command.Parameters.AddWithValue("@HR_MobileNo", request.HR_MobileNo);
                        command.Parameters.AddWithValue("@HR_Email", request.HR_Email);
                        command.Parameters.AddWithValue("@HR_SSOID", request.HR_SSOID);
                        command.Parameters.AddWithValue("@CampusVenue", request.CampusVenue);
                        command.Parameters.AddWithValue("@CampusFromDate", request.CampusFromDate);
                        command.Parameters.AddWithValue("@CampusFromTime", request.CampusFromTime);
                        command.Parameters.AddWithValue("@CampusToDate", request.CampusToDate);
                        command.Parameters.AddWithValue("@CampusToTime", request.CampusToTime);
                        command.Parameters.AddWithValue("@CampusAddress", request.CampusAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CampusPostType", request.CampusPostType);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@EligibilityCriteria_Str", JsonConvert.SerializeObject(request.EligibilityCriteriaModel));
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
            });
        }
        public async Task<bool> DeleteDataByID(CampusPostMasterModel request)
        {

            int result = 0;
            _actionName = "DeleteDataByID(CampusPostMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        _sqlQuery = $" update M_CampusPostMaster set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'                         Where PostID={request.PostID}";
                        _sqlQuery += $" update M_CampusPostMaster_EligibilityCriteria set ActiveStatus=0,DeleteStatus=1 Where PostID={request.PostID}";
                        command.CommandText = _sqlQuery;
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
            });
        }


        public async Task<DataTable> CampusValidationList(int CompanyID, int CollegeID, string Status, int DepartmentID)
        {
            _actionName = "CampusValidationList(int CollegeID, string Status)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CampusValidationList";
                        command.Parameters.AddWithValue("@CompanyID", CompanyID);
                        command.Parameters.AddWithValue("@CollegeID", CollegeID);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
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


        public async Task<DataTable> GetAllSignedCopyData(SignedCopyOfResultSearchModel signedCopy)
        {
            _actionName = " GetAllSignedCopyData(SignedCopyOfResultSearchModel signedCopy)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SignedCopyOfResult";
                        command.Parameters.AddWithValue("@DepartmentID", signedCopy.DepartmentID);
                        command.Parameters.AddWithValue("@RoleID", signedCopy.RoleID);
                        command.Parameters.AddWithValue("@CreatedById", signedCopy.CreatedBy);
                        command.Parameters.AddWithValue("@CampusPostID", signedCopy.CampusPostID);
                        command.Parameters.AddWithValue("@Action", "GetAll");
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

        public async Task<SignedCopyOfResultModel> GetSignedCopyById(int PK_ID)
        {
            _actionName = "GetSignedCopyById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        _sqlQuery = $"Select * from [dbo].[M_SignedCopyOfResult]  where SignedCopyOfResultID="+ PK_ID + "";
                        
                        command.CommandText = _sqlQuery;
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new SignedCopyOfResultModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<SignedCopyOfResultModel>(dataSet.Tables[0]);
                          
                        }
                    }
                    return data;
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

        public async Task<bool> DeleteSignedCopyDataByID(SignedCopyOfResultSearchModel request)
        {

            int result = 0;
            _actionName = "DeleteSignedCopyDataByID(SignedCopyOfResultSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        _sqlQuery = $" Update  [dbo].[M_SignedCopyOfResult] set ActiveStatus=0 , DeleteStatus=1 ,ModifyBy="+ request.ModifyBy+ ", ModifyDate= getdate()  where SignedCopyOfResultID=" + request.SignedCopyOfResultID+ "";
                        command.CommandText = _sqlQuery;
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
            });
        }

        public async Task<int> SaveSignedCopyData(SignedCopyOfResultModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveSignedCopyData(SignedCopyOfResultModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SignedCopyOfResult_IU";
                        command.Parameters.AddWithValue("@SignedCopyOfResultID", request.SignedCopyOfResultID);
                        command.Parameters.AddWithValue("@CampusPostID", request.CampusPostID);
                        command.Parameters.AddWithValue("@HRID", request.HRID);
                        command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@FileName", request.FileName);
                        command.Parameters.AddWithValue("@Dis_File", request.Dis_File);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@FileType", request.FileTypeID);
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
            });
        }


        public async Task<DataTable> CampusHistoryList(int CompanyID, int CollegeID, string Status, int DepartmentID)
        {
            _actionName = "CampusHistoryList(int CollegeID, string Status)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CampusValidation_History_List";
                        command.Parameters.AddWithValue("@CompanyID", CompanyID);
                        command.Parameters.AddWithValue("@CollegeID", CollegeID);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
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
    }
}
