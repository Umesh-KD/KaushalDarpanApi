using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CommonModel;
using Kaushal_Darpan.Models.SMSConfigurationSetting;
using Kaushal_Darpan.Models.Student;
using Microsoft.Data.SqlClient;
using System.Data;
using static QRCoder.PayloadGenerator.SwissQrCode;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class SMSMailRepository : ISMSMailRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private readonly DataTable _dataTable;
        public SMSMailRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "SMSMailRepository";
        }
        public async Task<SMSConfigurationSettingModel> GetSMSConfigurationSetting()
        {
            _actionName = "GetSMSConfigurationSetting()";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetSMSConfiguration";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dt = await command.FillAsync_DataTable();

                    }
                    var data = new SMSConfigurationSettingModel();
                    if (dt != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<SMSConfigurationSettingModel>(dt);
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
        public async Task<DataTable> GetSMSTemplateByMessageType(string MessageType)
        {
            _actionName = "GetSMSTemplateByMessageType(string MessageType)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetSMSTemplateByMessageType";
                        command.Parameters.AddWithValue("@MessageType", MessageType);

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
        public async Task<DataTable> GetAllUnsendSMS()
        {
            _actionName = "GetAllUnsendSMS()";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandText = " select  * from Trn_SendSMS where IsSend=0 ";

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
        public async Task<bool> UpdateUnsendSMSById(string AID, string response)
        {
            _actionName = "UpdateUnsendSMSById(string AID, string response)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandText = $"update Trn_SendSMS set SMS_Status='{response}',IsSend=1,Sending_RTS=Getdate() Where aid='{AID}'";

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

        public async Task<int> SendSMSForStudentEnrollmentData(List<ForSMSEnrollmentStudentMarkedModel> model)
        {
            _actionName = "SendSMSForStudentEnrollmentData(List<ForSMSEnrollmentStudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;


                    //using (var command = await _dbContext.CreateCommandAsync(true))
                    //{
                    //    // Set the stored procedure name and type
                    //    command.CommandText = "USP_SaveAdmittedStudents";
                    //    command.CommandType = CommandType.StoredProcedure;

                    //    // Add parameters with appropriate null handling
                    //    command.Parameters.AddWithValue("@action", "_addStudentAdmittedData");
                    //    command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));

                    //    command.Parameters.Add("@Retval", SqlDbType.Int);// out
                    //    command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                    //    _sqlQuery = command.GetSqlExecutableQuery();
                    //    result = await command.ExecuteNonQueryAsync();

                    //    retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
                    //}
                    return 0;
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

        public async Task<DataTable> GetEmailTemplateByTemplateCode(string TemplateCode)
        {
            _actionName = "GetEmailTemplateByTemplateCode(string TemplateCode)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEmailTemplate";
                        command.Parameters.AddWithValue("@TemplateCode", TemplateCode);

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

        public async Task<DataTable> GetDynamicData(string SQLQuery)
        {
            _actionName = "GetEmailTemplateByTemplateCode(string TemplateCode)";
            return await Task.Run(async () =>
            {
                try
                {
                    var dt = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "ExecuteStoredQuery";
                        command.Parameters.AddWithValue("@SQL", SQLQuery);

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

        public async Task<long> SaveEmailLog(EmailLog ModelEmailLog)
        {
            _actionName = "SaveEmailLog(EmailLog ModelEmailLog)";
            return await Task.Run(async () =>
            {
                try
                {
                    long result = 0;                  
                   
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SaveEmailLog";

                        command.Parameters.AddWithValue("@TemplateCode",(object?)ModelEmailLog.TemplateCode ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ToEmail",(object?)ModelEmailLog.ToEmail ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CcEmail",(object?)ModelEmailLog.CcEmail ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BccEmail",(object?)ModelEmailLog.BccEmail ?? DBNull.Value);
                        command.Parameters.AddWithValue("@EmailSubject", (object?)ModelEmailLog.EmailSubject ?? DBNull.Value);
                        command.Parameters.AddWithValue("@EmailBody",(object?)ModelEmailLog.EmailBody ?? DBNull.Value);
                        command.Parameters.AddWithValue("@EmailAttachment",(object?)ModelEmailLog.EmailAttachment ?? DBNull.Value);
                        command.Parameters.AddWithValue("@EmailStatus", ModelEmailLog.EmailStatus);
                        command.Parameters.AddWithValue("@ErrorMessage",(object?)ModelEmailLog.ErrorMessage ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReferenceID",(object?)ModelEmailLog.ReferenceID ?? DBNull.Value);
                        command.Parameters.AddWithValue("@SentDate",(object?)ModelEmailLog.SentDate ?? DBNull.Value);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        var outputId = new SqlParameter("@ID", SqlDbType.BigInt)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputId);

                        await command.ExecuteNonQueryAsync();

                         result = Convert.ToInt64(outputId.Value);

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


        public async Task<bool> SaveEmailTemplate(EmailTemplateModel request)
        {
            _actionName = "SaveEmailTemplate(EmailTemplateModel request)";

            try
            {
                int result = 0;

                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandText = "USP_EmailTemplate_AddUpdate";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", request.ID);
                    command.Parameters.AddWithValue("@TemplateCode", (object?)request.TemplateCode ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TemplateName", (object?)request.TemplateName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@EmailSubject", (object?)request.EmailSubject ?? DBNull.Value);
                    command.Parameters.AddWithValue("@EmailBody", (object?)request.EmailBody ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ToQuery", (object?)request.ToQuery ?? DBNull.Value);
                    command.Parameters.AddWithValue("@CcQuery", (object?)request.CcQuery ?? DBNull.Value);
                    command.Parameters.AddWithValue("@BccQuery", (object?)request.BccQuery ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DataQuery", (object?)request.DataQuery ?? DBNull.Value);
                    command.Parameters.AddWithValue("@EmailAttachment", (object?)request.EmailAttachment ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsHtml", request.IsHtml);
                    command.Parameters.AddWithValue("@IsActive", request.IsActive);
                    command.Parameters.AddWithValue("@CreatedBy", (object?)request.CreatedBy ?? request.UserID );
                    command.Parameters.AddWithValue("@UpdatedBy", (object?)request.UpdatedBy ?? request.UserID );
                    //command.Parameters.AddWithValue("@IPAddress", (object?)_IPAddress);

                    command.Parameters.Add("@Return", SqlDbType.Int);
                    command.Parameters["@Return"].Direction = ParameterDirection.Output;

                    _sqlQuery = command.GetSqlExecutableQuery();
                    await command.ExecuteNonQueryAsync();

                    result = Convert.ToInt32(command.Parameters["@Return"].Value);
                }

                return result > 0;
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

        //Task<List<EmailTemplateModel>>
        public async Task<DataTable> GetEmailTemplateList(int id = 0, string? searchText = null)
        {
            _actionName = "GetEmailTemplateList(int id, string searchText)";
            var DataTable = new DataTable();

            try
            {
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandText = "USP_EmailTemplate_GetList";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@SearchText", (object?)searchText ?? DBNull.Value);

                    _sqlQuery = command.GetSqlExecutableQuery();

                    //using (var reader = await command.ExecuteReaderAsync())
                    //{
                    //    while (await reader.ReadAsync())
                    //    {
                    //        list.Add(new EmailTemplateModel
                    //        {
                    //            ID = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                    //            TemplateCode = reader["TemplateCode"]?.ToString() ?? "",
                    //            TemplateName = reader["TemplateName"]?.ToString() ?? "",
                    //            EmailSubject = reader["EmailSubject"]?.ToString() ?? "",
                    //            EmailBody = reader["EmailBody"]?.ToString() ?? "",
                    //            ToQuery = reader["ToQuery"] != DBNull.Value ? reader["ToQuery"].ToString() : null,
                    //            CcQuery = reader["CcQuery"] != DBNull.Value ? reader["CcQuery"].ToString() : null,
                    //            BccQuery = reader["BccQuery"] != DBNull.Value ? reader["BccQuery"].ToString() : null,
                    //            DataQuery = reader["DataQuery"] != DBNull.Value ? reader["DataQuery"].ToString() : null,
                    //            EmailAttachment = reader["EmailAttachment"] != DBNull.Value ? reader["EmailAttachment"].ToString() : null,
                    //            IsHtml = reader["IsHtml"] != DBNull.Value && Convert.ToBoolean(reader["IsHtml"]),
                    //            IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"]),
                    //            CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : null,
                    //            //CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : null,
                    //            UpdatedBy = reader["UpdatedBy"] != DBNull.Value ? Convert.ToInt32(reader["UpdatedBy"]) : null
                    //            //UpdatedDate = reader["UpdatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedDate"]) : null
                    //        });
                    //    }
                    //}
                    DataTable = await command.FillAsync_DataTable();
                }

                //return list;
                return DataTable;
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
