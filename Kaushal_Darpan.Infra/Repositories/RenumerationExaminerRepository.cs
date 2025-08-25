using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.GroupCodeAllocation;
using Kaushal_Darpan.Models.RenumerationExaminer;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class RenumerationExaminerRepository : IRenumerationExaminerRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public RenumerationExaminerRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "RenumerationExaminerRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<List<RenumerationExaminerModel>> GetAllData(RenumerationExaminerRequestModel filterModel)
        {
            _actionName = "GetAllData(RenumerationExaminerRequestModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<RenumerationExaminerModel> obj = new List<RenumerationExaminerModel>();
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_RenumerationExaminerGroupCode";

                        // search
                        if (filterModel.RenumerationExaminerStatusID == (int)EnumRenumerationExaminer.Pending)
                        {
                            command.Parameters.AddWithValue("@action", "_getPendingGroupCodeOfExaminer");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@action", "_getSubmittedToJDGroupCodeOfExaminer");
                        }

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@Status", filterModel.RenumerationExaminerStatusID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    if (dt != null)
                    {
                        obj = CommonFuncationHelper.ConvertDataTable<List<RenumerationExaminerModel>>(dt);
                    }

                    return obj;
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


        public async Task<List<TrackStatusDataModel>> GetTrackStatusData(RenumerationExaminerRequestModel filterModel)
        {
            _actionName = "GetAllData(RenumerationExaminerRequestModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<TrackStatusDataModel> obj = new List<TrackStatusDataModel>();
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Trackstatus_GetRenumerationExaminer";
                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "_getTrackstatusRenumerationExaminer");
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    if (dt != null)
                    {
                        obj = CommonFuncationHelper.ConvertDataTable<List<TrackStatusDataModel>>(dt);
                    }

                    return obj;
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

        public async Task<DataTable> GetDataForGeneratePdf(RenumerationExaminerRequestModel filterModel)
        {
            _actionName = "GetDataForGeneratePdf(RenumerationExaminerRequestModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ReptGetExaminerReport";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@action", "_getGroupCodeOfExaminerPDF");
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@ExaminerID", filterModel.ExaminerID);
                        command.Parameters.AddWithValue("@GroupCodeID", filterModel.GroupCodeID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }

                    return dt;
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

        public async Task<int> SaveDataSubmitAndForwardToJD(RenumerationExaminerPDFModel request)
        {
            _actionName = "SaveDataSubmitAndForwardToJD(RenumerationExaminerPDFModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "usp_SaveRenumerationExaminerGroupCode";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters 
                        command.Parameters.AddWithValue("@action", "_saveSubmitAndForwardToJD");
                        command.Parameters.AddWithValue("@GroupCodeID", request.GroupCodeID);
                        command.Parameters.AddWithValue("@ExaminerID", request.ExaminerID);
                        command.Parameters.AddWithValue("@TotalSavedCopy", request.TotalSavedCopy);
                        command.Parameters.AddWithValue("@PerCopyCharge", request.PerCopyCharge);
                        command.Parameters.AddWithValue("@FileName", request.FileName);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);

                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

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

    }
}






