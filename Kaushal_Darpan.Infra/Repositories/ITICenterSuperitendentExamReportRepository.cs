using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITICenterObserver;
using Kaushal_Darpan.Models.ITICenterSuperitendentExamReport;
using Kaushal_Darpan.Models.ITICollegeMarksheetDownloadmodel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITICenterSuperitendentExamReportRepository : I_ITICenterSuperitendentExamReportRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITICenterSuperitendentExamReportRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITICenterSuperitendentExamReportRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> SaveData(ITICenterSuperitendentExamReportModel request)
        {
            _actionName = "SaveData(ITICenterSuperitendentExamReportModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICenterSuperintendentExamReport";
                        command.Parameters.AddWithValue("@Action","saveCenterSuperitendentExamReportData");
                        command.Parameters.AddWithValue("@ConfidentialityLevel", request.ConfidentialityLevel);
                        command.Parameters.AddWithValue("@ExamOnTime", request.ExamOnTime);
                        command.Parameters.AddWithValue("@ExamOnTimeRemark", request.ExamOnTimeRemark);
                        command.Parameters.AddWithValue("@ExamSchedule", request.ExamSchedule);
                        command.Parameters.AddWithValue("@MarkingGuidance", request.MarkingGuidance);
                        command.Parameters.AddWithValue("@MarkingGuidanceRemark", request.MarkingGuidanceRemark);
                        command.Parameters.AddWithValue("@MarkingGuidanceDocument", request.MarkingGuidanceDocument);
                        command.Parameters.AddWithValue("@ChangeSizeOfUnits", request.ChangeSizeOfUnits);
                        command.Parameters.AddWithValue("@ChangeSizeOfUnitsRemark", request.ChangeSizeOfUnitsRemark);
                        command.Parameters.AddWithValue("@ChangeSizeOfUnitsDocument", request.ChangeSizeOfUnitsDocument);
                        command.Parameters.AddWithValue("@LightFacilities", request.LightFacilities);
                        command.Parameters.AddWithValue("@WaterFacilities", request.WaterFacilities);
                        command.Parameters.AddWithValue("@Discipline", request.Discipline);
                        command.Parameters.AddWithValue("@ToiletFacilities", request.ToiletFacilities);
                        command.Parameters.AddWithValue("@IncidentOnExam", request.IncidentOnExam);
                        command.Parameters.AddWithValue("@IncidentOnExamRemark", request.IncidentOnExamRemark);
                        command.Parameters.AddWithValue("@IncidentOnExamDocument", request.IncidentOnExamDocument);

                        command.Parameters.AddWithValue("@examConductComment", request.examConductComment);
                        command.Parameters.AddWithValue("@examConductCommentRemark", request.examConductCommentRemark);
                        //command.Parameters.AddWithValue("@CommentsOnlightFacilities", request.CommentsOnlightFacilities);
                        //command.Parameters.AddWithValue("@CommentsOnwaterFacilities", request.CommentsOnwaterFacilities);
                        //command.Parameters.AddWithValue("@CommentsOndiscipline", request.CommentsOndiscipline);
                        //command.Parameters.AddWithValue("@CommentsOntoiletFacilities", request.CommentsOntoiletFacilities);
                        command.Parameters.AddWithValue("@futureExamCenterComment", request.futureExamCenterComment);
                        command.Parameters.AddWithValue("@otherFutureExamSuggestions", request.otherFutureExamSuggestions);
                        command.Parameters.AddWithValue("@ExamCenterCommentDocument", request.ExamCenterCommentDocument);

                        command.Parameters.AddWithValue("@FlyingSquadDetails", request.FlyingSquadDetails);
                        command.Parameters.AddWithValue("@ID", request.id);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
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


        public async Task<DataTable> GetCenterSuperitendentReportById(int id)
        {
            _actionName = "GetCenterSuperitendentReportById(int id)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICenterSuperintendentExamReport";
                        command.Parameters.AddWithValue("@Action", "GetCenterSuperitendentReportDataById");
                        command.Parameters.AddWithValue("@ID", id);

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


        public async Task<DataTable> GetCenterSuperitendentReportData()
        {
            _actionName = "GetCenterSuperitendentReportData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICenterSuperintendentExamReport";
                        command.Parameters.AddWithValue("@Action", "GetCenterSuperitendentReportData");
                        //command.Parameters.AddWithValue("@DistrictId", model.DistrictID);
                        //command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        //command.Parameters.AddWithValue("@InstituteId", model.InstituteID);
                        //command.Parameters.AddWithValue("@Code", model.CollegeCode);
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

    }



}

