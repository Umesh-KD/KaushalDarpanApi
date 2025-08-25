using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationStatus;
using Kaushal_Darpan.Models.CommonFunction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class UpwardMovementRepository : IUpwardMovementRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private readonly string _IPAddress;

        public UpwardMovementRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "UpwardMovementRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<bool> UpwardMomentUpdate(UpwardMoment model)
        {
            _actionName = "UpwardMomentUpdate(UpwardMoment model)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $" update BTER_StudentSeatAllotment set IsUpword='{model.IsUpward}', ModifyBy='{model.UserID} ',ModifyDate=GETDATE(), IPAddress='{_IPAddress}'Where AllotmentId={model.AllotmentId}";

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
                var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errorDetails, ex);
            }
        }

        public async Task<DataTable> GetDataItiStudentApplication(ItiStuAppSearchModelUpward searchModel)
        {
            _actionName = "GetDataItiStudentApplication(StudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetApplicationStatus";

                        // Add parameters to the stored procedure from the model

                        command.Parameters.AddWithValue("@ApplicationNo", searchModel.ApplicationNo);
                        command.Parameters.AddWithValue("@MobileNumber", searchModel.MobileNumber);
                        command.Parameters.AddWithValue("@DOB", searchModel.DOB);
                        command.Parameters.AddWithValue("@SSOID", searchModel.SSOID);
                        command.Parameters.AddWithValue("@EndTermID", searchModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", searchModel.DepartmentID);

                        command.Parameters.AddWithValue("@action", searchModel.action);
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
        public async Task<bool> ITIUpwardMomentUpdate(UpwardMoment model)
        {
            _actionName = "ITIUpwardMomentUpdate(UpwardMoment model)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $" update ITI_StudentSeatAllotment set IsUpword='{model.IsUpward}', ModifyBy='{model.UserID} ',ModifyDate=GETDATE(), IPAddress='{_IPAddress}'Where AllotmentId={model.AllotmentId}";

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
                var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errorDetails, ex);
            }
        }

        public async Task<DataTable> GetDataItiUpwardMoment(ItiStuAppSearchModelUpward searchModel)
        {
            _actionName = "GetDataItiUpwardMoment(StudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetApplicationStatus";

                        // Add parameters to the stored procedure from the model

                        command.Parameters.AddWithValue("@ApplicationNo", searchModel.ApplicationNo);
                        command.Parameters.AddWithValue("@MobileNumber", searchModel.MobileNumber);
                        command.Parameters.AddWithValue("@DOB", searchModel.DOB);
                        command.Parameters.AddWithValue("@SSOID", searchModel.SSOID);
                        command.Parameters.AddWithValue("@EndTermID", searchModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", searchModel.DepartmentID);

                        command.Parameters.AddWithValue("@action", searchModel.action);
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
    }
}
