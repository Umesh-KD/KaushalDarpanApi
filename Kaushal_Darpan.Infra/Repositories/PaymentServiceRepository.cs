using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.HostelManagementModel;
using Kaushal_Darpan.Models.PaymentServiceMaster;
using Kaushal_Darpan.Models.RoleMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class PaymentServiceRepository: IPaymentServiceRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public PaymentServiceRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "PaymentServiceRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> SaveData(PaymentServiceDataModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveData(PaymentServiceDataModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PaymentServiceDetails_IU";
                        command.Parameters.AddWithValue("@Action", "SaveServiceData");

                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@ServiceId", request.ServiceId);
                        command.Parameters.AddWithValue("@ServiceName", request.ServiceName);
                        command.Parameters.AddWithValue("@SchemeId", request.SchemeId);
                        command.Parameters.AddWithValue("@SubServiceId", request.SubServiceId);
                        command.Parameters.AddWithValue("@MerchantCode", request.MerchantCode);
                        command.Parameters.AddWithValue("@RevenueHead", request.RevenueHead);
                        command.Parameters.AddWithValue("@CommType", request.CommType);
                        command.Parameters.AddWithValue("@OfficeCode", request.OfficeCode);
                        command.Parameters.AddWithValue("@ServiceURL", request.ServiceURL);
                        command.Parameters.AddWithValue("@VerifyURL", request.VerifyURL);
                        command.Parameters.AddWithValue("@Flag", request.Flag);
                        command.Parameters.AddWithValue("@IsActive", request.IsActive);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@ViewName", request.ViewName);
                        command.Parameters.AddWithValue("@ControllerName", request.ControllerName);
                        command.Parameters.AddWithValue("@JanaadhaarSchemeCode", request.JanaadhaarSchemeCode);
                        command.Parameters.AddWithValue("@IsLive", request.IsLive);
                        command.Parameters.AddWithValue("@IsKiosk", request.IsKiosk);
                        command.Parameters.AddWithValue("@CHECKSUMKEY", request.CHECKSUMKEY);
                        command.Parameters.AddWithValue("@REDIRECTURL", request.REDIRECTURL);
                        command.Parameters.AddWithValue("@WebServiceURL", request.WebServiceURL);
                        command.Parameters.AddWithValue("@SuccessFailedURL", request.SuccessFailedURL);
                        command.Parameters.AddWithValue("@SuccessURL", request.SuccessURL);
                        command.Parameters.AddWithValue("@ExamStudentStatus", request.ExamStudentStatus);
                        command.Parameters.AddWithValue("@CourseType", request.CourseType);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@EncryptionKey", request.EncryptionKey);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
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

        public async Task<DataTable> GetAllData(PaymentServiceSearchModel body)
        {

            _actionName = "GetAllHostelList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PaymentServiceDetails_IU";
                        command.Parameters.AddWithValue("@ServiceName", body.ServiceName);
                        command.Parameters.AddWithValue("@Action", "getAllData");

                        _sqlQuery = command.GetSqlExecutableQuery();
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
    }
}
