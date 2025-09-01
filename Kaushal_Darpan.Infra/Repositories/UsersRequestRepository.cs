using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Kaushal_Darpan.Models.DispatchFormDataModel;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.UserMaster;
using Newtonsoft.Json;
using System.Data;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class UsersRequestRepository : IUsersRequestRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public UsersRequestRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "UsersRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> UserRequest(RequestSearchModel Model)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_UserRequest";
                        // Add parameters to the stored procedure from the model                        
                        command.Parameters.AddWithValue("@ServiceRequestId", Model.ServiceRequestId);
                        command.Parameters.AddWithValue("@RequestId", Model.RequestId);
                        command.Parameters.AddWithValue("@RequestType", Model.RequestType);
                        command.Parameters.AddWithValue("@UserId", Model.UserId);
                        command.Parameters.AddWithValue("@UserName", Model.UserName);
                        command.Parameters.AddWithValue("@SSOID", Model.SSOID);
                        command.Parameters.AddWithValue("@StatusId", Model.StatusId);
                        command.Parameters.AddWithValue("@PageNumber", Model.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", Model.PageSize);
                        command.Parameters.AddWithValue("@SearchText", Model.SearchText);
                        command.Parameters.AddWithValue("@PostID", Model.PostID);
                        command.Parameters.AddWithValue("@OfficeID", Model.OfficeID);
                        command.Parameters.AddWithValue("@LevelID", Model.LevelID);
                        command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                        command.Parameters.AddWithValue("@DesignationID", Model.DesignationID);
                        command.Parameters.AddWithValue("@InstituteID", Model.InstituteID);
                        command.Parameters.AddWithValue("@RequestRemarks", Model.RequestRemarks);
                        command.Parameters.AddWithValue("@OrderNo", Model.OrderNo);
                        command.Parameters.AddWithValue("@OrderDate", Model.OrderDate);
                        command.Parameters.AddWithValue("@JoiningDate", Model.JoiningDate);
                        command.Parameters.AddWithValue("@RequestDate", Model.RequestDate);
                        command.Parameters.AddWithValue("@CreatedBy", Model.CreatedBy);
                        command.Parameters.AddWithValue("@AttachDocument_file", Model.AttachDocument_file);
                        command.Parameters.AddWithValue("@AttachDocument_fileName", Model.AttachDocument_fileName);
                        command.Parameters.AddWithValue("@IPAddress", Model.IPAddress);
                        command.Parameters.AddWithValue("@NodalStateID", Model.NodalStateID);
                        command.Parameters.AddWithValue("@NodalDistrictID", Model.NodalDistrictID);
                        command.Parameters.AddWithValue("@DivisionID", Model.DivisionID);
                        command.Parameters.AddWithValue("@StaffTypeID", Model.StaffTypeID);
                        command.Parameters.AddWithValue("@ReqRoleID", Model.ReqRoleID);
                        command.Parameters.AddWithValue("@Action", Model.Action);
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


        public async Task<bool> UserRequestUpdateStatus(RequestUpdateStatus request)
        {
            _actionName = "UserRequestUpdateStatus(RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITI_Govt_UserRequestStatuUpdate";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ServiceRequestId", request.ServiceRequestId);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@StatusID", request.StatusIDs);
                        command.Parameters.AddWithValue("@RequestRemarks", request.Remark);
                        command.Parameters.AddWithValue("@RequestType", request.RequestType);
                        command.Parameters.AddWithValue("@LastWorkingDate", request.LastWorkingDate);
                        command.Parameters.AddWithValue("@JoiningDate", request.JoiningDate);

                        command.Parameters.AddWithValue("@IsEOL", request.IsEOL);
                        command.Parameters.AddWithValue("@EOLFromDate", request.EOLFromDate);
                        command.Parameters.AddWithValue("@EOLToDate", request.EOLToDate);
                        command.Parameters.AddWithValue("@IsEnquiries", request.IsEnquiries);
                        //command.Parameters.AddWithValue("@Comments", request.Comments);
                        command.Parameters.AddWithValue("@IsAccount", request.IsAccount);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@AccountComments", request.AccountComments);



                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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

        public async Task<int> StafffJoiningRequestUpdateAndPromotions(RequestUpdateStatus request)
        {
            _actionName = "StafffJoiningRequestUpdateAndPromotions(RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandText = "USP_ITI_GovtUserJoiningApprove";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID ", request.UserID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@IPAddress ", _IPAddress);
                        command.Parameters.AddWithValue("@StatusID", request.StatusIDs);
                        command.Parameters.AddWithValue("@RequestRemarks", request.Remark);

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

        public async Task<DataTable> UserRequestHistory(RequestUserRequestHistory Model)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_UserRequest";
                        // Add parameters to the stored procedure from the model                        
                        command.Parameters.AddWithValue("@ServiceRequestId", Model.ServiceRequestId);                      
                        command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);                      
                        command.Parameters.AddWithValue("@Action", "UserRequestHistory");
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
        //-------------Bter User Request ------------
        public async Task<DataTable> BterEmUserRequest(BterRequestSearchModel Model)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_EM_UserRequest";
                        // Add parameters to the stored procedure from the model                        
                        command.Parameters.AddWithValue("@ServiceRequestId", Model.ServiceRequestId);
                        command.Parameters.AddWithValue("@RequestId", Model.RequestId);
                        command.Parameters.AddWithValue("@RequestType", Model.RequestType);
                        command.Parameters.AddWithValue("@UserId", Model.UserId);
                        command.Parameters.AddWithValue("@UserName", Model.UserName);
                        command.Parameters.AddWithValue("@SSOID", Model.SSOID);
                        command.Parameters.AddWithValue("@StatusId", Model.StatusId);
                        command.Parameters.AddWithValue("@PageNumber", Model.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", Model.PageSize);
                        command.Parameters.AddWithValue("@SearchText", Model.SearchText);
                        command.Parameters.AddWithValue("@PostID", Model.PostID);
                        command.Parameters.AddWithValue("@OfficeID", Model.OfficeID);
                        command.Parameters.AddWithValue("@LevelID", Model.LevelID);
                        command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                        command.Parameters.AddWithValue("@DesignationID", Model.DesignationID);
                        command.Parameters.AddWithValue("@InstituteID", Model.InstituteID);
                        command.Parameters.AddWithValue("@RequestRemarks", Model.RequestRemarks);
                        command.Parameters.AddWithValue("@OrderNo", Model.OrderNo);
                        command.Parameters.AddWithValue("@OrderDate", Model.OrderDate);
                        command.Parameters.AddWithValue("@JoiningDate", Model.JoiningDate);
                        command.Parameters.AddWithValue("@RequestDate", Model.RequestDate);
                        command.Parameters.AddWithValue("@CreatedBy", Model.CreatedBy);
                        command.Parameters.AddWithValue("@AttachDocument_file", Model.AttachDocument_file);
                        command.Parameters.AddWithValue("@AttachDocument_fileName", Model.AttachDocument_fileName);
                        command.Parameters.AddWithValue("@IPAddress", Model.IPAddress);
                        command.Parameters.AddWithValue("@NodalStateID", Model.NodalStateID);
                        command.Parameters.AddWithValue("@NodalDistrictID", Model.NodalDistrictID);
                        command.Parameters.AddWithValue("@DivisionID", Model.DivisionID);
                        command.Parameters.AddWithValue("@StaffTypeID", Model.StaffTypeID);
                        command.Parameters.AddWithValue("@ReqRoleID", Model.ReqRoleID);
                        command.Parameters.AddWithValue("@Action", Model.Action);
                        command.Parameters.AddWithValue("@IsPensionable", Model.IsPensionable);
                        command.Parameters.AddWithValue("@NumberOfPensionable", Model.NumberOfPensionable);
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
        public async Task<bool> BterEmUserRequestUpdateStatus(BterRequestUpdateStatus request)
        {
            _actionName = "UserRequestUpdateStatus(RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_Bter_EM_Govt_UserRequestStatuUpdate";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ServiceRequestId", request.ServiceRequestId);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@StatusID", request.StatusIDs);
                        command.Parameters.AddWithValue("@RequestRemarks", request.Remark);
                        command.Parameters.AddWithValue("@RequestType", request.RequestType);
                        command.Parameters.AddWithValue("@LastWorkingDate", request.LastWorkingDate);
                        command.Parameters.AddWithValue("@JoiningDate", request.JoiningDate);


                        command.Parameters.AddWithValue("@IsEOL", request.IsEOL);
                        command.Parameters.AddWithValue("@EOLFromDate", request.EOLFromDate);
                        command.Parameters.AddWithValue("@EOLToDate", request.EOLToDate);
                        command.Parameters.AddWithValue("@IsEnquiries", request.IsEnquiries);
                        command.Parameters.AddWithValue("@Comments", request.Comments);
                        command.Parameters.AddWithValue("@IsAccount", request.IsAccount);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@AccountComments", request.AccountComments);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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
        public async Task<int> BterEmStafffJoiningRequestUpdateAndPromotions(BterRequestUpdateStatus request)
        {
            _actionName = "StafffJoiningRequestUpdateAndPromotions(RequestUpdateStatus request)";
            return await Task.Run(async () =>
            {

                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandText = "USP_BTER_EM_GovtUserJoiningApprove";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID ", request.UserID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@IPAddress ", _IPAddress);
                        command.Parameters.AddWithValue("@StatusID", request.StatusIDs);
                        command.Parameters.AddWithValue("@RequestRemarks", request.Remark);

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
        public async Task<DataTable> BterEmUserRequestHistory(BterRequestUserRequestHistory Model)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_EM_UserRequest";
                        // Add parameters to the stored procedure from the model                        
                        command.Parameters.AddWithValue("@ServiceRequestId", Model.ServiceRequestId);
                        command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                        command.Parameters.AddWithValue("@Action", "UserRequestHistory");
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


        #region GetJoiningLetter
        public async Task<DataSet> GetJoiningLetter(JoiningLetterSearchModel model)
        {
            _actionName = "GetJoiningLetter(JoiningLetterSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_EM_JoiningAndRelievingLetter";
                        command.Parameters.AddWithValue("@Action", "JoiningLetter");
                        command.Parameters.AddWithValue("@UserId", model.UserID);
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


        #region GetRelievingLetter
        public async Task<DataSet> GetRelievingLetter(RelievingLetterSearchModel model)
        {
            _actionName = "GetJRelievingLetter(RelievingLetterSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_EM_JoiningAndRelievingLetter";
                        command.Parameters.AddWithValue("@Action", "RelievingLetter");
                        command.Parameters.AddWithValue("@UserId", model.UserID);
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


        public async Task<DataTable> BterGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData(Bter_Govt_EM_ZonalOFFICERSSearchDataModel body)
        {
            _actionName = "BterGovtEM_Govt_SanctionedPostInstitutePersonnelBudget_GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Bter_EM_SanctionedPostInstitutePersonnelBudget";
                        command.Parameters.AddWithValue("@action", "GetData");
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
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


        public async Task<DataTable> BterGovtEM_Govt_EstablishUserRequestReportRelievingAndJoing(BterStaffUserRequestReportSearchModel body)
        {
            _actionName = "BterGovtEM_Govt_EstablishUserRequestReportRelievingAndJoing(BterStaffUserRequestReportSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_UserRequestReport";
                        command.Parameters.AddWithValue("@action", body.Action);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);

                        command.Parameters.AddWithValue("@OfficeID", body.OfficeID);
                        command.Parameters.AddWithValue("@PostID", body.PostID);
                        command.Parameters.AddWithValue("@StaffTypeID", body.StaffTypeID);
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

        public async Task<DataTable> GetBter_GetStaffDetailsVRS(BTER_EM_UnlockProfileDataModel filterModel)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStaffDetailsVRS";
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@StaffUserID", filterModel.StaffUserID);
                        command.Parameters.AddWithValue("@StaffID", filterModel.StaffID);
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



        #region  ds

        public async Task<DataTable> GetITI_GetStaffDetailsVRS(ITI_EM_UnlockProfileDataModel filterModel)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIStaffDetailsVRS";
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@StaffUserID", filterModel.StaffUserID);
                        command.Parameters.AddWithValue("@StaffID", filterModel.StaffID);
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


        #endregion
    }
}
