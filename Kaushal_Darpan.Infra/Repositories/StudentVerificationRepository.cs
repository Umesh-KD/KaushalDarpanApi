using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.studentve;
using Newtonsoft.Json;
using static Kaushal_Darpan.Models.BterApplication.PreviewApplicationFormmodel;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class StudentVerificationRepository : IStudentVerificationRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public StudentVerificationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "StudentVerificationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllStudentData(StudentVerificationSearchModel body)
        {
            _actionName = "GetAllStudentData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentVerificationData";
                        command.Parameters.AddWithValue("@action", body.action);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        if (body.StudentName != null)
                        {
                            command.Parameters.AddWithValue("@StudentName", body.StudentName);
                        }
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@Status", body.Status);
                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@CourseTypeId", body.CourseTypeId);
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

        public async Task<StudentVerificationDocumentsDataModel> GetById(int PK_ID)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetStudentDocumentData";
                        command.Parameters.AddWithValue("@ApplicationId", PK_ID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new StudentVerificationDocumentsDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<StudentVerificationDocumentsDataModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count > 0)
                            {

                                data.VerificationDocumentDetailList = CommonFuncationHelper.ConvertDataTable<List<VerificationDocumentDetailList>>(dataSet.Tables[1]);
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


        public async Task<int> Save_Documentscrutiny(DocumentScrutinyModel request)
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
                        command.CommandText = "USP_StudentDocument_StatusUpdate_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@StudentName", request.StudentName);
                        command.Parameters.AddWithValue("@FatherName", request.FatherName);
                        command.Parameters.AddWithValue("@MotherName", request.MotherName);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@DOB", request.DOB);
                        command.Parameters.AddWithValue("@CertificateGeneratDate", request.CertificateGeneratDate);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@CasteCertificateNo", request.CasteCertificateNo);
                        command.Parameters.AddWithValue("@CategoryA", request.CategoryA);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@JanAadharNo", request.JanAadharNo);
                        command.Parameters.AddWithValue("@JanAadharMemberID", request.JanAadharMemberID);
                        command.Parameters.AddWithValue("@CategoryPre", request.ENR_ID);
                        command.Parameters.AddWithValue("@IsRajasthani", request.IsRajasthani);
                        command.Parameters.AddWithValue("@CourseType", request.coursetype);

                        command.Parameters.AddWithValue("@TradeLevel", request.TradeLevel);
                        command.Parameters.AddWithValue("@TradeID", request.TradeID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@DirectAdmissionTypeID", request.DirectAdmissionTypeID);
                        command.Parameters.AddWithValue("@BranchID", request.BranchID);
                        command.Parameters.AddWithValue("@ApaarID", request.Apaarid);

                        command.Parameters.AddWithValue("@AadharNo", request.AadharNo);
                        command.Parameters.AddWithValue("@DepartmentName", request.DepartmentName);
                        command.Parameters.AddWithValue("@PrefentialCategoryType", request.PrefentialCategoryType);
          
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.AddWithValue("@StudentNameHindi", request.StudentNameHindi);
                        command.Parameters.AddWithValue("@FatherNameHindi", request.FatherNameHindi);
                        command.Parameters.AddWithValue("@MotherNameHindi", request.MotherNameHindi);
                        command.Parameters.AddWithValue("@WhatsNumber", request.WhatsNumber);
                        command.Parameters.AddWithValue("@LandlineNumber", request.LandlineNumber);
                        command.Parameters.AddWithValue("@IndentyProff", request.IndentyProff);
                        command.Parameters.AddWithValue("@DetailID", request.DetailID);
                        command.Parameters.AddWithValue("@Maritial", request.Maritial);
                        command.Parameters.AddWithValue("@Religion", request.Religion);
                        command.Parameters.AddWithValue("@Nationality", request.Nationality);
                        command.Parameters.AddWithValue("@CategoryB", request.CategoryB);
                        command.Parameters.AddWithValue("@CategoryC", request.CategoryC);
                        command.Parameters.AddWithValue("@CategoryE", request.CategoryE);
                        //command.Parameters.AddWithValue("@Prefential", request.Prefential);
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
                        command.Parameters.AddWithValue("@CreateByRoleID", request.RoleID);
                        command.Parameters.AddWithValue("@IsMBCCertificate", request.IsMBCCertificate);
                        command.Parameters.AddWithValue("@QualificationID", request.QualificationID);
                        command.Parameters.AddWithValue("@BoardID", request.BoardID);
                        command.Parameters.AddWithValue("@BoardStateID", request.BoardStateID);
                        command.Parameters.AddWithValue("@BoardExamID", request.BoardExamID);
                        command.Parameters.AddWithValue("@RollNumber", request.RollNumber);
                        command.Parameters.AddWithValue("@Percentage", request.Percentage);
                        command.Parameters.AddWithValue("@IsSupplement", request.IsSupplement);
                        command.Parameters.AddWithValue("@CategoryAbyChecker", request.CategoryAbyChecker);

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
                        command.Parameters.AddWithValue("@SubjectID", JsonConvert.SerializeObject(request.LateralEntryQualificationModel?.FirstOrDefault()?.SubjectID));
                        command.Parameters.AddWithValue("@DocumentList", JsonConvert.SerializeObject(request.VerificationDocumentDetailList));
         

                        command.Parameters.AddWithValue("@status", request.status);
                        command.Parameters.AddWithValue("@Remark", request.Remark);


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

        public async Task<DocumentScrutinyModel> DocumentScrunityData(BterSearchModel searchRequest)
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
                        command.CommandText = "USP_GetDocumentScrutinyData";
                        command.Parameters.AddWithValue("@SSOID", searchRequest.SSOID);
                        command.Parameters.AddWithValue("@DepartmentID", searchRequest.DepartmentID);
                        command.Parameters.AddWithValue("@JanAadharMemberID", searchRequest.JanAadharMemberID);
                        command.Parameters.AddWithValue("@JanAadharNo", searchRequest.JanAadharNo);
                        command.Parameters.AddWithValue("@ApplicationId", searchRequest.ApplicationId);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new DocumentScrutinyModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<DocumentScrutinyModel>(dataSet.Tables[0]);

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

                                data.RemarkModel= CommonFuncationHelper.ConvertDataTable<List<RemarkModel>>(dataSet.Tables[6]);

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




        public async Task<int> NotifyStudent(NotifyStudentModel request)
        {
            _actionName = "NotifyStudent(NotifyStudentModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_DTE_NotifyStudent";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@AcademicYear", request.AcademicYear);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@MessageType", EnumMessageType.Bter_NotifyCandidateDeficiency.GetDescription());
                        command.Parameters.AddWithValue("@StudentApplicationList", JsonConvert.SerializeObject(request.List));
                        // Add IP Address parameter

                        // Add the return parameter
                        command.Parameters.Add("@retval_TransactionId", SqlDbType.Int); // out
                        command.Parameters["@retval_TransactionId"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@retval_TransactionId"].Value); // out
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
