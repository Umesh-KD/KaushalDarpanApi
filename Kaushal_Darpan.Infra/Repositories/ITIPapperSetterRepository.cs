using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.DateConfiguration;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.ItiExaminer;
using Kaushal_Darpan.Models.ITIPapperSetter;
using Kaushal_Darpan.Models.ITIPlanning;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIPapperSetterRepository : IITIPapperSetterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIPapperSetterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITIPapperSetterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> SaveData(ITIPapperSetterModel request)
        {
            _actionName = "SaveData(ITIPapperSetterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0; DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITIPaperSetterAssign";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", request.PKID==0? "InsertPaperSetterAssignData" : "Update");
                        command.Parameters.AddWithValue("@yearTrade", request.yearTrade);
                        command.Parameters.AddWithValue("@TwoYearTradeID", request.yearTrade == 2 ? request.TwoYearTradeID : 1);
                        command.Parameters.AddWithValue("@TradeSchemeId", request.TradeSchemeId);
                        command.Parameters.AddWithValue("@ExamType", request.ExamType);
                        command.Parameters.AddWithValue("@TradeID", request.TradeID);
                        command.Parameters.AddWithValue("@SubjectId", request.SubjectId);
                        command.Parameters.AddWithValue("@PapperSubmitionLastDate", Convert.ToDateTime(request.PapperSubmitionLastDate));
                        command.Parameters.AddWithValue("@PaperCodeName", request.PaperCodeName);
                        command.Parameters.AddWithValue("@NumberofQuestion", request.NumberofQuestion);
                        command.Parameters.AddWithValue("@UploadGuidelineFile", request.GuideLinesDocumentFile);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@AddProfessorList", ToDataTable(request.PapperSetterListModel));

                        command.Parameters.AddWithValue("@Createdby", request.Createdby);
                        command.Parameters.AddWithValue("@Roleid", request.Roleid);
                        command.Parameters.AddWithValue("@FYID", request.FYID);
                        command.Parameters.AddWithValue("@EndtermId", request.Endtermid);
                        command.Parameters.AddWithValue("@PKID", request.PKID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                        // Execute the command
                        //result = await command.ExecuteNonQueryAsync();
                        //result = Convert.ToInt32(command.Parameters["@Return"].Value);// out
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

        public async Task<DataTable> GetSubjectList(int TradeID = 0, int ExamType = 0)
        {
            _actionName = "GetSubjectList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIPaperSetterAssign";
                        command.Parameters.AddWithValue("@Action", "GetSubjectListByTradeId");
                        command.Parameters.AddWithValue("@TradeID", TradeID);
                        command.Parameters.AddWithValue("@ExamType", ExamType);

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

        public async Task<DataTable> GetProfessorList(int SubjectId = 0)
        {
            _actionName = "GetSubjectList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIPaperSetterAssign";
                        command.Parameters.AddWithValue("@Action", "GetProfessorListBySubjectId");
                        command.Parameters.AddWithValue("@TradeID", SubjectId);

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

        public async Task<List<PaperSetterAssginListModel>> GetAllPaperSeterAssignList(ITIPapperSetterModel modal)
        {
            _actionName = "GetAllPaperSeterAssignList()";

            try
            {
                return await Task.Run(async () =>
                {
                    DataSet dataSet = new DataSet();

                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_ITIPaperSetterModule";
                        command.Parameters.AddWithValue("@Action", modal.ActionName);
                        command.Parameters.AddWithValue("@yearTrade", modal.yearTrade);
                        command.Parameters.AddWithValue("@TradeID", modal.TradeID);
                        command.Parameters.AddWithValue("@SubjectId", modal.SubjectId);
                        command.Parameters.AddWithValue("@Createdby", modal.Createdby);
                        command.Parameters.AddWithValue("@Roleid", modal.Roleid);
                        command.Parameters.AddWithValue("@ExamType", modal.ExamType);
                        command.Parameters.AddWithValue("@ID", modal.PKID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    List<PaperSetterAssginListModel> finalList = new List<PaperSetterAssginListModel>();

                    if (dataSet != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            DataTable table = dataSet.Tables[0];
                            foreach (DataRow row in table.Rows)
                            {
                                var data = new PaperSetterAssginListModel();

                                data.id = (int)row["id"];
                                data.yearTrade = row["yearTrade"]?.ToString();
                                data.Tradename = row["Tradename"]?.ToString();
                                data.SubjectName = row["SubjectName"]?.ToString();
                                data.papperSubmitionLastDate = row["papperSubmitionLastDate"]?.ToString();
                                data.PapperCode_Name = row["PapperCode_Name"]?.ToString();
                                data.Remark = row["Remark"]?.ToString();
                                data.UploadGuidelinePath = row["UploadGuidelinePath"]?.ToString();
                                data.IsAutoSelectComplete = Convert.ToInt32(row["IsAutoSelectComplete"]);
                                data.FinalAutoSelectedPaperFile = row["FinalAutoSelectedPaperFile"]?.ToString();
                                string json = row["AssignprofessorListModel"]?.ToString();
                                if (!string.IsNullOrWhiteSpace(json))
                                {
                                    data.AssignprofessorListModel = JsonConvert.DeserializeObject<List<AssignprofessorList>>(json);
                                }

                                finalList.Add(data);
                            }
                              
                        }
                    }
                    return finalList;
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


        public async Task<List<ITIPapperSetterModel>> PaperSetterAssignListByID(int ID = 0)
        {
            _actionName = "PaperSetterAssignListByID(ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_ITIPaperSetterModule";
                        command.Parameters.AddWithValue("@Action", "GetPaperSetterDetailByID");
                        command.Parameters.AddWithValue("@ID", ID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    List<ITIPapperSetterModel> finalList = new List<ITIPapperSetterModel>();

                    if (dataTable != null)
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            var data = new ITIPapperSetterModel();

                            data.PKID = Convert.ToInt32(dataTable.Rows[0]["id"]);
                            data.yearTrade = Convert.ToInt32(dataTable.Rows[0]["yearTrade"]);
                            data.TwoYearTradeID = Convert.ToInt32(dataTable.Rows[0]["TwoYearTradeYears"]);
                            data.TradeID = Convert.ToInt32(dataTable.Rows[0]["TradeId"]);
                            data.SubjectId = Convert.ToInt32(dataTable.Rows[0]["subjectID"]?.ToString());
                            data.TradeSchemeId = Convert.ToInt32(dataTable.Rows[0]["TradeSchemeId"]);
                            data.ExamType = Convert.ToInt32(dataTable.Rows[0]["ExamType"]);
                            data.ExamType = Convert.ToInt32(dataTable.Rows[0]["ExamType"]);
                            data.PapperSubmitionLastDate =  dataTable.Rows[0]["papperSubmitionLastDate"]?.ToString();
                            data.PaperCodeName = dataTable.Rows[0]["PapperCode_Name"]?.ToString();
                            data.NumberofQuestion = dataTable.Rows[0]["NumberOfQuestion"]?.ToString();
                            data.Remark = dataTable.Rows[0]["Remark"]?.ToString();
                            data.GuideLinesDocumentFile = dataTable.Rows[0]["UploadGuidelinePath"]?.ToString();


                            string json = dataTable.Rows[0]["AssignprofessorListModel"]?.ToString();
                            if (!string.IsNullOrWhiteSpace(json))
                            {
                                data.PapperSetterListModel = JsonConvert.DeserializeObject<List<AddprofessorList>>(json);
                            }

                            finalList.Add(data);
                        }
                    }
                    return finalList;
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

        public async Task<DataTable> PaperSetterAssignListRemoveByID(int ID = 0 , int Deletedby =0 , int roleid = 0)
        {
            _actionName = "PaperSetterAssignListRemoveByID(int ID = 0 , int Deletedby =0)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_ITIPaperSetterModule";
                        command.Parameters.AddWithValue("@Action", "DeleteAssignListByID");
                        command.Parameters.AddWithValue("@Id", ID);
                        command.Parameters.AddWithValue("@Createdby", Deletedby);
                        command.Parameters.AddWithValue("@RoleId", roleid);
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


        public async Task<DataTable> GetTradeListByYearTradeID(int YearTradeID = 0 , int CourseTypeID = 0)
        {
            _actionName = "GetTradeListByYearTradeID(YearTradeID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIPaperSetterAssign";
                        command.Parameters.AddWithValue("@Action", "TradeListByYearTradeID");
                        command.Parameters.AddWithValue("@yearTrade", YearTradeID);
                        command.Parameters.AddWithValue("@TradeSchemeId", CourseTypeID);

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

        public static DataTable ToDataTable<T>(List<T> items)
        {
            try
            {
                DataTable dataTable = new DataTable(typeof(T).Name);

                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Defining type of data column gives proper data table 
                    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                    //Setting column names as Property names

                    dataTable.Columns.Add(prop.Name, type);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DataTable> GetListForPaperUploadByProfessorID(int ProfessorID = 0, string SSOID = "" , int Roleid = 0 , int TypeID = 0)
        {
            _actionName = "GetAllData(int ProfessorID = 0, string SSOID )";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_ITIPaperSetterModule";
                        command.Parameters.AddWithValue("@Action", "GetListForPaperUpload");
                        command.Parameters.AddWithValue("@ProfessorID", ProfessorID);
                        command.Parameters.AddWithValue("@SSOID", SSOID);
                        command.Parameters.AddWithValue("@Roleid", Roleid);
                        command.Parameters.AddWithValue("@TypeID", TypeID);
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

        public async Task<DataTable> UpdateUploadedPaperData(string  UploadedPaperDocument = "", string Remark = "" , int userid = 0 , int PKID = 0 , int roleid = 0)
        {
            _actionName = "UpdateUploadedPaperData(int ProfessorID = 0, string SSOID )";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_ITIPaperSetterModule";
                        command.Parameters.AddWithValue("@Action", "UpdateUploadedPaperDataByProfesor");
                        command.Parameters.AddWithValue("@UploadedPaperDocument", UploadedPaperDocument);
                        command.Parameters.AddWithValue("@Remark", Remark);
                        command.Parameters.AddWithValue("@ProfessorID", userid);
                        command.Parameters.AddWithValue("@Id", PKID);
                        command.Parameters.AddWithValue("@Roleid", roleid);
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

        public async Task<DataTable> AutoSelectPaperDetailsUpdate(int SelectedProfessorID = 0, int PKID = 0, int userid = 0, int roleid = 0 , string ssoid = "")
        {
            _actionName = "GetSubjectList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_ITIPaperSetterModule";
                        command.Parameters.AddWithValue("@Action", "AutoSelectPaperDetailUpdate");
                        command.Parameters.AddWithValue("@ProfessorID", SelectedProfessorID);
                        command.Parameters.AddWithValue("@Id", PKID);
                        command.Parameters.AddWithValue("@Createdby", userid);
                        command.Parameters.AddWithValue("@Roleid", roleid);
                        command.Parameters.AddWithValue("@SSOID", ssoid);

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

        public async Task<DataTable> PaperSetterProfessorDashboardCount(int userid = 0, int EndTermID = 0, int RoleID = 0,  string ssoid = "" , string para1="")
        {
            _actionName = "PaperSetterProfessorDashboardCount(int userid = 0, int EndTermID = 0, int RoleID = 0,  string ssoid , string para1)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIPaperSetterDashboard";
                        command.Parameters.AddWithValue("@action", "ProfessorDashboardCounts");
                        command.Parameters.AddWithValue("@UserID", userid);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);
                        command.Parameters.AddWithValue("@RoleId", RoleID);
                        command.Parameters.AddWithValue("@Ssoid", ssoid);
                        command.Parameters.AddWithValue("@DepartmentID", 0);
                        command.Parameters.AddWithValue("@para1", para1);

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

        public async Task<DataTable> PaperRevertByExaminer(int ProfessorID = 0, int PKID = 0, int userid = 0, int roleid = 0, string ssoid = "" , string RevertReason ="")
        {
            _actionName = "PaperRevertByExaminer(int ProfessorID = 0, int PKID = 0, int userid = 0, int roleid = 0, string ssoid)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_ITIPaperSetterModule";
                        command.Parameters.AddWithValue("@action", "RevertPaperByExaminer");
                        command.Parameters.AddWithValue("@ProfessorID", ProfessorID);
                        command.Parameters.AddWithValue("@Id", PKID);
                        command.Parameters.AddWithValue("@Roleid", roleid);
                        command.Parameters.AddWithValue("@SSOID", ssoid);
                        command.Parameters.AddWithValue("@Createdby", userid);
                        command.Parameters.AddWithValue("@RevertReason", RevertReason);

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
