using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.ITICollegeMarksheetDownloadmodel;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Report;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITICollegeMarksheetDownloadRepository : I_ITICollegeMarksheetDownloadRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITICollegeMarksheetDownloadRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITICollegeMarksheetDownloadRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        #region college wise Student Marksheet
        //public async Task<DataSet> GetITICollegeStudent_Marksheet(ITICollegeStudentMarksheetSearchModel model)
        //{
        //    _actionName = "GetITICollegeStudent_Marksheet(ITICollegeStudentMarksheetSearchModel model)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            var ds = new DataSet();
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_Rpt_GetStudentMarksheet";
        //                command.Parameters.AddWithValue("@SemesterID", model.InstituteID);
        //                command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                ds = await command.FillAsync();
        //            }
        //            return ds;
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


        public async Task<DataTable> GetRollNumberOfStudentOfCollege(ITICollegeStudentMarksheetSearchModel model)
        {
            _actionName = "GetRollNumberOfStudentOfCollege(ITICollegeStudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIMarksheetDownload";
                        command.Parameters.AddWithValue("@Action", "GetRollNumbers");
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
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

        public async Task<DataSet> GetITICollegeStudent_Marksheet(ITICollegeStudentMarksheetSearchModel model)
        {
            _actionName = "GetITICollegeStudent_Marksheet(ITICollegeStudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIMarksheetDownload";
                        command.Parameters.AddWithValue("@Action", "DownloadITI_Marksheet");
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DistrictId", model.DistrictID);
                        command.Parameters.AddWithValue("@Code", model.CollegeCode);
                        command.Parameters.AddWithValue("@RollNo", model.RollNumber);
                        command.Parameters.AddWithValue("@ExamYearID", model.ExamYearID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
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


        public async Task<DataSet> ITIStateTradeCertificateReport(ITICollegeStudentMarksheetSearchModel model)
        {
            _actionName = "ITIStateTradeCertificateReport(ITICollegeStudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIMarksheetDownload";
                        command.Parameters.AddWithValue("@Action", "STATE_TRADE_CERTIFICATE");
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@RollNo", model.RollNumber);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
                        //command.Parameters.AddWithValue("@EnrollmentNo", model.EnrollmentNo);
                        //command.Parameters.AddWithValue("@ExamYearID", model.ExamYearID);
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

        public async Task<DataTable> GetITICollegeList(ITICollegeStudentMarksheetSearchModel model)
        {
            _actionName = "GetITICollegeList(ITICollegeStudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIMarksheetDownload";
                        command.Parameters.AddWithValue("@Action", "getCollegeListData");
                        command.Parameters.AddWithValue("@DistrictId", model.DistrictID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@InstituteId", model.InstituteID);
                        command.Parameters.AddWithValue("@Code", model.CollegeCode);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
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

        public async Task<DataSet> GetITIConsolidateCertificate(ITICollegeStudentMarksheetSearchModel model)
        {
            _actionName = "GetITICollegeStudent_Marksheet(ITICollegeStudentMarksheetSearchModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    var ds = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITIMarksheetDownload";
                        command.Parameters.AddWithValue("@Action", "DownloadConsolidate");
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DistrictId", model.DistrictID);
                        command.Parameters.AddWithValue("@Code", model.CollegeCode);
                        command.Parameters.AddWithValue("@RollNo", model.RollNumber);
                        command.Parameters.AddWithValue("@ExamYearID", model.ExamYearID);
                        command.Parameters.AddWithValue("@TradeScheme", model.TradeScheme);
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



        #endregion
    }
}
