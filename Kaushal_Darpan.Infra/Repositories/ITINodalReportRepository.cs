using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ITIPapperSetter;
using Kaushal_Darpan.Models.ITITimeTable;
using Kaushal_Darpan.Models.NodalApperentship;
using Kaushal_Darpan.Models.ScholarshipMaster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITINodalReportRepository: IITINodalReportRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITINodalReportRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITINodalReportRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> SaveData(ITIPMNAM_MelaReportBeforeAfterModal request)
        {
            _actionName = "SaveData(ITIPMNAM_MelaReportBeforeAfterModal model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {
               
                        command.CommandText = "USP_Save_PMNAM_MelaReportBeforeAfter";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", request.PKID == 0 ? "Insert" : "Update");
                        command.Parameters.AddWithValue("@EstablishmentsRegisterNoBefore", request.EstablishmentsRegisterNoBefore);
                        command.Parameters.AddWithValue("@NumberofSeatBefore", request.NumberofSeatBefore);
                        command.Parameters.AddWithValue("@NumberofEmployedStudentBefore", request.NumberofEmployedStudentBefore);
                        command.Parameters.AddWithValue("@EstablishmentsRegisterNoAfter", request.EstablishmentsRegisterNoAfter);
                        command.Parameters.AddWithValue("@NumberofSeatAfter", request.NumberofSeatAfter);
                        command.Parameters.AddWithValue("@NumberofEmployedStudentAfter", request.NumberofEmployedStudentAfter);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", request.Createdby);
                        command.Parameters.AddWithValue("@PKID", request.PKID);
                        command.Parameters.AddWithValue("@AfterDate", request.AfterDate);
                        command.Parameters.AddWithValue("@BeforeDate", request.BeforeDate);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);

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

        public async Task<DataTable> GetAllData(ITIPMNAM_Report_SearchModal request)
        {
            _actionName = "SaveData(ITIPMNAM_MelaReportBeforeAfterModal model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_Save_PMNAM_MelaReportBeforeAfter";
                        command.CommandType = CommandType.StoredProcedure;  
                        command.Parameters.AddWithValue("@Action", "GetReportData");
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", request.Createdby);
                        command.Parameters.AddWithValue("@PKID", request.PKID);

                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@BeforeMonth", request.BeforeMonth);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);

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

        public async Task<DataTable> PMNAM_report_DeletebyID(int PKID =0)
        {
            _actionName = "PMNAM_report_DeletebyID(ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_Save_PMNAM_MelaReportBeforeAfter";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "DeletebyID");
                        command.Parameters.AddWithValue("@PKID", PKID);

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
        public async Task<DataTable> GetReportDatabyID(int PKID = 0)
        {
            _actionName = "GetReportDatabyID(ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_Save_PMNAM_MelaReportBeforeAfter";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "GetReportData");
                        command.Parameters.AddWithValue("@PKID", PKID);

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

        public async Task<DataTable> SaveDataMelaReportCount(ITIPMNAMAppApprenticeshipReportEntity request)
        {
            _actionName = "SaveData(ITIPMNAM_MelaReportBeforeAfterModal model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_Save_PMNAM_MelaReport";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", request.ID == 0 ? "Insert" : "Update");
                        command.Parameters.AddWithValue("@PoliticalEstablishmentspartNo", request.PoliticalEstablishmentspartNo);
                        command.Parameters.AddWithValue("@PrivateEstablishmentspartNo", request.PrivateEstablishmentspartNo);
                        command.Parameters.AddWithValue("@PoliticalEstablishmentscontactedNo", request.PoliticalEstablishmentscontactedNo);
                        command.Parameters.AddWithValue("@PrivateEstablishmentscontactedNo", request.PrivateEstablishmentscontactedNo);
                        command.Parameters.AddWithValue("@CandidatespresentMaleNo", request.CandidatespresentMaleNo);
                        command.Parameters.AddWithValue("@CandidatespresentFemaleNo", request.CandidatespresentFemaleNo);
                        command.Parameters.AddWithValue("@CandidatessselectedMaleNo", request.CandidatessselectedMaleNo);
                        command.Parameters.AddWithValue("@CandidatessselectedFemaleNo", request.CandidatessselectedFemaleNo);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", request.Createdby);
                        command.Parameters.AddWithValue("@PKID", request.ID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);

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


        public async Task<int> Save_QuaterReport(ITIApprenticeshipWorkshop request)
        {
            _actionName = "SaveExaminerData(ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIApprenticeshipWorkshop_AddEdit";
                        command.CommandType = CommandType.StoredProcedure;




                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@QuaterID", request.QuaterID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@ParticipateSsoid", request.ParticipateSsoid);
                        command.Parameters.AddWithValue("@ParticipateName", request.ParticipateName);
                        command.Parameters.AddWithValue("@WorkshopeDate", request.WorkshopeDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@WorkshopDetail", request.WorkshopDetail);
                        command.Parameters.AddWithValue("@BeforeEstablishmentNo", request.BeforeEstablishmentNo);
                        command.Parameters.AddWithValue("@BeforeEstablishmentSeat", request.BeforeEstablishmentSeat);
                        command.Parameters.AddWithValue("@BeforeStudentCount", request.BeforeStudentCount);
                        command.Parameters.AddWithValue("@AfterEstablishmentNo", request.AfterEstablishmentNo);
                        command.Parameters.AddWithValue("@AfterEstablishmentSeat", request.AfterEstablishmentSeat);
                        command.Parameters.AddWithValue("@AfterStudentCount", request.AfterStudentCount);
                        command.Parameters.AddWithValue("@RegisterStudentPdf", request.RegisterStudentPdf);
                        command.Parameters.AddWithValue("@DisRegisterStudentPdf", request.DisRegisterStudentPdf);
                        command.Parameters.AddWithValue("@DisWorkshopPdf", request.DisWorkshopPdf);
                        command.Parameters.AddWithValue("@WorkshopPdf", request.WorkshopPdf);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@QuaterIncreaseEstablishment", request.QuaterIncreaseEstablishment);
                        command.Parameters.AddWithValue("@QuaterIncreaseSeat", request.QuaterIncreaseSeat);
                        command.Parameters.AddWithValue("@QuaterIncreaseStudent", request.QuaterIncreaseStudent);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);

                        command.Parameters.AddWithValue("@jsonData", JsonConvert.SerializeObject(request.ApprenticeshipWorkshopMembersList));

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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

        public async Task<DataTable> GetQuaterProgressList(ITIApprenticeshipWorkshop request)
        {
            _actionName = "GetQuaterProgressList(ITIPMNAM_MelaReportBeforeAfterModal model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_ITIApprenticeshipWorkshop_Get";
                        command.CommandType = CommandType.StoredProcedure;
       
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@QuaterID", request.QuaterID);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
              

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
        public async Task<DataTable> GetQuaterReportById(int PKID = 0)
        {
            _actionName = "GetReportDatabyID(ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITIApprenticeshipWorkshop_GetByI";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ID", PKID);
                        command.Parameters.AddWithValue("@Action", "GetById");

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

        public async Task<DataTable> GetAAADetailsById(int PKID = 0)
        {
            _actionName = "GetAAADetailsById(ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITIApprenticeshipWorkshop_GetByI";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ID", PKID);
                        command.Parameters.AddWithValue("@Action", "GetByAAADetails");

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
        public async Task<DataTable> QuaterListDelete(int PKID = 0)
        {
            _actionName = "PMNAM_report_DeletebyID(ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_ITIApprenticeshipWorkshop_GetByI";
                        command.CommandType = CommandType.StoredProcedure;
             
                        command.Parameters.AddWithValue("@PKID", PKID);
                        command.Parameters.AddWithValue("@Action", "Delete");

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

        public async Task<DataTable> GetAllData(int UserID,int DistrictID)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_Save_PMNAM_MelaReport";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "GetReportData");
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);
                        //command.Parameters.AddWithValue("@EndTermId", request.EndTermID);
                        //command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        //command.Parameters.AddWithValue("@CreatedBy", request.Createdby);
                        //command.Parameters.AddWithValue("@PKID", request.PKID);
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

        public async Task<DataTable> DeleteData_Pmnam_mela_Report(ITIPMNAMAppApprenticeshipReportEntity request)
        {
            _actionName = "DeleteData_Pmnam_mela_Report(ITIPMNAM_MelaReportBeforeAfterModal model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_Save_PMNAM_MelaReport";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "DeletebyID");
                        command.Parameters.AddWithValue("@PKID", request.ID);
                        command.Parameters.AddWithValue("@ActiveStatus", request.IsActive);

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


        public async Task<DataTable> Save_ITIWorkshopProgressRPT(List<workshopProgressRPTList> request)
        {
            _actionName = "Save_ITIWorkshopProgressRPT(ITI_workshopProgressReportSaveModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        foreach (var item in request)
                        {
                            command.Parameters.Clear();
                            command.CommandText = "USP_Save_WorkshopProgressReport";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Action", item.PKID == 0 ? "Insert" : "Update");
                            command.Parameters.AddWithValue("@workshopDate",item._workshopDate);
                            command.Parameters.AddWithValue("@OrganisedDistrictID",item._OrganisedDistrictID);
                            command.Parameters.AddWithValue("@ParticipatedDistrictListIDs",item._SelectedDistrictListIDs);
                            command.Parameters.AddWithValue("@establishmentName",item._establishmentName);
                            command.Parameters.AddWithValue("@establishmentAddress",item._establishmentAddress);
                            command.Parameters.AddWithValue("@representativeName",item._representativeName);
                            command.Parameters.AddWithValue("@representativedesignation",item._representativedesignation);
                            command.Parameters.AddWithValue("@representativeMobile",item._representativeMobile);
                            command.Parameters.AddWithValue("@Remars",item._Remars);
                            command.Parameters.AddWithValue("@DepartmentID",item._DepartmentID);
                            command.Parameters.AddWithValue("@CreatedBy",item._Createdby);
                            command.Parameters.AddWithValue("@PKID",item.PKID);
                            command.Parameters.AddWithValue("@EndTermID", item._EndTermID);
                            command.Parameters.AddWithValue("@Roleid",item._RoleID);

                            _sqlQuery = command.GetSqlExecutableQuery();
                            dataTable = await command.FillAsync_DataTable();
                        }

                    
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

        public async Task<DataTable> Submit_Apprenticeship_data(ApprenticeshipEntryDto entry, string businessNameCsv)
        {
            _actionName = "Submit_Apprenticeship_data(ApprenticeshipEntryDto model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_Save_Apprenticeship_Registration_Report";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", entry.PKID==0 ? "Insert" :"Update");
                        command.Parameters.AddWithValue("@Nameofinstitute", entry.Nameofinstitute);
                        command.Parameters.AddWithValue("@Dateofregistration", entry.Dateofregistration);
                        command.Parameters.AddWithValue("@BusinessName", businessNameCsv);
                        command.Parameters.AddWithValue("@NumberofTrainees", entry.NumberofTrainees);
                        command.Parameters.AddWithValue("@Numberofapprentices", entry.Numberofapprentices);
                        command.Parameters.AddWithValue("@Remarks", entry.Remarks);
                        command.Parameters.AddWithValue("@PKID", entry.PKID);
                        command.Parameters.AddWithValue("@CreateBy", entry.Createdby);
                        command.Parameters.AddWithValue("@DepartmentId", entry.DepartmentID);
                        command.Parameters.AddWithValue("@EndtermId", entry.EndTermID);
                        command.Parameters.AddWithValue("@NumberOfRegistrationDoc", entry.NumberOfRegistrationDoc);
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


        public async Task<DataTable> Get_WorkshopProgressRPT_AllData(WorkshopProgressRPTSearchModal request)
        {
            _actionName = "Get_WorkshopProgressRPT_AllData(WorkshopProgressRPTSearchModal model)";
            return await Task.Run(async () =>
            {
                try
                {
                      DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_Save_WorkshopProgressReport";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "GetWorkshopProgressReportData");
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CreatedBy", request.Createdby);
                        command.Parameters.AddWithValue("@PKID", request.PKID);
                        command.Parameters.AddWithValue("@OrganisedDistrictID", request.SearchDistrictID);

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

        public async Task<DataTable> WorkshopProgressRPTDelete_byID(int PKID = 0)
        {
            _actionName = "PMNAM_report_DeletebyID(ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_Save_WorkshopProgressReport";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "DeletebyID");
                        command.Parameters.AddWithValue("@PKID", PKID);

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



        public async Task<DataTable> GetSamplePassoutStudent(ITITimeTableSearchModel request)
        {
            _actionName = "GetSampleTimeTableITI(TimeTableSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetSamplePassoutDetails";
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

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

        public async Task<DataTable> SampleImportExcelFileFresher(ITITimeTableSearchModel request)
        {
            _actionName = "GetSampleTimeTableITI(TimeTableSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetSampleFresherDetails";
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

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

        public async Task<DataTable> MelaSampleImportExcelFile(ITITimeTableSearchModel request)
        {
            _actionName = "MelaSampleImportExcelFile(TimeTableSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetSampleMelaSampleReport";
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

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
        public async Task<int> SavePassoutReport(ITIApprenticeshipRegPassOutModel request)
        {
            _actionName = "SaveExaminerData(ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ItiPassoutRegSave";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", request.PKID == 0 ? "Insert" : "Update");
                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);
                        command.Parameters.AddWithValue("@RegCount", request.RegCount);
                        command.Parameters.AddWithValue("@Dis_FilePath", request.Dis_FilePath);
                        command.Parameters.AddWithValue("@FileName", request.FileName);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@RegDate", request.RegDate);
                        command.Parameters.AddWithValue("@UserID", request.UserID);



                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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

        public async Task<int> SaveFresherReport(ITIApprenticeshipRegPassOutModel request)
        {
            _actionName = "SaveFresherReport(ExaminerMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ItiFresherRegSave";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", request.PKID == 0 ? "Insert" : "Update");
                        command.Parameters.AddWithValue("@ID", request.ID);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);
                        command.Parameters.AddWithValue("@RegCount", request.RegCount);
                        command.Parameters.AddWithValue("@Dis_FilePath", request.Dis_FilePath);
                        command.Parameters.AddWithValue("@FileName", request.FileName);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@RegDate", request.RegDate);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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

        public async Task<DataTable> Get_ApprenticeshipRegistrationReportAllData(ApprenticeshipRegistrationSearchModal request)
        {
            _actionName = "Get_ApprenticeshipRegistrationReportAllData(WorkshopProgressRPTSearchModal model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_Save_Apprenticeship_Registration_Report";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "GetApprenticeshipReportData");
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CreateBy", request.Createdby);
                        command.Parameters.AddWithValue("@PKID", request.PKID);

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


        public async Task<DataTable> ApprenticeshipRegistrationRPTDelete_byID(int PKID = 0)
        {
            _actionName = "ApprenticeshipRegistrationRPTDelete_byID(ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_Save_Apprenticeship_Registration_Report";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "DeletebyID");
                        command.Parameters.AddWithValue("@PKID", PKID);

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

        public async Task<DataTable> Get_PassingRegistrationReportAllData(ApprenticeshipRegistrationSearchModal request)
        {
            _actionName = "Get_PassingRegistrationReportAllData(WorkshopProgressRPTSearchModal model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_ItiPassoutRegDetail";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "GetPassingReportData");
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CreateBy", request.Createdby);
                        command.Parameters.AddWithValue("@PKID", request.PKID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);

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


        public async Task<DataTable> Get_FresherRegistrationReportAllData(ApprenticeshipRegistrationSearchModal request)
        {
            _actionName = "Get_FresherRegistrationReportAllData(WorkshopProgressRPTSearchModal model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_ItiFresherRegDetail";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "GetPassingReportData");
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CreateBy", request.Createdby);
                        command.Parameters.AddWithValue("@PKID", request.PKID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);

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

        public async Task<int> PassoutRegistrationRPTDelete_byID(int PKID = 0)
        {
            _actionName = "PassoutRegistrationRPTDelete_byID(ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_ItiPassoutRegSave";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "DeletebyID");
                        command.Parameters.AddWithValue("@PKID", PKID);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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
        public async Task<int> FresherRegistrationRPTDelete_byID(int PKID = 0)
        {
            _actionName = "FresherRegistrationRPTDelete_byID(ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_ItiFresherRegSave";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "DeletebyID");
                        command.Parameters.AddWithValue("@PKID", PKID);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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

