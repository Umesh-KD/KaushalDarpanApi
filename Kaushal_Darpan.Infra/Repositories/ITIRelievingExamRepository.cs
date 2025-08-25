using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITIRelievingExam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIRelievingExamRepository : IITIRelievingExamRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIRelievingExamRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIRelievingExamRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        public async Task<DataTable> SaveRelievingExaminerDataAsync(ITIExaminerRelievingModel request)
        {
            _actionName = "SaveRelievingExaminerDataAsync(ITIExaminerRelievingModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    DataTable dt = new DataTable();
                    // Create the command and set up parameters
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "USP_ITIExaminerRelieving";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Action", "Insert");
                        command.Parameters.AddWithValue("@NCVTPracticalExam", request.NCVTPracticalExam);
                        command.Parameters.AddWithValue("@DateOfExamination", request.DateOfExamination);
                        command.Parameters.AddWithValue("@Trade", request.Trade);
                        command.Parameters.AddWithValue("@PracticalExamCentre", request.PracticalExamCentre);
                        command.Parameters.AddWithValue("@PracticalExaminerName", request.PracticalExaminerName);
                        command.Parameters.AddWithValue("@PracticalExaminerDesignation", request.PracticalExaminerDesignation);
                        command.Parameters.AddWithValue("@PracticalExaminerNumber", request.PracticalExaminerNumber);
                        command.Parameters.AddWithValue("@NoOfTotalTrainees", request.NoOfTotalTrainees);
                        command.Parameters.AddWithValue("@NoOfPresentTrainees", request.NoOfPrsentTrainees);
                        command.Parameters.AddWithValue("@NoOfAbsentTrainees", request.NoOfAbsentTrainees);
                        command.Parameters.AddWithValue("@MarckSheetPacket", request.MarckSheetPacket);
                        command.Parameters.AddWithValue("@CopyPacket", request.CopyPacket);
                        command.Parameters.AddWithValue("@PracticalPacket", request.PracticalPacket);
                        command.Parameters.AddWithValue("@PracticalTeacherPacket", request.PracticalTeacherPacket);
                        command.Parameters.AddWithValue("@SealedPacket", request.SealedPacket);
                        command.Parameters.AddWithValue("@BillPacket", request.BillPacket);
                        command.Parameters.AddWithValue("@OtherInfo", request.OtherInfo);
                        command.Parameters.AddWithValue("@OtherInfoText", request.OtherInfoText);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CenterAssignedID", request.CenterAssignedID);


                        _sqlQuery = command.GetSqlExecutableQuery(); // Logging SQL query for debugging

                        // Execute the command and get the result
                        // result = await command.ExecuteNonQueryAsync();
                        dt  = await command.FillAsync_DataTable();
                    }

                    // Return true if the result is greater than 0, meaning rows were affected
                    //return result > 0;
                    return dt;
                    
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

        public async Task<bool> SaveRelievingCoOrdinatorData(ITICoordinatorRelievingModel request)
        {
            _actionName = "SaveRelievingCoOrdinatorData(ITICoordinatorRelievingModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    // Create the command and set up parameters
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "USP_ITICoordinatorRelievingForm";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        //command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        //command.Parameters.AddWithValue("@ABCID", request.ABCID);
                        command.Parameters.AddWithValue("@Action", "Insert");
                        command.Parameters.AddWithValue("@NCVTPracticalExam", request.NCVTPracticalExam);
                        command.Parameters.AddWithValue("@DateOfExamination", request.DateOfExamination);
                        command.Parameters.AddWithValue("@Trade", request.Trade);
                        command.Parameters.AddWithValue("@PracticalExamCentre", request.PracticalExamCentre);
                        command.Parameters.AddWithValue("@PracticalSuperintendentName", request.PracticalSuperintendentName);
                        command.Parameters.AddWithValue("@PracticalSuperintendentNumber", request.PracticalSuperintendentNumber);
                        command.Parameters.AddWithValue("@PracticalCoOrdinatorName", request.PracticalCoOrdinatorName);
                        command.Parameters.AddWithValue("@PracticalCoOrdinatorDesignation", request.PracticalCoOrdinatorDesignation);
                        command.Parameters.AddWithValue("@PracticalCoOrdinatorNumber", request.PracticalCoOrdinatorNumber);
                        command.Parameters.AddWithValue("@NoOfRegisteredInstitutes", request.NoOfRegisteredInstitutes);
                        command.Parameters.AddWithValue("@DetailsOfPresentExaminers", request.DetailsOfPresentExaminers);
                        command.Parameters.AddWithValue("@IsMarkingSheetEnvelopeSubmitted", request.IsMarkingSheetEnvelopeSubmitted);
                        command.Parameters.AddWithValue("@IsCopyEnvelopeJob2Submitted", request.IsCopyEnvelopeJob2Submitted);
                        command.Parameters.AddWithValue("@IsPracticalCopyEnvelopeSubmitted", request.IsPracticalCopyEnvelopeSubmitted);
                        command.Parameters.AddWithValue("@IsHonorariumEnvelopeSubmitted", request.IsHonorariumEnvelopeSubmitted);
                        command.Parameters.AddWithValue("@IsSealedPracticalJobsSubmitted", request.IsSealedPracticalJobsSubmitted);
                        command.Parameters.AddWithValue("@IsCenterReportAttached", request.IsCenterReportAttached);
                        command.Parameters.AddWithValue("@IsHonorariumPaidOrVerified", request.IsHonorariumPaidOrVerified);
                        command.Parameters.AddWithValue("@HonorariumAmount", request.HonorariumAmount);
                        command.Parameters.AddWithValue("@OtherInfo", request.OtherInfoText);
                        command.Parameters.AddWithValue("@AdditionalExaminerRemarksSubmitted", request.AdditionalExaminerRemarksSubmitted);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ExamCoordinatorID", request.ExamCoordinatorID);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);


                        // Make sure you are adding all required fields here if they exist in the procedure
                        // For example, if Gender, SSOID are required, add them too:
                        // command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        // command.Parameters.AddWithValue("@Gender", request.Gender);
                        // Add other parameters as needed

                        _sqlQuery = command.GetSqlExecutableQuery(); // Logging SQL query for debugging

                        // Execute the command and get the result
                        result = await command.ExecuteNonQueryAsync();
                    }

                    // Return true if the result is greater than 0, meaning rows were affected
                    return result > 0;
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


        public async Task<DataTable> GetRelievingExaminerByIdAsync(int id)
        {
            _actionName = "GetRelievingExaminerByIdAsync(int id)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                         command.CommandText = "USP_Get_CenterPracticalExaminerRelieving_ById";

                        command.Parameters.AddWithValue("@Id", id);

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

        public async Task<DataTable> GetDataBySSOId(string SSOId)
        {
            _actionName = "GetDataBySSOId(string SSOId)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_UndertakingByExaminer";
                        command.Parameters.AddWithValue("@Action", "GetSSOData");
                        command.Parameters.AddWithValue("@SSOId", SSOId);
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

        public async Task<bool> SaveUndertakingExaminerData(UndertakingExaminerFormModel model)
        {
            _actionName = "SaveRelievingCoOrdinatorData(UndertakingExaminerFormModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;

                    // Create the command and set up parameters
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = "USP_UndertakingByExaminer";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Action", "Save");
                        command.Parameters.AddWithValue("@SSOId", model.SsoId);

                        command.Parameters.AddWithValue("@LetterNumber", model.LetterNumber);
                        command.Parameters.AddWithValue("@AppointingDate", model.AppointingDate);

                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@Designation", model.Designation);
                        command.Parameters.AddWithValue("@Organization", model.Organization);
                        command.Parameters.AddWithValue("@ContactNumber", model.ContactNumber);
                        command.Parameters.AddWithValue("@EmailId", model.EmailId);
                        command.Parameters.AddWithValue("@Address", model.Address);

                        command.Parameters.AddWithValue("@ItiCollege", model.ItiCollege);
                        command.Parameters.AddWithValue("@MisCode", model.MisCode);
                        command.Parameters.AddWithValue("@DateOfExamination", model.DateOfExamination);
                        command.Parameters.AddWithValue("@SubjectAppointed", model.SubjectAppointed);
                        command.Parameters.AddWithValue("@ItiCompleteAddress", model.ItiCompleteAddress);

                        command.Parameters.AddWithValue("@DeficienciesDetails", model.DeficienciesDetails);
                        command.Parameters.AddWithValue("@ModifyBy", model.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CenterAssignedID", model.CenterAssignedID);
             

                        // Make sure you are adding all required fields here if they exist in the procedure
                        // For example, if Gender, SSOID are required, add them too:
                        // command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        // command.Parameters.AddWithValue("@Gender", request.Gender);
                        // Add other parameters as needed

                        _sqlQuery = command.GetSqlExecutableQuery(); // Logging SQL query for debugging

                        // Execute the command and get the result
                        result = await command.ExecuteNonQueryAsync();
                    }

                    // Return true if the result is greater than 0, meaning rows were affected
                    return result > 0;
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

        public async Task<DataTable> GetRelievingByExamCoordinatorByIdAsync(int id)
        {
            _actionName = "GetRelievingByExamCoordinatorIdAsync";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_CenterExamCoordinatorRelieving_ById";
                        command.Parameters.AddWithValue("@Id", id);

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

        public async Task<DataTable> GetUndertakingExaminerDetailsByIdAsync(int id)
        {
            _actionName = "GetUndertakingExaminerDetailsByIdAsync(int id)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCenterExaminerUndertakingDetails";

                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Action", "GetPraticalExaminerDetailbyID");

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

        public async Task<DataTable> Get_CenterListByUserid(int Userid)
        {
            _actionName = "Get_CenterListByUserid(int Userid)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCenterExaminerUndertakingDetails";

                        command.Parameters.AddWithValue("@Userid", Userid);
                        command.Parameters.AddWithValue("@Action", "GetUnderTakingExamCenterList");

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
