using Azure;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.ApplicationMessageModel;
using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.DateConfiguration;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.StaffMaster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class BterApplicationRepository : IBterApplicationRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public BterApplicationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "BterApplicationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> SaveData(ApplicationDataModel request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ApplicationData_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@StudentName", request.StudentName);
                        command.Parameters.AddWithValue("@StudentNameHindi", request.StudentNameHindi);
                        command.Parameters.AddWithValue("@FatherName", request.FatherName);
                        command.Parameters.AddWithValue("@FatherNameHindi", request.FatherNameHindi);
                        command.Parameters.AddWithValue("@MotherName", request.MotherName);
                        command.Parameters.AddWithValue("@MotherNameHindi", request.MotherNameHindi);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@DOB", request.DOB);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@WhatsNumber", request.WhatsNumber);
                        command.Parameters.AddWithValue("@LandlineNumber", request.LandlineNumber);
                        command.Parameters.AddWithValue("@IndentyProff", request.IndentyProff);
                        command.Parameters.AddWithValue("@DetailID", request.DetailID);
                        command.Parameters.AddWithValue("@Maritial", request.Maritial);
                        command.Parameters.AddWithValue("@Religion", request.Religion);
                        command.Parameters.AddWithValue("@Nationality", request.Nationality);
                        command.Parameters.AddWithValue("@CategoryA", request.CategoryA);
                        command.Parameters.AddWithValue("@CategoryB", request.CategoryB);
                        command.Parameters.AddWithValue("@CategoryC", request.CategoryC);
                        command.Parameters.AddWithValue("@CategoryE", request.CategoryE);
                        command.Parameters.AddWithValue("@Prefential", request.Prefential);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@JanAadharNo", request.JanAadharNo);
                        command.Parameters.AddWithValue("@JanAadharMemberID", request.JanAadharMemberID);
                        command.Parameters.AddWithValue("@IsMinority", request.IsMinority);
                        command.Parameters.AddWithValue("@IsTSP", request.IsTSP);
                        command.Parameters.AddWithValue("@IsSaharia", request.IsSaharia);
                        command.Parameters.AddWithValue("@TspDistrictID", request.TspDistrictID);
                        command.Parameters.AddWithValue("@IsDevnarayan", request.IsDevnarayan);
                        command.Parameters.AddWithValue("@DevnarayanDistrictID", request.DevnarayanDistrictID);
                        command.Parameters.AddWithValue("@DevnarayanTehsilID", request.DevnarayanTehsilID);
                        command.Parameters.AddWithValue("@TSPTehsilID", request.TSPTehsilID);
                        command.Parameters.AddWithValue("@subCategory", request.subCategory);
                        command.Parameters.AddWithValue("@CategoryD", request.CategoryD);
                        command.Parameters.AddWithValue("@IsPH", request.IsPH);
                        command.Parameters.AddWithValue("@IsKM", request.IsKM);

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }

        public async Task<ApplicationDataModel> GetApplicationDatabyID(BterSearchModel searchRequest)
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
                        //command.CommandText = " select * from M_StreamMaster Where StreamID='" + PK_ID + "' "; ;
                        command.CommandText = "USP_GetApplicationData_ByID";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@ApplicationId", searchRequest.ApplicationId);



                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new ApplicationDataModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ApplicationDataModel>(dataTable);
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


        public async Task<int> SaveQualificationDataveData(QualificationDataModel request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        if(request.isCorrectMerit == true)
                        {
                            command.CommandText = "USP_BTER_CorrectMerit_Qualification";
                        } 
                        else
                        {
                            command.CommandText = "USP_QualificationMaster_IU";
                        }
                        // Set the stored procedure name and type
                        
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@QualificationID", request.QualificationID);
                        command.Parameters.AddWithValue("@BoardID", request.BoardID);
                        command.Parameters.AddWithValue("@BoardStateID", request.BoardStateID);
                        command.Parameters.AddWithValue("@BoardExamID", request.BoardExamID);
                        command.Parameters.AddWithValue("@RollNumber", request.RollNumber);
                        command.Parameters.AddWithValue("@Percentage", request.Percentage);
                        command.Parameters.AddWithValue("@IsSupplement", request.IsSupplement);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@AggMaxMark", request.AggMaxMark);
                        command.Parameters.AddWithValue("@AggObtMark", request.AggObtMark);
                        command.Parameters.AddWithValue("@PassingID", request.PassingID);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@MarkType", request.MarkType);
                        command.Parameters.AddWithValue("@LateralCourseID", request.LateralCourseID);
                        command.Parameters.AddWithValue("@CourseTypeId", request.CourseType);
                        command.Parameters.AddWithValue("@SupplementaryDataModel", JsonConvert.SerializeObject(request.SupplementaryDataModel));
                        command.Parameters.AddWithValue("@LateralEntryQualificationModel", JsonConvert.SerializeObject(request.LateralEntryQualificationModel));
                        command.Parameters.AddWithValue("@HighestQualificationModel", JsonConvert.SerializeObject(request.HighestQualificationModel));
                        //command.Parameters.AddWithValue("@EnglishQualification", JsonConvert.SerializeObject(request.EnglishQualificationDataModel));
                        command.Parameters.AddWithValue("@SubjectID", JsonConvert.SerializeObject(request.LateralEntryQualificationModel?.FirstOrDefault()?.SubjectID));

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }


        public async Task<int> SaveHighQualificationData(QualificationDataModel request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_InsertHighestQualificationDetails";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@LateralCourseID", request.LateralCourseID);
                        command.Parameters.AddWithValue("@CourseTypeId", request.CourseType);
                        command.Parameters.AddWithValue("@HighestQualificationModel", JsonConvert.SerializeObject(request.HighestQualificationModel));

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }


        public async Task<int> SaveAddressData(BterAddressDataModel request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BterAddressForm_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@AddressLine1", request.AddressLine1);
                        command.Parameters.AddWithValue("@AddressLine2", request.AddressLine2);
                        command.Parameters.AddWithValue("@AddressLine3", request.AddressLine3);
                        command.Parameters.AddWithValue("@CorsAddressLine1", request.CorsAddressLine1);
                        command.Parameters.AddWithValue("@CorsAddressLine2", request.CorsAddressLine2);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@CorsAddressLine3", request.CorsAddressLine3);
                        command.Parameters.AddWithValue("@CityVillage", request.CityVillage);
                        command.Parameters.AddWithValue("@CorsDistrictID", request.CorsDistrictID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@TehsilID", request.TehsilID);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@CorsCityVillage", request.CorsCityVillage);
                        command.Parameters.AddWithValue("@CorsNonRajasthanBlockName", request.CorsNonRajasthanBlockName);
                        command.Parameters.AddWithValue("@NonRajasthanBlockName", request.NonRajasthanBlockName);
                        command.Parameters.AddWithValue("@CorsStateID", request.CorsStateID);
                        command.Parameters.AddWithValue("@CorsTehsilID", request.CorsTehsilID);
                        command.Parameters.AddWithValue("@Pincode", request.Pincode);
                        command.Parameters.AddWithValue("@CorsPincode", request.CorsPincode);



                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }

        public async Task<int> SaveOtherData(BterOtherDetailsModel request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BterOtherData_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@ParentsIncome", request.ParentsIncome);
                        command.Parameters.AddWithValue("@ApplyScheme", request.ApplyScheme);
                        command.Parameters.AddWithValue("@EWS", request.EWS);
                        command.Parameters.AddWithValue("@Residence", request.Residence);
                        command.Parameters.AddWithValue("@IncomeSource", request.IncomeSource);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@IsSportsQuota", request.IsSportsQuota);

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }



        public async Task<int> SaveDocumentData(List<DocumentDetailsModel> request)
        {
            _actionName = "SaveDocumentData(List<DocumentDetailsModel> request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BterApplication_DocumentsData_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@retval_ID", SqlDbType.Int); // out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value); // out
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }


        public async Task<int> SaveOptionalData(List<BterOptionsDetailsDataModel> request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BteroptionDetailForm_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));



                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@retval_ID", SqlDbType.Int); // out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value); // out
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }


        public async Task<QualificationDataModel> GetQualificationDatabyID(BterSearchModel searchRequest)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetQualificationData_ByID";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@ApplicationId", searchRequest.ApplicationId);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new QualificationDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<QualificationDataModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.HighestQualificationModel = CommonFuncationHelper.ConvertDataTable<List<HighestQualificationModel>>(dataSet.Tables[1]);
                            }

                            if (dataSet.Tables[2].Rows.Count > 0)
                            {

                                data.SupplementaryDataModel = CommonFuncationHelper.ConvertDataTable<List<SupplementaryDataModel>>(dataSet.Tables[2]);

                            }
                            if (dataSet.Tables[3].Rows.Count > 0)
                            {

                                data.LateralEntryQualificationModel = CommonFuncationHelper.ConvertDataTable<List<LateralEntryQualificationModel>>(dataSet.Tables[3]);

                            }

                            //if (dataSet.Tables[4].Rows.Count > 0)
                            //{

                            //    data.EnglishQualificationDataModel = CommonFuncationHelper.ConvertDataTable<List<EnglishQualificationDataModel>>(dataSet.Tables[4]);

                            //}
                            if (dataSet.Tables[4].Rows.Count > 0)
                            {

                                data.LateralEntryQualificationModel.FirstOrDefault().SubjectID = CommonFuncationHelper.ConvertDataTable<List<Lateralsubject>>(dataSet.Tables[4]);

                            }

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

        public async Task<BterAddressDataModel> GetAddressDatabyID(BterSearchModel searchRequest)
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
                        //command.CommandText = " select * from M_StreamMaster Where StreamID='" + PK_ID + "' "; ;
                        command.CommandText = "USP_GetAddressData_ByID";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@ApplicationId", searchRequest.ApplicationId);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new BterAddressDataModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<BterAddressDataModel>(dataTable);
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

        public async Task<List<BterOptionsDetailsDataModel>> GetOptionalDatabyID(BterSearchModel searchRequest)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetOptionalData_ByID";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@ApplicationId", searchRequest.ApplicationId);
                        _sqlQuery = command.GetSqlExecutableQuery(); // Get SQL query for debugging
                        dataSet = await command.FillAsync();
                    }

                    // Check if dataset has data
                    if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        // Convert the data table to a list of objects
                        return CommonFuncationHelper.ConvertDataTable<List<BterOptionsDetailsDataModel>>(dataSet.Tables[0]);
                    }

                    // Return an empty list if no data is found
                    return new List<BterOptionsDetailsDataModel>();
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
                var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errorDetails, ex);
            }
        }



        public async Task<BterOtherDetailsModel> GetOtherDatabyID(BterSearchModel searchRequest)
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
                        //command.CommandText = " select * from M_StreamMaster Where StreamID='" + PK_ID + "' "; ;
                        command.CommandText = "USP_GetbterOtherData_ByID";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@ApplicationId", searchRequest.ApplicationId);



                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new BterOtherDetailsModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<BterOtherDetailsModel>(dataTable);
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
        public async Task<PreviewApplicationModel> GetPreviewDatabyID(BterSearchModel searchRequest)
        {
            _actionName = "GetPreviewDatabyID(BterSearchModel searchRequest)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetbterpreviewData_ByID";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@ApplicationId", searchRequest.ApplicationId);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new PreviewApplicationModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<PreviewApplicationModel>(dataSet.Tables[0]);

                            if (dataSet.Tables.Count > 0 && dataSet.Tables[1].Rows.Count > 0)
                            {

                                data.QualificationViewDetails = CommonFuncationHelper.ConvertDataTable<List<QualificationViewDetails>>(dataSet.Tables[1]);   
                            }

                            if (dataSet.Tables.Count > 1 && dataSet.Tables[2].Rows.Count > 0)
                            {
                                data.optionalviewDatas = CommonFuncationHelper.ConvertDataTable<List<OptionalviewData>>(dataSet.Tables[2]);
                            }
                            if (dataSet.Tables.Count>2 && dataSet.Tables[3].Rows.Count > 0)
                            {
                                try
                                {
                                    data.PendingDataModel = CommonFuncationHelper.ConvertDataTable<List<PendingDataModel>>(dataSet.Tables[3]);
                                }
                                catch { }
                                }
                            if (dataSet.Tables.Count > 3 && dataSet.Tables[4].Rows.Count > 0)
                            {
                                data.DocumentDetailList = CommonFuncationHelper.ConvertDataTable<List<DocumentDetailsModel>>(dataSet.Tables[4]);
                            }
                            if (dataSet.Tables.Count > 4 && dataSet.Tables[5].Rows.Count > 0)
                            {
                                data.SupplementaryDetailList = CommonFuncationHelper.ConvertDataTable<List<SupplementaryviewDetails>>(dataSet.Tables[5]);
                            }
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

        public async Task<DataTable> GetStudentProfileDownload(BterSearchModel searchRequest)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetStudentProfileDownload";
                    command.Parameters.AddWithValue("@EnrollmentNo", searchRequest.EnrollmentNo);
                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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
        }


        public async Task<BterDocumentsDataModel> GetDocumentDatabyID(BterSearchModel searchRequest)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetDocumentsData_ByID";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@ApplicationId", searchRequest.ApplicationId);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new BterDocumentsDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<BterDocumentsDataModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count > 0)
                            {

                                data.DocumentDetails = CommonFuncationHelper.ConvertDataTable<List<DocumentDetailsModel>>(dataSet.Tables[1]);
                            }
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


        public async Task<int> SaveFinalData(int ApplicationID, int Status)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveFinalData_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling

                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@Status", Status);

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }


        public async Task<bool> PriorityChange(BterOptionsDetailsDataModel model)
        {
            _actionName = "PriorityChange(BterOptionsDetailsDataModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BteroptionDetailForm_IU";
                        command.Parameters.AddWithValue("@OptionID", model.OptionID);
                        command.Parameters.AddWithValue("@ApplicationID", model.ApplicationID);
                        command.Parameters.AddWithValue("@Type", model.Type);
                        command.Parameters.AddWithValue("@action", "PriorityChange");

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

        public async Task<bool> DeleteOptionByID(BterOptionsDetailsDataModel model)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BteroptionDetailForm_IU";
                        command.Parameters.AddWithValue("@OptionID", model.OptionID);
                        command.Parameters.AddWithValue("@action", "DeleteOption");
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


        public async Task<bool> DeleteByID(HighestQualificationModel model)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Application_Qualification_Delete";
                        command.Parameters.AddWithValue("@ApplicationQualificationId", model.ApplicationQualificationId);
                        command.Parameters.AddWithValue("@action", "DeleteQualification");

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



        public async Task<DataTable> GetDetailsbyID(HighestQualificationModel model)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetQualification";
                    command.Parameters.AddWithValue("@ApplicationQualificationId", model.ApplicationQualificationId);
                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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
        }

        public async Task<DataTable> GetDetailsbyApplicationNo(List<ApplicationDetails> ApplicationDetails)
        {
            _actionName = "GetDetailsbyApplicationNo(List<ApplicationDetails>? ApplicationDetails)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetApplicationDetails";
                    command.Parameters.AddWithValue("@ApplicationDetails", JsonConvert.SerializeObject(ApplicationDetails));
                    _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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
        }

        public async Task<bool> DeleteDocumentById(DocumentDetailsModel model)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BterApplication_DeleteDocument";
                        command.Parameters.AddWithValue("@ApplicationID", model.TransactionID);
                        command.Parameters.AddWithValue("@DocumentDetailsID", model.DocumentDetailsID);
                        command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);
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

        public async Task<bool> DirectAdmissionPaymentUpdate(DirectAdmissionUpdatePayment model)
        {
            _actionName = " DirectAdmissionPaymentUpdate(DirectAdmissionUpdatePayment model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_UpdateDirectPaymentStatus";
                        command.Parameters.AddWithValue("@ApplicationId", model.ApplicationId);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);

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


        public async Task<bool> JailAdmissionFinalSubmit(DirectAdmissionUpdatePayment model)
        {
            _actionName = " JailAdmissionFinalSubmit(DirectAdmissionUpdatePayment model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_UpdateapplicationFees";
                        command.Parameters.AddWithValue("@ApplicationId", model.ApplicationId);
       

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


    }
}




