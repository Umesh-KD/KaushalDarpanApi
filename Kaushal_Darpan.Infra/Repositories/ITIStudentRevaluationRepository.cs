using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CounsellingImportCandidateListModel;
using Kaushal_Darpan.Models.DTEInventoryModels;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.ITIFeeModel;
using Kaushal_Darpan.Models.RevaluationDataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIStudentRevaluationRepository : IITIStudentRevaluationRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIStudentRevaluationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIStudentRevaluationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetStudentRevaluationDetails(ITIStudentRevaluationDataModel body)
        {
            _actionName = "GetTeacherForExaminer()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIGetStudentRevaluationDetailsByRollNo";

                        command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        command.Parameters.AddWithValue("@DOB", body.DOB);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<DataTable> GetAllStudentRevaluation(StudentDetailsByRollNoModel body)
        {
            _actionName = "GetTeacherForExaminer()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_RVL_PaymentRevaluationDetails";

                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@StudentExamID", body.StudentExamID);
                        command.Parameters.AddWithValue("@SemesterID", body.SemesterID);
                        command.Parameters.AddWithValue("@CourseType", body.CourseTypeID);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<DataTable> GetAll_INV_returnItem(ItemsIssueReturnModels SearchReq)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_INV_StaffIssueReturnItems";

                        command.Parameters.AddWithValue("@Type", "ReturnItemUpdate");
                        command.Parameters.AddWithValue("@Remarks", SearchReq.Remarks);
                        command.Parameters.AddWithValue("@ItemCategoryId", SearchReq.ItemCategoryId);
                        command.Parameters.AddWithValue("@ReturnDate", SearchReq.ReturnDate);
                        command.Parameters.AddWithValue("@ConditionAtReturn", SearchReq.ConditionAtReturn);
                        command.Parameters.AddWithValue("@ItemList", JsonConvert.SerializeObject(SearchReq.ItemList));


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

        public async Task<DataTable> SaveRVLPaymentData(RVLStudentDetailsModel body)
        {
            _actionName = "SaveRVLPaymentData()";

            return await Task.Run(async () =>
            {
                try
                {
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_RVL_InsertStudentRevalRequest";

                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        command.Parameters.AddWithValue("@PaymentAmount", body.PaymentAmount);
                        command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
                        command.Parameters.AddWithValue("@Remarks", body.Remarks ?? "Student requested revaluation");
                        command.Parameters.AddWithValue("@StudentExamID", body.StudentExamID);

                        if (body.ItemList != null && body.ItemList.Any())
                        {
                            var json = JsonConvert.SerializeObject(body.ItemList);
                            command.Parameters.AddWithValue("@Subjects", json);
                        }


                        _sqlQuery = command.GetSqlExecutableQuery();

                       
                        DataTable dt = await command.FillAsync_DataTable();
                        return dt;
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
                    throw new Exception(CommonFuncationHelper.MakeError(errorDesc), ex);
                }
            });
        }

        public async Task<DataTable> GetRVLDetailByStudentApplicationNo(RVLStudentRevalRequestModel body)
        {
            _actionName = "GetRVLDetailByStudentApplicationNo()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_RVL_GetDetailByStudentApplicationNo";

                        command.Parameters.AddWithValue("@ApplicationNo", body.ApplicationNo);
                        command.Parameters.AddWithValue("@RollNo", body.RollNo);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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


        #region

        public async Task<DataTable> GetAllRevalRequestDetails(ITIRevalRequestStudentDetailsModel body)
        {
            _actionName = " GetAllRevalRequestDetails(ITIRevalRequestStudentDetailsModel body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_RVl_StudentRevalRequest_Details";
                        if (!string.IsNullOrWhiteSpace(body.RollNo))
                        {
                            command.Parameters.AddWithValue("@RollNo", body.RollNo);
                        }
                        if (!string.IsNullOrEmpty(body.DOB))
                        {
                            command.Parameters.AddWithValue("@DOB", body.DOB);
                        }
                        if (!string.IsNullOrEmpty(body.Name))
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        if (body.RevalReqID!=null )
                        {
                            command.Parameters.AddWithValue("@RevalReqID", body.RevalReqID);
                        }
                     
                        command.Parameters.AddWithValue("@action", body.action);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<bool> UploadDocument(ITIRevalRequestStudentDetailsModel request)
        {
            _actionName = "UploadDocument(ITIRevalRequestStudentDetailsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_RVl_StudentRevalRequest_Details";


                        //// Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request.StudentOptionList));
                        if (request.RevalReqID != null)
                        {
                            command.Parameters.AddWithValue("@RevalReqID", request.RevalReqID);
                        }
                        if (request.ActionBy != null)
                        {
                            command.Parameters.AddWithValue("@ActionBy", request.ActionBy);
                        }
                        command.Parameters.AddWithValue("@action", "_updateRevalReqUploadFile");



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

        #endregion


        #region update enrollresponse in bulk excel

        public async Task<bool> ImportExcelFile(List<UpdateEnrollResponseBulkExcelModel> model)
        {
            _actionName = "ImportExcelFile(TimeTableModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    //DataTable dataTable = new DataTable();
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_SaveEnrollresponse_BulkExcel";
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));

                        command.Parameters.Add("@Retval", SqlDbType.Int); // out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output; // out


                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Retval"].Value); // out

                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //dataTable = await command.FillAsync_DataTable();

                    }
                    if (result > 0)
                        return true;
                    else
                        return false;
                    //var data = new List<UpdateEnrollResponseBulkExcelModel>();
                    //if (dataTable != null)
                    //{
                    //    data = CommonFuncationHelper.ConvertDataTable<List<UpdateEnrollResponseBulkExcelModel>>(dataTable);
                    //}
                    //return data;
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


        #endregion
    }
}
