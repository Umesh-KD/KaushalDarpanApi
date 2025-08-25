using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.DispatchFormDataModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Kaushal_Darpan.Models.IssuedItems;
using Newtonsoft.Json;
using Kaushal_Darpan.Models.DispatchMaster;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.ScholarshipMaster;
using Kaushal_Darpan.Models.GenerateEnroll;
using System.Reflection;






namespace Kaushal_Darpan.Infra.Repositories
{
    public class DispatchRepository : IDispatchRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public DispatchRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "DispatchRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<bool> DeleteDataByID(DispatchSearchModel productDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<DataTable> GetAllData(DispatchSearchModel SearchReq)
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
                        command.CommandText = "USP_DispatchMaster";
                        command.Parameters.AddWithValue("@DispatchID", SearchReq.DispatchID);
                        command.Parameters.AddWithValue("@DispatchDate", SearchReq.DispatchDate);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@BundelID", SearchReq.BundelID);
                        command.Parameters.AddWithValue("@Status", SearchReq.Status);
                        command.Parameters.AddWithValue("@Action", SearchReq.Action);
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


        public async Task<DataTable> GetAllReceivedData(DispatchSearchModel SearchReq)
        {
            _actionName = "GetAllReceivedData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ReceivedDispatchGroup";
                        //command.Parameters.AddWithValue("@DispatchID", SearchReq.DispatchID);
                        //command.Parameters.AddWithValue("@DispatchDate", SearchReq.DispatchDate);
                        //command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        //command.Parameters.AddWithValue("@BundelID", SearchReq.BundelID);
                        //command.Parameters.AddWithValue("@Status", SearchReq.Status);
                        command.Parameters.AddWithValue("@Action", SearchReq.Action);
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


        public async Task<DataTable> GetBundelDataAllData(BundelDataModel SearchReq)
        {
            _actionName = "GetBundelDataAllData(BundelDataModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    //using (var command = _dbContext.CreateCommand())
                    //{
                    //    command.CommandType = CommandType.StoredProcedure;
                    //    command.CommandText = "USP_DispatchMaster";
                    //    command.Parameters.AddWithValue("@DispatchID", SearchReq.DispatchID);
                    //    command.Parameters.AddWithValue("@DispatchDate", "");
                    //    command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                    //    command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                    //    command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                    //    command.Parameters.AddWithValue("@ExamDate", SearchReq.ExamDate);
                    //    command.Parameters.AddWithValue("@BranchCode", SearchReq.BranchCode);
                    //    command.Parameters.AddWithValue("@CenterCode", SearchReq.CenterCode);
                    //    command.Parameters.AddWithValue("@SubjectCode", SearchReq.SubjectCode);
                    //    command.Parameters.AddWithValue("@ExamShift", SearchReq.ExamShift);
                    //    command.Parameters.AddWithValue("@TotalPresentStudent", SearchReq.TotalPresentStudent);
                    //    command.Parameters.AddWithValue("@Action", "ViewBundelData");
                    //    _sqlQuery = command.GetSqlExecutableQuery();
                    //    dataTable = await command.FillAsync_DataTable();
                    //}DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchSuperintendentExamDateWiseList";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@ExamDate", SearchReq.ExamDate);
                        command.Parameters.AddWithValue("@CCCode", SearchReq.CCCode);
                        command.Parameters.AddWithValue("@SubjectCode1", SearchReq.SubjectCode);
                        command.Parameters.AddWithValue("@BranchCode", SearchReq.BranchCode);
                        command.Parameters.AddWithValue("@ShiftID1", Convert.ToInt32( SearchReq.ExamShift));
                        command.Parameters.AddWithValue("@UserID", SearchReq.UserID);
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

        public async Task<int> SaveData(DispatchFormDataModel request)
        {
            _actionName = "SaveData(IssuedItems request)";
            return await Task.Run(async () =>
            {
                var jsonData = JsonConvert.SerializeObject(request.BundelDataModel);

                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                       
                        command.CommandText = "USP_DispatchMaster_IU";
                        command.CommandType = CommandType.StoredProcedure;
                       
                        command.Parameters.AddWithValue("@DispatchID", request.DispatchID);
                        command.Parameters.AddWithValue("@DispatchDate", request.DispatchDate);
                        command.Parameters.AddWithValue("@CompanyName", request.CompanyName);
                        command.Parameters.AddWithValue("@ChallanNo", request.ChallanNo);
                        command.Parameters.AddWithValue("@SupplierName", request.SupplierName);
                        command.Parameters.AddWithValue("@SupplierVehicleNo", request.SupplierVehicleNo);
                        command.Parameters.AddWithValue("@SupplierMobileNo", request.SupplierMobileNo);
                        command.Parameters.AddWithValue("@SupplierDate", request.SupplierDate);
                        command.Parameters.AddWithValue("@RecipientName", request.RecipientName);
                        command.Parameters.AddWithValue("@RecipientPost", request.RecipientPost);
                        command.Parameters.AddWithValue("@RecipientMobileNo", request.RecipientMobileNo);
                        command.Parameters.AddWithValue("@RecipientDate", request.RecipientDate);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@Action", "Insert");
                        command.Parameters.AddWithValue("@BundelDataJson", jsonData);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@CenterCode", request.CenterCode);
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }


        public async Task<int> SaveDispatchReceived(List<DispatchReceivedModel> request)
        {
            _actionName = "SaveDispatchReceived((List<DispatchReceivedModel> request)";
            return await Task.Run(async () =>
            {
                var jsonData = JsonConvert.SerializeObject(request);

                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandText = "USP_DispatchReceived";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", "SaveDispatchReceived");
                        command.Parameters.AddWithValue("@BundelDataJson", jsonData);
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }


        public async Task<InstituteGroupDetail> GetGroupDataAllData(DispatchGroupSearchModel SearchReq)
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
                        command.CommandText = "USP_GetDispatchGroupcode";
                        //command.Parameters.AddWithValue("@DispatchID", SearchReq.DispatchID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        //command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new InstituteGroupDetail();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<InstituteGroupDetail>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count>0)
                            {

                                data.GroupDataModel = CommonFuncationHelper.ConvertDataTable<List<DispatchGroupCodeModel>>(dataSet.Tables[1]);

                            }
                           

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

        public async Task<int> SaveDispatchGroup(DispatchGroupModel request)
        {
            _actionName = "SaveDispatchGroup(IssuedItems request)";
            return await Task.Run(async () =>
            {
                var jsonData = JsonConvert.SerializeObject(request.GroupDataModel);

                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandText = "USP_DispatchGroup_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@DispatchID", request.DispatchGroupID);
                        command.Parameters.AddWithValue("@DispatchDate", request.DispatchDate);
                        command.Parameters.AddWithValue("@CompanyName", request.CompanyName);
                        command.Parameters.AddWithValue("@ChallanNo", request.ChallanNo);
                        command.Parameters.AddWithValue("@SupplierName", request.SupplierName);
                        command.Parameters.AddWithValue("@SupplierVehicleNo", request.SupplierVehicleNo);
                        command.Parameters.AddWithValue("@SupplierMobileNo", request.SupplierMobileNo);
                        command.Parameters.AddWithValue("@SupplierDate", request.SupplierDate);
                        command.Parameters.AddWithValue("@RecipientName", request.RecipientName);
                        command.Parameters.AddWithValue("@RecipientPost", request.RecipientPost);
                        command.Parameters.AddWithValue("@RecipientMobileNo", request.RecipientMobileNo);
                        command.Parameters.AddWithValue("@RecipientDate", request.RecipientDate);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@Action", "Insert");
                        command.Parameters.AddWithValue("@GroupDataModel", jsonData);
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }
        public async Task<DataTable> GetAllGroupData(DispatchSearchModel SearchReq)
        {
            _actionName = "GetAllGroupData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchGroup";
                        //command.Parameters.AddWithValue("@DispatchID", SearchReq.DispatchID);
                        //command.Parameters.AddWithValue("@DispatchDate", SearchReq.DispatchDate);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        //command.Parameters.AddWithValue("@BundelID", SearchReq.BundelID);
                        command.Parameters.AddWithValue("@Status", SearchReq.Status);
                        command.Parameters.AddWithValue("@Action", "_getall");
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

        public async Task<DispatchGroupModel> GetGroupdetailsId(int PK_Id)
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
                        command.CommandText = "USP_DispatchGroupById";
                        command.Parameters.AddWithValue("@PK_Id", PK_Id);
     
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new DispatchGroupModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<DispatchGroupModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count>0)
                            {

                                data.GroupDataModel = CommonFuncationHelper.ConvertDataTable<List<DispatchGroupCodeModel>>(dataSet.Tables[1]);

                            }


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

        public async Task<DataTable> GetDownloadDispatchReceived(DownloadDispatchReceivedSearchModel SearchReq, string DownloadFile, string Dis_DownloadFile)
        {
            _actionName = "GetDownloadDispatchReceived(DownloadDispatchReceivedSearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DownloadDispatchReceived";
                        command.Parameters.AddWithValue("@DispatchID", SearchReq.DispatchID);
                        command.Parameters.AddWithValue("@Dis_DownloadFile", Dis_DownloadFile);
                        command.Parameters.AddWithValue("@DownloadFile", DownloadFile);
                        command.Parameters.AddWithValue("@Action", SearchReq.Action);
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

        public async Task<bool> DeleteGroupById(int ID,int ModifyBy)
        {
            _actionName = "DeleteGroupById(ID ,ModifyBy)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
              
                        command.CommandText = "USP_DeleteDispatchGroupbyId";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@ModifyBy", ModifyBy);

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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> getgroupteacherData(DispatchSearchModel SearchReq)
        {
            _actionName = "getgroupteacherData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchGroupPrinciple";
                        //command.Parameters.AddWithValue("@DispatchID", SearchReq.DispatchID);
                        //command.Parameters.AddWithValue("@DispatchDate", SearchReq.DispatchDate);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@ExaminerStatus", SearchReq.ExaminerStatus);
                        //command.Parameters.AddWithValue("@BundelID", SearchReq.BundelID);
                        command.Parameters.AddWithValue("@Status", SearchReq.Status);
                        command.Parameters.AddWithValue("@Action", "_getall");
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

        public async Task<DataTable> getgroupExaminerData(DispatchSearchModel SearchReq)
        {
            _actionName = "getgroupExaminerData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchGroupExaminer";
                        //command.Parameters.AddWithValue("@DispatchID", SearchReq.DispatchID);
                        //command.Parameters.AddWithValue("@DispatchDate", SearchReq.DispatchDate);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@SSOID", SearchReq.SSOID);
                        //command.Parameters.AddWithValue("@BundelID", SearchReq.BundelID);
                        command.Parameters.AddWithValue("@Status", SearchReq.Status);
                        command.Parameters.AddWithValue("@Action", "_getall");
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

        public async Task<bool> UpdateGroupfile(string File,int ID)
        {
            _actionName = "UpdateGroupfile(ID ,ModifyBy)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "USP_UpdateGroupfile";
                        command.Parameters.AddWithValue("@File", File);
                        command.Parameters.AddWithValue("@ID", ID);
             

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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }
        public async Task<int> OnSTatusUpdate(List<DispatchStatusUpdate> model)
        {
            _actionName = "OnSTatusUpdate(List<DispatchStatusUpdate> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchStatusUpdate_IU";

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_OnPublish");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> OnSTatusUpdateByExaminer(List<DispatchStatusUpdate> model)
        {
            _actionName = "OnSTatusUpdateByExaminer(List<DispatchStatusUpdate> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchStatusUpdateByexaminer_IU";

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_OnPublish");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> OnSTatusUpdateByBTER(List<DispatchStatusUpdate> model)
        {
            _actionName = "OnSTatusUpdateByBTER(List<DispatchStatusUpdate> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchStatusUpdateByBTER_IU";

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_OnPublish");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> SaveDispatchPrincipalGroupCodeData(DispatchPrincipalGroupCodeDataModel request)
        {
            _actionName = " SaveDispatchPrincipalGroupCodeData(DispatchPrincipalGroupCodeDataModel request)";
            return await Task.Run(async () =>
            {
                

                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandText = "USP_DispatchPrincipalGroupCode_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DPGCID", request.DPGCID);
                        command.Parameters.AddWithValue("@DispatchGroupID", request.DispatchGroupID);
                        command.Parameters.AddWithValue("@DispatchDate", request.DispatchDate);
                        command.Parameters.AddWithValue("@CompanyName", request.CompanyName);
                        command.Parameters.AddWithValue("@ChallanNo", request.ChallanNo);
                        command.Parameters.AddWithValue("@SupplierName", request.SupplierName);
                        command.Parameters.AddWithValue("@SupplierVehicleNo", request.SupplierVehicleNo);
                        command.Parameters.AddWithValue("@SupplierMobileNo", request.SupplierMobileNo);
                        command.Parameters.AddWithValue("@SupplierDate", request.SupplierDate);
                        command.Parameters.AddWithValue("@RecipientName", request.RecipientName);
                        command.Parameters.AddWithValue("@RecipientPost", request.RecipientPost);
                        command.Parameters.AddWithValue("@RecipientMobileNo", request.RecipientMobileNo);
                        command.Parameters.AddWithValue("@RecipientDate", request.RecipientDate);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@Action", "Insert");
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@CenterCode", request.CenterCode);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@Json", JsonConvert.SerializeObject(request.groupCodeModels) );
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetDispatchGroupcodeDetails(DispatchPrincipalGroupCodeSearchModel SearchReq)
        {
            _actionName = "GetDispatchGroupcodeDetails(DispatchPrincipalGroupCodeSearchModel SearchReq)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetDispatchGroupcodeDetails";
                       
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                    
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
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

        public async Task<DataTable> GetDispatchGroupcodeList(DispatchPrincipalGroupCodeSearchModel SearchReq)
        {
            _actionName = "GetDispatchGroupcodeList(DispatchPrincipalGroupCodeSearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_M_DispatchPrincipalGroupCode";
                        command.Parameters.AddWithValue("@DPGCID", SearchReq.DPGCID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@Action", "ViewData");
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

        public async Task<DispatchPrincipalGroupCodeDataModel> GetDispatchGroupcodeId(int PK_Id)
        {
            _actionName = "GetDispatchGroupcodeId(int PK_Id)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_M_DispatchPrincipalGroupCode";
                        command.Parameters.AddWithValue("@DPGCID", PK_Id);
                        command.Parameters.AddWithValue("@Action", "ViewByID");
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new DispatchPrincipalGroupCodeDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<DispatchPrincipalGroupCodeDataModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count > 0)
                            {

                                data.groupCodeModels = CommonFuncationHelper.ConvertDataTable<List<ViewByIDDispatchGroupCodeModel>>(dataSet.Tables[1]);

                            }


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

        public async Task<int> OnSTatusUpdateDispatchl(List<UpdateStatusDispatchPrincipalGroupCodeModel> model)
        {
            _actionName = "OnSTatusUpdateDispatchl(List<UpdateStatusDispatchPrincipalGroupCodeModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchPrincipalGroupCodeStatusUpdate_IU";

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_OnPublish");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> DeleteDispatchPrincipalGroupCodeById(int ID, int ModifyBy)
        {
            _actionName = "DeleteDispatchPrincipalGroupCodeById(ID ,ModifyBy)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "USP_DeleteDispatchPrincipalGroupCodebyId";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@ModifyBy", ModifyBy);

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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> UpdateDispatchPrincipalGroupCodefile(string File, int ID)
        {
            _actionName = "UpdateDispatchPrincipalGroupCodefile(ID ,ModifyBy)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "USP_UpdateDispatchPrincipalGroupCodefile";
                        command.Parameters.AddWithValue("@File", File);
                        command.Parameters.AddWithValue("@ID", ID);


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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }
        public async Task<DataSet> DownloadAckReportPri(DispatchSearchModel SearchReq)
        {
            _actionName = "getgroupteacherData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchGroupPrinciple_Rpt";
                        //command.Parameters.AddWithValue("@DispatchID", SearchReq.DispatchID);
                        //command.Parameters.AddWithValue("@DispatchDate", SearchReq.DispatchDate);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        //command.Parameters.AddWithValue("@BundelID", SearchReq.BundelID);
                        command.Parameters.AddWithValue("@Status", SearchReq.Status);
                        command.Parameters.AddWithValue("@Action", "_getall");
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

        public async Task<bool> DeleteDispatchMasterById(int ID, int ModifyBy)
        {
            _actionName = "DeleteDispatchMasterById(ID ,ModifyBy)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "USP_DispatchMasterViewByIdData";
                        command.Parameters.AddWithValue("@DispatchID", ID);
                        command.Parameters.AddWithValue("@Action", "DeleteByID");
                        command.Parameters.AddWithValue("@ModifyBy", ModifyBy);

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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<bool> UpdateDownloadFileDispatchMaster(string File, int ID)
        {
            _actionName = "UpdateDownloadFileDispatchMaster(string File, int ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "USP_UpdateDownloadFileDispatchMaster";
                        command.Parameters.AddWithValue("@Dis_DownloadFile", File);
                        command.Parameters.AddWithValue("@DispatchID", ID);


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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllDataDispatchMaster(DispatchSearchModel SearchReq)
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
                        command.CommandText = "USP_DispatchMasterViewByIdData";
                        command.Parameters.AddWithValue("@Action", "ViewData");
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
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

        public async Task<int> OnSTatusUpdateDispatchMaster(List<DispatchMasterStatusUpdate> model)
        {
            _actionName = "OnSTatusUpdateDispatchMaster(List<DispatchMasterStatusUpdate> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchMasterStatusUpdate_IU";

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_OnPublish");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DispatchFormDataModel> GetById(int PK_ID)
        {
            _actionName = "GetById(int PK_Id)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchMasterViewByIdData";
                        command.Parameters.AddWithValue("@DispatchID", PK_ID);
                        command.Parameters.AddWithValue("@Action", "ViewByIdData");
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new DispatchFormDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<DispatchFormDataModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                data.BundelDataModel = CommonFuncationHelper.ConvertDataTable<List<BundelDataModel>>(dataSet.Tables[1]);
                            }


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

        public async Task<bool> UpdateDownloadFileDispatchReceived(string File, int ID)
        {
            _actionName = "UpdateDownloadFileDispatchReceived(string File, int ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "USP_DownloadDispatchReceived";
                        command.Parameters.AddWithValue("@Dis_DownloadFile", File);
                        command.Parameters.AddWithValue("@DispatchID", ID);
                        command.Parameters.AddWithValue("@Action", "UpdateFileNameOrPathDownloadDispatchReceived");


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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> CheckDateDispatchSearch(CheckDateDispatchSearchModel SearchReq)
        {
            _actionName = "CheckDateDispatchSearch(CheckDateDispatchSearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = SearchReq.SPName;
                        command.Parameters.AddWithValue("@ID", SearchReq.ID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
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

        public async Task<int> UpdateBundleHandovertoExaminerByPrincipal(List<DispatchStatusUpdate> model)
        {
            _actionName = "UpdateBundleHandovertoExaminerByPrincipal(List<DispatchStatusUpdate> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchUpdateBundleHandovertoExaminerByPrincipal";

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_OnPublish");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<int> BundelNoSendToThePrincipalByTheExaminer(List<DispatchStatusUpdate> model)
        {
            _actionName = "BundelNoSendToThePrincipalByTheExaminer(List<DispatchStatusUpdate> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchBundelNoSendToThePrincipalByTheExaminer";

                        // Add parameters with appropriate null handling
                        //command.Parameters.AddWithValue("@action", "_OnPublish");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> DispatchSuperintendentAllottedExamDateList(DispatchSearchModel model)
        {
            _actionName = "DispatchSuperintendentAllottedExamDateList(DispatchGroupSearchModel SearchReq)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DispatchSuperintendentAllottedExamDateList";
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@UserID", model.UserID);
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


        public async Task<int> UpdateRemarkImageHandedOverToExaminerByPrincipal(UpdateFileHandovertoExaminerByPrincipalModel request)
        {
            _actionName = "UpdateRemarkImageHandedOverToExaminerByPrincipal(UpdateFileHandovertoExaminerByPrincipalModel request)";
            return await Task.Run(async () =>
            {
               

                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandText = "USP_Update_Remark_Image_HandedOverToExaminerByPrincipal";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@DueDate", request.DueDate);
                        command.Parameters.AddWithValue("@DispatchGroupFileName", request.FileName);
                        command.Parameters.AddWithValue("@DispatchGroup_Dis_FileName", request.Dis_File);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@DispatchGroupID", request.DispatchGroupID);
                        command.Parameters.AddWithValue("@GroupCodeIDs", request.GroupCodeIDs);

                        command.Parameters.Add("@Return", SqlDbType.Int); // out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output; // out

                        _sqlQuery = command.GetSqlExecutableQuery();

                        // Execute the command
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

        public async Task<DataTable> GetAllDataCompanyDispatch(CompanyDispatchMasterSearchModel SearchReq)
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
                        command.CommandText = "USP_M_Company_DispatchMaster";
                        command.Parameters.AddWithValue("@CompanyID", SearchReq.CompanyID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@Status", SearchReq.Status);
                        command.Parameters.AddWithValue("@Action", "GetAll");
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

        public async Task<int> SaveDataCompanyDispatch(CompanyDispatchMasterModel request)
        {
            _actionName = "SaveDataCompanyDispatch(CompanyDispatchMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandText = "USP_M_Company_DispatchMaster_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CompanyID", request.CompanyID);
                        command.Parameters.AddWithValue("@CompanyName", request.CompanyName);
                        command.Parameters.AddWithValue("@SupplierName", request.SupplierName);
                        command.Parameters.AddWithValue("@SupplierVehicleNo", request.SupplierVehicleNo);
                        command.Parameters.AddWithValue("@SupplierMobileNo", request.SupplierMobileNo);
                        command.Parameters.AddWithValue("@SupplierDate", request.SupplierDate);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@Action", "Insert");
                        command.Parameters.AddWithValue("@Status", request.Status);
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<CompanyDispatchMasterModel> GetByIdCompanyDispatch(int ID)
        {
            _actionName = "GetById(CompanyDispatchMasterSearchModel SearchReq)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_M_Company_DispatchMaster";
                        command.Parameters.AddWithValue("@Action", "CompanyDispatchById");
                        command.Parameters.AddWithValue("@CompanyID", ID);
                        //command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        //command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        //command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    var data = new CompanyDispatchMasterModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<CompanyDispatchMasterModel>(dataSet.Tables[0]);
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

        public async Task<bool> DeleteDataCompanyDispatchByID(int ID, int ModifyBy)
        {
            _actionName = "DeleteDataCompanyDispatchByID(int ID, int ModifyBy)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_M_Company_DispatchMaster";
                        command.Parameters.AddWithValue("@CompanyID", ID);
                        command.Parameters.AddWithValue("@Action", "DeleteCompany");
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }


        public async Task<DataTable> GetDispatchGroupcodeDetailsCheck(DispatchPrincipalGroupCodeSearchModel SearchReq)
        {
            _actionName = "GetDispatchGroupcodeDetails(DispatchPrincipalGroupCodeSearchModel SearchReq)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetDispatchGroupcodeDetailsCheck";

                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);

                        command.Parameters.AddWithValue("@CourseTypeID", SearchReq.CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@DispatchGroupID", SearchReq.DispatchGroupID);
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

        
    }
}








