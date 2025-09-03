using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.DTEApplicationDashboardModel;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.IssuedItems;
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using Kaushal_Darpan.Models.ItemsMaster;
using Kaushal_Darpan.Models.StudentApplyForHostel;
using Kaushal_Darpan.Models.StudentRequestsModel;
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
    internal class StudentRequestsRepository : IStudentRequestsRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public StudentRequestsRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "StudentApplyForHostelRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(SearchStudentApplyForHostel SearchReq)
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
                        command.CommandText = "USP_GetAllHostelStudent";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@SemesterId", SearchReq.SemesterId);
                        command.Parameters.AddWithValue("@HostelID", SearchReq.HostelID);
                        command.Parameters.AddWithValue("@BrachId", SearchReq.BrachId);
                        command.Parameters.AddWithValue("@EndTermId", SearchReq.EndTermId);
                        //command.Parameters.AddWithValue("@status", SearchReq.status);
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

        public async Task<DataTable> GetAllRoomAllotment(SearchStudentAllotment SearchReq)
        {
            _actionName = "GetAllRoomAllotment()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllHostelRoomAllotment";
                        command.Parameters.AddWithValue("@ApplicationId", SearchReq.ApplicationId);
                        command.Parameters.AddWithValue("@SemesterId", SearchReq.SemesterId);
                        command.Parameters.AddWithValue("@BrachId", SearchReq.BrachId);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@HostelID", SearchReq.HostelID);
                        command.Parameters.AddWithValue("@EndTermID", SearchReq.EndTermID);
                        command.Parameters.AddWithValue("@AffidavitDoc", SearchReq.AffidavitDoc);
                        command.Parameters.AddWithValue("@SupportingDocument", SearchReq.SupportingDocument);
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



        public async Task<bool> SaveData(RoomAllotmentModel request)
        {
            _actionName = "SaveData(RoomAllotmentModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_RoomAllotment_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                       
                        command.Parameters.AddWithValue("@AllotSeatId", request.AllotSeatId);
                        command.Parameters.AddWithValue("@ReqId", request.ReqId);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@RoomTypeId", request.RoomTypeId);
                        command.Parameters.AddWithValue("@RoomNoId", request.RoomNoId);
                        command.Parameters.AddWithValue("@HostelFeesReciept", request.HostelFeesReciept);
                        command.Parameters.AddWithValue("@Dis_HostelFeesReciept", request.Dis_HostelFeesReciept);
                        command.Parameters.AddWithValue("@Relation", request.Relation);
                        command.Parameters.AddWithValue("@ContactPersonName", request.ContactPersonName);
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@FessAmount", request.FessAmount);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
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


        public async Task<DataSet> GetAllRoomAvailabilties(RoomAllotmentModel request)
        {
            _actionName = "GetAllRoomAvailabilties()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataTable = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllRoomAvailabilties";
                        command.Parameters.AddWithValue("@HostelID", request.HostelID);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync();
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
        
        


        public async Task<bool> ApprovedReq(RoomAllotmentModel request)
        {
            _actionName = "ApprovedReq(RoomAllotmentModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandText = $"update Tbl_StudentApplyForHostel  set AllotmentStatus = 2,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ReqId = {request.ReqId}";

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



        public async Task<bool> AllotmentCancelData(RoomAllotmentModel request)
        {
            _actionName = "SaveData(RoomAllotmentModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_RoomAllotmentCancel_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling

                        command.Parameters.AddWithValue("@ReqId", request.ReqId);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@AllotmentStatus", request.AllotmentStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
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



        public async Task<DataTable> GetReportData(SearchStudentApplyForHostel SearchReq)
        {
            _actionName = "GetReportData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if(SearchReq.status == 296)
                        {
                            command.CommandText = "USP_EnrollmentCancleData_Hostel";
                        }
                        else
                        {
                            command.CommandText = "USP_GetAllHostelReportData";
                        }
                       
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@SemesterId", SearchReq.SemesterId);
                        command.Parameters.AddWithValue("@BrachId", SearchReq.BrachId);
                        command.Parameters.AddWithValue("@EndTermId", SearchReq.EndTermId);
                        command.Parameters.AddWithValue("@HostelID", SearchReq.HostelID);
                        command.Parameters.AddWithValue("@DepartmentID", SearchReq.DepartmentID);
                        command.Parameters.AddWithValue("@status", SearchReq.status);
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


        public async Task<DataTable> GetHostelDashboard(DTEApplicationDashboardModel filterModel)
        {
            _actionName = "GetHostelDashboard()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_AdminHostelDashboard";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@ModifyBy", filterModel.ModifyBy);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@HostelID", filterModel.HostelID);
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

        public async Task<DataTable> GetGuestRoomDashboard(DTEApplicationDashboardModel filterModel)
        {
            _actionName = "GetGuestRoomDashboard()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_AdminGuestRoomDashboard";

                        // Add parameters to the stored procedure from the model
                        command.Parameters.AddWithValue("@DepartmentID", filterModel.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", filterModel.Eng_NonEng);
                        command.Parameters.AddWithValue("@ModifyBy", filterModel.ModifyBy);
                        command.Parameters.AddWithValue("@EndTermID", filterModel.EndTermID);
                        command.Parameters.AddWithValue("@GuestHouseID", filterModel.GuestHouseID);
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


        public async Task<DataTable> GetAllHostelStudentMeritlist(SearchStudentApplyForHostel SearchReq)
         {
            _actionName = "GetAllHostelStudentMeritlist()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllHostelStudentMeritlist";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@SemesterId", SearchReq.SemesterId);
                        command.Parameters.AddWithValue("@HostelID", SearchReq.HostelID);
                        command.Parameters.AddWithValue("@BrachId", SearchReq.BrachId);
                        command.Parameters.AddWithValue("@EndTermId", SearchReq.EndTermId);
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

        public async Task<DataTable> GetAllGenerateHostelStudentMeritlist(SearchStudentApplyForHostel SearchReq)
        {
            _actionName = "GetAllGenerateHostelStudentMeritlist()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllGenerateHostelStudentMeritlist";
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(SearchReq.ReqId));
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



        public async Task<DataTable> GetAllGenerateHostelWardenStudentMeritlist(SearchStudentApplyForHostel SearchReq)
        
        {
            _actionName = "GetAllGenerateHostelWardenStudentMeritlist()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllGenerateHostelWardenStudentMeritlist";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@SemesterId", SearchReq.SemesterId);
                        command.Parameters.AddWithValue("@HostelID", SearchReq.HostelID);
                        command.Parameters.AddWithValue("@BrachId", SearchReq.BrachId);
                        command.Parameters.AddWithValue("@EndTermId", SearchReq.EndTermId);
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

        public async Task<DataTable> GetAllObjectionWardenStudentMeritlist(SearchStudentApplyForHostel SearchReq)

        {
            _actionName = "GetAllObjectionWardenStudentMeritlist()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllGenerateHostelWardenStudentMeritlist";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@SemesterId", SearchReq.SemesterId);
                        command.Parameters.AddWithValue("@HostelID", SearchReq.HostelID);
                        command.Parameters.AddWithValue("@BrachId", SearchReq.BrachId);
                        command.Parameters.AddWithValue("@EndTermId", SearchReq.EndTermId);
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

        public async Task<int> GetAllPublishHostelStudentMeritlist(List<PublishHostelMeritListDataModel> model)
        {
            _actionName = "GetAllPublishHostelStudentMeritlist(List<SearchStudentApplyForHostel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_GeneratePublishMeritList";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@action", "_PublishMeritList");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }







        //public async Task<int> GetAllPublishHostelStudentMeritlist(List<PublishHostelMeritListDataModel> model)
        //{
        //    _actionName = "GetAllPublishHostelStudentMeritlist(List<SearchStudentApplyForHostel> model)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            int retval = 0;
        //            using (var command = _dbContext.CreateCommand(true))
        //            {
        //                command.CommandText = "USP_GeneratePublishMeritList";
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandTimeout = 0;
        //                command.Parameters.AddWithValue("@action", "_PublishMeritList");
        //                command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
        //                command.Parameters.Add("@Retval", SqlDbType.Int);
        //                command.Parameters["@Retval"].Direction = ParameterDirection.Output;
        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                result = await command.ExecuteNonQueryAsync();
        //                retval = Convert.ToInt32(command.Parameters["@Retval"].Value);
        //            }
        //            return retval;
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


        //public async Task<int> GetAllFinalPublishHostelStudentMeritlist(List<PublishHostelMeritListDataModel> model)
        //{
        //    _actionName = "GetAllFinalPublishHostelStudentMeritlist(List<SearchStudentApplyForHostel> model)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            int retval = 0;
        //            using (var command = _dbContext.CreateCommand(true))
        //            {

        //                command.CommandText = "USP_FinalPublishMeritList";
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandTimeout = 0;
        //                command.Parameters.AddWithValue("@action", "_FinalPublishMeritList");
        //                command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
        //                command.Parameters.Add("@Retval", SqlDbType.Int);
        //                command.Parameters["@Retval"].Direction = ParameterDirection.Output;

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                result = await command.ExecuteNonQueryAsync();

        //                retval = Convert.ToInt32(command.Parameters["@Retval"].Value);

        //            }
        //            return retval;
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

        public async Task<int> GetAllFinalPublishHostelStudentMeritlist(List<PublishHostelMeritListDataModel> model)
        {
            _actionName = "GetAllFinalPublishHostelStudentMeritlist(List<PublishHostelMeritListDataModel> model)";

            return await Task.Run(async () =>
            {
                try
                {
                    if (model == null || model.Count == 0)
                        return -99;

                    int retval = 0;
                    string jsonData = JsonConvert.SerializeObject(model);
                    Console.WriteLine("Serialized JSON: " + jsonData);

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_FinalPublishMeritList";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@action", "_FinalPublishMeritList");
                        command.Parameters.AddWithValue("@rowJson", jsonData);
                        command.Parameters.Add("@Retval", SqlDbType.Int).Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery(); // for logging
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }



        public async Task<int> GetAllFinalCorrectionPublishHostelStudentMeritlist(List<PublishHostelMeritListDataModel> model)
        {
            _actionName = "GetAllFinalCorrectionPublishHostelStudentMeritlist(List<PublishHostelMeritListDataModel> model)";

            return await Task.Run(async () =>
            {
                try
                {
                    if (model == null || model.Count == 0)
                        return -99; // no data passed

                    int retval = 0;
                    string jsonData = JsonConvert.SerializeObject(model); // Serialize once for debug/log

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_CorrectionPublishMeritList";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@action", "_CorrectionPublishMeritList");
                        command.Parameters.AddWithValue("@rowJson", jsonData);

                        command.Parameters.Add("@Retval", SqlDbType.Int).Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery(); // for error tracking
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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }

        public async Task<DataTable> GetAllfinalHostelStudentMeritlist(SearchStudentApplyForHostel SearchReq)

        {
            _actionName = "GetAllfinalHostelStudentMeritlist()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllfinalHostelStudentMeritlist";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@SemesterId", SearchReq.SemesterId);
                        command.Parameters.AddWithValue("@HostelID", SearchReq.HostelID);
                        command.Parameters.AddWithValue("@BrachId", SearchReq.BrachId);
                        command.Parameters.AddWithValue("@EndTermId", SearchReq.EndTermId);
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


        public async Task<int> GetAllAffidavitApproved(List<PublishHostelMeritListDataModel> model)
        {
            _actionName = "GetAllAffidavitApproved(List<SearchStudentApplyForHostel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_StudentAffidavitApproved";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;
                        command.Parameters.AddWithValue("@action", "_AffidavitApproved");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.Add("@Retval", SqlDbType.Int);
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }


        public async Task<int> GetAllAffidavitObjection(List<PublishHostelMeritListDataModel> model)
        {
            _actionName = "GetAllAffidavitObjection(List<SearchStudentApplyForHostel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {

                        command.CommandText = "USP_StudentAffidavitObjection";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;
                        command.Parameters.AddWithValue("@action", "_AffidavitObjection");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));
                        command.Parameters.Add("@Retval", SqlDbType.Int);
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }





        public async Task<DataTable> GetAllPrincipalstudentmeritlist(SearchStudentApplyForHostel SearchReq)
        {
            _actionName = "GetAllPrincipalstudentmeritlist()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllHostelStudentMeritlist";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@SemesterId", SearchReq.SemesterId);
                        command.Parameters.AddWithValue("@HostelID", SearchReq.HostelID);
                        command.Parameters.AddWithValue("@BrachId", SearchReq.BrachId);
                        command.Parameters.AddWithValue("@EndTermId", SearchReq.EndTermId);
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


        public async Task<DataTable> GetAllDataStatus(SearchStudentApplyForHostel SearchReq)
        {
            _actionName = "GetAllDataStatus()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetAllStudentReportByHostelWarden";
                        command.Parameters.AddWithValue("@InstituteID", SearchReq.InstituteID);
                        command.Parameters.AddWithValue("@SemesterId", SearchReq.SemesterId);
                        command.Parameters.AddWithValue("@HostelID", SearchReq.HostelID);
                        command.Parameters.AddWithValue("@BrachId", SearchReq.BrachId);
                        command.Parameters.AddWithValue("@EndTermId", SearchReq.EndTermId);
                        command.Parameters.AddWithValue("@Action", SearchReq.Action);
                        command.Parameters.AddWithValue("@AllotmentStatus", SearchReq.AllotmentStatus);
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

        public async Task<bool> DeallocateRoom(DeallocateRoomDataModel request)
        {
            _actionName = "DeallocateRoom(DeallocateRoomDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_BTER_Hostel_DeallocateRoom";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling

                        command.Parameters.AddWithValue("@ReqId", request.ReqId);
                        command.Parameters.AddWithValue("@AllotSeatId", request.AllotSeatId);
                        command.Parameters.AddWithValue("@RoleID", request.RoleID);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
                        command.Parameters.AddWithValue("@action", request.Action);

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
    }
}
