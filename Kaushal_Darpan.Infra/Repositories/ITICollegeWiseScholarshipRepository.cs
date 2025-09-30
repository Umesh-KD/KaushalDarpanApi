using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITICollegeWiseScholarship;
using Kaushal_Darpan.Models.CompanyMaster;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json.Nodes;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITICollegeWiseScholarshipRepository : I_ITICollegeWiseScholarshipRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITICollegeWiseScholarshipRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CompanyMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
         
        public async Task<bool> SaveCollegeWiseScholarshipDetails(List<SaveITICollegeWiseScholershipDetails> model)
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



        public async Task<DataTable> GetCollegeWiseScholarshipList(ITICollegeWiseScholarshipSearchModel body)
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
                        command.Parameters.AddWithValue("@Action", "GetScheme");
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
                        command.Parameters.AddWithValue("@Action", "GetType");
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
                        command.Parameters.AddWithValue("@Action", "GetDataByStudentId");
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

    }
}
