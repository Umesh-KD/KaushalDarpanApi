using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CenterCreationMaster;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.ITICenterAllocaqtion;
using Kaushal_Darpan.Models.ITIPracticalExaminer;
using Kaushal_Darpan.Models.TheoryMarks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITICenterAllocationRepository : IITICenterAllocationRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITICenterAllocationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CenterAllocationRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ITICenterAllocationSearchFilter filterModel)
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
                        command.CommandText = "USP_ITICenterAllocation";
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                        command.Parameters.AddWithValue("@Name", filterModel.Name);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
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


        public async Task<DataTable> GetExamCoordinatorData(ITICenterAllocationSearchFilter filterModel)
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
                        command.CommandText = "USP_ItiExamcoordinator";
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                        command.Parameters.AddWithValue("@Name", filterModel.CenterName);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@CenterID", filterModel.CenterID);
                        command.Parameters.AddWithValue("@DistrictID", filterModel.DistrictID);
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


        public async Task<DataTable> GetInstituteByCenterID(ITICenterAllocationSearchFilter filterModel)
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
                        command.CommandText = "USP_ITIInstituteListWithCenterID";
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@CenterID", filterModel.CenterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
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

        public async Task<int> SaveData(List<ITICenterAllocationModel> entity)
        {
            _actionName = "SaveAllData(List<CreateTpoAddEditModel> entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITICenter_Alloocation_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_addEditData");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entity));
                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);// out
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


        public async Task<DataTable> CenterSuperintendent(ITICenterAllocationSearchFilter filterModel)
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
                        command.CommandText = "USP_ItiCenterSuperintendentAllocation";
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@Name", filterModel.CenterName);
                        command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
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

        public async Task<int> AssignCenterSuperintendent(CenterSuperintendentDetailsModel request)
        {
            _actionName = "UpdateCCCode(CenterCreationAddEditModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ItiCenterSuperintendentDetails";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CenterID", request.CenterID);
                        command.Parameters.AddWithValue("@CenterAssignedID", request.CenterAssignedID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);// out
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
        public async Task<DataSet> DownloadCenterSuperintendent(ITICenterAllocationSearchFilter filterModel)
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
                        command.CommandText = "USP_ItiCenterSuperintendentAllocation";
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@Name", filterModel.CenterName);
                        command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
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

        public async Task<int> ITISaveWorkflow(DownloadnRollNoModel request)
        {
            _actionName = "SaveRollNumbePDFData(DownloadnRollNoModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ItiCenterSuperWorkflow_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RollListID", request.RollListID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@FileName", request.FileName);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@PDFType", request.PDFType);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@TotalStudent", request.TotalStudent);
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


        public async Task<int> AssignExamCoordinatorData(PracticalExaminerDetailsModel request)
        {
            _actionName = "UpdateCCCode(CenterCreationAddEditModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ItiExamCoordinatorIu";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CenterID", request.CenterID);
                        command.Parameters.AddWithValue("@CenterAssignedID", request.CenterAssignedID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@TimeTableID", request.TimeTableID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@InsituteID", request.InsituteID);

                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);// out
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


        public async Task<DataTable> GetExamCoordinatorData_ByInstitute(ITICenterAllocationSearchFilter filterModel)
        {
            _actionName = "GetExamCoordinatorData_ByInstitute()";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiCenterExamcoordinator";

                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Name", string.IsNullOrWhiteSpace(filterModel.CenterName) ? (object)DBNull.Value : filterModel.CenterName);
                        command.Parameters.AddWithValue("@CenterCode", string.IsNullOrWhiteSpace(filterModel.CenterCode) ? (object)DBNull.Value : filterModel.CenterCode);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
                        command.Parameters.AddWithValue("@DistrictID", filterModel.DistrictID);

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

        public async Task<DataTable> GetExamCoordinatorData_ByUserId(ITICenterAllocationSearchFilter filterModel)
        {
            _actionName = "GetExamCoordinatorData_ByInstitute()";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiCenterExamcoordinator_ByUserID";
                        command.Parameters.AddWithValue("@UserID", filterModel.UserID);
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Name", string.IsNullOrWhiteSpace(filterModel.CenterName) ? (object)DBNull.Value : filterModel.CenterName);
                        command.Parameters.AddWithValue("@CenterCode", string.IsNullOrWhiteSpace(filterModel.CenterCode) ? (object)DBNull.Value : filterModel.CenterCode);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);

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

        public async Task<DataTable> GetCenterSuperDashboard(ExaminerDashboardModel filterModel)
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
                        command.CommandText = "USP_ITI_Centersuperintendentdashboard";
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
              
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", filterModel.InstituteID);
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

        public async Task<DataSet> DownloadExamCoordinate(ITICenterAllocationSearchFilter filterModel)
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
                        command.CommandText = "USP_ItiExamcoordinatorRpt";
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@CenterCode", filterModel.CenterCode);
                        command.Parameters.AddWithValue("@Name", filterModel.CenterName);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@CenterID", filterModel.CenterID);
                        command.Parameters.AddWithValue("@DistrictID", filterModel.DistrictID);
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
    }
}
