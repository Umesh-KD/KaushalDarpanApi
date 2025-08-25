using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.ApplicationMessageModel;
using Kaushal_Darpan.Models.BTER;
using Kaushal_Darpan.Models.studentve;
using Newtonsoft.Json;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class CorrectMeritRepository : ICorrectMeritRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CorrectMeritRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CorrectMeritRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        public async Task<DataTable> CorrectMeritList(CorrectMeritSearchModel body)
        {
            _actionName = "CorrectMeritList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_CorrectMerit_List";
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@AcademicYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@StreamID", body.BranchID);
                        command.Parameters.AddWithValue("@ApplicationNo", body.ApplicationNo);
                        command.Parameters.AddWithValue("@CategoryA", body.CategoryA);
                        command.Parameters.AddWithValue("@CategoryB", body.CategoryB);
                        command.Parameters.AddWithValue("@CategoryC", body.CategoryC);
                        command.Parameters.AddWithValue("@CategoryD", body.CategoryD);
                        command.Parameters.AddWithValue("@CategoryE", body.CategoryE);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
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

        public async Task<MeritDocumentScrutinyModel> MeritDocumentScrunityData(BterSearchModel searchRequest)
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
                        command.CommandText = "USP_Get_MeritDocumentScrutinyData";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@ApplicationId", searchRequest.ApplicationId);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new MeritDocumentScrutinyModel();

                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<MeritDocumentScrutinyModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count > 0)
                            {

                                data.HighestQualificationModel = CommonFuncationHelper.ConvertDataTable<List<HighestQualificationModel>>(dataSet.Tables[1]);

                            }

                            if (dataSet.Tables[3].Rows.Count > 0)
                            {

                                data.LateralEntryQualificationModel = CommonFuncationHelper.ConvertDataTable<List<LateralEntryQualificationModel>>(dataSet.Tables[3]);
                            }

                            if (dataSet.Tables[2].Rows.Count > 0)
                            {
                                data.SupplementaryDataModel = CommonFuncationHelper.ConvertDataTable<List<SupplementaryDataModel>>(dataSet.Tables[2]);
                            }

                            if (dataSet.Tables[4].Rows.Count > 0)
                            {
                                data.VerificationDocumentDetailList = CommonFuncationHelper.ConvertDataTable<List<VerificationDocumentDetailList>>(dataSet.Tables[4]);
                            }
                            if (dataSet.Tables[5].Rows.Count > 0)
                            {

                                data.LateralEntryQualificationModel.FirstOrDefault().SubjectID = CommonFuncationHelper.ConvertDataTable<List<Lateralsubject>>(dataSet.Tables[5]);

                            }
                            if (dataSet.Tables[6].Rows.Count > 0)
                            {

                                data.RemarkModel = CommonFuncationHelper.ConvertDataTable<List<RemarkModel>>(dataSet.Tables[6]);

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

        public async Task<int> Save_MeritDocumentscrutiny(MeritDocumentScrutinyModel request)
        {
            _actionName = "Save_CompanyValidation_NodalAction(StudentVerificationDocumentsDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveCorrectMerit_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditData");
                        command.Parameters.AddWithValue("@SupplementaryDataModel", JsonConvert.SerializeObject(request.SupplementaryDataModel));
                        command.Parameters.AddWithValue("@DocumentList", JsonConvert.SerializeObject(request.DocumentPushModel));
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@MeritId", request.MeritId);
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
                        //command.Parameters.AddWithValue("@WhatsNumber", request.WhatsNumber);
                        //command.Parameters.AddWithValue("@LandlineNumber", request.LandlineNumber);
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
                        command.Parameters.AddWithValue("@JanAadharMemberID", request.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", request.JanAadharNo);
                        //command.Parameters.AddWithValue("@Dis_SignaturePhoto", request.Dis_SignaturePhoto);
                        //command.Parameters.AddWithValue("@Dis_StudentPhoto", request.Dis_StudentPhoto);
                        //command.Parameters.AddWithValue("@SignaturePhoto", request.SignaturePhoto);
                        //command.Parameters.AddWithValue("@StudentPhoto", request.StudentPhoto);
                        command.Parameters.AddWithValue("@QualificationID", request.QualificationID);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@BoardID", request.BoardID);
                        command.Parameters.AddWithValue("@PassingID", request.PassingID);
                        command.Parameters.AddWithValue("@RollNumber", request.RollNumber);
                        command.Parameters.AddWithValue("@MarkType", request.MarkType);
                        command.Parameters.AddWithValue("@AggMaxMark", request.AggMaxMark);
                        command.Parameters.AddWithValue("@Percentage", request.Percentage);
                        command.Parameters.AddWithValue("@AggObtMark", request.AggObtMark);
                        command.Parameters.AddWithValue("@status", request.status);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@IsSupplement", request.IsSupplement);
                        command.Parameters.AddWithValue("@LateralEntryQualificationModel", JsonConvert.SerializeObject(request.LateralEntryQualificationModel));
                        command.Parameters.AddWithValue("@SubjectID", JsonConvert.SerializeObject(request.LateralEntryQualificationModel?.FirstOrDefault()?.SubjectID));
                        command.Parameters.AddWithValue("@IsTSP", request.IsTSP);
                        command.Parameters.AddWithValue("@IsSaharia", request.IsSaharia);
                        command.Parameters.AddWithValue("@TspDistrictID", request.TspDistrictID);
                        command.Parameters.AddWithValue("@IsDevnarayan", request.IsDevnarayan);
                        command.Parameters.AddWithValue("@DevnarayanDistrictID", request.DevnarayanDistrictID);
                        command.Parameters.AddWithValue("@DevnarayanTehsilID", request.DevnarayanTehsilID);
                        command.Parameters.AddWithValue("@TSPTehsilID", request.TSPTehsilID);
                        command.Parameters.AddWithValue("@subCategory", request.subCategory);
                        command.Parameters.AddWithValue("@CategoryD", request.CategoryD);




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
    


     public async Task<int> Reject_Document(RejectModel request)
        {
            _actionName = "Save_CompanyValidation_NodalAction(StudentVerificationDocumentsDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_RejectDocumentScrutiny";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling

                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@MeritId", request.MeritId);

                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);


                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add IP Address parameter

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

        public async Task<DataTable> GetApplicationIDByMeritID(CorrectMerit_ApplicationSearchModel body)
        {
            _actionName = "GetApplicationIDByMeritID(CorrectMerit_ApplicationSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Merit_GetAppIDByMeritID";
                        command.Parameters.AddWithValue("@MeritID", body.MeritID);
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

        public async Task<bool> ApproveMerit(CorrectMeritApproveDataModel model)
        {
            _actionName = "ApproveMerit(BterOptionsDetailsDataModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_CorrectMerit_Approve";
                        command.Parameters.AddWithValue("@ApplicationID", model.ApplicationID);
                        command.Parameters.AddWithValue("@MeritId", model.MeritId);
                        command.Parameters.AddWithValue("@Remark", model.Remark);
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@status", model.status);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
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

        public async Task<DataTable> GetApplicationDetails_ByMeritId(int MeritId)
        {
            _actionName = "GetApplicationDetails_ByMeritId(int MeritId)";
            try
            {
                DataTable dataTable = new DataTable();
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetApplicationDetails_ByMeritId";
                    command.Parameters.AddWithValue("@MeritId", MeritId);
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
    }
}
