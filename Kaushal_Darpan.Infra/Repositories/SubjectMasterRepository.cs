using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.StreamMaster;
using Kaushal_Darpan.Models.SubjectMaster;
using Kaushal_Darpan.Models.TimeTable;
using Newtonsoft.Json;
using System.Data;
using System.Windows.Input;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class SubjectMasterRepository : ISubjectMasterRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public SubjectMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "SubjectMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(SubjectSearchModel model)
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
                        command.CommandText = "USP_SubjectMasterList";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", model.CourseType);

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
        public async Task<SubjectMaster> GetById(int PK_ID, int DepartmentID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.CommandText = " select * from M_StreamMaster Where StreamID='" + PK_ID + "' "; ;
                        command.CommandText = "GetSubjectMasterById";
                        command.Parameters.AddWithValue("@SubjectID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new SubjectMaster();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<SubjectMaster>(dataTable);
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

        public async Task<bool> SaveData(SubjectMaster request)
        {
            _actionName = "SaveData(SubjectMaster request)";
                return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; 
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SubjectMaster_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
                        command.Parameters.AddWithValue("@SubjectName", request.SubjectName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SubjectCode", request.SubjectCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SemesterId", request.SemesterID);
                        command.Parameters.AddWithValue("@StreamId", request.StreamID);
                        command.Parameters.AddWithValue("@isParent", request.isParent);
                        command.Parameters.AddWithValue("@IsInternalAssisment", request.is_ia);
                        command.Parameters.AddWithValue("@IsStudentCenteredActivity", request.is_sca);
                        command.Parameters.AddWithValue("@IsPractical", request.is_pr);
                        command.Parameters.AddWithValue("@IsTheory", request.is_th);
                        command.Parameters.AddWithValue("@MaxInternalAssisment", request.max_ia);
                        command.Parameters.AddWithValue("@MaxPractical", request.max_pr);
                        command.Parameters.AddWithValue("@MaxTheory", request.max_th);
                        command.Parameters.AddWithValue("@MaxStudentCenteredActivity", request.sca_grade);
                        command.Parameters.AddWithValue("@ParentId", request.ParentID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", request.CourseType);


                        command.Parameters.AddWithValue("@SubjectCategory", request.SubjectType);


                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);


                        command.Parameters.AddWithValue("@CreatedBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@Credits", request.SubjectCredits);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                 


                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);

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
        public async Task<bool> DeleteDataByID(SubjectMaster request)
        {
            _actionName = "DeleteDataByID(SubjectMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "USP_DeleteSubject";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
                        command.Parameters.AddWithValue("@isParent", request.isParent);
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
            });
        }



        public async Task<bool> SaveParentData(ParentSubjectMap request)
        {
            _actionName = "SaveParentData(SubjectMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddParentSubject";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@SubjectID", request.SubjectID);
                        command.Parameters.AddWithValue("@rowjson", JsonConvert.SerializeObject(request.SubjectList));


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


        public async Task<ParentSubjectMap> GetChildSubject(int PK_ID)
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
                        command.CommandText = "USP_GetChildSubject";
                        command.Parameters.AddWithValue("@SubjectID", PK_ID);
                     
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ParentSubjectMap();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ParentSubjectMap>(dataSet.Tables[0]);
                            data.SubjectList = CommonFuncationHelper.ConvertDataTable<List<SubjectList>>(dataSet.Tables[1]);
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

    }
}








