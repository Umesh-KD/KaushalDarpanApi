using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.ITIAdminDashboard;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.ReportFeesTransactionModel;
using Kaushal_Darpan.Models.RPPPayment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;
using static Kaushal_Darpan.Models.ITIApplication.ItiApplicationPreviewDataModel;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ItiApplicationFormRepository : IItiApplicationFormRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ItiApplicationFormRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ItiApplicationFormRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> SavePersonalDetailsData(PersonalDetailsDataModel request)
        {
            _actionName = "SaveData(PersonalDetailsDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_ApplicationData_IU";
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
                        command.Parameters.AddWithValue("@ParentIncome", request.ParentIncome);
                        command.Parameters.AddWithValue("@IsMinority", request.IsMinority);
                        command.Parameters.AddWithValue("@IsEWSCategory", request.IsEWSCategory);
                        command.Parameters.AddWithValue("@Eligible8thTradesID", request.Eligible8thTradesID);
                        command.Parameters.AddWithValue("@Eligibl10thTradesID", request.Eligibl10thTradesID);
                        command.Parameters.AddWithValue("@PWDCategoryID", request.PWDCategoryID);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IsTSP", request.IsTSP);
                        command.Parameters.AddWithValue("@IsSaharia", request.IsSaharia);
                        command.Parameters.AddWithValue("@TspDistrictID", request.TspDistrictID);
                        command.Parameters.AddWithValue("@IsDevnarayan", request.IsDevnarayan);
                        command.Parameters.AddWithValue("@DevnarayanDistrictID", request.DevnarayanDistrictID);
                        command.Parameters.AddWithValue("@DevnarayanTehsilID", request.DevnarayanTehsilID);
                        command.Parameters.AddWithValue("@TSPTehsilID", request.TSPTehsilID);
                        command.Parameters.AddWithValue("@PH8thTradeList", request.PH8thTradeList);
                        command.Parameters.AddWithValue("@PH10thTradeList", request.PH10thTradeList);


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

        public async Task<PersonalDetailsDataModel> GetApplicationDatabyID(ItiApplicationSearchModel request)
        {
            _actionName = "GetApplicationDatabyID(ItiApplicationSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetApplicationData_ByID";
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new PersonalDetailsDataModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<PersonalDetailsDataModel>(dataTable);
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

        public async Task<int> SaveOptionDetailsData_Back(List<OptionDetailsDataModel> request)
        {
            _actionName = "SaveOptionDetailsData(List<OptionDetailsDataModel> request)";


           


            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIApplication_Options_IU";
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


        public async Task<int> SaveOptionDetailsData(List<OptionDetailsDataModel> request)
        {
            _actionName = "SaveOptionDetailsData(List<OptionDetailsDataModel> request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIApplicationOptionsForm";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditData");
                        //command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
                        //command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
                        //command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
                        //command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
                        //command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
                        //command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
                        command.Parameters.AddWithValue("@OptionID", request[0].OptionID);
                        command.Parameters.AddWithValue("@ApplicationID", request[0].ApplicationID);
                        command.Parameters.AddWithValue("@ManagementTypeID", request[0].ManagementTypeID);
                        command.Parameters.AddWithValue("@DistrictID", request[0].DistrictID);
                        command.Parameters.AddWithValue("@InstituteID", request[0].InstituteID);
                        command.Parameters.AddWithValue("@TradeID", request[0].TradeID);
                        command.Parameters.AddWithValue("@DepartmentID", request[0].DepartmentID);
                        command.Parameters.AddWithValue("@ModifyBy", request[0].ModifyBy);
                        command.Parameters.AddWithValue("@Priority", request[0].Priority);
                        command.Parameters.AddWithValue("@TradeLevel", request[0].TradeLevel);
                        command.Parameters.AddWithValue("@AcademicYear", request[0].AcademicYear);
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


        public async Task<int> SaveQualificationDetailsData(List<QualificationDetailsDataModel> request)
        {
            _actionName = "SaveQualificationDetailsData(List<QualificationDetailsDataModel> request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIApplication_Qualification_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@QualificationDetails", JsonConvert.SerializeObject(request));

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

        public async Task<int> SaveDocumentDetailsData(List<DocumentDetailsModel> request)
        {
            _actionName = " SaveDocumentDetailsData(List<DocumentDetailsModel> request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIApplication_DocumentsData_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@action", "_addEditData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));

                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

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

        public async Task<int> SaveAddressDetailsData(AddressDetailsDataModel request)
        {
            _actionName = "SaveAddressDetailsData(AddressDetailsDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIApplication_Address_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@AddressLine1", request.AddressLine1);
                        command.Parameters.AddWithValue("@AddressLine2", request.AddressLine2);
                        command.Parameters.AddWithValue("@AddressLine3", request.AddressLine3);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@TehsilID", request.TehsilID);
                        command.Parameters.AddWithValue("@CityVillage", request.CityVillage);
                        command.Parameters.AddWithValue("@Pincode", request.Pincode);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@NonRajasthanBlockName", request.NonRajasthanBlockName);
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

        public async Task<List<QualificationDetailsDataModel>> GetQualificationDatabyID(ItiApplicationSearchModel request)
        {
            _actionName = "GetQualificationDatabyID(int PK_ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIApplication_QualificationData_ByID";
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<QualificationDetailsDataModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<QualificationDetailsDataModel>>(dataTable);
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

        public async Task<AddressDetailsDataModel> GetAddressDetailsbyID(ItiApplicationSearchModel request)
        {
            _actionName = "GetAddressDetailsbyID(ItiApplicationSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetAddressDetails_ByID";
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new AddressDetailsDataModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<AddressDetailsDataModel>(dataTable);
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

        public async Task<List<OptionDetailsDataModel>> GetOptionDetailsbyID(ItiApplicationSearchModel request)
        {
            _actionName = "GetOptionDetailsbyID(int PK_ID, int DepartmentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIApplication_GetOptions_ByID";
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<OptionDetailsDataModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<OptionDetailsDataModel>>(dataTable);
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

        public async Task<ItiApplicationPreviewModel> GetApplicationPreviewbyID(ItiApplicationSearchModel searchRequest)
        {
            _actionName = "GetApplicationPreviewbyID(ItiApplicationSearchModel searchRequest)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIApplication_GetPreview_ByID";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@ApplicationID", searchRequest.ApplicationID);
                        command.Parameters.AddWithValue("@RoleID", searchRequest.RoleID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ItiApplicationPreviewModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ItiApplicationPreviewModel>(dataSet.Tables[0]);
                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.QualificationViewDetails = CommonFuncationHelper.ConvertDataTable<List<ItiQualificationViewDetails>>(dataSet.Tables[1]);
                            }
                            if (dataSet.Tables[2].Rows.Count > 0)
                            {
                                data.OptionsViewData = CommonFuncationHelper.ConvertDataTable<List<ItiOptionsviewData>>(dataSet.Tables[2]);
                            }
                            if (dataSet.Tables[3].Rows.Count > 0)
                            {
                                data.PendingDataModel = CommonFuncationHelper.ConvertDataTable<List<ItiPendingDataModel>>(dataSet.Tables[3]);
                            }
                            if (dataSet.Tables[4].Rows.Count > 0)
                            {
                                data.DocumentDetailList = CommonFuncationHelper.ConvertDataTable<List<DocumentDetailsModel>>(dataSet.Tables[4]);

                            }
                            if (dataSet.Tables.Count > 5)
                            {
                                if (dataSet.Tables[5].Rows.Count > 0)
                                {
                                    data.EmitraTransactionsModelList = dataSet.Tables[5];
                                }
                            }
                            //data.QualificationViewDetails = CommonFuncationHelper.ConvertDataTable<List<ItiQualificationViewDetails>>(dataSet.Tables[1]);
                            //data.OptionsViewData = CommonFuncationHelper.ConvertDataTable<List<ItiOptionsviewData>>(dataSet.Tables[2]);
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

        public async Task<DocumentDetailsDataModel> GetDocumentDatabyID(ItiApplicationSearchModel searchRequest)
        {
            _actionName = "GetDocumentDatabyID(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIApplication_GetDocumentData_ByID";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@ApplicationID", searchRequest.ApplicationID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@Eng_NonEng", searchRequest.Eng_NonEng);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new DocumentDetailsDataModel();

                    if (dataSet != null)
                        if (dataSet != null)
                        {
                            if (dataSet.Tables.Count > 0)
                            {
                                data = CommonFuncationHelper.ConvertDataTable<DocumentDetailsDataModel>(dataSet.Tables[0]);

                                if (dataSet.Tables[1].Rows.Count > 0)
                                {

                                    data.DocumentDetails = CommonFuncationHelper.ConvertDataTable<List<DocumentDetailsModel>>(dataSet.Tables[1]);
                                }
                            }
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

        public async Task<int> FinalSubmit(int ApplicationID, int Status)
        {
            _actionName = "FinalSubmit(int ApplicationID, int Status)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIApplication_FinalSubmit_IU";
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


        public async Task<DataTable> GetITIStudentProfileDownload(ItiApplicationSearchModel searchRequest)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetITIStudentProfileDownload";
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

        public async Task<bool> DeleteOptionByID(OptionDetailsDataModel model)
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
                        command.CommandText = "USP_ITIApplication_Options_IU";
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

        public async Task<bool> PriorityChange(OptionDetailsDataModel model)
        {
            _actionName = "PriorityChange(OptionDetailsDataModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIApplication_Options_IU";
                        command.Parameters.AddWithValue("@OptionID", model.OptionID);
                        command.Parameters.AddWithValue("@ApplicationID", model.ApplicationID);
                        command.Parameters.AddWithValue("@Type", model.Type);
                        command.Parameters.AddWithValue("@TradeLevel", model.TradeLevel);
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

        public async Task<DataTable> GetItiApplicationData(ItiAdminDashApplicationSearchModel body)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_UnlockApplicationForm";
                        command.Parameters.AddWithValue("@action", "GetApplicationData");
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@UrlStatus", body.UrlStatus);
                        command.Parameters.AddWithValue("@ApplicationID", body.ApplicationID);
                        command.Parameters.AddWithValue("@StudentName", body.StudentName);
                        command.Parameters.AddWithValue("@MobileNumber", body.MobileNumber);
                        command.Parameters.AddWithValue("@Gender", body.Gender);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@CategoryA", body.CategoryA);
                        command.Parameters.AddWithValue("@CategoryB", body.CategoryB);
                        command.Parameters.AddWithValue("@CategoryC", body.CategoryC);
                        command.Parameters.AddWithValue("@CategoryD", body.CategoryD);
                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);

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

        public async Task<bool> UnlockApplication(ItiApplicationUnlockDataModel model)
        {
            _actionName = "UnlockApplication(ItiApplicationUnlockDataModel productDetails);";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_UnlockApplicationForm";
                        command.Parameters.AddWithValue("@ApplicationID", model.ApplicationID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
                        command.Parameters.AddWithValue("@action", "UnlockApplication");

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

        public async Task<int> ITI_DirectAdmissionApply(ITI_DirectAdmissionApplyDataModel model)
        {
            _actionName = "ITI_DirectAdmissionApply(int ApplicationId, int UserId)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_DirectAdmissionApply";

                        command.Parameters.AddWithValue("@ApplicationID", model.ApplicationID);
                        command.Parameters.AddWithValue("@UserId", model.UserID);
                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

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
    }
}










