using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.CollegeWiseScholarship;
using Kaushal_Darpan.Models.CompanyMaster;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json.Nodes;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class ApplyDuplicateDocumentRepository :I_ApplyDuplicateDocument
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ApplyDuplicateDocumentRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ApplyDuplicateDocumentRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetApplyDuplicateDocumentTypeList()
        {

            _actionName = "GetApplyDuplicateDocumentTypeList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ApplyDuplicateDocumentDetails";
                        command.Parameters.AddWithValue("@ActionName", "_GetDocumentType"); // Assuming you are using the action filter  
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
        public async Task<DataTable> GetApplyDuplicateDocumentList(ApplyDuplicateDocumentDataModel body)
        {

            _actionName = "GetApplyDuplicateDocumentList(ApplyDuplicateDocumentDataModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ApplyDuplicateDocumentDetails";
                        command.Parameters.AddWithValue("@ActionName", "_GetDetail"); // Assuming you are using the action filter  
                        command.Parameters.AddWithValue("@Student_Id", body.StudentID);
                       //command.Parameters.AddWithValue("@ID", body.ID);
                       //command.Parameters.AddWithValue("@Document_ID", body.DocumentID);
                       //command.Parameters.AddWithValue("@Semester_ID", body.SemesterID);
                       //command.Parameters.AddWithValue("@Department_ID", body.StudentID);
                       //command.Parameters.AddWithValue("@Institute_ID", body.InstituteID);
                       //command.Parameters.AddWithValue("@IsPayment", body.IsPayment);
                       //command.Parameters.AddWithValue("@IsActive", body.IsActive);
                       //command.Parameters.AddWithValue("@IsDelete", body.IsDelete);
                       //command.Parameters.AddWithValue("@CreatedBy", body.createdBy);
                       //command.Parameters.AddWithValue("@ModifyBy", body.modifyBy); 
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


        public async Task<DataTable> GetDuplicateDocInstituteWise(DuplicateDocumentSearchModel body)
        {

            _actionName = "GetDuplicateDocInstituteWise(ApplyDuplicateDocumentDataModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ApplyDuplicateDocumentDetails";
                        command.Parameters.AddWithValue("@ActionName", body.action); // Assuming you are using the action filter                     
                        command.Parameters.AddWithValue("@Name", body.Name);
                        //command.Parameters.AddWithValue("@Document_ID", body.DocumentID);
                        //command.Parameters.AddWithValue("@Semester_ID", body.SemesterID);
                        command.Parameters.AddWithValue("@Department_ID", body.StudentID);
                        command.Parameters.AddWithValue("@Institute_ID", body.InstituteID);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@CourseTypeID", body.Eng_NonEng);
                        //command.Parameters.AddWithValue("@IsPayment", body.IsPayment);
                        //command.Parameters.AddWithValue("@IsActive", body.IsActive);
                        //command.Parameters.AddWithValue("@IsDelete", body.IsDelete);
                        //command.Parameters.AddWithValue("@CreatedBy", body.createdBy);
                        //command.Parameters.AddWithValue("@ModifyBy", body.modifyBy); 
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

        public async Task<bool> SaveDuplicateDocumentDetails(ApplyDuplicateDocumentDataModel model)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveDuplicateDocumentDetails(ApplyDuplicateDocumentDataModel request)";
                try
                {


                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ApplyDuplicateDocumentDetails";
                        command.Parameters.AddWithValue("@ActionName", "_InsertDetail"); 
                        command.Parameters.AddWithValue("@ID", model.ID);
                        command.Parameters.AddWithValue("@Student_Id", model.StudentID);
                        command.Parameters.AddWithValue("@Document_ID", model.DocumentID);
                        command.Parameters.AddWithValue("@Semester_ID", model.SemesterID);
                        command.Parameters.AddWithValue("@Department_ID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Institute_ID", model.InstituteID);
                        command.Parameters.AddWithValue("@IsPayment", model.IsPayment);
                        command.Parameters.AddWithValue("@IsActive", model.IsActive);
                        command.Parameters.AddWithValue("@IsDelete", model.IsDelete);
                        command.Parameters.AddWithValue("@CreatedBy", model.createdBy);
                        command.Parameters.AddWithValue("@ModifyBy", model.modifyBy);
                        command.Parameters.AddWithValue("@ConfigurationTypeID", model.ConfigurationTypeID);
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
    }
}
