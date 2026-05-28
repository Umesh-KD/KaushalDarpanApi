using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.HrMaster;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cms;
using System.ComponentModel.Design;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CompanyMasterRepository : ICompanyMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CompanyMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CompanyMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(CompanyMasterSearchModel body)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCompanyMaster";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null)
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        command.Parameters.AddWithValue("@Status", body.Status);
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
            });
        }

        //public async Task<bool> SaveData(CompanyMasterModels request)
        //{
        //    _actionName = "SaveData(CompanyMasterModels request)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = await _dbContext.CreateCommandAsync(true))
        //            {
        //                // Set the stored procedure name and type
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_PlacementCompanyMaster_IU";


        //                // Add parameters with appropriate null handling
        //                command.Parameters.AddWithValue("@ID", request.ID);
        //                command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
        //                command.Parameters.AddWithValue("@CompanyTypeId", request.CompanyTypeId);
        //                command.Parameters.AddWithValue("@StateID", request.StateID);
        //                command.Parameters.AddWithValue("@Website", request.Website);
        //                command.Parameters.AddWithValue("@Address", request.Address);

        //                command.Parameters.AddWithValue("@Logo", request.CompanyPhoto);
        //                command.Parameters.AddWithValue("@Dis_Name", request.Dis_CompanyName);
        //                command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
        //                command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
        //                command.Parameters.AddWithValue("@TierID", request.TierID);
        //                command.Parameters.AddWithValue("@PackageID", request.PackageID);
        //                command.Parameters.AddWithValue("@ISIIP", request.ISIIP);
        //                command.Parameters.AddWithValue("@ISPlacement", request.ISPlacement);
        //                command.Parameters.AddWithValue("@RoleID", request.RoleID);
        //                command.Parameters.AddWithValue("@MouAdded", 0);

        //                //command.Parameters.AddWithValue("@HRName", request.HRName);
        //                //command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
        //                //command.Parameters.AddWithValue("@EmailId", request.EmailId);

        //                command.Parameters.AddWithValue("@HrList", JsonConvert.SerializeObject(request.ListCompanyHRDetails));

        //                command.Parameters.AddWithValue("@IPAddress", _IPAddress);

        //                command.Parameters.Add("@Return", SqlDbType.Int); // out
        //                command.Parameters["@Return"].Direction = ParameterDirection.Output; // out


        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                // Execute the command
        //                result = await command.ExecuteNonQueryAsync();
        //                result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
        //            }
        //            if (result > 0)
        //                return true;
        //            else
        //                return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            var errorDesc = new ErrorDescription
        //            {
        //                Message = ex.Message,
        //                PageName = _pageName,
        //                ActionName = _actionName,
        //                SqlExecutableQuery = _sqlQuery
        //            };
        //            var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //            throw new Exception(errordetails, ex);
        //        }
        //    });
        //}

        //public async Task<bool> SaveData(CompanyMasterModels request)
        //{
        //    _actionName = "SaveData(CompanyMasterModels request)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = await _dbContext.CreateCommandAsync(true))
        //            {
        //                // Set the stored procedure name and type
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_PlacementCompanyMaster_IU";


        //                // Add parameters with appropriate null handling
        //                command.Parameters.AddWithValue("@ID", request.ID);
        //                command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
        //                command.Parameters.AddWithValue("@CompanyTypeId", request.CompanyTypeId);
        //                command.Parameters.AddWithValue("@StateID", request.StateID);
        //                command.Parameters.AddWithValue("@Website", request.Website);
        //                command.Parameters.AddWithValue("@Address", request.Address);

        //                command.Parameters.AddWithValue("@Logo", request.CompanyPhoto);
        //                command.Parameters.AddWithValue("@Dis_Name", request.Dis_CompanyName);
        //                command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
        //                command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
        //                command.Parameters.AddWithValue("@TierID", request.TierID);
        //                command.Parameters.AddWithValue("@PackageID", request.PackageID);
        //                command.Parameters.AddWithValue("@ISIIP", request.ISIIP);
        //                command.Parameters.AddWithValue("@ISPlacement", request.ISPlacement);
        //                command.Parameters.AddWithValue("@RoleID", request.RoleID);
        //                command.Parameters.AddWithValue("@MouAdded", 0);

        //                //command.Parameters.AddWithValue("@HRName", request.HRName);
        //                //command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
        //                //command.Parameters.AddWithValue("@EmailId", request.EmailId);

        //                command.Parameters.AddWithValue("@HrList", JsonConvert.SerializeObject(request.ListCompanyHRDetails));

        //                command.Parameters.AddWithValue("@IPAddress", _IPAddress);

        //                command.Parameters.Add("@Return", SqlDbType.Int); // out
        //                command.Parameters["@Return"].Direction = ParameterDirection.Output; // out


        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                // Execute the command
        //                result = await command.ExecuteNonQueryAsync();
        //                result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
        //            }
        //            if (result > 0)
        //                return true;
        //            else
        //                return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            var errorDesc = new ErrorDescription
        //            {
        //                Message = ex.Message,
        //                PageName = _pageName,
        //                ActionName = _actionName,
        //                SqlExecutableQuery = _sqlQuery
        //            };
        //            var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //            throw new Exception(errordetails, ex);
        //        }
        //    });
        //}
        public async Task<int> SaveData(CompanyMasterModels request)
        {
            _actionName = "SaveData(CompanyMasterModels request)";

            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PlacementCompanyMaster_IU";

                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CompanyRegNo", request.CompanyRegNo);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@CompanyTypeId", request.CompanyTypeId);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@Website", request.Website ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Address", request.Address ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Logo", request.CompanyPhoto ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Dis_Name", request.Dis_CompanyName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@TierID", request.TierID);
                        command.Parameters.AddWithValue("@PackageID", request.PackageID);
                        command.Parameters.AddWithValue("@ISIIP", request.ISIIP);
                        command.Parameters.AddWithValue("@ISPlacement", request.ISPlacement);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@MouAdded", 0);

                        command.Parameters.AddWithValue(
                            "@HrList",
                            JsonConvert.SerializeObject(request.ListCompanyHRDetails)
                        );

                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();

                        await command.ExecuteNonQueryAsync();

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
            });
        }
        //public async Task<CompanyMasterResponsiveModel> GetById(int PK_ID)
        //{
        //    _actionName = "GetById(int PK_ID)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = await _dbContext.CreateCommandAsync())
        //            {
        //                command.CommandText = " select * from M_PlacementCompanyMaster Where ID='" + PK_ID + "' ";

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataTable = await command.FillAsync_DataTable();
        //            }
        //            var data = new CompanyMasterResponsiveModel();
        //            if (dataTable != null)
        //            {
        //                data = CommonFuncationHelper.ConvertDataTable<CompanyMasterResponsiveModel>(dataTable);
        //            }
        //            return data;
        //        }
        //        catch (Exception ex)
        //        {
        //            var errorDesc = new ErrorDescription
        //            {
        //                Message = ex.Message,
        //                PageName = _pageName,
        //                ActionName = _actionName,
        //                SqlExecutableQuery = _sqlQuery
        //            };
        //            var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //            throw new Exception(errordetails, ex);
        //        }
        //    });
        //}


        //public async Task<CompanyMasterResponsiveModel> GetById(int PK_ID)
        //{
        //    _actionName = "GetById(int PK_ID)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            DataTable dataTable = new DataTable();

        //            using (var command = await _dbContext.CreateCommandAsync())
        //            {

        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_CompanyUpdateAction";
        //                command.Parameters.AddWithValue("@PK_ID", PK_ID);
        //                command.Parameters.AddWithValue("@Action", "_GetDataById");
        //                _sqlQuery =command.GetSqlExecutableQuery();
        //                dataTable=await command.FillAsync_DataTable();

        //            }

        //            var data = new CompanyMasterResponsiveModel();
        //            if (dataTable != null && dataTable.Rows.Count > 0)
        //            {
        //                data = CommonFuncationHelper.ConvertDataTable<CompanyMasterResponsiveModel>(dataTable);
        //            }

        //            return data;
        //        }
        //        catch (Exception ex)
        //        {
        //            var errorDesc = new ErrorDescription
        //            {
        //                Message = ex.Message,
        //                PageName = _pageName,
        //                ActionName = _actionName,
        //                SqlExecutableQuery = _sqlQuery
        //            };
        //            var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //            throw new Exception(errordetails, ex);
        //        }
        //    });
        //}

        public async Task<CompanyMasterModels> GetByID(CompanyMasterSearchModel request)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    //DataTable dataTable = new DataTable();
                    DataSet ds = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CompanyUpdateAction";
                        command.Parameters.AddWithValue("@Action", "_GetDataById");

                        command.Parameters.AddWithValue("@PK_ID", request.ID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    var data = new CompanyMasterModels();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<CompanyMasterModels>(ds.Tables[0]);
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                data.ListCompanyHRDetails = CommonFuncationHelper.ConvertDataTable<List<HRMaster>>(ds.Tables[1]);
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
            });
        }

        public async Task<bool> DeleteDataByID(CompanyMasterModels request)
        {
            _actionName = "DeleteDataByID(CompanyMasterModels request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        //command.CommandType = CommandType.Text;
                       // command.CommandText = $" update [M_PlacementCompanyMaster] set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ID={request.ID}";

                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //result = await command.ExecuteNonQueryAsync();

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CompanyUpdateAction";
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@_IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@Action", "_DeleteDataByID");

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

        public async Task<bool> Save_CompanyValidation_NodalAction(CompanyMaster_Action request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "Save_CompanyValidation_NodalAction(CompanyMaster_Action request)";
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CompanyValidation_NodalAction";
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
            });
        }

        public async Task<DataTable> CompanyValidationList(CompanyMasterSearchModel body)
        {
            _actionName = "CompanyValidationList(CompanyMasterSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CompanyValidationList";
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

        public async Task<DataSet> GetCampusHr_Trail(int CompanyID)
        {
            _actionName = "GetCampusHr_Trail(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    //DataTable dataTable = new DataTable();
                    DataSet ds = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CompanyUpdateAction";
                        command.Parameters.AddWithValue("@Action", "_GetCompanyHrTrailById");

                        command.Parameters.AddWithValue("@PK_ID", CompanyID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    //var data = new CompanyMasterModels();
                    //if (ds != null)
                    //{
                    //    if (ds.Tables.Count > 0)
                    //    {
                    //        data = CommonFuncationHelper.ConvertDataTable<CompanyMasterModels>(ds.Tables[0]);
                    //        if (ds.Tables[1].Rows.Count > 0)
                    //        {
                    //            data.ListCompanyHRDetails = CommonFuncationHelper.ConvertDataTable<List<HRMaster>>(ds.Tables[1]);
                    //        }
                    //    }

                    //}

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



        public async Task<DataTable> CompanyMasterReport(CompanyMasterSearchModel body)
        {
            _actionName = "CompanyMasterReport(CompanyMasterSearchModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CompanyMasterReport";
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


        public async Task<DataTable> GetEligibleStudentListData(EligibleStudentListMasterSearchModel body)
        {
            _actionName = "GetEligibleStudentListData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEligiblePlacementStudentMaster";

                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null)
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@StreamID", body.StreamID);

                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
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


        public async Task<DataTable> GetPlacementAllStudentList(PlacementStudentListSearchModel body)
        {
            _actionName = "GetPlacementAllStudentList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PlacementStudentMaster";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter


                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null)
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);

                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@action", body.action);
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


        public async Task<DataTable> GetDataByStudentId(EligibleStudentForPlacement request)
        {
            _actionName = "GetDataByStudentId(EligibleStudentForPlacement request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        //command.CommandType = CommandType.Text;
                        ////command.CommandText = $" update [M_PlacementCompanyMaster] set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ID={request.ID}";
                        //command.CommandText = $" SELECT ApplicationID,StudentID,SSOID,EnrollmentNo,DOB, StudentName as Name,SemesterID FROM M_StudentMaster WHERE StudentID='{request.ID}' and ActiveStatus = 1 AND SemesterID in (5,6)";
                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //dataTable = await command.FillAsync_DataTable();

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CompanyUpdateAction";
                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@Action", "_GetDataByStudentId");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();



                        //command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = "USP_GetCompanyMaster";
                        ////command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter
                        //command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        //command.Parameters.AddWithValue("@Status", body.Status);
                        //command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        //command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //dataTable = await command.FillAsync_DataTable();
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

        public async Task<int> InsertCompanyMoUDetails(CompanyMoUDetailsModel request)
        {          

            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CompanyMoUDetails_IU";

                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@CompanyId", request.CompanyId);
                        command.Parameters.AddWithValue("@MoUStartDate", request.MoUStartDate);
                        command.Parameters.AddWithValue("@MoUValidTill", request.MoUValidTill);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@MoUDoc", (object?)request.MoUDoc ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DisMoUDoc", (object?)request.DisMoUDoc ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", (object?)request.ModifyBy ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IPAddress", (object?)request.IPAddress ?? DBNull.Value);

                        result = Convert.ToInt32(await command.ExecuteScalarAsync());
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

        public async Task<CompanyMoUDetailsModel> GetCompanyMoUDetails(CompanyMoUDetailsModel Model)
        {
            try
            {
                _actionName = "GetCompanyMoUDetails";
                DataTable dt = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_CompanyMoUDetails_IU";

                    command.Parameters.AddWithValue("@Action", "GETDATA");
                    command.Parameters.AddWithValue("@ID", Model.CompanyId);
                    command.Parameters.AddWithValue("@CompanyId", Model.CompanyId);
                    _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                    dt = await command.FillAsync_DataTable();
                }

                // class
                var data = new CompanyMoUDetailsModel();
                data = CommonFuncationHelper.ConvertDataTable<CompanyMoUDetailsModel>(dt);
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

        public async Task<CompanyMoUDetailsModel> SendForApprove(int CompanyID)
        {
            try
            {
                _actionName = "SendForApprove(int CompanyID)";
                DataTable dt = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_CompanyMoUDetails_IU";

                    command.Parameters.AddWithValue("@Action", "SendForApprove");
                    command.Parameters.AddWithValue("@ID", CompanyID);
                    command.Parameters.AddWithValue("@CompanyId", CompanyID);
                    _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                    dt = await command.FillAsync_DataTable();
                }

                // class
                var data = new CompanyMoUDetailsModel();
                data = CommonFuncationHelper.ConvertDataTable<CompanyMoUDetailsModel>(dt);
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
    }
}
