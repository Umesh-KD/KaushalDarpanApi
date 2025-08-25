using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.ItiCompanyMaster;
using Kaushal_Darpan.Models.ITIPlanning;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using static Kaushal_Darpan.Models.CommonFunction.ItiTradeAndCollegesDDL;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITICollegeMasterRepository : IITICollegeMasterRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITICollegeMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CollegeMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<DataTable> GetAllData(ITIsSearchModel model)
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
                        command.CommandText = "USP_ITICollegeMasterList";
                    
                        command.Parameters.AddWithValue("@ZoneID", model.ZoneID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@TradeID", model.TradeID);
                        command.Parameters.AddWithValue("@TehsilID", model.TehsilID);
                        command.Parameters.AddWithValue("@FeeStatus", model.FeeStatus);
                        command.Parameters.AddWithValue("@ITItypeID", model.ITItypeID);
                        command.Parameters.AddWithValue("@ExamTypeId", model.ExamTypeId);
                        command.Parameters.AddWithValue("@ExamSystemId", model.ExamSystemId);
                        command.Parameters.AddWithValue("@CourseID", model.CourseID);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@Name", model.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ItiCode", model.ItiCode ?? (object)DBNull.Value);
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
        
        public async Task<ITICollegeMasterModel> Get_ITIsData_ByID(int Id)
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
                        command.CommandText = "USP_ITICollegeMasterGetById";
                        command.Parameters.AddWithValue("@action", "_getAllData");
                        command.Parameters.AddWithValue("@Id",Id);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITICollegeMasterModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITICollegeMasterModel>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count>0)
                            {

                                data.SeatIntakes = CommonFuncationHelper.ConvertDataTable<List<SeatIntakesModel>>(dataSet.Tables[1]);

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

        public async Task<ITI_PlanningColleges> Get_ITIsPlanningData_ByID(int Id)
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
                        command.CommandText = "USP_ITICollegePlanningGetById";
                        command.Parameters.AddWithValue("@action", "_getAllData");
                        command.Parameters.AddWithValue("@Id", Id);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITI_PlanningColleges();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ITI_PlanningColleges>(dataSet.Tables[0]);

                            if (dataSet.Tables[1].Rows.Count>0)
                            {

                                data.ItiMembersModel = CommonFuncationHelper.ConvertDataTable<List<ItiMembersModel>>(dataSet.Tables[1]);

                            }
                            if (dataSet.Tables[2].Rows.Count>0)
                            {

                                data.ItiAffiliationList = CommonFuncationHelper.ConvertDataTable<List<ItiAffiliationList>>(dataSet.Tables[2]);

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

        public async Task<DataSet> Get_ITIsPlanningDataByID(int Id)
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
                        command.CommandText = "USP_ITICollegePlanningGetById";
                        command.Parameters.AddWithValue("@action", "_getAllData");
                        command.Parameters.AddWithValue("@Id", Id);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ITI_PlanningColleges();
                    //if (dataSet != null)
                    //{
                    //    if (dataSet.Tables.Count > 0)
                    //    {
                    //        data = CommonFuncationHelper.ConvertDataTable<ITI_PlanningColleges>(dataSet.Tables[0]);

                    //        if (dataSet.Tables[1].Rows.Count > 0)
                    //        {

                    //            data.ItiMembersModel = CommonFuncationHelper.ConvertDataTable<List<ItiMembersModel>>(dataSet.Tables[1]);

                    //        }
                    //        if (dataSet.Tables[2].Rows.Count > 0)
                    //        {

                    //            data.ItiAffiliationList = CommonFuncationHelper.ConvertDataTable<List<ItiAffiliationList>>(dataSet.Tables[2]);

                    //        }

                    //    }
                    //}
                    return dataSet;
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




        public async Task<int> SaveData(ITICollegeMasterModel request)
        {
                        _actionName = "SaveData(ITICollegeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int returnValue = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        
                        command.CommandText = "USP_ITICollegeMaster_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", request.Id);
                        command.Parameters.AddWithValue("@InstituteTypeID", request.InstituteTypeID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@DGETCode", request.DGETCode);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
                        command.Parameters.AddWithValue("@EmailAddress", request.EmailAddress);
                        command.Parameters.AddWithValue("@FaxNumber", request.FaxNumber);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@Pincode", request.Pincode);
                        command.Parameters.AddWithValue("@ManagementTypeID", request.ManagementTypeID);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@Has8th", request.Has8th);
                        command.Parameters.AddWithValue("@Has10th", request.Has10th);
                        command.Parameters.AddWithValue("@Has12th", request.Has12th);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                   
                  
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ITISeatIntakesModel", JsonConvert.SerializeObject(request.SeatIntakes));

                     
                       
                        var returnParam = new SqlParameter("@Return", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(returnParam);

                       
                        _sqlQuery = command.GetSqlExecutableQuery();                       
                        await command.ExecuteNonQueryAsync();                        
                        returnValue = (int)returnParam.Value;                        
                        return returnValue;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors by logging them
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

        public async Task<bool> UpdateActiveStatusByID(ITICollegeMasterModel request)
        {
            _actionName = "UpdateActiveStatusByID(CollegeMasterModel request)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandText = "USP_UpdateITICollegeStatus";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", request.Id);
                    command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                    command.Parameters.AddWithValue("@Remark", request.Remark);
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
            

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

        public async Task<DataTable> GetItiTradeData_ByID(int Id)
        {
            _actionName = "GetItiTradeData_ByID(int Id)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICollegeMasterGetById ";

                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@action", "_getCollegeTrades");

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

        public async Task<bool> ResetSSOID(int id, int ModifyBy,string remark,string SSOID)
        {
            _actionName = "ResetSSOID(ITICollegeMasterModel request)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandText = "USP_UpdateSSOIDTICollegeStatus";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@ModifyBy", ModifyBy);
                    command.Parameters.AddWithValue("@Remark", remark);
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                    command.Parameters.AddWithValue("@SSOID", SSOID);

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

        public async Task<bool> DeleteDataById(int id, int ModifyBy, string remark)
        {
            _actionName = "DeleteDataById(int id, int ModifyBy)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandText = "USP_DeleteITICollegeStatus";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@ModifyBy", ModifyBy);
                    command.Parameters.AddWithValue("@Remark", remark);
                    command.Parameters.AddWithValue("@IPAddress",_IPAddress);

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


        public async Task<bool> SaveDataPlanning(ITI_PlanningColleges request)
        {
            _actionName = "SaveDataPlanning(ITI_PlanningColleges request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int returnValue = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITICollegePlanning_IU"; // Your stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PlanningID", request.PlanningID);
                        command.Parameters.AddWithValue("@CollegeId", request.CollegeId);
                        command.Parameters.AddWithValue("@InstitutionCategoryId", request.InstitutionCategoryId);
                        command.Parameters.AddWithValue("@InstituteManagementId", request.InstituteManagementId);
                        command.Parameters.AddWithValue("@TrustSociety", request.TrustSociety);
                        command.Parameters.AddWithValue("@TrustSocietyName", request.TrustSocietyName);
                        command.Parameters.AddWithValue("@RegNo", request.RegNo);
                        command.Parameters.AddWithValue("@RegDate", request.RegDate);
                        command.Parameters.AddWithValue("@ManageRegOffice", request.ManageRegOffice);
                        command.Parameters.AddWithValue("@RegOfficeStateID", request.RegOfficeStateID);
                        command.Parameters.AddWithValue("@RegOfficeDistrictID", request.RegOfficeDistrictID);
                        command.Parameters.AddWithValue("@RegFileName", request.RegFileName);
                        command.Parameters.AddWithValue("@RegDisFileName", request.RegDisFileName);
                        command.Parameters.AddWithValue("@LastElectionDate", request.LastElectionDate);
                        command.Parameters.AddWithValue("@LastElectionValidUpTo", request.LastElectionValidUpTo);
                        command.Parameters.AddWithValue("@MemberIdProofName", request.MemberIdProofName);
                        command.Parameters.AddWithValue("@MemberIdDisProofName", request.MemberIdDisProofName);
                        command.Parameters.AddWithValue("@OwnerShipID", request.OwnerShipID);
                        command.Parameters.AddWithValue("@IsRented", request.IsRented);
                        command.Parameters.AddWithValue("@AgreementLeaseDate", request.AgreementLeaseDate);
                        command.Parameters.AddWithValue("@ValidUpToLeaseDate", request.ValidUpToLeaseDate);
                        command.Parameters.AddWithValue("@InstituteRegOffice", request.InstituteRegOffice);
                        command.Parameters.AddWithValue("@InstituteStateID", request.InstituteStateID);
                        command.Parameters.AddWithValue("@InstituteDistrictID", request.InstituteDistrictID);
                        command.Parameters.AddWithValue("@RegStateID", request.RegStateID);
                        command.Parameters.AddWithValue("@RegDistrictID", request.RegDistrictID);
                        command.Parameters.AddWithValue("@AgreementFileName", request.AgreementFileName);
                        command.Parameters.AddWithValue("@AgreementDisFileName", request.AgreementDisFileName);
                        command.Parameters.AddWithValue("@IsOwnRented", request.IsOwnRented);
                        command.Parameters.AddWithValue("@PlotHouseBuildingNo", request.PlotHouseBuildingNo);
                        command.Parameters.AddWithValue("@StreetRoadLane", request.StreetRoadLane);
                        command.Parameters.AddWithValue("@AreaLocalitySector", request.AreaLocalitySector);
                        command.Parameters.AddWithValue("@LandMark", request.LandMark);
                        command.Parameters.AddWithValue("@InstituteDivisionID", request.InstituteDivisionID);
                        command.Parameters.AddWithValue("@InstituteSubDivisionID", request.InstituteSubDivisionID);
                        command.Parameters.AddWithValue("@PropDistrictID", request.PropDistrictID);
                        command.Parameters.AddWithValue("@PropTehsilID", request.PropTehsilID);
                        command.Parameters.AddWithValue("@PropUrbanRural", request.PropUrbanRural);
                        command.Parameters.AddWithValue("@AdministrativeBodyId", request.AdministrativeBodyId);
                            command.Parameters.AddWithValue("@VillageID", request.VillageID);
                                command.Parameters.AddWithValue("@GramPanchayatSamiti", request.GramPanchayatSamiti);
                            command.Parameters.AddWithValue("@PanchayatSamiti", request.PanchayatSamiti);
                        command.Parameters.AddWithValue("@CityID", request.CityID);
                        command.Parameters.AddWithValue("@WardNo", request.WardNo);
                        command.Parameters.AddWithValue("@KhasraKhataNo", request.KhasraKhataNo);
                        command.Parameters.AddWithValue("@BighaYard", request.BighaYard);
                        command.Parameters.AddWithValue("@LatLongFileName", request.LatLongFileName);
                        command.Parameters.AddWithValue("@LatLongDisFileName", request.LatLongDisFileName);
                        command.Parameters.AddWithValue("@ContactNo", request.ContactNo);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@AlternateEmail", request.AlternateEmail);
                        command.Parameters.AddWithValue("@Website", request.Website);
                        command.Parameters.AddWithValue("@ConsumerName", request.ConsumerName);
                        command.Parameters.AddWithValue("@ConnectionType", request.ConnectionType);
                        command.Parameters.AddWithValue("@SanctionLoad", request.SanctionLoad);
                        command.Parameters.AddWithValue("@ContractDemand", request.ContractDemand);
                        command.Parameters.AddWithValue("@DISCOM", request.DISCOM);
                        command.Parameters.AddWithValue("@SubDivOffice", request.SubDivOffice);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@Latitude", request.Latitude);
                        command.Parameters.AddWithValue("@Longitude", request.Longitude);
            
                        command.Parameters.AddWithValue("@Bill_DisFilename", request.Bill_DisFilename);
                        command.Parameters.AddWithValue("@Bill_Filename", request.Bill_Filename);
                        command.Parameters.AddWithValue("@KNo", request.KNo);
                        command.Parameters.AddWithValue("@ItiAffiliationList", JsonConvert.SerializeObject(request.ItiAffiliationList));
                        command.Parameters.AddWithValue("@ItiMembers", JsonConvert.SerializeObject(request.ItiMembersModel));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        var returnParam = new SqlParameter("@Return", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(returnParam);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        await command.ExecuteNonQueryAsync();
                        returnValue = (int)returnParam.Value;

                        return returnValue > 0;
                    }
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

        public async Task<bool> SaveItiworkflow(ItiVerificationModel request)
        {
            _actionName = "SaveDataPlanning(ITI_PlanningColleges request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int returnValue = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITIPlanningWorkflow_IU"; // Your stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CollegeId", request.InstituteID);
                        command.Parameters.AddWithValue("@Status", request.Status);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);
                        command.Parameters.AddWithValue("@UserID", request.UserID);
    

                        var returnParam = new SqlParameter("@Return", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(returnParam);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        await command.ExecuteNonQueryAsync();
                        returnValue = (int)returnParam.Value;

                        return returnValue > 0;
                    }
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

        public async Task<bool> SaveDataReport(ItiReportDataModel request)
        {
            _actionName = "SaveData(ItiReportDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int returnValue = 0;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandText = "USP_ITICollegeReport_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CollegeID", request.CollegeID);
                        //command.Parameters.AddWithValue("@CollegeName", request.CollegeName);
                        command.Parameters.AddWithValue("@Loksabha", request.Loksabha);
                        command.Parameters.AddWithValue("@Vidhansabha", request.Vidhansabha);
                        command.Parameters.AddWithValue("@LandAvailable", request.LandAvailable);
                        command.Parameters.AddWithValue("@PanchayatDis", request.PanchayatDis);
                        command.Parameters.AddWithValue("@SanctionOrderNo", request.SanctionOrderNo);
                        command.Parameters.AddWithValue("@SanctionOrderDate", request.SanctionOrderDate);
                        command.Parameters.AddWithValue("@TradeOrderNo", request.TradeOrderNo);
                        command.Parameters.AddWithValue("@AdministrativeOrderNo", request.AdministrativeOrderNo);
                        command.Parameters.AddWithValue("@TradeOrderDate", request.TradeOrderDate);
                        command.Parameters.AddWithValue("@AdministrativeOrderDate", request.AdministrativeOrderDate);
                        command.Parameters.AddWithValue("@ApproachRoad", request.ApproachRoad);
                        command.Parameters.AddWithValue("@InternalRoad", request.InternalRoad);
                        command.Parameters.AddWithValue("@WaterSupply", request.WaterSupply);
                        command.Parameters.AddWithValue("@Harvesting", request.Harvesting);
                        command.Parameters.AddWithValue("@ElectPhase", request.ElectPhase);
                        command.Parameters.AddWithValue("@ElectConnection", request.ElectConnection);
                        command.Parameters.AddWithValue("@IsSolarPanel", request.IsSolarPanel);
                        command.Parameters.AddWithValue("@PanelCapacity", request.PanelCapacity);
                        command.Parameters.AddWithValue("@IsBoundaryWall", request.IsBoundaryWall);
                        command.Parameters.AddWithValue("@BuildShortage", request.BuildShortage);
                        command.Parameters.AddWithValue("@IsHostel", request.IsHostel);
                        command.Parameters.AddWithValue("@HostelUtilized", request.HostelUtilized);
                        command.Parameters.AddWithValue("@NoOfTree", request.NoOfTree);
                        command.Parameters.AddWithValue("@Remarks", request.Remarks);
                        command.Parameters.AddWithValue("@FrontPhoto", request.FrontPhoto);
                        command.Parameters.AddWithValue("@SidePhoto", request.SidePhoto);
                        command.Parameters.AddWithValue("@InteriorPhoto", request.InteriorPhoto);
                        command.Parameters.AddWithValue("@SanctionOrderCopy", request.SanctionOrderCopy);
                        command.Parameters.AddWithValue("@TradeCopy", request.TradeCopy);
                        command.Parameters.AddWithValue("@AdministrativeCopy", request.AdministrativeCopy);
                        command.Parameters.AddWithValue("@ConstructionAgency", request.ConstructionAgency);
                        command.Parameters.AddWithValue("@PDName", request.PDName);
                        command.Parameters.AddWithValue("@ContractorName", request.ContractorName);
                        command.Parameters.AddWithValue("@PDMobile", request.PDMobile);
                        command.Parameters.AddWithValue("@ContractorMobile", request.ContractorMobile);
                        command.Parameters.AddWithValue("@IsDispute", request.IsDispute);
                        command.Parameters.AddWithValue("@FinancialSanction", request.FinancialSanction);
                        command.Parameters.AddWithValue("@FinancialCopy", request.FinancialCopy);
                        command.Parameters.AddWithValue("@PercentCivilWork", request.PercentCivilWork);
                        command.Parameters.AddWithValue("@PercentCivilDate", request.PercentCivilDate);
                        command.Parameters.AddWithValue("@IsPurposeHall", request.IsPurposeHall);
                        command.Parameters.AddWithValue("@IsMainITI", request.IsMainITI);
                        command.Parameters.AddWithValue("@IsBuildingTaken", request.IsBuildingTaken);
                        command.Parameters.AddWithValue("@TakenOverDate", request.TakenOverDate);
                        command.Parameters.AddWithValue("@IsOperatingOwn", request.IsOperatingOwn);
                        command.Parameters.AddWithValue("@ShilanyasDate", request.ShilanyasDate);
                        command.Parameters.AddWithValue("@LokarpanDate", request.LokarpanDate);
                        command.Parameters.AddWithValue("@LokarpanName", request.LokarpanName);
                        command.Parameters.AddWithValue("@LokarpanPost", request.LokarpanPost);
                        command.Parameters.AddWithValue("@AllotmentLetter", request.AllotmentLetter);
                        command.Parameters.AddWithValue("@BuildingPlanCopy", request.BuildingPlanCopy);
                        command.Parameters.AddWithValue("@DomeViewCopy", request.DomeViewCopy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@Esttablishment_Year", request.Esttablishment_Year);
                        command.Parameters.AddWithValue("@ElectPhaserequired", request.ElectPhaserequired);
                        command.Parameters.AddWithValue("@ContractLoad", request.ContractLoad);
                        command.Parameters.AddWithValue("@ShilanyasName", request.ShilanyasName);
                        command.Parameters.AddWithValue("@ShilanyasPost", request.ShilanyasPost);
                        //command.Parameters.AddWithValue("@IsNewCollege", request.IsNewCollege);

                        // Output parameter
                        var returnParam = new SqlParameter("@Return", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(returnParam);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        await command.ExecuteNonQueryAsync();
                        returnValue = (int)returnParam.Value;

                        return returnValue > 1;
                    }
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

        public async Task<ItiReportDataModel> Get_ITIsReportData_ByID(int Id)
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
                        command.CommandText = "USP_ITICollegeReportGetById";
                        //command.Parameters.AddWithValue("@action", "_getAllData");
                        command.Parameters.AddWithValue("@Id", Id);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    var data = new ItiReportDataModel();
                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<ItiReportDataModel>(dataSet.Tables[0]);

                           
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

        public async Task<bool> unlockfee(int id, int ModifyBy, string remark)
        {
            _actionName = "ResetSSOID(ITICollegeMasterModel request)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandText = "USP_unlockfeeTICollegeStatus";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@ModifyBy", ModifyBy);
                    command.Parameters.AddWithValue("@Remark", remark);
                    command.Parameters.AddWithValue("@IPAddress", _IPAddress);
           

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


        public async Task<DataTable> GetPlanningList(int CollegeID,int Status)
        {
            _actionName = "GetPlanningList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITiPlanningList";

                        command.Parameters.AddWithValue("@CollegeID", CollegeID);
                        command.Parameters.AddWithValue("@Status", Status);

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
          

        public async Task<DataTable> ViewWorkflow(int CollegeID)
        {
            _actionName = "GetPlanningList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITiPlanningWorkflow";

                        command.Parameters.AddWithValue("@CollegeID", CollegeID);
   

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

        public async Task<DataTable> ItiSearchCollege(ItiSearchCollegeModel model)
        {
            _actionName = "ItiSearchCollege()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICollegeSearch";
                        command.Parameters.AddWithValue("@action", "_getAllData");
                        command.Parameters.AddWithValue("@DivisionID", model.DivisionID);                       
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);                       
                        command.Parameters.AddWithValue("@SearchText", model.SearchText ?? (object)DBNull.Value);                       
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

        public async Task<DataSet> Get_ITIsPlanningData_ByIDReport(int Id)
        {
            _actionName = "GetById(int PK_ID)";

            return await Task.Run(async () =>
            {
                try
                {
                    var dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICollegePlanningGetByIdReport";
                        command.Parameters.AddWithValue("@action", "_getAllData");
                        command.Parameters.AddWithValue("@Id", Id);
                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataSet = await command.FillAsync();
                    }
                    return dataSet;
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
                //var data = new ITI_PlanningColleges();
                //if (dataSet != null)
                //{
                //    if (dataSet.Tables.Count > 0)
                //    {
                //        data = CommonFuncationHelper.ConvertDataTable<ITI_PlanningColleges>(dataSet.Tables[0]);

                //        if (dataSet.Tables[1].Rows.Count > 0)
                //        {

                //            data.ItiMembersModel = CommonFuncationHelper.ConvertDataTable<List<ItiMembersModel>>(dataSet.Tables[1]);

                //        }
                //        if (dataSet.Tables[2].Rows.Count > 0)
                //        {

                //            data.ItiAffiliationList = CommonFuncationHelper.ConvertDataTable<List<ItiAffiliationList>>(dataSet.Tables[2]);

                //        }

                //    }
                //}

            });




        }


        public async Task<DataTable> AllNCVTInstituteList(ITIsSearchModel model)
        {
            _actionName = "AllNCVTInstituteList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_NCVTInstituteList";

                        command.Parameters.AddWithValue("@ZoneID", model.ZoneID);
                        command.Parameters.AddWithValue("@DistrictID", model.DistrictID);
                        command.Parameters.AddWithValue("@TradeID", model.TradeID);
                        command.Parameters.AddWithValue("@TehsilID", model.TehsilID);
                        command.Parameters.AddWithValue("@FeeStatus", model.FeeStatus);
                        command.Parameters.AddWithValue("@ITItypeID", model.ITItypeID);
                        command.Parameters.AddWithValue("@ExamTypeId", model.ExamTypeId);
                        command.Parameters.AddWithValue("@ExamSystemId", model.ExamSystemId);
                        command.Parameters.AddWithValue("@CourseID", model.CourseID);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", model.CourseTypeID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@Name", model.Name ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ItiCode", model.ItiCode ?? (object)DBNull.Value);
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








