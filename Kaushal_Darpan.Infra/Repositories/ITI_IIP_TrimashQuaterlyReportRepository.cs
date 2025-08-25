using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITI_IIP_TrimashQuaterlyReportModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITI_IIP_TrimashQuaterlyReportRepository : I_ITI_IIP_TrimashQuaterlyReportRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITI_IIP_TrimashQuaterlyReportRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITI_IIP_TrimashQuaterlyReportRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> SaveIIPmasterData(ITI_IIP_TrimashQuaterlyReportModel request)
        {
            _actionName = "SaveIIPmasterData(ITI_IIP_TrimashQuaterlyReportModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_IIT_IIP_InsertTrimashAmountCalculation";
                        command.Parameters.AddWithValue("@Action", "SaveData");
                        command.Parameters.AddWithValue("@AmountOfFirstdate_3", request.AmountOfFirstdate_3);
                        command.Parameters.AddWithValue("@AmountOfTrimash_4", request.AmountOfTrimash_4);
                        command.Parameters.AddWithValue("@AmountOfTrimash_5", request.AmountOfTrimash_5);
                        command.Parameters.AddWithValue("@AmountOfTrimash_6", request.AmountOfTrimash_6);
                        command.Parameters.AddWithValue("@CalculateofTriamsh_7", request.CalculateofTriamsh_7);
                        command.Parameters.AddWithValue("@AddAmountofTrimash", request.AddAmountofTrimash);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);
                        //command.Parameters.AddWithValue("@ID", request.id);
                        //command.Parameters.AddWithValue("@Uid", request.Uid);
                        //command.Parameters.AddWithValue("@Name", request.Name);
                        //command.Parameters.AddWithValue("@FatherOrHusbandName", request.FatherOrHusbandName);
                        //command.Parameters.AddWithValue("@MotherName", request.MotherName);
                        //command.Parameters.AddWithValue("@Dob", request.Dob);
                        //command.Parameters.AddWithValue("@Gender", request.Gender);
                        //command.Parameters.AddWithValue("@MaritalStatus", request.MaritalStatus);
                        //command.Parameters.AddWithValue("@Category", request.Category);
                        //command.Parameters.AddWithValue("@Mobile", request.Mobile);
                        //command.Parameters.AddWithValue("@Email", request.Email);

                        //command.Parameters.AddWithValue("@PlotHouseBuildingNo", request.PlotHouseBuildingNo);
                        //command.Parameters.AddWithValue("@StreetRoadLane", request.StreetRoadLane);
                        //command.Parameters.AddWithValue("@AreaLocalitySector", request.AreaLocalitySector);
                        //command.Parameters.AddWithValue("@LandMark", request.LandMark);
                        //command.Parameters.AddWithValue("@ddlState", request.DdlState);
                        //command.Parameters.AddWithValue("@ddlDistrict", request.DdlDistrict);
                        //command.Parameters.AddWithValue("@PropTehsilID", request.PropTehsilID);
                        //command.Parameters.AddWithValue("@City", request.City);
                        //command.Parameters.AddWithValue("@pincode", request.Pincode);

                        //command.Parameters.AddWithValue("@Correspondence_PlotHouseBuildingNo", request.Correspondence_PlotHouseBuildingNo);
                        //command.Parameters.AddWithValue("@Correspondence_StreetRoadLane", request.Correspondence_StreetRoadLane);
                        //command.Parameters.AddWithValue("@Correspondence_AreaLocalitySector", request.Correspondence_AreaLocalitySector);
                        //command.Parameters.AddWithValue("@Correspondence_LandMark", request.Correspondence_LandMark);
                        //command.Parameters.AddWithValue("@Correspondence_ddlState", request.Correspondence_ddlState);
                        //command.Parameters.AddWithValue("@Correspondence_ddlDistrict", request.Correspondence_ddlDistrict);
                        //command.Parameters.AddWithValue("@Correspondence_PropTehsilID", request.Correspondence_PropTehsilID);
                        //command.Parameters.AddWithValue("@Correspondence_City", request.Correspondence_City);
                        //command.Parameters.AddWithValue("@Correspondence_pincode", request.Correspondence_Pincode);

                        //command.Parameters.AddWithValue("@Education_Exam", request.Education_Exam);
                        //command.Parameters.AddWithValue("@Education_Board", request.Education_Board);
                        //command.Parameters.AddWithValue("@Education_Year", request.Education_Year);
                        //command.Parameters.AddWithValue("@Education_Subjects", request.Education_Subjects);
                        //command.Parameters.AddWithValue("@Education_Percentage", request.Education_Percentage);

                        //command.Parameters.AddWithValue("@Tech_Exam", request.Tech_Exam);
                        //command.Parameters.AddWithValue("@Tech_Board", request.Tech_Board);
                        //command.Parameters.AddWithValue("@Tech_Subjects", request.Tech_Subjects);
                        //command.Parameters.AddWithValue("@Tech_Year", request.Tech_Year);
                        //command.Parameters.AddWithValue("@Tech_Percentage", request.Tech_Percentage);

                        //command.Parameters.AddWithValue("@Pan_No", request.Pan_No);
                        //command.Parameters.AddWithValue("@Employee_Type", request.Employee_Type);
                        //command.Parameters.AddWithValue("@Employer_Name", request.Employer_Name);
                        //command.Parameters.AddWithValue("@Employer_Address", request.Employer_Address);
                        //command.Parameters.AddWithValue("@Tan_No", request.Tan_No);
                        //command.Parameters.AddWithValue("@Employment_From", request.Employment_From);
                        //command.Parameters.AddWithValue("@Employment_To", request.Employment_To);
                        //command.Parameters.AddWithValue("@Basic_Pay", request.Basic_Pay);

                        //command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        //command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

                        //command.Parameters.AddWithValue("@InstituteID", request.InstituteID);

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


        public async Task<DataTable> GetIIPmasterDataByID(int id)
        {
            _actionName = "GetIIPmasterDataByID(int id)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIP_GetCollegeQReportsDetails";
                        command.Parameters.AddWithValue("@Action", "GetById");
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


        public async Task<int> deleteIIPDataByID(int id)
        {
            _actionName = " deleteIIPDataByID(int id)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIP_GetCollegeQReportsDetails";
                        command.Parameters.AddWithValue("@Action", "deleteIIPDataByID");

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
        //            using (var command = _dbContext.CreateCommand())
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



        public async Task<DataTable> GetIIPmasterData()
        {
            _actionName = "GetIIPmasterData(ITI_IIP_TrimashQuaterlyReportModel model )";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_IIP_GetCollegeQReportsDetails";
                        command.Parameters.AddWithValue("@Action", "GetIIPmasterData");
                        //command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        //command.Parameters.AddWithValue("@Uid", model.Uid);
                        //command.Parameters.AddWithValue("@Name", model.Name);
                        //command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        //command.Parameters.AddWithValue("@RoleId", model.RoleID);
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

