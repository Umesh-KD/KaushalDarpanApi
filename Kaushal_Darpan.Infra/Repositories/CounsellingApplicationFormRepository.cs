using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.DocumentDetails;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CounsellingApplicationFormRepository : ICounsellingApplicationFormRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CounsellingApplicationFormRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CounsellingApplicationFormRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<CounsellingApplicationFormDataModel> GetApplicationDataByID_Counselling(CounsellingApplicationSearchModel searchRequest)
        {
            _actionName = "GetApplicationDataByID_Counselling(CounsellingApplicationSearchModel searchRequest)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_GetApplicationDataById";
                        
                        command.Parameters.AddWithValue("@CandidateId", searchRequest.CandidateId);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new CounsellingApplicationFormDataModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<CounsellingApplicationFormDataModel>(dataTable);
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

        public async Task<int> SavePersonalDetails(CounsellingApplicationFormDataModel request)
        {
            _actionName = "SavePersonalDetails(CounsellingApplicationFormDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Counselling_PersonalDetails_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CandidateID", request.CandidateID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@CandidateName", request.CandidateName);
                        command.Parameters.AddWithValue("@FatherName", request.FatherName);
                        command.Parameters.AddWithValue("@MotherName", request.MotherName);
                        command.Parameters.AddWithValue("@GenderId", request.GenderId);
                        command.Parameters.AddWithValue("@DOB", request.DOB);
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@Address1", request.Address1);
                        command.Parameters.AddWithValue("@Address2", request.Address2);
                        command.Parameters.AddWithValue("@Address3", request.Address3);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@BlockID", request.BlockID);
                        command.Parameters.AddWithValue("@Pincode", request.Pincode);
                        command.Parameters.AddWithValue("@AadharNo", request.AadharNo);
                        command.Parameters.AddWithValue("@JanAadharNo", request.JanAadharNo);
                        command.Parameters.AddWithValue("@CategoryA_ID", request.CategoryA_ID);
                        
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", request.CourseType);
                        command.Parameters.AddWithValue("@ProfileStatus", request.ProfileStatus);
                        command.Parameters.AddWithValue("@ApplicationNo", request.ApplicationNo);
                        command.Parameters.AddWithValue("@ReligionID", request.ReligionID);
                        command.Parameters.AddWithValue("@NationalityID", request.NationalityID);
                        command.Parameters.AddWithValue("@MaritalID", request.MaritalID);
                        command.Parameters.AddWithValue("@PWDCategoryID", request.PWDCategoryID);
                        command.Parameters.AddWithValue("@IsMinority", request.IsMinority);
                        command.Parameters.AddWithValue("@IsFinalSubmit", request.IsFinalSubmit);
                        command.Parameters.AddWithValue("@DepartmentName", request.DepartmentName);
                        command.Parameters.AddWithValue("@SubmittedStep", request.SubmittedStep);

                        command.Parameters.AddWithValue("@RollNumber", request.RollNumber);
                        command.Parameters.AddWithValue("@Designation", request.Designation);
                        command.Parameters.AddWithValue("@Trade", request.Trade);
                        command.Parameters.AddWithValue("@MeritNo", request.MeritNo);
                        command.Parameters.AddWithValue("@SelectionCategoryID", request.SelectionCategoryID);
                        command.Parameters.AddWithValue("@IsTSP", request.IsTSP);
                        command.Parameters.AddWithValue("@HomeDistrictID", request.HomeDistrictID);
                        command.Parameters.AddWithValue("@IsPH", request.IsPH);
                        command.Parameters.AddWithValue("@IsExServicemen", request.IsExServicemen);
                        command.Parameters.AddWithValue("@IsSportsPerson", request.IsSportsPerson);
                        command.Parameters.AddWithValue("@IsSpouseInSameService", request.IsSpouseInSameService);
                        command.Parameters.AddWithValue("@IsShahidDependent", request.IsShahidDependent);
                        command.Parameters.AddWithValue("@IsAnyIncurableDiseases", request.IsAnyIncurableDiseases);

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

        public async Task<int> Counselling_SaveOption(CounsellingOptionFormDataModel request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandText = "USP_Counselling_OptionDetails_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@action", "_addEditData");
                        command.Parameters.AddWithValue("@OptionID", request.OptionID);
                        command.Parameters.AddWithValue("@Priority", request.Priority);
                        command.Parameters.AddWithValue("@CandidateID", request.CandidateID);
                        command.Parameters.AddWithValue("@TradeId", request.TradeId);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@InstituteList", JsonConvert.SerializeObject(request.InstituteList));

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

        //public async Task<DataTable> Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel model)
        //{
        //    _actionName = "Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel model)";
        //    try
        //    {
        //        DataTable dataTable = new DataTable();
        //        using (var command = await _dbContext.CreateCommandAsync())
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.CommandText = "USP_Counselling_GetOptionsById";
        //            command.Parameters.AddWithValue("@CandidateID", model.CandidateID);
        //            command.Parameters.AddWithValue("@action", "GetOptionsByID");
        //            _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
        //            dataTable = await command.FillAsync_DataTable();
        //        }
        //        return dataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorDesc = new ErrorDescription
        //        {
        //            Message = ex.Message,
        //            PageName = _pageName,
        //            ActionName = _actionName,
        //            SqlExecutableQuery = _sqlQuery
        //        };
        //        var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //        throw new Exception(errordetails, ex);
        //    }
        //}

        public async Task<List<CounsellingOptionFormDataModel>> Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel searchRequest)
        {
            _actionName = "Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel searchRequest)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_GetOptionsById";
                        command.Parameters.AddWithValue("@CandidateID", searchRequest.CandidateID);
                        command.Parameters.AddWithValue("@action", "GetOptionsByID");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new List<CounsellingOptionFormDataModel>();
                    var childData = new List<InstituteListDataModel_Coun>();

                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<List<CounsellingOptionFormDataModel>>(dataSet.Tables[0]);

                            childData = CommonFuncationHelper.ConvertDataTable<List<InstituteListDataModel_Coun>>(dataSet.Tables[1]);

                            data.ForEach(option =>
                            {
                                if (option.OptionID.HasValue)
                                {
                                    option.InstituteList = childData.Where(x => x.OptionID == option.OptionID).ToList();
                                }
                            });
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

        public async Task<DataTable> Counselling_GetDropdownByAction(Counselling_DropdownDataModel model)
        {
            _actionName = "Counselling_GetOptionDetailsByID(CounsellingOptionFormDataModel model)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = await _dbContext.CreateCommandAsync())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_Counselling_Dropdowns";
                    command.Parameters.AddWithValue("@TradeId", model.TradeID);
                    command.Parameters.AddWithValue("@Action", model.Action);
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

        public async Task<DataTable> MapCandidateSSO(CounsellingApplicationSearchModel filterModel)
        {
            _actionName = "MapCandidateSSO()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_GetCandidateDetails";
                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@MobileNo", filterModel.MobileNo);
                        command.Parameters.AddWithValue("@AadharNo", filterModel.AadharNo);
                        command.Parameters.AddWithValue("@Action", filterModel.Action ?? string.Empty);
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


        public async Task<int> UpdateStudentSsoMapping(CounsellingApplicationSearchModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "UpdateStudentSsoMapping(StudentDetailsModel request)";
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_UpdateSsoMapping";
                        command.Parameters.AddWithValue("@CandidateId", request.CandidateId);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
                        await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);// out
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

        public async Task<bool> DeleteOptionByID_Counselling(CounsellingOptionFormDataModel model)
        {
            _actionName = "DeleteOptionByID_Counselling(CounsellingOptionFormDataModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_OptionDetails_IU";
                        command.Parameters.AddWithValue("@action", "DeleteOption");

                        command.Parameters.AddWithValue("@OptionID", model.OptionID);
                        
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

        public async Task<bool> PriorityChange_Counselling(CounsellingOptionFormDataModel model)
        {
            _actionName = "PriorityChange_Counselling(CounsellingOptionFormDataModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_OptionDetails_IU";
                        command.Parameters.AddWithValue("@action", "PriorityChange");

                        command.Parameters.AddWithValue("@OptionID", model.OptionID);
                        command.Parameters.AddWithValue("@CandidateID", model.CandidateID);
                        command.Parameters.AddWithValue("@Type", model.Type);

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

        public async Task<Counselling_DocumentDataModel> GetDocumentDatabyID_Counselling(CounsellingApplicationSearchModel searchRequest)
        {
            _actionName = "GetDocumentDatabyID_Counselling(CounsellingApplicationSearchModel searchRequest)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_GetDocumentsData_ByID";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@CandidateID", searchRequest.CandidateId);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new Counselling_DocumentDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<Counselling_DocumentDataModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count > 0)
                            {

                                data.Counselling_DocumentDetails = CommonFuncationHelper.ConvertDataTable<List<Counselling_DocumentDetailsModel>>(dataSet.Tables[1]);
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

        public async Task<int> SaveDocumentData_Counselling(List<Counselling_DocumentDetailsModel> request)
        {
            _actionName = "SaveDocumentData_Counselling(List<Counselling_DocumentDetailsModel> request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_DocumentsData_IU";
                        command.Parameters.AddWithValue("@action", "_addEditData");

                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(request));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@retval_ID", SqlDbType.Int); // out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

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

        public async Task<CounsellingApplicationPreviewDataModel> PreviewData_ByID_Counselling(CounsellingApplicationSearchModel searchRequest)
        {
            _actionName = "PreviewData_ByID_Counselling(CounsellingApplicationSearchModel searchRequest)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_PreviewData_ByID";
                        command.Parameters.AddWithValue("@CandidateID", searchRequest.CandidateId);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new CounsellingApplicationPreviewDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<CounsellingApplicationPreviewDataModel>(dataSet.Tables[0]);

                            if (dataSet.Tables.Count > 0 && dataSet.Tables[1].Rows.Count > 0)
                            {

                                data.OptionViewData = CommonFuncationHelper.ConvertDataTable<List<OptionviewData_Counselling>>(dataSet.Tables[1]);
                            }

                            if (dataSet.Tables.Count > 1 && dataSet.Tables[2].Rows.Count > 0)
                            {
                                try
                                {
                                    data.PendingDataModel = CommonFuncationHelper.ConvertDataTable<List<PendingDataModel_Counselling>>(dataSet.Tables[2]);
                                }
                                catch { }
                            }
                            if (dataSet.Tables.Count > 2 && dataSet.Tables[3].Rows.Count > 0)
                            {
                                try
                                {
                                    data.DocumentDetailList = CommonFuncationHelper.ConvertDataTable<List<Counselling_DocumentDetailsModel>>(dataSet.Tables[3]);
                                }
                                catch { }
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

        public async Task<bool> DeleteChildOptionByID_Counselling(InstituteListDataModel_Coun model)
        {
            _actionName = "DeleteChildOptionByID_Counselling(InstituteListDataModel_Coun model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_OptionDetails_IU";
                        command.Parameters.AddWithValue("@action", "DeleteOption_child");

                        command.Parameters.AddWithValue("@InstituteOptionID", model.InstituteOptionID);

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

        public async Task<bool> ChildPriorityChange_Counselling(InstituteListDataModel_Coun model)
        {
            _actionName = "ChildPriorityChange_Counselling(InstituteListDataModel_Coun model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_OptionDetails_IU";
                        command.Parameters.AddWithValue("@action", "PriorityChange_Child");

                        command.Parameters.AddWithValue("@OptionID", model.OptionID);
                        command.Parameters.AddWithValue("@InstituteOptionID", model.InstituteOptionID);
                        command.Parameters.AddWithValue("@Type", model.Type);

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
