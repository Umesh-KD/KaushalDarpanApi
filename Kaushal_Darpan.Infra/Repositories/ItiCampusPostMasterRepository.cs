using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.ITICampusPostMaster;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ItiCampusPostMasterRepository : I_ItiCampusPostMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ItiCampusPostMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ItiCampusPostMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        //public async Task<DataTable> GetAllData(string SSOID, int DepartmentID)
        //{
        //    _actionName = "GetAllData(string SSOID)";
        //    try
        //    {
        //        return await Task.Run(async () =>
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_ITICampusPostMasterList";
        //                command.Parameters.AddWithValue("@RoleID", SSOID);
        //                command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
        //                _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
        //                dataTable = await command.FillAsync_DataTable();
        //            }
        //            return dataTable;
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorDesc = new ErrorDescription
        //        {
        //            Message = ex.Message,
        //            PageName = _pageName,
        //            ActionName = _actionName,
        //            SqlExecutableQuery = _sqlQuery
        //        };
        //        var errordetails = CommonFuncationHelper.MakeError(errorDesc);
        //        throw new Exception(errordetails, ex);
        //    }
        //}

        public async Task<DataTable> GetAllData(int CompanyID, int CollegeID, string Status, int DepartmentID)
        {
            _actionName = "CampusValidationList(int CollegeID, string Status)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICampusPostMasterList";
                        command.Parameters.AddWithValue("@CompanyID", CompanyID);
                        command.Parameters.AddWithValue("@CollegeID", CollegeID);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
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


        public async Task<List<ItiCampusPostMasterModel>> GetNameWiseData(int PK_ID, int DepartmentID)
        {
            _actionName = "GetNameWiseData(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIGetDataCampusPost";
                        command.Parameters.AddWithValue("@ID", PK_ID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<ItiCampusPostMasterModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<ItiCampusPostMasterModel>>(dataTable);
                    }
                    return data;
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

        public async Task<ItiCampusPostMasterModel> GetById(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICampusPostMaster_Edit";
                        command.Parameters.AddWithValue("@PostID", PK_ID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ItiCampusPostMasterModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ItiCampusPostMasterModel>(dataSet.Tables[0]);
                            data.EligibilityCriteriaModel = CommonFuncationHelper.ConvertDataTable<List<ItiCampusPostMaster_EligibilityCriteria>>(dataSet.Tables[1]);
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
        public async Task<bool> SaveData(ItiCampusPostMasterModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveData(ItiCampusPostMasterModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICampusPostMaster_IU";

                        command.Parameters.AddWithValue("@PostID", request.PostID);
                        command.Parameters.AddWithValue("@PostNo", request.PostNo);
                        command.Parameters.AddWithValue("@PostCollegeID", request.PostCollegeID);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@PostSSOID", request.PostSSOID);
                        command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                        command.Parameters.AddWithValue("@Website", request.Website);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@Address", request.Address);
                        command.Parameters.AddWithValue("@HR_Name", request.HR_Name);
                        command.Parameters.AddWithValue("@HR_MobileNo", request.HR_MobileNo);
                        command.Parameters.AddWithValue("@HR_Email", request.HR_Email);
                        command.Parameters.AddWithValue("@HR_SSOID", request.HR_SSOID);
                        command.Parameters.AddWithValue("@CampusVenue", request.CampusVenue);
                        command.Parameters.AddWithValue("@CampusVenueLocation", request.CampusVenueLocation);
                        command.Parameters.AddWithValue("@CampusFromDate", request.CampusFromDate);
                        command.Parameters.AddWithValue("@CampusFromTime", request.CampusFromTime);
                        command.Parameters.AddWithValue("@CampusToDate", request.CampusToDate);
                        command.Parameters.AddWithValue("@CampusToTime", request.CampusToTime);
                        command.Parameters.AddWithValue("@CampusAddress", request.CampusAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@JobDiscription", request.JobDiscription);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@EligibilityCriteria_Str", JsonConvert.SerializeObject(request.EligibilityCriteriaModel));


                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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

        public async Task<bool> Save_CampusValidation_NodalAction(ItiCampusPostMaster_Action request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "Save_CampusValidation_NodalAction(ItiCampusPostMaster_Action request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICampusValidation_NodalAction";
                        command.Parameters.AddWithValue("@PostID", request.PostID);
                        command.Parameters.AddWithValue("@Action", request.Action);
                        command.Parameters.AddWithValue("@ActionRemarks", request.ActionRemarks);
                        command.Parameters.AddWithValue("@ActionBy", request.ActionBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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
        public async Task<bool> UpdateData(ItiCampusPostMasterModel request)
        {

            return await Task.Run(async () =>
            {
                _actionName = "UpdateData(ItiCampusPostMasterModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICampusPostMaster_IU";

                        command.Parameters.AddWithValue("@PostID", request.PostID);
                        command.Parameters.AddWithValue("@PostNo", request.PostNo);
                        command.Parameters.AddWithValue("@PostCollegeID", request.PostCollegeID);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@PostSSOID", request.PostSSOID);
                        command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                        command.Parameters.AddWithValue("@Website", request.Website);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@Address", request.Address);
                        command.Parameters.AddWithValue("@HR_Name", request.HR_Name);
                        command.Parameters.AddWithValue("@HR_MobileNo", request.HR_MobileNo);
                        command.Parameters.AddWithValue("@HR_Email", request.HR_Email);
                        command.Parameters.AddWithValue("@HR_SSOID", request.HR_SSOID);
                        command.Parameters.AddWithValue("@CampusVenue", request.CampusVenue);
                        command.Parameters.AddWithValue("@CampusFromDate", request.CampusFromDate);
                        command.Parameters.AddWithValue("@CampusFromTime", request.CampusFromTime);
                        command.Parameters.AddWithValue("@CampusToDate", request.CampusToDate);
                        command.Parameters.AddWithValue("@CampusToTime", request.CampusToTime);
                        command.Parameters.AddWithValue("@CampusAddress", request.CampusAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);

                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@EligibilityCriteria_Str", JsonConvert.SerializeObject(request.EligibilityCriteriaModel));
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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
        public async Task<bool> DeleteDataByID(ItiCampusPostMasterModel request)
        {

            int result = 0;
            _actionName = "DeleteDataByID(ItiCampusPostMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        _sqlQuery = $" update M_ITICampusPostMaster set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'                         Where PostID={request.PostID}";
                        _sqlQuery += $" update M_ITICampusPostMaster_EligibilityCriteria set ActiveStatus=0,DeleteStatus=1 Where PostID={request.PostID}";
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


        public async Task<DataTable> CampusValidationList(int CompanyID, int CollegeID, string Status, int DepartmentID)
        {
            _actionName = "CampusValidationList(int CollegeID, string Status)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICampusValidationList";
                        command.Parameters.AddWithValue("@CompanyID", CompanyID);
                        command.Parameters.AddWithValue("@CollegeID", CollegeID);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
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



        public async Task<List<CommonDDLModel>> GetHiringRoleMaster()
        {
            _actionName = "GetHiringRoleMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_HiringRoleMaster";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        studentMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return studentMaster;
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

        public async Task<DataTable> Iticollege(int DepartmentID, int EndTermId)

        {
            _actionName = "InstituteMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiCollege";
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

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
