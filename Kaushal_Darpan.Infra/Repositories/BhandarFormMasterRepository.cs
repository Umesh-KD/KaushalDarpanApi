using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.BhandarFormDataModel;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class BhandarFormMasterRepository : IBhandarFormMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public BhandarFormMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "BhandarFormMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<AddBhandarFormDataModel> GetExamStudentData(AddBhandarFormDataModel body)
        {
            _actionName = "GetExamStudentData()";

            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();

                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIBhandarForm_GetByid";

                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@CenterID", body.CenterID);
                        command.Parameters.AddWithValue("@ShiftID", body.ShiftID);
                        command.Parameters.AddWithValue("@ExamDate", body.ExamDate);
          
         

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }

                    var data = new AddBhandarFormDataModel();

                    if (dataSet != null && dataSet.Tables.Count > 0)
                    {
                        // 🔹 Table 0 → Main Header / Master Data
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            data = CommonFuncationHelper
                                   .ConvertDataTable<AddBhandarFormDataModel>(dataSet.Tables[0]);
                        }

                        // 🔹 Table 1 → Student List
                        if (dataSet.Tables.Count > 1 && dataSet.Tables[1].Rows.Count > 0)
                        {
                            data.BhandarDetailsModel = CommonFuncationHelper
                                               .ConvertDataTable<List<BhandarDetailsModel>>(dataSet.Tables[1]);
                        }

                        // 🔹 Table 2 → Summary / Counts (optional)
                        if (dataSet.Tables.Count > 2 && dataSet.Tables[2].Rows.Count > 0)
                        {
                            data.BhandarStudentModel = CommonFuncationHelper
                                               .ConvertDataTable<List<BhandarStudentModel>>(dataSet.Tables[2]);
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


        public async Task<bool> SaveData(AddBhandarFormDataModel request)
        {
            _actionName = "SaveData(ItiReportDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int returnValue = 0;

                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandText = "USP_ITIBhandarForm_iu";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@BhandarID", request.BhandarID);
                        command.Parameters.AddWithValue("@MoharID", request.MoharID);
                        command.Parameters.AddWithValue("@UserName", request.UserName);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@FileName", request.FileName);
                        command.Parameters.AddWithValue("@DisFileName", request.DisFileName);
                        command.Parameters.AddWithValue("@IsOpen", request.IsOpen);
                        command.Parameters.AddWithValue("@CenterID", request.CenterID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@ShiftID", request.ShiftID);
                        command.Parameters.AddWithValue("@ExamDate", request.ExamDate);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                    
                        command.Parameters.AddWithValue("@rowjson", JsonConvert.SerializeObject(request.BhandarDetailsModel));
                        command.Parameters.AddWithValue("@rowjson2", JsonConvert.SerializeObject(request.BhandarStudentModel));

                                
                           // Output parameter
                           var returnParam = new SqlParameter("@Return", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(returnParam);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        await command.ExecuteNonQueryAsync();
                        returnValue = (int)returnParam.Value;

                        return returnValue > 1;
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
