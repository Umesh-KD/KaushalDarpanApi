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

                        command.Parameters.AddWithValue("@LogID", body.LogID);
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


    }





}
