using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ITIMaster;
using Microsoft.Data.SqlClient;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class BTERMasterRepository : IBTERMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public BTERMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "BTERMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetBTER_CollegeLoginInfoMaster(BTERCollegeLoginInfoSearchModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetBTER_CollegeLoginInfoMaster(BTERCollegeLoginInfoSearchModel request)";
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_CollegeLoginInfoMaster";
                        command.Parameters.AddWithValue("@Action", "BTER_CollegeLoginInfo");
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
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





        public async Task<int> BTERUpdate_CollegeLoginInfo(BTERCollegeLoginInfoSearchModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "BTERUpdate_CollegeLoginInfo(BTERCollegeLoginInfoSearchModel request)";
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
                            command.CommandText = "USP_BTER_CollegeLoginInfoMaster";
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
                                command.CommandText = "USP_BTER_CollegeLoginInfoMaster";
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


        public async Task<DataTable> BTERGetCollegeLoginInfoByCode(BTERCollegeLoginInfoSearchModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "BTERGetCollegeLoginInfoByCode(BTERCollegeLoginInfoSearchModel request)";
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_CollegeLoginInfoMaster";
                        command.Parameters.AddWithValue("@Action", "BTER_CollegeLoginInfoByCode");
                        command.Parameters.AddWithValue("@Ssoid", request.SSOID);
                        command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@ITItypeID", request.ITItypeID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
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



    }
}
