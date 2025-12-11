using Azure;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.MenuMaster;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentJanAadharDetail;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIDataMasterRepository : IITIDataMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIDataMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "IITIDataMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        //public async Task<TechnicalDataModel> GetAllData(SeatIntakesDataListSearchModel request)
        //{
        //    _actionName = "GetAllData(SeatIntakeSearchModel request)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            //DataTable dataTable = new DataTable();
        //            DataSet dataset = new DataSet();
        //            using (var command = await _dbContext.CreateCommandAsync())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_ITI_GetDataMaster";
        //                command.Parameters.AddWithValue("@AcademicYearID", request.AcademicYearID);
        //                //command.Parameters.AddWithValue("@RequestType", request.RequestType);
        //                command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
        //                command.Parameters.AddWithValue("@action", request.action);

        //                //command.Parameters.AddWithValue("@action", "_getAllData");

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataset = await command.FillAsync();
        //            }


        //            //TechnicalDataModel obj = new TechnicalDataModel();
        //            //obj.APPLICATIONID= dataSet.Tables[1]['']
        //            //obj.CourseDetails = CommonFuncationHelper.ConvertDataTable<List<CourseDetail>>(dataSet.Tables[1]);
        //            TechnicalDataModel data = new TechnicalDataModel();
        //            var coursedata = new List<CourseDetail>();

        //            if (dataset != null)
        //            {
        //                if (dataset.Tables.Count > 1)
        //                {
        //                    //data.COLLEGECODE = dataset.Tables[0]['collegecode']
        //                    data = CommonFuncationHelper.ConvertDataTable<TechnicalDataModel>(dataset.Tables[0]);
        //                    coursedata = CommonFuncationHelper.ConvertDataTable<List<CourseDetail>>(dataset.Tables[1]);
        //                    data.CourseDetailsList = coursedata;
        //                }
        //                else
        //                {
        //                    data = CommonFuncationHelper.ConvertDataTable<TechnicalDataModel>(dataset.Tables[0]);
        //                }


        //            }
        //            return data;
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



        public async Task<DataTable> GetAllData(DataListSearchModel request)
        {
            _actionName = "GetAllData(SeatIntakeSearchModel request)";
            return await Task.Run(async () =>
            {
                //string apiUsername = "ITIINSTITUE";
                //string apiPassword = "DSP@@pMzxalWNz77kZXXW8hQ==";
                try
                {
                    DataTable dataTable = new DataTable();
                    
                    //DataSet dataset = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetDataMaster";
                        //if (request.Username == apiUsername && request.Password == apiPassword)
                        //{
                            command.Parameters.AddWithValue("@sessionYear", request.SessionYear);
                            //command.Parameters.AddWithValue("@RequestType", request.RequestType);
                            command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
                            command.Parameters.AddWithValue("@action", request.RequestType);
                        //}
                        //else
                        //{
                        //    request.RequestType = "UserNotValid";
                        //    command.Parameters.AddWithValue("@sessionYear", request.SessionYear);
                        //    //command.Parameters.AddWithValue("@RequestType", request.RequestType);
                        //    command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
                        //    command.Parameters.AddWithValue("@action", request.RequestType);
                        //}
                        

                        //command.Parameters.AddWithValue("@action", "_getAllData");

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


        #region ncvt student corrected data

        public async Task<DataTable> GetStudentCorrectionListData(StudentCorrectionMasterSearchModel body)
        {
            _actionName = "GetStudentCorrectionListData(StudentCorrectionMasterSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_StudData_CorrectionMaster";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter


                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null || body.Name!="")
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);

                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@action", body.action);
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


        public async Task<DataTable> GetBTERStudentDetailsList(BTERStudentDetailsMasterSearchModel body)
        {
            _actionName = "GetBTERStudentDetailsList(BTERStudentDetailsMasterSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentDetails";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter


                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null || body.Name != "")
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@Eng_nonEng", body.EngNonEng);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@action", body.action);
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

        public async Task<DataTable> GetStudentDetailsBYID(BTERStudentDetailsMasterSearchModel body)
        {
            _actionName = "GetStudentDetailsBYID(BTERStudentDetailsMasterSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentDetails";
                        //command.Parameters.AddWithValue("@action", "_getAllData"); // Assuming you are using the action filter


                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.Name != null || body.Name != "")
                        {
                            command.Parameters.AddWithValue("@Name", body.Name);
                        }
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@Eng_nonEng", body.EngNonEng);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@action", body.action);
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

        public async Task<DataTable> GetStudentCorrectionDataByID(StudentCorrectionMasterSearchModel body)
        {
            _actionName = "GetStudentCorrectionDataByID(StudentCorrectionMasterSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_StudData_CorrectionMaster";

                        //command.Parameters.AddWithValue("@TradeID", body.TradeID);
                        if (body.CandidateID != null && body.CandidateID != 0)
                        {
                            command.Parameters.AddWithValue("@CandidateID", body.CandidateID);
                        }
                        //command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        //command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        //command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        //command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@action", body.action);
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

        public async Task<bool> SaveStudentCorrectionData(StudentCorrectionMasterSearchModel request)
        {
            _actionName = "SaveStudentCorrectionData(StudentCorrectionMasterSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_StudData_CorrectionMaster";


                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CandidateID", request.CandidateID);
                        command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UIDNumber", request.UIDNumber);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@FatherGuardianName", request.CandidateFatherName);
                        command.Parameters.AddWithValue("@MotherName", request.CandidateMotherName);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNo);
                        command.Parameters.AddWithValue("@DateOfBirth", request.DateOfBirth);
                        command.Parameters.AddWithValue("@EmailID", request.Email);

                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@action", request.action);



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



        public async Task<DataTable> GetTraineeLogsList(UploadTrainee_LogsModel body)
        {
            _actionName = "GetTraineeLogsList(UploadTrainee_LogsModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_StudData_CorrectionMaster";
                        command.Parameters.AddWithValue("@action", "_getAPILogsData");
                        if (!string.IsNullOrEmpty(body.log_id))
                        {
                            command.Parameters.AddWithValue("@LogID", body.log_id);
                        }
                        
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


        #endregion



        public async Task<DataTable> GetNcvt_APIDetails()
        {
            _actionName = "GetNcvt_APIDetails(PreExamStudentModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_StudData_CorrectionMaster";
                        command.Parameters.AddWithValue("@action", "_GetNcvt_APIDetails");


                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    return dataTable;
                });

                //string apiUrl = "https://iti-api.skillindiadigital.gov.in/v1/state/api-upload-status";

                //using (HttpClient client = new HttpClient())
                //{
                //    client.Timeout = TimeSpan.FromSeconds(60);
                //    client.DefaultRequestHeaders.Accept.Clear();
                //    client.DefaultRequestHeaders.Accept.Add(
                //        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                //    var requestBody = new
                //    {
                //        state_code = "RJ" // <-- example; replace with dynamic value if needed
                //    };

                //    string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                //    var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");


                //    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                //    response.EnsureSuccessStatusCode();

                //    string jsonResponse = await response.Content.ReadAsStringAsync();


                //    DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(jsonResponse);

                //    return dataTable;
                //}
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


        public async Task<List<ResultModel>> UploadStatusCheck(NCVTUploadStatusCheckDataModel model)
        {
            _actionName = "UploadStatusCheck()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_UploadStatusCheck";
                        command.Parameters.AddWithValue("@action", "GetNcvtData");
                        
                        command.Parameters.AddWithValue("@Log_id", model.Log_id ?? (object)DBNull.Value);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    List<ResultModel> resultList = CommonFuncationHelper.ConvertDataTable<List<ResultModel>>(dataTable);
                    return resultList;
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
                
        public async Task<bool> SaveUploadTraineeLog(UploadTrainee_LogsModel request)
        {
            _actionName = "SaveUploadTraineeLogs (UploadTrainee_LogsModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandText = "USP_ITI_StudData_CorrectionMaster";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RequestID", request.RequestID);
                        command.Parameters.AddWithValue("@LogID", request.LogID);
                        command.Parameters.AddWithValue("@Response", request.Response);
                        command.Parameters.AddWithValue("@LogID", request.log_id);
                        command.Parameters.AddWithValue("@action", "_SaveUploadTraineeLogs");
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
