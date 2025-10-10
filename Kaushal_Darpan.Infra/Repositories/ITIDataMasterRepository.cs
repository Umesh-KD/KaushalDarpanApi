using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.CounsellingMaster;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.MenuMaster;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ITIDataMasterRepository : IITIDataMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITIDataMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "IITIDataMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        //public async Task<TechnicalDataModel> GetAllData(SeatIntakesDataListSearchModel request)
        //{
        //    _actionName = "GetAllData(SeatIntakeSearchModel request)";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            //DataTable dataTable = new DataTable();
        //            DataSet dataset = new DataSet();
        //            using (var command = await _dbContext.CreateCommandAsync())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_ITI_GetDataMaster";
        //                command.Parameters.AddWithValue("@AcademicYearID", request.AcademicYearID);
        //                //command.Parameters.AddWithValue("@RequestType", request.RequestType);
        //                command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
        //                command.Parameters.AddWithValue("@action", request.action);

        //                //command.Parameters.AddWithValue("@action", "_getAllData");

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataset = await command.FillAsync();
        //            }


        //            //TechnicalDataModel obj = new TechnicalDataModel();
        //            //obj.APPLICATIONID= dataSet.Tables[1]['']
        //            //obj.CourseDetails = CommonFuncationHelper.ConvertDataTable<List<CourseDetail>>(dataSet.Tables[1]);
        //            TechnicalDataModel data = new TechnicalDataModel();
        //            var coursedata = new List<CourseDetail>();

        //            if (dataset != null)
        //            {
        //                if (dataset.Tables.Count > 1)
        //                {
        //                    //data.COLLEGECODE = dataset.Tables[0]['collegecode']
        //                    data = CommonFuncationHelper.ConvertDataTable<TechnicalDataModel>(dataset.Tables[0]);
        //                    coursedata = CommonFuncationHelper.ConvertDataTable<List<CourseDetail>>(dataset.Tables[1]);
        //                    data.CourseDetailsList = coursedata;
        //                }
        //                else
        //                {
        //                    data = CommonFuncationHelper.ConvertDataTable<TechnicalDataModel>(dataset.Tables[0]);
        //                }


        //            }
        //            return data;
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



        public async Task<DataTable> GetAllData(DataListSearchModel request)
        {
            _actionName = "GetAllData(SeatIntakeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    //DataSet dataset = new DataSet();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetDataMaster";
                        command.Parameters.AddWithValue("@AcademicYearID", request.AcademicYearID);
                        //command.Parameters.AddWithValue("@RequestType", request.RequestType);
                        command.Parameters.AddWithValue("@CollegeCode", request.CollegeCode);
                        command.Parameters.AddWithValue("@action", request.RequestType);

                        //command.Parameters.AddWithValue("@action", "_getAllData");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }


                    //TechnicalDataModel obj = new TechnicalDataModel();
                    //obj.APPLICATIONID= dataSet.Tables[1]['']
                    //obj.CourseDetails = CommonFuncationHelper.ConvertDataTable<List<CourseDetail>>(dataSet.Tables[1]);
                    // ✅ Build a dynamic object to hold everything
                    //dynamic result = new ExpandoObject();
                    //var resultDict = (IDictionary<string, object>)result;

                    //if (dataTable != null && dataTable.Tables.Count > 0)
                    //if (dataTable != null )
                    //{
                    //    // Convert first table dynamically
                    //    resultDict["MainData"] = ConvertDataTableToDynamicList(dataTable.Tables[0]);

                    //    // If additional tables exist, add them too
                    //    for (int i = 1; i < dataTable.Tables.Count; i++)
                    //    {
                    //        resultDict[$"Table{i}"] = ConvertDataTableToDynamicList(dataTable.Tables[i]);
                    //    }
                    //}

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


        private List<dynamic> ConvertDataTableToDynamicList(DataTable table)
        {
            var list = new List<dynamic>();

            foreach (DataRow row in table.Rows)
            {
                IDictionary<string, object> expando = new ExpandoObject();
                foreach (DataColumn col in table.Columns)
                {
                    expando[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
                }
                list.Add(expando);
            }

            return list;
        }


    }
}
