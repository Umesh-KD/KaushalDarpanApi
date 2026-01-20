using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITI_InstructorModel;
using Kaushal_Darpan.Models.ITIAllotment;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITI_InstructorRepository : I_ITI_InstructorRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITI_InstructorRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITI_InstructorRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        //public async Task<int> SaveInstructorData(ITI_InstructorModel request)
        //{
        //    _actionName = "SaveInstructorData(ITI_InstructorModel request)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = await _dbContext.CreateCommandAsync(true))
        //            {
        //                // Set the stored procedure name and type
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_ITI_Instructor";
        //                command.Parameters.AddWithValue("@Action", "Insert");

        //                command.Parameters.AddWithValue("@ID", request.id);
        //                command.Parameters.AddWithValue("@Uid", request.Uid);
        //                command.Parameters.AddWithValue("@Name", request.Name);
        //                command.Parameters.AddWithValue("@FatherOrHusbandName", request.FatherOrHusbandName);
        //                command.Parameters.AddWithValue("@MotherName", request.MotherName);
        //                command.Parameters.AddWithValue("@Dob", request.Dob);
        //                command.Parameters.AddWithValue("@Gender", request.Gender);
        //                command.Parameters.AddWithValue("@MaritalStatus", request.MaritalStatus);
        //                command.Parameters.AddWithValue("@Category", request.Category);
        //                command.Parameters.AddWithValue("@Mobile", request.Mobile);
        //                command.Parameters.AddWithValue("@Email", request.Email);

        //                command.Parameters.AddWithValue("@BankAccountNumber", request.BankAccountNumber);
        //                command.Parameters.AddWithValue("@IFSCCode", request.IFSCCode);
        //                command.Parameters.AddWithValue("@BankName", request.BankName);
        //                command.Parameters.AddWithValue("@ConsentToAssignAsExaminer", request.ConsentToAssignAsExaminer);

        //                command.Parameters.AddWithValue("@PlotHouseBuildingNo", request.PlotHouseBuildingNo);
        //                command.Parameters.AddWithValue("@StreetRoadLane", request.StreetRoadLane);
        //                command.Parameters.AddWithValue("@AreaLocalitySector", request.AreaLocalitySector);
        //                command.Parameters.AddWithValue("@LandMark", request.LandMark);
        //                command.Parameters.AddWithValue("@ddlState", request.DdlState);
        //                command.Parameters.AddWithValue("@ddlDistrict", request.DdlDistrict);
        //                command.Parameters.AddWithValue("@PropTehsilID", request.PropTehsilID);
        //                command.Parameters.AddWithValue("@City", request.City);
        //                command.Parameters.AddWithValue("@pincode", request.Pincode);

        //                command.Parameters.AddWithValue("@Correspondence_PlotHouseBuildingNo", request.Correspondence_PlotHouseBuildingNo);
        //                command.Parameters.AddWithValue("@Correspondence_StreetRoadLane", request.Correspondence_StreetRoadLane);
        //                command.Parameters.AddWithValue("@Correspondence_AreaLocalitySector", request.Correspondence_AreaLocalitySector);
        //                command.Parameters.AddWithValue("@Correspondence_LandMark", request.Correspondence_LandMark);
        //                command.Parameters.AddWithValue("@Correspondence_ddlState", request.Correspondence_ddlState);
        //                command.Parameters.AddWithValue("@Correspondence_ddlDistrict", request.Correspondence_ddlDistrict);
        //                command.Parameters.AddWithValue("@Correspondence_PropTehsilID", request.Correspondence_PropTehsilID);
        //                command.Parameters.AddWithValue("@Correspondence_City", request.Correspondence_City);
        //                command.Parameters.AddWithValue("@Correspondence_pincode", request.Correspondence_Pincode);

        //                //command.Parameters.AddWithValue("@Education_Exam", request.Education_Exam);
        //                //command.Parameters.AddWithValue("@Education_Board", request.Education_Board);
        //                //command.Parameters.AddWithValue("@Education_Year", request.Education_Year);
        //                //command.Parameters.AddWithValue("@Education_Subjects", request.Education_Subjects);
        //                //command.Parameters.AddWithValue("@Education_Percentage", request.Education_Percentage);

        //                //command.Parameters.AddWithValue("@Tech_Exam", request.Tech_Exam);
        //                //command.Parameters.AddWithValue("@Tech_Board", request.Tech_Board);
        //                //command.Parameters.AddWithValue("@Tech_Subjects", request.Tech_Subjects);
        //                //command.Parameters.AddWithValue("@Tech_Year", request.Tech_Year);
        //                //command.Parameters.AddWithValue("@Tech_Percentage", request.Tech_Percentage);

        //                //command.Parameters.AddWithValue("@Pan_No", request.Pan_No);
        //                //command.Parameters.AddWithValue("@Employee_Type", request.Employee_Type);
        //                //command.Parameters.AddWithValue("@Employer_Name", request.Employer_Name);
        //                //command.Parameters.AddWithValue("@Employer_Address", request.Employer_Address);
        //                //command.Parameters.AddWithValue("@Tan_No", request.Tan_No);
        //                //command.Parameters.AddWithValue("@Employment_From", request.Employment_From);
        //                //command.Parameters.AddWithValue("@Employment_To", request.Employment_To);
        //                //command.Parameters.AddWithValue("@Basic_Pay", request.Basic_Pay);

        //                command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
        //                command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

        //                command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
        //                command.Parameters.AddWithValue("@IsDomicile", request.IsDomicile ?? (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@Aadhar", string.IsNullOrEmpty(request.Aadhar) ? (object)DBNull.Value : request.Aadhar);
        //                command.Parameters.AddWithValue("@JanAadhar", string.IsNullOrEmpty(request.JanAadhar) ? (object)DBNull.Value : request.JanAadhar);
        //                command.Parameters.AddWithValue("@QualificationDocument", string.IsNullOrEmpty(request.QualificationDocument) ? (object)DBNull.Value : request.QualificationDocument);
        //                command.Parameters.AddWithValue("@TechQualificationDocument", string.IsNullOrEmpty(request.TechQualificationDocument) ? (object)DBNull.Value : request.TechQualificationDocument);
        //                command.Parameters.AddWithValue("@EmploymentDocument", string.IsNullOrEmpty(request.EmploymentDocument) ? (object)DBNull.Value : request.EmploymentDocument);
        //                command.Parameters.AddWithValue("@TehsilName", string.IsNullOrEmpty(request.TehsilName) ? (object)DBNull.Value : request.TehsilName);
        //                var eduJson = request.EducationalQualifications != null && request.EducationalQualifications.Any()
        //                    ? JsonConvert.SerializeObject(request.EducationalQualifications)
        //                    : null;

        //                var techJson = request.TechnicalQualifications != null && request.TechnicalQualifications.Any()
        //                    ? JsonConvert.SerializeObject(request.TechnicalQualifications)
        //                    : null;

        //                var empJson = request.EmploymentDetails != null && request.EmploymentDetails.Any()
        //                    ? JsonConvert.SerializeObject(request.EmploymentDetails)
        //                    : null;

        //                command.Parameters.AddWithValue("@EducationJson", (object?)eduJson ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@TechJson", (object?)techJson ?? DBNull.Value);
        //                command.Parameters.AddWithValue("@EmploymentJson", (object?)empJson ?? DBNull.Value);

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                result = await command.ExecuteNonQueryAsync();
        //            }
        //            return result;
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


        public async Task<int> SaveInstructorData(ITI_InstructorModel request)
        {
            _actionName = "SaveInstructorData(ITI_InstructorModel request)";
            return await Task.Run(async () =>
            {
                //int result = 0;
                int retval = 0;

                try
                {
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Instructor";
                        command.Parameters.AddWithValue("@Action", "Insert");

                        // Basic Details
                        command.Parameters.AddWithValue("@ID", request.id);

                        command.Parameters.AddWithValue("@Uid", request.Uid ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Name", request.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@FatherOrHusbandName", request.FatherOrHusbandName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MotherName", request.MotherName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Dob", request.Dob ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Gender", request.Gender ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MaritalStatus", request.MaritalStatus ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Category", request.Category ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Mobile", request.Mobile ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", request.Email ?? (object)DBNull.Value);

                        // Address & Bank Info
                        command.Parameters.AddWithValue("@BankAccountNumber", request.BankAccountNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IFSCCode", request.IFSCCode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BankName", request.BankName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ConsentToAssignAsExaminer", request.ConsentToAssignAsExaminer);
                        //command.Parameters.AddWithValue("@ConsentToAssignAsExaminer", request.ConsentToAssignAsExaminer);
                        command.Parameters.AddWithValue("@PlotHouseBuildingNo", request.PlotHouseBuildingNo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@StreetRoadLane", request.StreetRoadLane ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@AreaLocalitySector", request.AreaLocalitySector ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LandMark", request.LandMark ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ddlState", request.DdlState ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ddlDistrict", request.DdlDistrict ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PropTehsilID", request.PropTehsilID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@City", request.City ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@pincode", request.Pincode ?? (object)DBNull.Value);

                        // Correspondence
                        command.Parameters.AddWithValue("@Correspondence_PlotHouseBuildingNo", request.Correspondence_PlotHouseBuildingNo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Correspondence_StreetRoadLane", request.Correspondence_StreetRoadLane ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Correspondence_AreaLocalitySector", request.Correspondence_AreaLocalitySector ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Correspondence_LandMark", request.Correspondence_LandMark ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Correspondence_ddlState", request.Correspondence_ddlState ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Correspondence_ddlDistrict", request.Correspondence_ddlDistrict ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Correspondence_PropTehsilID", request.Correspondence_PropTehsilID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Correspondence_City", request.Correspondence_City ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Correspondence_pincode", request.Correspondence_Pincode ?? (object)DBNull.Value);

                        // Common fields
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@IsDomicile", request.IsDomicile ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Aadhar", string.IsNullOrEmpty(request.Aadhar) ? (object)DBNull.Value : request.Aadhar);
                        command.Parameters.AddWithValue("@JanAadhar", string.IsNullOrEmpty(request.JanAadhar) ? (object)DBNull.Value : request.JanAadhar);
                        command.Parameters.AddWithValue("@QualificationDocument", string.IsNullOrEmpty(request.QualificationDocument) ? (object)DBNull.Value : request.QualificationDocument);
                        command.Parameters.AddWithValue("@TechQualificationDocument", string.IsNullOrEmpty(request.TechQualificationDocument) ? (object)DBNull.Value : request.TechQualificationDocument);
                        command.Parameters.AddWithValue("@EmploymentDocument", string.IsNullOrEmpty(request.EmploymentDocument) ? (object)DBNull.Value : request.EmploymentDocument);
                        command.Parameters.AddWithValue("@TehsilName", string.IsNullOrEmpty(request.TehsilName) ? (object)DBNull.Value : request.TehsilName);

                        // Serialize main collections first
                        string eduJson = request.EducationalQualifications?.Any() == true
                            ? JsonConvert.SerializeObject(request.EducationalQualifications)
                            : null;

                        string techJson = request.TechnicalQualifications?.Any() == true
                            ? JsonConvert.SerializeObject(request.TechnicalQualifications)
                            : null;

                        string empJson = request.EmploymentDetails?.Any() == true
                            ? JsonConvert.SerializeObject(request.EmploymentDetails)
                            : null;

                        // 1) Try to flatten from model objects (preferred)
                        List<ITI_Instructor_TechCITSDetails> allCITS = new List<ITI_Instructor_TechCITSDetails>();
                        if (request.TechnicalQualifications != null && request.TechnicalQualifications.Any())
                        {
                            allCITS = request.TechnicalQualifications
                                .Where(t => t != null && t.OtherCITSQualification != null && t.OtherCITSQualification.Any())
                                .SelectMany(t => t.OtherCITSQualification!)
                                .Where(c => c != null)
                                .ToList();
                        }

                        if (!allCITS.Any() && !string.IsNullOrWhiteSpace(techJson))
                        {
                            try
                            {
                                var jarr = JArray.Parse(techJson);
                                foreach (var jTech in jarr)
                                {
                                    var jCITS = jTech["OtherCITSQualification"] ?? jTech["OtherCITSQualification"] ?? jTech["Tech_CITSDetails"] ?? jTech["tech_cits_details"];
                                    if (jCITS != null && jCITS.Type == JTokenType.Array)
                                    {
                                        var list = jCITS.ToObject<List<ITI_Instructor_TechCITSDetails>>();
                                        if (list != null && list.Any())
                                            allCITS.AddRange(list);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine("Fallback parse error techJson -> CITS: " + ex.Message);
                            }
                        }

                        if (allCITS.Any() && request.id.HasValue && request.id.Value > 0)
                        {
                            allCITS.ForEach(c => { if (!c.InstructorID.HasValue) c.InstructorID = request.id; });
                        }

                        string TechCITSJson = allCITS.Any() ? JsonConvert.SerializeObject(allCITS) : null;

                        System.Diagnostics.Debug.WriteLine("TechCITSJson => " + (TechCITSJson ?? "<NULL>"));
                        System.Diagnostics.Debug.WriteLine("TechJson => " + (techJson ?? "<NULL>"));

                        command.Parameters.Add(new SqlParameter("@EducationJson", SqlDbType.NVarChar, -1) { Value = (object?)eduJson ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@TechJson", SqlDbType.NVarChar, -1) { Value = (object?)techJson ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@EmploymentJson", SqlDbType.NVarChar, -1) { Value = (object?)empJson ?? DBNull.Value });
                        command.Parameters.Add(new SqlParameter("@CITSJson", SqlDbType.NVarChar, -1) { Value = (object?)TechCITSJson ?? DBNull.Value });


                        command.Parameters.AddWithValue("@AadharDocument", string.IsNullOrEmpty(request.AadharDocument) ? (object)DBNull.Value : request.AadharDocument);
                        command.Parameters.AddWithValue("@PermanentDocument", string.IsNullOrEmpty(request.PermanentDocument) ? (object)DBNull.Value : request.PermanentDocument);




                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //result = await command.ExecuteNonQueryAsync();

                        command.Parameters.Add("@Retval", SqlDbType.Int).Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();
                        await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Retval"].Value);
                    }

                    return retval;
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
                    throw new Exception(CommonFuncationHelper.MakeError(errorDesc), ex);
                }
            });
        }



        public async Task<DataTable> GetInstructorDataByID(int id)
        {
            _actionName = "GetInstructorDataByID(int id)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Instructor";
                        command.Parameters.AddWithValue("@Action", "GetInstructorDataByID");
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


        public async Task<int> deleteInstructorDataByID(int id)
        {
            _actionName = " deleteInstructorDataByID(int id)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Instructor";
                        command.Parameters.AddWithValue("@Action", "deleteInstructorDataByID");

                        command.Parameters.AddWithValue("@ID", id);


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


        //public async Task<DataTable> GetCenterSuperitendentReportData()
        //{
        //    _actionName = "GetCenterSuperitendentReportData()";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = await _dbContext.CreateCommandAsync())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_ITICenterSuperintendentExamReport";
        //                command.Parameters.AddWithValue("@Action", "GetCenterSuperitendentReportData");
        //                //command.Parameters.AddWithValue("@DistrictId", model.DistrictID);
        //                //command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
        //                //command.Parameters.AddWithValue("@InstituteId", model.InstituteID);
        //                //command.Parameters.AddWithValue("@Code", model.CollegeCode);
        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataTable = await command.FillAsync_DataTable();
        //            }

        //            return dataTable;
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



        public async Task<DataTable> GetInstructorData(ITI_InstructorDataSearchModel model)
        {
            _actionName = "GetInstructorData(ITI_InstructorDataSearchModel model )";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Instructor";
                        command.Parameters.AddWithValue("@Action", "GetInstructorData");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Uid", model.Uid);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        //command.Parameters.AddWithValue("@RoleId", model.RoleID);
                        command.Parameters.AddWithValue("@RoleId", 0);
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


        public async Task<DataTable> GetGridInstructorData(ITI_InstructorApplicationNoDataSearchModel model)
        {
            _actionName = "GetGridInstructorData(ITI_InstructorApplicationNoDataSearchModel model )";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIInstructorByApplicationID";
                        command.Parameters.AddWithValue("@ApplicationID", model.ApplicationID);

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



        public async Task<DataTable> GetGridBindInstructorData(ITI_InstructorBindDataSearchModel model)
        {
            _actionName = "GetGridBindInstructorData(ITI_InstructorBindDataSearchModel model )";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIInstructorByApplicationNo";
                        //command.Parameters.AddWithValue("@ApplicationNo", model.ApplicationNo);
                        command.Parameters.AddWithValue("@Name", model.Name);
                        command.Parameters.AddWithValue("@Uid", model.Uid);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);

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


        public async Task<DataSet> GetInstructorDataBySsoid(string SSOID)
        {
            _actionName = "GetAllDataPhoneVerify()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataTable = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Instructorgetbyssoid";

                        command.Parameters.AddWithValue("@SSOID", SSOID);



                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync();
                    }
                    return dataTable;
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

        public async Task<DataSet> GetAllTechCITSDetails(ITI_Instructor_TechCITSDetailsSearchModel model)
        {
            _actionName = "GetAllTechCITSDetails()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataTable = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Instructor_GetAllTechCITSDetails";

                        command.Parameters.AddWithValue("@TechCITSId", model.TechCITSId);



                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync();
                    }
                    return dataTable;
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



        public async Task<int> UpdateInstructorDataAsync(ITI_InstructorModel request)
        {
            _actionName = "UpdateInstructorDataAsync(ITI_InstructorModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_ITI_Instructor_UpdateByUid";

                        // No @Action param now, this SP only updates
                        command.Parameters.AddWithValue("@Uid", request.Uid);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@FatherOrHusbandName", request.FatherOrHusbandName);
                        command.Parameters.AddWithValue("@MotherName", request.MotherName);
                        command.Parameters.AddWithValue("@Dob", request.Dob);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@MaritalStatus", request.MaritalStatus);
                        command.Parameters.AddWithValue("@Category", request.Category);
                        command.Parameters.AddWithValue("@Mobile", request.Mobile);
                        command.Parameters.AddWithValue("@Email", request.Email);

                        command.Parameters.AddWithValue("@BankAccountNumber", request.BankAccountNumber);
                        command.Parameters.AddWithValue("@IFSCCode", request.IFSCCode);
                        command.Parameters.AddWithValue("@BankName", request.BankName);
                        command.Parameters.AddWithValue("@ConsentToAssignAsExaminer", request.ConsentToAssignAsExaminer);

                        command.Parameters.AddWithValue("@PlotHouseBuildingNo", request.PlotHouseBuildingNo);
                        command.Parameters.AddWithValue("@StreetRoadLane", request.StreetRoadLane);
                        command.Parameters.AddWithValue("@AreaLocalitySector", request.AreaLocalitySector);
                        command.Parameters.AddWithValue("@LandMark", request.LandMark);
                        command.Parameters.AddWithValue("@ddlState", request.DdlState);
                        command.Parameters.AddWithValue("@ddlDistrict", request.DdlDistrict);
                        command.Parameters.AddWithValue("@PropTehsilID", request.PropTehsilID);
                        command.Parameters.AddWithValue("@City", request.City);
                        command.Parameters.AddWithValue("@Pincode", request.Pincode);

                        command.Parameters.AddWithValue("@Correspondence_PlotHouseBuildingNo", request.Correspondence_PlotHouseBuildingNo);
                        command.Parameters.AddWithValue("@Correspondence_StreetRoadLane", request.Correspondence_StreetRoadLane);
                        command.Parameters.AddWithValue("@Correspondence_AreaLocalitySector", request.Correspondence_AreaLocalitySector);
                        command.Parameters.AddWithValue("@Correspondence_LandMark", request.Correspondence_LandMark);
                        command.Parameters.AddWithValue("@Correspondence_ddlState", request.Correspondence_ddlState);
                        command.Parameters.AddWithValue("@Correspondence_ddlDistrict", request.Correspondence_ddlDistrict);
                        command.Parameters.AddWithValue("@Correspondence_PropTehsilID", request.Correspondence_PropTehsilID);
                        command.Parameters.AddWithValue("@Correspondence_City", request.Correspondence_City);
                        command.Parameters.AddWithValue("@Correspondence_Pincode", request.Correspondence_Pincode);

                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@IsDomicile", request.IsDomicile ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Aadhar", string.IsNullOrEmpty(request.Aadhar) ? (object)DBNull.Value : request.Aadhar);
                        command.Parameters.AddWithValue("@JanAadhar", string.IsNullOrEmpty(request.JanAadhar) ? (object)DBNull.Value : request.JanAadhar);
                        command.Parameters.AddWithValue("@TehsilName", string.IsNullOrEmpty(request.TehsilName) ? (object)DBNull.Value : request.TehsilName);

                        // Serialize collections to JSON
                        var eduJson = request.EducationalQualifications != null && request.EducationalQualifications.Any()
                            ? JsonConvert.SerializeObject(request.EducationalQualifications)
                            : null;

                        var techJson = request.TechnicalQualifications != null && request.TechnicalQualifications.Any()
                            ? JsonConvert.SerializeObject(request.TechnicalQualifications)
                            : null;

                        var empJson = request.EmploymentDetails != null && request.EmploymentDetails.Any()
                            ? JsonConvert.SerializeObject(request.EmploymentDetails)
                            : null;

                        command.Parameters.AddWithValue("@EducationJson", (object?)eduJson ?? DBNull.Value);
                        command.Parameters.AddWithValue("@TechJson", (object?)techJson ?? DBNull.Value);
                        command.Parameters.AddWithValue("@EmploymentJson", (object?)empJson ?? DBNull.Value);
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
       
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
                        //_sqlQuery = command.GetSqlExecutableQuery();
                        //result = await command.ExecuteNonQueryAsync();
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


        public async Task<DataTable> GetInstructorListIsAssign(ITI_InstructorDataAssign model)
        {
            _actionName = "GetInstructorListIsAssign(ITI_InstructorBindDataSearchModel model )";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiInstructorDataAssigned";

                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CollegeID", model.CollegeId);

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


        public async Task<DataTable> ToggleAssignStatusAsync(string uid)
        {
            _actionName = "ToggleAssignStatusAsync(uid)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Instructor_ToggleAssignStatus";
                        command.Parameters.AddWithValue("@Uid", uid);

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

