using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.HostelManagementModel;
using Kaushal_Darpan.Models.StudentApplyForHostel;
using Kaushal_Darpan.Models.TSPAreaMaster;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class HostelManagementRepository : IHostelManagementRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public HostelManagementRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "HostelManagementRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllHostelList(HostelManagementSearchModel body)
        {

            _actionName = "GetAllHostelList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelMaster_IU";
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@HostelName", body.HostelName);
                        command.Parameters.AddWithValue("@HostelType", body.HostelType);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@action", "List");

                        _sqlQuery = command.GetSqlExecutableQuery();
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




        public async Task<int> SaveData(HostelManagementDataModel request)
        {


            return await Task.Run(async () =>
            {
                _actionName = "SaveData(HostelMaster request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelMaster_IU";
                        command.Parameters.AddWithValue("@HostelID", request.HostelID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@HostelName", request.HostelName);
                        command.Parameters.AddWithValue("@HostelType", request.HostelType);
                        command.Parameters.AddWithValue("@PhoneNumber", request.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", request.Address);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                        command.Parameters.AddWithValue("@Action", "SaveHostelMaster");

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


        public async Task<HostelManagementDataModel> GetByHostelId(int PK_ID)
        {
            _actionName = "GetByHostelId(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        _sqlQuery = $"select HostelID, HostelName, HostelID, HostelType, PhoneNumber, [Address] from M_HostelMaster where HostelID='{PK_ID}'";
                        command.CommandText = _sqlQuery;
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new HostelManagementDataModel();
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<HostelManagementDataModel>(dataTable);
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

        public async Task<bool> DeleteDataByID(StatusChangeModel request)
        {

            int result = 0;
            _actionName = "DeleteDataByID(GroupMaster request)";
            return await Task.Run(async () =>
            {
                try
                {
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        _sqlQuery = $" update M_HostelMaster set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'  Where HostelID={request.PK_ID}";
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

        public async Task<DataTable> GetStudentDetailsForHostelApply(HostelStudentSearchModel body)
        {

            _actionName = "GetStudentDetailsForHostelApply()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (body.DepartmentID == 1)
                        {
                            command.CommandText = "USP_ApplyHostelByStudent";
                        }
                        else if (body.DepartmentID == 2)
                        {
                            command.CommandText = "USP_ITI_ApplyHostelByStudent";
                        }


                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@PartnerApplicationID", body.PartnerApplicationID);
                        command.Parameters.AddWithValue("@Action", body.Action);

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<DataTable> GetStudentDetailsForHostel_Principle(HostelStudentSearchModel body)
        {

            _actionName = "GetStudentDetailsForHostelApply()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "USP_ApplyHostelByStudent_new";

                        command.Parameters.AddWithValue("@ReqId", body.ReqId);
                        command.Parameters.AddWithValue("@PartnerApplicationID", body.PartnerApplicationID);
                        command.Parameters.AddWithValue("@Action", "_StudentDetails_AtPrinciple");

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<DataTable> GetHostelNameList(HostelRoomSeatSearchModel body)
        {

            _actionName = "GetHostelNameList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelRoomSeatMaster";
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@action", "_GetHostelNameList");

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<int> SaveRoomSeatData(HostelRoomSeatDataModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "SaveRoomSeatData(HostelRoomSeatDataModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelRoomSeatMaster";
                        command.Parameters.AddWithValue("@Action", "SaveRoomSeatMaster");
                        command.Parameters.AddWithValue("@HRSMasterID", request.HRSMasterID);
                        command.Parameters.AddWithValue("@HostelID", request.HostelID);
                        command.Parameters.AddWithValue("@RoomType", request.RoomType);
                        command.Parameters.AddWithValue("@RoomFee", request.RoomFee);
                        command.Parameters.AddWithValue("@SeatCapacity", request.SeatCapacity);
                        command.Parameters.AddWithValue("@RoomQuantity", request.RoomQuantity);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
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




        //public async Task<bool> SaveRoomSeatData(HostelRoomSeatDataModel request)
        //{
        //    _actionName = "SaveRoomSeatData(HostelRoomSeatDataModel request)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            int result = 0;
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_HostelRoomSeatMaster";
        //                command.Parameters.AddWithValue("@Action", "SaveRoomSeatMaster");
        //                command.Parameters.AddWithValue("@HRSMasterID", request.HRSMasterID);
        //                command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
        //                command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
        //                command.Parameters.AddWithValue("@HostelID", request.HostelID);
        //                command.Parameters.AddWithValue("@RoomType", request.RoomType);
        //                command.Parameters.AddWithValue("@SeatCapacity", request.SeatCapacity);
        //                command.Parameters.AddWithValue("@RoomQuantity", request.RoomQuantity);
        //                command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
        //                command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
        //                command.Parameters.AddWithValue("@RoomFee", request.RoomFee);
        //                command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);


        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                result = await command.ExecuteNonQueryAsync();
        //            }

        //            return result > 0; 
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
        //            var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
        //            throw new Exception(errorDetails, ex);
        //        }
        //    });
        //}




        public async Task<DataTable> GetAllRoomSeatList(HostelRoomSeatSearchModel body)
        {

            _actionName = "GetAllRoomSeatList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelRoomSeatMaster";
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@HostelID", body.HostelID);
                        command.Parameters.AddWithValue("@RoomType", body.RoomType);
                        command.Parameters.AddWithValue("@SeatCapacity", body.SeatCapacity);
                        command.Parameters.AddWithValue("@RoomQuantity", body.RoomQuantity);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@action", "List");

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<HostelRoomSeatDataModel> GetByHRSMasterID(int PK_ID)
        {
            _actionName = "GetByHRSMasterID(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        _sqlQuery = $"select HRSMasterID, HostelID, RoomType, SeatCapacity, RoomQuantity,FeePerBad as RoomFee  from M_HostelRoomSeatMaster where HRSMasterID='{PK_ID}'";
                        command.CommandText = _sqlQuery;
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new HostelRoomSeatDataModel();
                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<HostelRoomSeatDataModel>(dataTable);
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

        public async Task<int> DeleteDataByHRSMasterID(StatusChangeModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "DeleteDataByHRSMasterID(HostelRoomSeatDataModel request)";
           
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelRoomSeatMaster";
                        command.Parameters.AddWithValue("@HRSMasterID", request.PK_ID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@action", "Delete");
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


       


        public async Task<int> SaveFacilities(HostelFacilitiesDataModel request)
        {


            return await Task.Run(async () =>
            {
                _actionName = "SaveFacilities(HostelMaster request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelFacilities";
                        command.Parameters.AddWithValue("@HFID", request.HFID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@HostelID", request.HostelID);
                        command.Parameters.AddWithValue("@FacilitiesName", request.FacilitiesName);
                        command.Parameters.AddWithValue("@IsFacilities", request.IsFacilities);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                        command.Parameters.AddWithValue("@Action", "SaveHostelFacility");
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

        public async Task<DataTable> HostelFacilityList(HostelFacilitiesSearchModel body)
        {

            _actionName = "HostelFacilityList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelFacilities";
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@HostelID", body.HostelID);
                        command.Parameters.AddWithValue("@FacilitiesName", body.FacilitiesName);
                        command.Parameters.AddWithValue("@IsFacilities", body.IsFacilities);

                        command.Parameters.AddWithValue("@action", "List");

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<DataTable> GetByHFID(int PK_ID)
        {
            _actionName = "GetByHFID(int PK_ID)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        //_sqlQuery = $"select HFID,HostelID, WaterCooler, RoWater, NearbyMarket, MarketDistance, PlayGround, PlayGroundDistance from M_HostelFacilities where HFID='{PK_ID}'";
                        //command.CommandText = _sqlQuery;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelFacilities";
                        command.Parameters.AddWithValue("@HFID", PK_ID);
                        command.Parameters.AddWithValue("@action", "GetByHFID");
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    //var data = new HostelFacilitiesDataModel();
                    //if (dataTable != null)
                    //{
                    //    if (dataTable.Rows.Count > 0)
                    //    {
                    //        data = CommonFuncationHelper.ConvertDataTable<HostelFacilitiesDataModel>(dataTable);
                    //    }
                    //}
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

        public async Task<bool> DeleteDataByHFID(StatusChangeModel request)
        {
            _actionName = "DeleteDataByHFID(HostelFacilitiesDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        //command.CommandText = $"update M_ExaminerMaster  set ActiveStatus=0,DeleteStatus=1,ModifyBy='{request.ModifyBy} ',ModifyDate=GETDATE(),IPAddress='{_IPAddress}'Where ExaminerID={request.ExaminerID}";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelFacilities";
                        command.Parameters.AddWithValue("@HFID", request.PK_ID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@action", "Delete");
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









        public async Task<int> StudentApplyHostel(StudentApplyHostelData request)
        {


            return await Task.Run(async () =>
            {
                _actionName = "StudentApplyHostel(StudentApplyHostelData request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentApplyHostel_IU";
                        command.Parameters.AddWithValue("@ReqId", request.ReqId);
                        command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        command.Parameters.AddWithValue("@PartnerApplicationID", request.PartnerApplicationID);
                        command.Parameters.AddWithValue("@FatherContactNo", request.FatherContactNo);
                        command.Parameters.AddWithValue("@LocalGuardianName", request.LocalGuardianName);
                        command.Parameters.AddWithValue("@LocalGuardianContactNo", request.LocalGuardianContactNo);
                        command.Parameters.AddWithValue("@AllotedHostelLastEndTerm", request.AllotedHostelLastEndTerm);
                        command.Parameters.AddWithValue("@AllotedHostelInLastSessionRoomNo", request.AllotedHostelInLastSessionRoomNo);
                        command.Parameters.AddWithValue("@AllotedHostelInLastSessionFeeDetails", request.AllotedHostelInLastSessionFeeDetails);
                        command.Parameters.AddWithValue("@AnyWorningForShortOfAttendance", request.AnyWorningForShortOfAttendance);
                        command.Parameters.AddWithValue("@AnyWarningForInvovementAgainstDiscipline", request.AnyWarningForInvovementAgainstDiscipline);
                        command.Parameters.AddWithValue("@RoomPartnerName", request.RoomPartnerName);
                        command.Parameters.AddWithValue("@RoomPartnerYear", request.RoomPartnerYear);
                        command.Parameters.AddWithValue("@RoomPartnerBranch", request.RoomPartnerBranch);
                        command.Parameters.AddWithValue("@RoomPartnerSFS", request.RoomPartnerSFS);
                        command.Parameters.AddWithValue("@RoomPartnerRegular", request.RoomPartnerRegular);
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermId);
                        command.Parameters.AddWithValue("@HostelID", request.HostelID);
                        command.Parameters.AddWithValue("@TotalAvg", request.TotalAvg);
                        command.Parameters.AddWithValue("@AffidavitDocument", request.AffidavitDocument);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@dis_AffidavitDocument", request.dis_AffidavitDocument);


                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);


                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out
                        command.Parameters.AddWithValue("@Action", "_ApplyHostel");

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

        public async Task<int> EditStudentApplyHostel(StudentApplyHostelData request)
        {


            return await Task.Run(async () =>
            {
                _actionName = " EditStudentApplyHostel(StudentApplyHostelData request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentApplyHostel_Update";
                        command.Parameters.AddWithValue("@ReqId", request.ReqId);
                        command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        command.Parameters.AddWithValue("@PartnerApplicationID", request.PartnerApplicationID);
                        command.Parameters.AddWithValue("@FatherContactNo", request.FatherContactNo);
                        command.Parameters.AddWithValue("@LocalGuardianName", request.LocalGuardianName);
                        command.Parameters.AddWithValue("@LocalGuardianContactNo", request.LocalGuardianContactNo);
                        command.Parameters.AddWithValue("@AllotedHostelLastEndTerm", request.AllotedHostelLastEndTerm);
                        command.Parameters.AddWithValue("@AllotedHostelInLastSessionRoomNo", request.AllotedHostelInLastSessionRoomNo);
                        command.Parameters.AddWithValue("@AllotedHostelInLastSessionFeeDetails", request.AllotedHostelInLastSessionFeeDetails);
                        command.Parameters.AddWithValue("@AnyWorningForShortOfAttendance", request.AnyWorningForShortOfAttendance);
                        command.Parameters.AddWithValue("@AnyWarningForInvovementAgainstDiscipline", request.AnyWarningForInvovementAgainstDiscipline);
                        command.Parameters.AddWithValue("@RoomPartnerName", request.RoomPartnerName);
                        command.Parameters.AddWithValue("@RoomPartnerYear", request.RoomPartnerYear);
                        command.Parameters.AddWithValue("@RoomPartnerBranch", request.RoomPartnerBranch);
                        command.Parameters.AddWithValue("@RoomPartnerSFS", request.RoomPartnerSFS);
                        command.Parameters.AddWithValue("@RoomPartnerRegular", request.RoomPartnerRegular);
                        command.Parameters.AddWithValue("@HostelID", request.HostelID);
                        command.Parameters.AddWithValue("@TotalAvg", request.TotalAvg);
                        command.Parameters.AddWithValue("@AffidavitDocument", request.AffidavitDocument);
                        command.Parameters.AddWithValue("@SupportingDocument", request.SupportingDocument);
                        command.Parameters.AddWithValue("@dis_AffidavitDocument", request.dis_AffidavitDocument);
                        command.Parameters.AddWithValue("@dis_SupportingDocument", request.dis_SupportingDocument);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
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






        public async Task<int> HostelWardenupdateData(StudentApplyHostelData request)
        {
            return await Task.Run(async () =>
            {
                _actionName = " HostelWardenupdateData(StudentApplyHostelData request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelWarden_Update";
                        command.Parameters.AddWithValue("@ReqId", request.ReqId);
                        command.Parameters.AddWithValue("@StudentID", request.StudentID);
                        command.Parameters.AddWithValue("@FatherContactNo", request.FatherContactNo);
                        command.Parameters.AddWithValue("@LocalGuardianName", request.LocalGuardianName);
                        command.Parameters.AddWithValue("@LocalGuardianContactNo", request.LocalGuardianContactNo);
                        command.Parameters.AddWithValue("@AllotedHostelLastEndTerm", request.AllotedHostelLastEndTerm);
                        command.Parameters.AddWithValue("@HostelID", request.HostelID);
                        command.Parameters.AddWithValue("@SupportingDocument", request.SupportingDocument);
                        command.Parameters.AddWithValue("@dis_SupportingDocument", request.dis_SupportingDocument);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.Add("@Return", SqlDbType.Int);// out
                        command.Parameters["@Return"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<DataSet> CollegeHostelDetailsList(CollegeHostelDetailsSearchModel body)
        {
            _actionName = "CollegeHostelDetailsList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataTable = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CollegeHostelDetails";
                        command.Parameters.AddWithValue("@UserID", body.UserID);
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@HostelName", body.HostelName);
                        command.Parameters.AddWithValue("@HostelType", body.HostelType);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@action", "ShowHostelDetails");

                        _sqlQuery = command.GetSqlExecutableQuery();
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


        public async Task<bool> IsFacilitiesStatusByID(StatusChangeModel request)
        {
            _actionName = "IsFacilitiesStatusByID(HostelFacilitiesDataModel request)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandType = CommandType.Text;
                    var Query = @"
                UPDATE M_HostelFacilities
                SET 
                    IsFacilities = CASE 
                        WHEN IsFacilities = 1 THEN 0
                        WHEN IsFacilities = 0 THEN 1
                    END,
                    ModifyDate = GETDATE() WHERE HFID ='" + request.PK_ID + "' ";
                    command.CommandText = Query;

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



        public async Task<DataSet> GetLastFYEndTerm(HostelStudentSearchModel request)
        {
            _actionName = "GetLastFYEndTerm()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataTable = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ApplyHostelByStudent";
                        command.Parameters.AddWithValue("@EndTermId", request.EndTermID);
                        command.Parameters.AddWithValue("@action", request.Action);
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


        public async Task<DataTable> GetAllotedHostelDetails(HostelStudentSearchModel body)
        {

            _actionName = "GetAllRoomSeatList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelRoomDetails";
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@HostelID", body.HostelID);
                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@action", "GetHostelAllotedStudentDetails");

                        _sqlQuery = command.GetSqlExecutableQuery();
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




        public async Task<DataTable> EditAllotedHostelDetails(EditHostelStudentSearchModel body)
        {

            _actionName = "GetAllRoomSeatList()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_HostelRoomDetails";
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@HostelID", body.HostelID);
                        command.Parameters.AddWithValue("@StudentID", body.StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@ReqId", body.ReqId);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
                        command.Parameters.AddWithValue("@action", "GetHostelAllotedStudentDetails");

                        _sqlQuery = command.GetSqlExecutableQuery();
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








