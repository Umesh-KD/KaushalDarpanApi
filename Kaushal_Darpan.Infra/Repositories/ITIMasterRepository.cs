using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITIMaster;
using Kaushal_Darpan.Models.NodalOfficer;
using System.Data;
using static Kaushal_Darpan.Core.Helper.CommonFuncationHelper;
using System.Drawing.Printing;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text.Json;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIMasterRepository : IITIMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<int> SaveData(ITIMasterModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveData(GroupMaster request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITITrade";
                        command.Parameters.AddWithValue("@TradeId", request.TradeId);
                        command.Parameters.AddWithValue("@TradeName", request.TradeName);
                        command.Parameters.AddWithValue("@TradeTypeId", request.TradeTypeId);
                        command.Parameters.AddWithValue("@TradeLevelId", request.TradeLevelId);
                        command.Parameters.AddWithValue("@MinPercentageInMath", request.MinPercentageInMath);
                        command.Parameters.AddWithValue("@MinPercentageInScience", request.MinPercentageInScience);
                        command.Parameters.AddWithValue("@DurationYear", request.DurationYear);
                        command.Parameters.AddWithValue("@NoOfSemesters", request.NoOfSemesters);
                        command.Parameters.AddWithValue("@NoOfSanctionedSeats", request.NoOfSanctionedSeats);
                        command.Parameters.AddWithValue("@MinAgeLimit", request.MinAgeLimit);
                        command.Parameters.AddWithValue("@TradeCode", request.TradeCode);
                        command.Parameters.AddWithValue("@IsAdmission", request.IsAdmission);
                        command.Parameters.AddWithValue("@QualificationDetails", request.QualificationDetails);
                        command.Parameters.AddWithValue("@IsMathsScienceCompulsory", request.IsMathsScienceCompulsory);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@RoleId", request.RoleId);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@OnlyForWomen", request.OnlyForWomen);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                        command.Parameters.AddWithValue("@Action", "SaveITITrade");

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


        public async Task<DataTable> GetAllData(ITISearchModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITITrade";

                        command.Parameters.AddWithValue("@TradeName", body.TradeName);
                        command.Parameters.AddWithValue("@TradeTypeId", body.TradeTypeId);
                        command.Parameters.AddWithValue("@TradeLevelId", body.TradeLevelId);
                        command.Parameters.AddWithValue("@DurationYear", body.DurationYear);
                        command.Parameters.AddWithValue("@TradeCode", body.TradeCode);
                        command.Parameters.AddWithValue("@CourseTypeID", body.CourseTypeID);
                        command.Parameters.AddWithValue("@Action", "getTradetblListList");

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
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


        public async Task<ITIMasterModel> GetById(int TradeID)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        _sqlQuery = $" select * from M_ITITrade Where TradeID='{TradeID}'";
                        command.CommandText = _sqlQuery;
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new ITIMasterModel();
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITIMasterModel>(dataTable);
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

        public async Task<bool> DeleteDataByID(ITIMasterModel request)
        {

            int result = 0;
            _actionName = "DeleteDataByID(GroupMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        _sqlQuery = $" update M_ITITrade set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'  Where TradeId={request.TradeId}";
                        command.CommandText = _sqlQuery;
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



        public async Task<DataTable> GetAllITIStudents(SearchITIModelRequest filterModel)
        {

            _actionName = "GetAllITIStudents()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIStudentMasterList";

                        command.Parameters.AddWithValue("@CategoryID", filterModel.CategoryID);
                        command.Parameters.AddWithValue("@EduID", filterModel.Class);

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


        public async Task<DataTable> GetAllPaperUploadData(ITIPaperUploadSearchModel body)
        {
            _actionName = "GetAllPaperUploadData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIPaperUpload";
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@CourseType", body.Eng_NonEng);
                        // command.Parameters.AddWithValue("@Action", "getTradetblListList");

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
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

        public async Task<int> SavePaperUploadData(ITIPaperUploadModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveITIPaperUploadData(GroupMaster request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIPaperUpload_IU";

                        // Add parameters
                        //command.Parameters.AddWithValue("@PaperUploadID", request.PaperUploadID ?? (object)DBNull.Value); // Handle nullable
                        command.Parameters.AddWithValue("@ExamID", request.ExamID);
                        command.Parameters.AddWithValue("@ExamName", request.ExamName);
                        command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@Password", request.Password);
                        command.Parameters.AddWithValue("@PaperID", request.PaperID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@FileName", request.FileName);
                        command.Parameters.AddWithValue("@Dis_FileName", request.Dis_FileName);
                        //command.Parameters.AddWithValue("@PaperDate", request.PaperDate?.ToString("yyyy-MM-ddTHH:mm"));  // Handle nullable dates
                        command.Parameters.AddWithValue("@PaperDate", request.PaperDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CenterCode", request.CenterCode);
                        command.Parameters.AddWithValue("@Active", request.Active == true ? 1 : 0);  // Ensure BIT type
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate?.ToString("yyyy-MM-dd"));  // Handle nullable dates
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value); // Use actual value for IP address
                        command.Parameters.AddWithValue("@CourseType", request.CourseType);
                        // Add the output parameter for return value
                        var returnParam = new SqlParameter("@Return", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(returnParam);

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        await command.ExecuteNonQueryAsync();

                        // Get the output value
                        result = Convert.ToInt32(returnParam.Value); // This gets the return value set in the stored procedure

                    }

                    return result;  // Return the result (1 for success, 2 for update, etc.)
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

        public async Task<DataTable> GetITIFeesPerYearList(ITIFeesPerYearSearchModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetITIFeesPerYearList(ITIFeesPerYearSearchModel request)";
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_College_Update";

                        // Add parameters
                        //command.Parameters.AddWithValue("@PaperUploadID", request.PaperUploadID ?? (object)DBNull.Value); // Handle nullable
                        command.Parameters.AddWithValue("@CollegeId", request.CollegeId);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@Action", "List");
                        command.Parameters.AddWithValue("@TradeName", request.TradeName);
                        command.Parameters.AddWithValue("@TradeCode", request.TradeCode);
                        command.Parameters.AddWithValue("@TradeSchemeId", request.TradeSchemeId);
                        command.Parameters.AddWithValue("@FeeStatus", request.FeeStatus);
                        //command.Parameters.AddWithValue("@ReportingStatus", request.ReportingStatus);





                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();


                    }

                    return dataTable;  // Return the result (1 for success, 2 for update, etc.)
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

        public async Task<DataSet> ItiFeesPerYearListDownload(ITIFeesPerYearSearchModel request)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_College_Update";

                        // Add parameters
                        //command.Parameters.AddWithValue("@PaperUploadID", request.PaperUploadID ?? (object)DBNull.Value); // Handle nullable
                        command.Parameters.AddWithValue("@CollegeId", request.CollegeId);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@Action", "List");
                        command.Parameters.AddWithValue("@TradeName", request.TradeName);
                        command.Parameters.AddWithValue("@TradeCode", request.TradeCode);
                        command.Parameters.AddWithValue("@TradeSchemeId", request.TradeSchemeId);
                        command.Parameters.AddWithValue("@FeeStatus", request.FeeStatus);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    return ds;
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


        public async Task<bool> unlockfee(int id, int ModifyBy, int FeePdf)
        {
            _actionName = "ResetSSOID(ITICollegeMasterModel request)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandText = "USP_ITI_College_Update";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CollegeId", id);
                    command.Parameters.AddWithValue("@UpdatedBy", ModifyBy);
                    command.Parameters.AddWithValue("@FeePdf", FeePdf);
                    //command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    command.Parameters.AddWithValue("@Action", "LockFee");


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
                var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errorDetails, ex);
            }
        }

        public async Task<DataTable> GetITI_CollegeLoginInfoMaster(CollegeLoginInfoSearchModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetITI_CollegeLoginInfoMaster(CollegeLoginInfoSearchModel request)";
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_CollegeLoginInfoMaster";
                        command.Parameters.AddWithValue("@Action", "ITI_CollegeLoginInfo");
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
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
            });
        }





        public async Task<int> Update_CollegeLoginInfo(CollegeLoginInfoSearchModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "Update_CollegeLoginInfo(CollegeLoginInfoSearchModel request)";
                try
                {
                    int result = 0;
                    if (request.CollegeId != 0 && request.collegeIdsString == "")
                    {
                        string generatedPassword;
                        using (var rng = RandomNumberGenerator.Create())
                        {
                            byte[] bytes = new byte[4];
                            int number;
                            do
                            {
                                rng.GetBytes(bytes);
                                number = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF;
                                number %= 1000000;
                            } while (number < 100000);
                            generatedPassword = number.ToString("D6");
                        }

                        using (var command = _dbContext.CreateCommand(true))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "USP_ITI_CollegeLoginInfoMaster";
                            command.Parameters.AddWithValue("@Action", "Update_CollegeLoginInfo");
                            command.Parameters.AddWithValue("@ID", request.CollegeId);
                            command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                            command.Parameters.AddWithValue("@Ssoid", "");
                            command.Parameters.AddWithValue("@Password", generatedPassword);


                            _sqlQuery = command.GetSqlExecutableQuery();


                            result = await command.ExecuteNonQueryAsync();

                        }
                    }
                    else
                    {

                        if (!string.IsNullOrWhiteSpace(request.collegeIdsString))
                        {
                            string[] collegeIds = request.collegeIdsString.Split(',');
                            var passwordDataList = new List<CollegeLoginInfoSearchModel>();
                            foreach (var collegeId in collegeIds)
                            {
                                string generatedPassword;
                                using (var rng = RandomNumberGenerator.Create())
                                {
                                    byte[] bytes = new byte[4];
                                    int number;
                                    do
                                    {
                                        rng.GetBytes(bytes);
                                        number = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF;
                                        number %= 1000000; // ensure it's a 6-digit number
                                    } while (number < 100000); // ensures it doesn't go below 100000

                                    generatedPassword = number.ToString("D6");
                                }


                                var passwordData = new CollegeLoginInfoSearchModel
                                {
                                    CollegeId = Convert.ToInt32(collegeId),
                                    Password = generatedPassword
                                };

                                passwordDataList.Add(passwordData);
                                Console.WriteLine($"College ID: {collegeId}, Password: {generatedPassword}");

                            }
                            string jsonOutput = JsonSerializer.Serialize(passwordDataList);
                            Console.WriteLine(jsonOutput);

                            using (var command = _dbContext.CreateCommand(true))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "USP_ITI_CollegeLoginInfoMaster";
                                command.Parameters.AddWithValue("@Action", "AllUpdate_collegeLoginInfo");
                                command.Parameters.AddWithValue("@DataJson", jsonOutput);
                                _sqlQuery = command.GetSqlExecutableQuery();


                                result = await command.ExecuteNonQueryAsync();

                            }

                        }
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


        public async Task<DataTable> GetCollegeLoginInfoByCode(CollegeLoginInfoSearchModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetCollegeLoginInfoByCode(CollegeLoginInfoSearchModel request)";
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_CollegeLoginInfoMaster";
                        command.Parameters.AddWithValue("@Action", "ITI_CollegeLoginInfoByCode");
                        command.Parameters.AddWithValue("@Ssoid", request.SSOID);
                        command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@ITItypeID", request.ITItypeID);
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
            });
        }

        public async Task<DataTable> GetCenterDetailByPaperUploadID(int PaperUploadID ,int Userid , int Roleid)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIGetCenterDetail";

                        
                        command.Parameters.AddWithValue("@PaperUploadID", PaperUploadID);
                        command.Parameters.AddWithValue("@Userid", Userid);
                        command.Parameters.AddWithValue("@RoleID", Roleid);
                        command.Parameters.AddWithValue("@Action", "GetCenterDetail");

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
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

        public async Task<DataTable> GetCenterWisePaperDetail(CenterWisePaperDetailModal request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetCenterWisePaperDetail(CenterWisePaperDetailModal request)";
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIGetCenterDetail";
                        command.Parameters.AddWithValue("@Action", "GetCenterWisePaperDetail");
                        command.Parameters.AddWithValue("@EndtermID", request.EndTermID);
                        command.Parameters.AddWithValue("@CenterID", request.CenterID);
                        command.Parameters.AddWithValue("@FYID", request.FYID);
                        command.Parameters.AddWithValue("@Userid", request.Userid);
                        command.Parameters.AddWithValue("@RoleID", request.Roleid);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeid);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
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
            });
        }

        public async Task<DataTable> PaperDownloadValidationCheck(DownloadPaperValidationModal request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "PaperDownloadValidationCheck(DownloadPaperValidationModal request)";
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIGetCenterDetail";
                        command.Parameters.AddWithValue("@Action", "CheckDateTimeForPaperDownload");
                        command.Parameters.AddWithValue("@CenterID", request.CenterID);
                        command.Parameters.AddWithValue("@PaperUploadID", request.PaperUploadID);
                        command.Parameters.AddWithValue("@Userid", request.Userid);
                        command.Parameters.AddWithValue("@RoleID", request.Roleid);
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
            });
        }

        public async Task<DataTable> UpdatePaperDownloadFalg(UpdateDownloadPaperFalgModal request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "UpdatePaperDownloadFalg(UpdateDownloadPaperFalgModal request)";
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIGetCenterDetail";
                        command.Parameters.AddWithValue("@Action", "UpdateFalg");
                        command.Parameters.AddWithValue("@CenterID", request.CenterID);
                        command.Parameters.AddWithValue("@PaperUploadID", request.PaperUploadID);
                        command.Parameters.AddWithValue("@Userid", request.Userid);
                        command.Parameters.AddWithValue("@RoleID", request.Roleid);
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
            });
        }

        //public async Task<DataTable> GetCenterIDByLoginUser(CenterWisePaperDetailModal request)
        //{
        //    return await Task.Run(async () =>
        //    {
        //        _actionName = "GetCenterWisePaperDetail(CenterWisePaperDetailModal request)";
        //        try
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_ITIGetCenterDetail";
        //                command.Parameters.AddWithValue("@Action", "GetCenterID");
        //                command.Parameters.AddWithValue("@EndtermID", request.EndTermID);
        //                command.Parameters.AddWithValue("@FYID", request.FYID);
        //                command.Parameters.AddWithValue("@Userid", request.Userid);
        //                command.Parameters.AddWithValue("@RoleID", request.Roleid);
        //                _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

    }
}

