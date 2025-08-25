using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.RenumerationAccounts;
using Kaushal_Darpan.Models.RenumerationJD;
using Newtonsoft.Json;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class RenumerationAccountsRepository : IRenumerationAccounts
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public RenumerationAccountsRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "RenumerationAccountsRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<List<RenumerationAccountsModel>> GetAllData(RenumerationAccountsRequestModel filterModel)
        {
            _actionName = "GetAllData(RenumerationExaminerRequestModel filterModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<RenumerationAccountsModel> obj = new List<RenumerationAccountsModel>();
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_RenumerationAccounts";

                        if (filterModel.RenumerationExaminerStatusID == 43)
                        {
                            command.Parameters.AddWithValue("@action", "_GetApprovedfromAccounts");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@action", "_getRenumerationByStatus");
                        }

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@SSOID", filterModel.SSOID);
                        command.Parameters.AddWithValue("@Status", filterModel.RenumerationExaminerStatusID);
                        command.Parameters.AddWithValue("@RoleID", filterModel.RoleID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();
                    }
                    if (dt != null)
                    {
                        obj = CommonFuncationHelper.ConvertDataTable<List<RenumerationAccountsModel>>(dt);
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
        public async Task<int> SaveDataApprovedFromAccounts(RenumerationAccountsSaveModel request)
        {
            _actionName = "SaveDataApprovedFromAccounts(RenumerationAccountsSaveModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "usp_SaveRenumerationAccounts";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters 
                        command.Parameters.AddWithValue("@action", "_ApprovedFromAccounts");
                        command.Parameters.AddWithValue("@RenumerationExaminerID", request.RenumerationExaminerID);
                        command.Parameters.AddWithValue("@TVNo", request.TVNo);
                        command.Parameters.AddWithValue("@VoucharNo", request.VoucharNo);
                        command.Parameters.AddWithValue("@ClearDate", request.ClearDate);
                        command.Parameters.AddWithValue("@BillStatus", request.BillStatus);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@CreatedBy", request.UserId);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
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
        public async Task<int> HasDblicateTvNoAndVoucharNo(RenumerationAccountsSaveModel request)
        {
            _actionName = "HasDblicateTvNoAndVoucharNo(RenumerationAccountsSaveModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "usp_CheckDublicateRenumeration";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@action", "_CheckDuplicateVoucher");
                        command.Parameters.AddWithValue("@TVNo", request.TVNo);
                        command.Parameters.AddWithValue("@VoucharNo", request.VoucharNo);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        var result1 = await command.ExecuteScalarAsync();
                        int.TryParse(result1?.ToString(),out result);
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

        public async Task<int> UpdateDataApprovedFromAccounts(RenumerationAccountsSaveModel request)
        {
            _actionName = "UpdateDataApprovedFromAccounts(RenumerationAccountsSaveModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "usp_SaveRenumerationAccounts";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters 
                        command.Parameters.AddWithValue("@action", "_UpdateDataApprovedFromAccounts");
                        command.Parameters.AddWithValue("@RenumerationExaminerID", request.RenumerationExaminerID);
                        command.Parameters.AddWithValue("@TVNo", request.TVNo);
                        command.Parameters.AddWithValue("@VoucharNo", request.VoucharNo);
                        command.Parameters.AddWithValue("@ClearDate", request.ClearDate);
                        command.Parameters.AddWithValue("@BillStatus", request.BillStatus);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@CreatedBy", request.UserId);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@IsBillGenerated", request.IsBillGenerated);
                        command.Parameters.AddWithValue("@BillGeneratedDate", request.BillGeneratedDate);

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
