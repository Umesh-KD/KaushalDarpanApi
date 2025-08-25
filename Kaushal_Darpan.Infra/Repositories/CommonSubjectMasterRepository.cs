using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CommonSubjectMaster;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CommonSubjectMasterRepository : ICommonSubjectMasterRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CommonSubjectMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CommonSubjectMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<List<CommonSubjectMasterResponseModel>> GetAllData(CommonSubjectMasterSearchModel model)
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
                        command.CommandText = "USP_GetCommonSubject";
                        command.Parameters.AddWithValue("@action", "_getAllData");
                        command.Parameters.AddWithValue("@CommonSubjectName", model.CommonSubjectName);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonSubjectMasterResponseModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonSubjectMasterResponseModel>>(dataTable);
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

        public async Task<M_CommonSubject> GetById(int commonSubjectId)
        {
            _actionName = "GetById(int commonSubjectId)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCommonSubject";
                        command.Parameters.AddWithValue("@action", "_getDataId");
                        command.Parameters.AddWithValue("@commonSubjectId", commonSubjectId);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    var data = new M_CommonSubject();
                    if (ds?.Tables?.Count == 2)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<M_CommonSubject>(ds.Tables[0]);
                        data.commonSubjectDetails = CommonFuncationHelper.ConvertDataTable<List<M_CommonSubject_Details>>(ds.Tables[1]);
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

        public async Task<int> SaveData(M_CommonSubject entity)
        {
            _actionName = "SaveData(M_CommonSubject entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddEditCommonSubject";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditData");
                        command.Parameters.AddWithValue("@CommonSubjectID", entity.CommonSubjectID);
                        command.Parameters.AddWithValue("@CommonSubjectName", entity.CommonSubjectName);
                        command.Parameters.AddWithValue("@SemesterID", entity.SemesterID);
                        command.Parameters.AddWithValue("@Remark", entity.Remark);
                        command.Parameters.AddWithValue("@ActiveStatus", entity.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", entity.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", entity.RTS);
                        command.Parameters.AddWithValue("@CreatedBy", entity.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", entity.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", entity.ModifyDate);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", entity.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", entity.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", entity.EndTermID);
                        command.Parameters.AddWithValue("@SubjectCode", entity.SubjectCode);



                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);// out
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

        public async Task<bool> SaveDataChild(List<M_CommonSubject_Details> entity)
        {
            _actionName = "SaveDataChild(List<M_CommonSubject_Details> entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddEditCommonSubject";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditChildData");
                        command.Parameters.AddWithValue("@Child_Json", JsonConvert.SerializeObject(entity));

                        _sqlQuery = command.GetSqlExecutableQuery();
                            await command.ExecuteNonQueryAsync();
                    }
                    if (result == 0)
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

        public async Task<bool> DeleteById(M_CommonSubject entity)
        {
            _actionName = "DeleteById(M_CommonSubject entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_AddEditCommonSubject";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_deleteById");
                        command.Parameters.AddWithValue("@CommonSubjectID", entity.CommonSubjectID);
                        command.Parameters.AddWithValue("@ActiveStatus", entity.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", entity.DeleteStatus);
                        command.Parameters.AddWithValue("@ModifyBy", entity.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", entity.ModifyDate);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                    }
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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








