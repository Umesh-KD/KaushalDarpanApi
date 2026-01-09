using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CollegeWiseScholarship;
using Kaushal_Darpan.Models.CompanyMaster;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json.Nodes;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CollegeWiseScholarshipRepository : ICollegeWiseScholarshipRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CollegeWiseScholarshipRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CompanyMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        //public async Task<DataTable> GetAllData(CompanyMasterSearchModel body)
        //{
        //    _actionName = "GetAllData()";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = await _dbContext.CreateCommandAsync())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_GetCompanyMaster";
        //                //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter
        //                command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
        //                if (body.Name != null)
        //                {
        //                    command.Parameters.AddWithValue("@Name", body.Name);
        //                }
        //                command.Parameters.AddWithValue("@Status", body.Status);
        //                command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
        //                command.Parameters.AddWithValue("@RoleID", body.RoleID);
        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataTable = await command.FillAsync_DataTable();
        //            }

        //            return dataTable;
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

        //                command.Parameters.AddWithValue("@HRName", request.HRName);
        //                command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
        //                command.Parameters.AddWithValue("@EmailId", request.EmailId);

        //                command.Parameters.AddWithValue("@IPAddress", _IPAddress);

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                // Execute the command
        //                result = await command.ExecuteNonQueryAsync();
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
        ////public async Task<CompanyMasterResponsiveModel> GetById(int PK_ID)
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
        //            //    command.CommandText = @"
        //            //SELECT pcm.*, hr.Name As HRName, hr.EmailId,hr.MobileNo
        //            //FROM M_PlacementCompanyMaster pcm
        //            //LEFT JOIN M_HRManagerMaster hr ON pcm.ID = hr.PlacementCompanyID
        //            //WHERE pcm.ID = @PK_ID";

        //            //    // Parameterize the query
        //            //    var parameter = command.CreateParameter();
        //            //    parameter.ParameterName = "@PK_ID";
        //            //    parameter.Value = PK_ID;
        //            //    command.Parameters.Add(parameter);

        //            //    _sqlQuery = command.GetSqlExecutableQuery();
        //            //    dataTable = await command.FillAsync_DataTable();

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


        //public async Task<bool> DeleteDataByID(CompanyMasterModels request)
        //{
        //    _actionName = "DeleteDataByID(CompanyMasterModels request)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = await _dbContext.CreateCommandAsync(true))
        //            {
        //                //command.CommandType = CommandType.Text;
        //               // command.CommandText = $" update [M_PlacementCompanyMaster] set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ID={request.ID}";

        //                //_sqlQuery = command.GetSqlExecutableQuery();
        //                //result = await command.ExecuteNonQueryAsync();

        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_CompanyUpdateAction";
        //                command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
        //                command.Parameters.AddWithValue("@ID", request.ID);
        //                command.Parameters.AddWithValue("@_IPAddress", _IPAddress);
        //                command.Parameters.AddWithValue("@Action", "_DeleteDataByID");

        //                _sqlQuery = command.GetSqlExecutableQuery();// sql query
        //                result = await command.ExecuteNonQueryAsync();
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

        public async Task<bool> SaveCollegeWiseScholarshipDetails(List<SaveCollegeWiseScholershipDetails> model)
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
                        command.CommandText = "USP_CollegeWiseScholarship";
                        command.Parameters.AddWithValue("@action", "SaveData");
                        command.Parameters.AddWithValue("@data", JsonConvert.SerializeObject(model));

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

        //public async Task<DataTable> CompanyValidationList(CompanyMasterSearchModel body)
        //{
        //    _actionName = "CompanyValidationList(CompanyMasterSearchModel body)";
        //    try
        //    {
        //        return await Task.Run(async () =>
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = await _dbContext.CreateCommandAsync())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_CompanyValidationList";
        //                command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
        //                if (body.Name != null)
        //                {
        //                    command.Parameters.AddWithValue("@Name", body.Name);
        //                }
        //                command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
        //                command.Parameters.AddWithValue("@RoleID", body.RoleID);
        //                command.Parameters.AddWithValue("@Status", body.Status);
        //                _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
        //                dataTable = await command.FillAsync_DataTable();
        //            }
        //            return dataTable;
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorDesc = new ErrorDescription
        //        {
        //            Message = ex.Message,
        //            PageName = _pageName,
        //            ActionName = _actionName,
        //            SqlExecutableQuery = _sqlQuery
        //        };
        //        var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //        throw new Exception(errordetails, ex);
        //    }
        //}




        //public async Task<DataTable> CompanyMasterReport(CompanyMasterSearchModel body)
        //{
        //    _actionName = "CompanyMasterReport(CompanyMasterSearchModel body)";
        //    try
        //    {
        //        return await Task.Run(async () =>
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = await _dbContext.CreateCommandAsync())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_CompanyMasterReport";
        //                command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
        //                if (body.Name != null)
        //                {
        //                    command.Parameters.AddWithValue("@Name", body.Name);
        //                }
        //                command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
        //                command.Parameters.AddWithValue("@RoleID", body.RoleID);
        //                command.Parameters.AddWithValue("@Status", body.Status);
        //                _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
        //                dataTable = await command.FillAsync_DataTable();
        //            }
        //            return dataTable;
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorDesc = new ErrorDescription
        //        {
        //            Message = ex.Message,
        //            PageName = _pageName,
        //            ActionName = _actionName,
        //            SqlExecutableQuery = _sqlQuery
        //        };
        //        var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //        throw new Exception(errordetails, ex);
        //    }
        //}


        public async Task<DataTable> GetCollegeWiseScholarshipList(CollegeWiseScholarshipSearchModel body)
        {
            _actionName = "GetCollegeWiseScholarshipList(CollegeWiseScholarshipSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeWiseScholarship";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter
                        
                        
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null)
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        if (body.Enrollment != null)
                        {
                            command.Parameters.AddWithValue("@Enrollment", body.Enrollment);
                        }
                        if (body.Category != null)
                        {
                            command.Parameters.AddWithValue("@Category", body.Category);
                        }
                        if (body.ScholarshipMode != null)
                        {
                            command.Parameters.AddWithValue("@ScholarshipMode", body.ScholarshipMode);
                        }
                        command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);

                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@action", "_GetCollegeWiseScholarshipList");
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
       
        public async Task<DataSet> GetCollegeWiseScholarshipListReport(CollegeWiseScholarshipSearchModel body)
        {
            _actionName = "_GetCollegeWiseScholarshipListRpt(CollegeWiseScholarshipSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    //DataTable dataTable = new DataTable();
                    DataSet dataset = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeWiseScholarship";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter


                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null)
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        if (body.Enrollment != null)
                        {
                            command.Parameters.AddWithValue("@Enrollment", body.Enrollment);
                        }
                        if (body.Category != null)
                        {
                            command.Parameters.AddWithValue("@Category", body.Category);
                        }
                        if (body.SchemeName != null)
                        {
                            command.Parameters.AddWithValue("@SchemeName", body.SchemeName);
                        }
                        if (body.CourseType != null)
                        {
                            command.Parameters.AddWithValue("@CourseType", body.CourseType);
                        } 
                        command.Parameters.AddWithValue("@GenderID", body.GenderID);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);

                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@action", "_GetCollegeWiseScholarshipListRpt");
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataset = await command.FillAsync();
                    }

                    return dataset;
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

        public async Task<DataTable> GetSchemeList()
        {
            _actionName = "GetSchemeList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeWiseScholarship";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter


                        //command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        //if (body.Name != null)
                        //{
                        //    command.Parameters.AddWithValue("@Name", body.Name);
                        //}
                        //command.Parameters.AddWithValue("@Status", body.Status);
                        //command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        //command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        //command.Parameters.AddWithValue("@InstituteID", body.InstituteID);

                        //command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        //command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        //command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        //command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@action", "GetScheme");
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


        public async Task<DataTable> GetTypeList()
        {
            _actionName = "GetTypeList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeWiseScholarship";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter


                        //command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        //if (body.Name != null)
                        //{
                        //    command.Parameters.AddWithValue("@Name", body.Name);
                        //}
                        //command.Parameters.AddWithValue("@Status", body.Status);
                        //command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        //command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        //command.Parameters.AddWithValue("@InstituteID", body.InstituteID);

                        //command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        //command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        //command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        //command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@action", "GetType");
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


        public async Task<DataTable> GetDetailList(int id)
        {
            _actionName = "GetDetailList(int id)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeWiseScholarship";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter


                        //command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        //if (body.Name != null)
                        //{
                        //    command.Parameters.AddWithValue("@Name", body.Name);
                        //}
                        //command.Parameters.AddWithValue("@Status", body.Status);
                        //command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        //command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        //command.Parameters.AddWithValue("@InstituteID", body.InstituteID);

                        //command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        //command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        //command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        //command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@action", "GetDataByStudentId");
                        command.Parameters.AddWithValue("@studentId", id);
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


        //public async Task<DataTable> GetDataByStudentId(EligibleStudentForPlacement request)
        //{
        //    _actionName = "GetDataByStudentId(EligibleStudentForPlacement request)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = await _dbContext.CreateCommandAsync())
        //            {
        //                //command.CommandType = CommandType.Text;
        //                ////command.CommandText = $" update [M_PlacementCompanyMaster] set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ID={request.ID}";
        //                //command.CommandText = $" SELECT ApplicationID,StudentID,SSOID,EnrollmentNo,DOB, StudentName as Name,SemesterID FROM M_StudentMaster WHERE StudentID='{request.ID}' and ActiveStatus = 1 AND SemesterID in (5,6)";
        //                //_sqlQuery = command.GetSqlExecutableQuery();
        //                //dataTable = await command.FillAsync_DataTable();

        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_CompanyUpdateAction";
        //                command.Parameters.AddWithValue("@ID", request.ID);
        //                command.Parameters.AddWithValue("@Action", "_GetDataByStudentId");
        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataTable = await command.FillAsync_DataTable();



        //                //command.CommandType = CommandType.StoredProcedure;
        //                //command.CommandText = "USP_GetCompanyMaster";
        //                ////command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter
        //                //command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
        //                //command.Parameters.AddWithValue("@Status", body.Status);
        //                //command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
        //                //command.Parameters.AddWithValue("@RoleID", body.RoleID);
        //                //_sqlQuery = command.GetSqlExecutableQuery();
        //                //dataTable = await command.FillAsync_DataTable();
        //            }
        //            return dataTable;
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

        public async Task<string> GetScholarship1(ScholarshipRequest body)
        {
            _actionName = "GetCollegeWiseScholarshipList(CollegeWiseScholarshipSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    var client = new HttpClient();
                    //var request = new HttpRequestMessage(HttpMethod.Post, "https://sjmsnew.rajasthan.gov.in/ScholarShipApi/api/Scholarship?RequestId=60787706086");
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://sjmsnew.rajasthan.gov.in/ScholarShipApi/api/Scholarship?RequestId=" + body.RequestId);
                    var content = new StringContent("{\"RequestType\": \"" + body.RequestType + "\",\"CollegeType\": \"" + body.CollegeType + "\"}", null, "application/json");
                    //var content = new StringContent("{\"RequestType\": \"Janaadhaar_Aadhaar\",\"CollegeType\": \"ITI\"}", null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    string responseString = await response.Content.ReadAsStringAsync();
                    return responseString;
          
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
