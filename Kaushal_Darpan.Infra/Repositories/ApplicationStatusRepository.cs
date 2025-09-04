using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationStatus;
using Kaushal_Darpan.Models.CampusPostMaster;
using Kaushal_Darpan.Models.CreateTpoMaster;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.studentve;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ApplicationStatusRepository : IApplicationStatusRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ApplicationStatusRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "StudentRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

    
        public async Task<DataTable> GetAllData(StudentSearchModel searchModel)
        {
            _actionName = "GetAllData(StudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GeTApplicationStatus";


                        // Add parameters to the stored procedure from the model

                        command.Parameters.AddWithValue("@ApplicationNo", searchModel.ApplicationNo);
                        command.Parameters.AddWithValue("@MobileNumber", searchModel.MobileNumber);
                        command.Parameters.AddWithValue("@DOB", searchModel.DOB);
                        command.Parameters.AddWithValue("@SSOID", searchModel.SsoID);
                        command.Parameters.AddWithValue("@EndTermID", searchModel.EndTermID);
                        command.Parameters.AddWithValue("@StudentID", searchModel.StudentID);
                        command.Parameters.AddWithValue("@SemesterID", searchModel.SemesterID);
                        command.Parameters.AddWithValue("@DocumentMasterID", searchModel.DocumentMasterID);
                        command.Parameters.AddWithValue("@ChallanNo", searchModel.ChallanNo);
                        command.Parameters.AddWithValue("@DepartmentID", searchModel.DepartmentID);
                        command.Parameters.AddWithValue("@FinancialYearID", searchModel.FinancialYearID);
                        command.Parameters.AddWithValue("@InstituteID", searchModel.InstituteID);
                        command.Parameters.AddWithValue("@RoleID", searchModel.RoleID);
                        command.Parameters.AddWithValue("@ServiceID", searchModel.ServiceID);
                        command.Parameters.AddWithValue("@StudentExamID", searchModel.StudentExamID);
        
                        command.Parameters.AddWithValue("@action", searchModel.Action);
                        
                        
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


        public async Task<DataTable> StudentJailAdmission(StudentSearchModel searchModel)
        {
            _actionName = "StudentJailAdmission(StudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GeTJailApplication";


                        // Add parameters to the stored procedure from the model

                        command.Parameters.AddWithValue("@ApplicationNo", searchModel.ApplicationNo);
                        command.Parameters.AddWithValue("@MobileNumber", searchModel.MobileNumber);
                        command.Parameters.AddWithValue("@DOB", searchModel.DOB);
                        command.Parameters.AddWithValue("@SSOID", searchModel.SsoID);
                        command.Parameters.AddWithValue("@EndTermID", searchModel.EndTermID);
                        command.Parameters.AddWithValue("@StudentID", searchModel.StudentID);
                        command.Parameters.AddWithValue("@SemesterID", searchModel.SemesterID);
                        command.Parameters.AddWithValue("@DocumentMasterID", searchModel.DocumentMasterID);
                        command.Parameters.AddWithValue("@ChallanNo", searchModel.ChallanNo);
                        command.Parameters.AddWithValue("@DepartmentID", searchModel.DepartmentID);
                        command.Parameters.AddWithValue("@FinancialYearID", searchModel.FinancialYearID);
                        command.Parameters.AddWithValue("@InstituteID", searchModel.InstituteID);
                        command.Parameters.AddWithValue("@RoleID", searchModel.RoleID);
                        command.Parameters.AddWithValue("@ServiceID", searchModel.ServiceID);
                        command.Parameters.AddWithValue("@StudentExamID", searchModel.StudentExamID);

                        command.Parameters.AddWithValue("@action", searchModel.Action);


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


        public async Task<List<DocumentDetailsModel>> GetByID(int ApplicationID)
        {
            _actionName = "GetAllData(StudentSearchModel searchModel)";
            return await Task.Run(async () =>
                {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GeTrevertdocument";


                        // Add parameters to the stored procedure from the model

                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
   

                 
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<DocumentDetailsModel>();
                    if (dataTable != null)
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<DocumentDetailsModel>>(dataTable);
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

        public async Task<int> SaveRevertData(List<DocumentDetailsModel> entity)
        {
            _actionName = "SaveRevertData(List<VerificationDocumentDetailList> entity)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Save_RevertData";
                        command.CommandType = CommandType.StoredProcedure;

                         // Add parameters with appropriate null handling
                            
                 
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(entity));
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);// out
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



    }
}








