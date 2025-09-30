using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CollegeWiseScholarship;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CounsellingMasterRepository: ICounsellingMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CounsellingMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CounsellingMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> SaveData(ApplicationDataModel request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ApplicationData_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
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
                        command.Parameters.AddWithValue("@WhatsNumber", request.WhatsNumber);
                        command.Parameters.AddWithValue("@LandlineNumber", request.LandlineNumber);
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
                        command.Parameters.AddWithValue("@JanAadharNo", request.JanAadharNo);
                        command.Parameters.AddWithValue("@JanAadharMemberID", request.JanAadharMemberID);
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

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Return"].Value); // out
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

        public async Task<DataTable> MapCandidateSSO(CounsellingApplicationSearchModel filterModel)
        {
            _actionName = "MapCandidateSSO()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Counselling_GetCandidateDetails";
                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@MobileNo", filterModel.MobileNo);
                        command.Parameters.AddWithValue("@AadharNo", filterModel.AadharNo);
                        command.Parameters.AddWithValue("@Action", filterModel.Action ?? string.Empty);
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

        public async Task<DataTable> GetCounsellingAllotmentList(CounsellingAllotmentListModel body)
        {
            _actionName = "GetCounsellingAllotmentList(CollegeWiseScholarshipSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CounsellingAllotmentList";
                        
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@action", "_GetTradeWiseList");
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


        public async Task<DataTable> GetCandidateList(CounsellingAllotmentListModel body)
        {
            _actionName = "GetCounsellingAllotmentList(CollegeWiseScholarshipSearchModel body)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CounsellingAllotmentList";
                      
                        command.Parameters.AddWithValue("@TradeID", body.TradeID);
                        command.Parameters.AddWithValue("@PageNumber", body.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", body.PageSize);
                        command.Parameters.AddWithValue("@sortOrder", body.SortOrder);
                        command.Parameters.AddWithValue("@sortColumn", body.SortColumn);
                        command.Parameters.AddWithValue("@action", "_GetcandidateList");
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
