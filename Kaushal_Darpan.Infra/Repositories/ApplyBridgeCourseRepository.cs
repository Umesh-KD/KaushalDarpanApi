using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.BridgeCourse;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.PrometedStudentMaster;
using Kaushal_Darpan.Models.StaffMaster;
using Kaushal_Darpan.Models.StudentMaster;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ApplyBridgeCourseRepository : IApplyBridgeCourseRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ApplyBridgeCourseRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "PromotedStudentRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<List<BridgeCourseStudentMasterModel>> GetAllStudent(BridgeCourseStudentSearchModel model)
        {
            _actionName = "GetAllStudent(BridgeCourseStudentSearchModel model)";
            try
            {
                return await Task.Run(async () =>
                {
                    List<BridgeCourseStudentMasterModel> data = new List<BridgeCourseStudentMasterModel>();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetBridgeCourseStudentData";
                        //parameter
                        command.Parameters.AddWithValue("@action", "_getStudentData");
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", model.StreamID);
                        command.Parameters.AddWithValue("@StudentTypeID", model.StudentTypeID);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        var dt = await command.FillAsync_DataTable();
                        if (dt != null)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<List<BridgeCourseStudentMasterModel>>(dt);
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

        public async Task<int> SaveStudent(List<BridgeCourseStudentMarkedModel> model)
        {
            _actionName = "SaveStudent(List<BridgeCourseStudentMarkedModel> model)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    int retval = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveSelectedForBridgeCourse";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@action", "_SaveSelectedForBridgeCourse");
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(model));

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        retval = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
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

    }
}


