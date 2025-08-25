using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITIFeeModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIFeeRepository: IITIFeeRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIFeeRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIFeeRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<bool> SaveITIFeeData(ITIFeeModel request)
        {
            _actionName = "SaveITIFeeData(ITIFeeModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIFeesIU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@Id", request.Id);
                        command.Parameters.AddWithValue("@AdmissionFee", request.AdmissionFee);
                        command.Parameters.AddWithValue("@ApplicationProcessingFee", request.ApplicationProcessingFee);
                        command.Parameters.AddWithValue("@ApplicationFormFeeGen", request.ApplicationFormFeeGen);
                        command.Parameters.AddWithValue("@ApplicationFormFeeSc", request.ApplicationFormFeeSc);
                        command.Parameters.AddWithValue("@ApplicationFormFeeSt", request.ApplicationFormFeeSt);
                        command.Parameters.AddWithValue("@ApplicationFormFeeObc", request.ApplicationFormFeeObc);
                        command.Parameters.AddWithValue("@ApplicationFormFeeMbc", request.ApplicationFormFeeMbc);
                        command.Parameters.AddWithValue("@AcademicYear", request.AcademicYear);

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

        public async Task<ITIFeeModel> UpdateData(int Id)
        {
            _actionName = "UpdateData";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "USP_ITIFeesIU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Id", Id);
                       // command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new ITIFeeModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ITIFeeModel>(dataTable);
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
        public async Task<ITIFeeModel> GetById(int id)
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
                        //command.CommandText = " select * from M_PlacementCompanyMaster Where ID='" + id + "' ";
                        command.CommandText = "USP_ITIFees_GetData";
                        command.Parameters.AddWithValue("@AcademicYear", id);
                        command.Parameters.AddWithValue("@action", "_getDataById");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new ITIFeeModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ITIFeeModel>(dataTable);
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


        //public async Task<bool> DeleteDataByID(ExaminerMaster request)
        //{
        //    _actionName = "DeleteDataByID(ExaminerMaster request)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = _dbContext.CreateCommand(true))
        //            {
        //                command.CommandType = CommandType.Text;
        //                command.CommandText = $" update M_ExaminerMaster set ActiveStatus=0, DeleteStatus=1, IsAppointed=0,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ExaminerID={request.ExaminerID}";

        //                _sqlQuery = command.GetSqlExecutableQuery();
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
    }
}
