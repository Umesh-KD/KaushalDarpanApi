using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITINodalOfficerExminerReport;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.StudentMaster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class ITINodalOfficerExminerReportRepository : IITINodalOfficerExminerReportRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITINodalOfficerExminerReportRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIExaminationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<int> ITINodalOfficerExminerReportSave(ITINodalOfficerExminerReport body)
        {
            _actionName = "ITINodalOfficerExminerReportSave(ITINodalOfficerExminerReport body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    var jsonData = JsonConvert.SerializeObject(body.InspectExaminationCentersList);

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_trn_ITI_NodalOfficersCenterReport";
                        command.Parameters.AddWithValue("@Action", "InsertUpdate");
                        command.Parameters.AddWithValue("@ID", body.ID);
                        command.Parameters.AddWithValue("@ExamCenterUnderYourAreaID", body.ExamCenterUnderYourAreaID);
                        command.Parameters.AddWithValue("@MediumQuestionPaperSent", body.MediumQuestionPaperSent);
                        command.Parameters.AddWithValue("@Date", body.Date);
                        command.Parameters.AddWithValue("@CoordinatorReachOnTime", body.CoordinatorReachOnTime);
                        command.Parameters.AddWithValue("@Reason", body.Reason);
                        command.Parameters.AddWithValue("@SupportingDocument_file", body.SupportingDocument_file);
                        command.Parameters.AddWithValue("@SupportingDocument_fileName", body.SupportingDocument_fileName);
                        command.Parameters.AddWithValue("@InspectTheExaminationCenters", body.InspectTheExaminationCenters);
                        command.Parameters.AddWithValue("@AdditionalDetails", body.AdditionalDetails);
                        command.Parameters.AddWithValue("@UploadDocument_file", body.UploadDocument_file);
                        command.Parameters.AddWithValue("@UploadDocument_fileName", body.UploadDocument_fileName);
                        command.Parameters.AddWithValue("@ExamSmooth", body.ExamSmooth);
                        command.Parameters.AddWithValue("@DocumentsSubmitted", body.DocumentsSubmitted);
                        command.Parameters.AddWithValue("@ExamIncident", body.ExamIncident);                        
                        command.Parameters.AddWithValue("@ExamRemarks", body.ExamRemarks);
                        command.Parameters.AddWithValue("@FutureCentreRemarks", body.FutureCentreRemarks);
                        command.Parameters.AddWithValue("@FutureExamSuggestions", body.FutureExamSuggestions);
                        command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
                        command.Parameters.AddWithValue("@UpdatedBy", body.UpdatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@CoordinatorNotReached", body.CoordinatorNotReached);
                        command.Parameters.AddWithValue("@ExamDate", body.ExamDate);
                        command.Parameters.AddWithValue("@ToDate", body.ToDate);
                        command.Parameters.AddWithValue("@NodalOfficersCenterDataJson", jsonData);
                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
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



        public async Task<DataTable> ITINodalOfficerExminerReport_GetAllData(ITINodalOfficerExminerReportSearch body)
        {
            _actionName = "ITINodalOfficerExminerReport_GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_trn_ITI_NodalOfficersCenterReportGetData";
                        command.Parameters.AddWithValue("@action", "GetData");
                        command.Parameters.AddWithValue("@ID", 0);
                        command.Parameters.AddWithValue("@FinancialYearID", body.FinancialYearID);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        //command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
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

        public async Task<DataTable> GetAllData(Nodalsearchmodel body)
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
                        command.CommandText = "USP_GetDistrictExamNodalMapping";
                     
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", body.AcademicYearID);
                        //command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
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



        public async Task<DataTable> ITINodalOfficerExminerReport_GetAllDataByID(ITINodalOfficerExminerReportSearch body)
        {
            _actionName = "ITINodalOfficerExminerReport_GetAllDataByID()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_trn_ITI_NodalOfficersCenterReportGetData";
                        command.Parameters.AddWithValue("@action", "GetDataByNodalOfficersCenterID");
                        command.Parameters.AddWithValue("@ID", body.ID);
                        command.Parameters.AddWithValue("@FinancialYearID", 0);
                        //command.Parameters.AddWithValue("@CreatedBy", body.CreatedBy);
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

        public async Task<ITINodalOfficerExminerReportByID> ITINodalOfficerExminerReport_GetDataByID(ITINodalOfficerExminerReportSearch body)
        {
            _actionName = "ITINodalOfficerExminerReport_GetDataByID(ITINodalOfficerExminerReportSearch body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    var data = new ITINodalOfficerExminerReportByID();

                   
                        using (var command = _dbContext.CreateCommand())
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "usp_trn_ITI_NodalOfficersCenterReportGetData";
                            command.Parameters.AddWithValue("@Action", "GetDataByID");                          
                            command.Parameters.AddWithValue("@ID", body.ID);
                            command.Parameters.AddWithValue("@FinancialYearID", 0);
                            command.Parameters.AddWithValue("@Date", body.Date);
                            _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                            dataSet = await command.FillAsync();
                        }

                        if (dataSet != null)
                        {
                            if (dataSet.Tables.Count > 0)
                            {
                                data.ITINodalOfficerExminerReports = CommonFuncationHelper.ConvertDataTable<ITINodalOfficerExminerReport>(dataSet.Tables[0]);
                            }
                            if (dataSet.Tables.Count > 0)
                            {
                                data.InspectExaminationCentersList = CommonFuncationHelper.ConvertDataTable<List<ITIInspectExaminationCenters>>(dataSet.Tables[1]);
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

        public async Task<ITIInspectExaminationCenters> ITINodalOfficerExminerReportDetails_GetByID(int PK_Id)
        {
            _actionName = "ITINodalOfficerExminerReportDetails_GetByID(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_trn_ITI_NodalOfficersCenterReportGetData";
                        command.Parameters.AddWithValue("@action", "GetDetailsDataByID");
                        command.Parameters.AddWithValue("@ID", PK_Id);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITIInspectExaminationCenters();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITIInspectExaminationCenters>(dataSet.Tables[0]);




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

        public async Task<int> ITINodalOfficerExminerReportDetailsUpdate(ITIInspectExaminationCenters body)
        {
            _actionName = "ITINodalOfficerExminerReportDetailsUpdate(ITIInspectExaminationCenters body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;                  

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_trn_ITI_NodalOfficersCenterReportDetailsUpdate";                      
                        command.Parameters.AddWithValue("@Action", "Update");
                        command.Parameters.AddWithValue("@ID", body.ID);
                        command.Parameters.AddWithValue("@DateAndTimeOfInspection", body.DateAndTimeOfInspection);
                        command.Parameters.AddWithValue("@TotalNumberOfCandidatesEnrolled", body.TotalNumberOfCandidatesEnrolled);
                        command.Parameters.AddWithValue("@CandidatesHadLeftAfterCompletingTheExam", body.CandidatesHadLeftAfterCompletingTheExam);
                        command.Parameters.AddWithValue("@JobsCreated", body.JobsCreated);
                        command.Parameters.AddWithValue("@JobsBeingCreated", body.JobsBeingCreated);
                        command.Parameters.AddWithValue("@VivaConducted", body.VivaConducted);
                        command.Parameters.AddWithValue("@LineDiagramPrepared", body.LineDiagramPrepared);
                        command.Parameters.AddWithValue("@ReadingTaken", body.ReadingTaken);
                        command.Parameters.AddWithValue("@NameOfExaminationCentreID", body.NameOfExaminationCentreID);
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
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

        public async Task<int> ITINodalOfficerExminerReportDetailsDelete(ITIInspectExaminationCenters body)
        {
            _actionName = "ITINodalOfficerExminerReportDetailsDelete(ITIInspectExaminationCenters body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_trn_ITI_NodalOfficersCenterReportDetailsUpdate";
                        command.Parameters.AddWithValue("@Action", "Delete");
                        command.Parameters.AddWithValue("@ID", body.ID);
                        command.Parameters.AddWithValue("@DateAndTimeOfInspection", "");
                        command.Parameters.AddWithValue("@TotalNumberOfCandidatesEnrolled", 0);
                        command.Parameters.AddWithValue("@CandidatesHadLeftAfterCompletingTheExam", 0);
                        command.Parameters.AddWithValue("@JobsCreated", 0);
                        command.Parameters.AddWithValue("@JobsBeingCreated", 0);
                        command.Parameters.AddWithValue("@VivaConducted", 0);
                        command.Parameters.AddWithValue("@LineDiagramPrepared", 0);
                        command.Parameters.AddWithValue("@ReadingTaken", 0);
                        command.Parameters.AddWithValue("@NameOfExaminationCentreID", 0);
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.Add("@Return", SqlDbType.Int);
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);
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


        public async Task<DataSet> Generate_ITINodalOfficerExminerReport_ByID(int id,int DistrictID,string ExamDateTime)
        {
            _actionName = "ITINodalOfficerExminerReport_GetDataByID(ITINodalOfficerExminerReportSearch body)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    var data = new ITINodalOfficerExminerReportByID();


                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_trn_ITI_NodalOfficersCenterReportGetData";
                        command.Parameters.AddWithValue("@Action", "Generate_ITINodalOfficerExminerReport_ByID");
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);
                        command.Parameters.AddWithValue("@Date", ExamDateTime);
                        command.Parameters.AddWithValue("@FinancialYearID", 9);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }

                    //if (dataSet != null)
                    //{
                    //    if (dataSet.Tables.Count > 0)
                    //    {
                    //        data.ITINodalOfficerExminerReports = CommonFuncationHelper.ConvertDataTable<ITINodalOfficerExminerReport>(dataSet.Tables[0]);
                    //    }
                    //    if (dataSet.Tables.Count > 0)
                    //    {
                    //        data.InspectExaminationCentersList = CommonFuncationHelper.ConvertDataTable<List<ITIInspectExaminationCenters>>(dataSet.Tables[1]);
                    //    }

                    //}



                    return dataSet;
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



        public async Task<int> SaveAllData(NodalExamMapping body)
        {
            _actionName = "SaveAllData(ITIInspectExaminationCenters body)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiExamNodalExamMapping";

                        command.Parameters.AddWithValue("@UserName", body.UserName);
                        command.Parameters.AddWithValue("@Email", body.Email);
                        command.Parameters.AddWithValue("@MobileNumber", body.MobileNumber);
                        command.Parameters.AddWithValue("@ExamNodalID", body.ExamNodalID);
                        command.Parameters.AddWithValue("@FinancialYearID", body.AcademicYearID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@ModifyBy", body.ModifyBy);
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);



                        command.Parameters.Add("@retval_ID", SqlDbType.Int);
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);
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

    }
}
