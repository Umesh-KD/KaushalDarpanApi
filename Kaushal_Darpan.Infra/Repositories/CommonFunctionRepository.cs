
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models;
using Kaushal_Darpan.Models.Allotment;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.ApplicationStatus;
using Kaushal_Darpan.Models.CenterObserver;
using Kaushal_Darpan.Models.CenterSuperitendent;
using Kaushal_Darpan.Models.CollegeMaster;
using Kaushal_Darpan.Models.CommonFunction;
using Kaushal_Darpan.Models.DocumentDetails;
using Kaushal_Darpan.Models.DTE_Verifier;
using Kaushal_Darpan.Models.EgrassPayment;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.Results;
using Kaushal_Darpan.Models.RPPPayment;
using Kaushal_Darpan.Models.SSOUserDetails;
using Kaushal_Darpan.Models.Student;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.StudentMeritIInfoModel;
using Kaushal_Darpan.Models.SubjectMaster;
using Kaushal_Darpan.Models.Test;
using Kaushal_Darpan.Models.UploadFileWithPathData;
using Kaushal_Darpan.Models.UserMaster;
using Kaushal_Darpan.Models.ViewStudentDetailsModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using static Kaushal_Darpan.Core.Helper.CommonFuncationHelper;
using static Kaushal_Darpan.Models.CommonFunction.ItiTradeAndCollegesDDL;
using static System.Collections.Specialized.BitVector32;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CommonFunctionRepository : ICommonFunctionRepository

    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CommonFunctionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CommonFunctionRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<List<CommonDDLModel>> GetLevelMaster()
        {
            _actionName = "GetLevelMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetLevelMaster";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    // class
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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
        public async Task<List<CommonDDLModel>> PWDCategory()
        {
            _actionName = "PWDCategory()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_PWDCategory";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    // class
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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
        public async Task<List<CommonDDLModel>> GetDesignationMaster()
        {
            _actionName = "GetDesignationMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetDesignationMaster";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    //class
                    List<CommonDDLModel> dataModels = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        dataModels = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return dataModels;
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
        public async Task<List<CommonDDLModel>> GetDistrictMaster()
        {
            _actionName = "GetDistrictMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_DistricMaster";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    List<CommonDDLModel> dataModels = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        dataModels = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return dataModels;
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

        public async Task<List<CommonDDLModel>> GetParliamentMaster()
        {
            _actionName = "GetParliamentMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ParliamentMaster";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    List<CommonDDLModel> dataModels = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        dataModels = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return dataModels;
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

        public async Task<List<CommonDDLModel>> GetDivisionMaster()
        {
            _actionName = "GetDesignationMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_DivisionMaster";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    //class
                    List<CommonDDLModel> dataModels = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        dataModels = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return dataModels;
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
        public async Task<List<CommonDDLModel>> GetTehsilMaster()
        {
            _actionName = "GetTehsilMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_TehsilMaster";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    List<CommonDDLModel> dataModels = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        dataModels = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return dataModels;
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
        public async Task<DataTable> ManagementType(int DepartmentID)
        {
            _actionName = "ManagementType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ManagementType";
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
        public async Task<DataTable> InstitutionCategory()
        {
            _actionName = "InstitutionCategory()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InstitutionCategory";

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
        public async Task<DataTable> StreamType()
        {
            _actionName = "StreamType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StreamType";

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

        public async Task<DataTable> InstituteMaster(int DepartmentID, int Eng_NonEng, int EndTermId)

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
                        command.CommandText = "USP_InstituteMaster";
                        if (DepartmentID == 1)
                        {
                            command.Parameters.AddWithValue("@action", "BTERInstitute");
                        }
                        else if (DepartmentID == 2)
                        {
                            command.Parameters.AddWithValue("@action", "ITIInstitute");
                        }

                        command.Parameters.AddWithValue("@Eng_NonEng", Eng_NonEng);
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



        public async Task<DataTable> Iticollege(int DepartmentID, int Eng_NonEng, int EndTermId , int InsutiteId)

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
                        command.CommandText = "USP_ItiCollegeDDl";




                        command.Parameters.AddWithValue("@Eng_NonEng", Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@InsutiteId", InsutiteId);

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

        public async Task<DataTable> IticenterColleges(int DepartmentID, int Eng_NonEng, int EndTermId, int InstituteID)

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
                        command.CommandText = "USP_ItiCenterwisecollege";




                        command.Parameters.AddWithValue("@Eng_NonEng", Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);

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








        // Comment 02-01-2025 by Pawan using for ITI College together by another tables
        //public async Task<DataTable> InstituteMaster(int DepartmentID)
        //{
        //    _actionName = "InstituteMaster()";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_InstituteMaster";
        //                command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataTable = await command.FillAsync_DataTable();
        //            }
        //            return dataTable;
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
        public async Task<DataTable> StreamMaster(int DepartmentID = 0, int StreamType = 0, int EndTermId = 0)
        {
            _actionName = "StreamMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_StreamMaster";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);
                        command.Parameters.AddWithValue("@StreamType", StreamType);
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

        public async Task<DataTable> ItiTrade(int DepartmentID = 0, int StreamType = 0, int EndTermId = 0, int InstituteID = 0, int DivisionId = 0)
        {
            _actionName = "StreamMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiCollegeWiseTrade";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);
                        command.Parameters.AddWithValue("@StreamType", StreamType);
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);
                        command.Parameters.AddWithValue("@DivisionId", DivisionId);
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



        public async Task<DataTable> StreamMasterwithcount(int DepartmentID = 0, int StreamType = 0, int EndTermId = 0)
        {
            _actionName = "StreamMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_StreamMasterWithCount";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);
                        command.Parameters.AddWithValue("@StreamType", StreamType);
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
        public async Task<DataTable> StreamMasterByCampus(int CampusPostID, int DepartmentID, int EndTermId = 0)
        {
            _actionName = "StreamMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_StreamMasterByCampus";
                        command.Parameters.AddWithValue("@CampusPostID", CampusPostID);
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
        public async Task<DataTable> SemesterMaster(int ShowAllSemester = 0)
        {
            _actionName = "SemesterMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SemesterMaster";
                        command.Parameters.AddWithValue("@ShowAllSemester", ShowAllSemester);
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

        public async Task<DataTable> SemesterGenerateMaster()
        {
            _actionName = "SemesterGenerateMaster";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SemesterGenerateMaster";

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

        public async Task<DataTable> CenterMaster()
        {
            _actionName = "CenterMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CenterMaster";

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


        public async Task<List<CommonDDLModel>> StudentType()
        {
            _actionName = "StudentType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_StudentType";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    //class
                    List<CommonDDLModel> dataModels = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        dataModels = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return dataModels;
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

        //public async Task<List<CommonDDLModel>> StudentStatus()
        //{
        //    _actionName = "StudentStatus()";
        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            DataTable dataTable = new DataTable();
        //            using (var command = _dbContext.CreateCommand())
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = "USP_DDL_StudentStatus";

        //                _sqlQuery = command.GetSqlExecutableQuery();
        //                dataTable = await command.FillAsync_DataTable();
        //            }
        //            //class
        //            List<CommonDDLModel> dataModels = new List<CommonDDLModel>();
        //            if (dataTable != null)
        //            {
        //                dataModels = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
        //            }
        //            return dataModels;
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
        public async Task<DataTable> ExamCategory()
        {
            _actionName = "ExamCategory()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ExamCategory";

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
        public async Task<DataTable> CasteCategoryA()
        {
            _actionName = "CasteCategoryA()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CasteCategoryA";

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
        public async Task<DataTable> CasteCategoryB()
        {
            _actionName = "CasteCategoryB()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CasteCategoryB";

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
        public async Task<DataTable> Board_UniversityMaster()
        {
            _actionName = "Board_UniversityMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_Board_UniversityMaster";

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
        public async Task<ViewStudentDetailsModel> ViewStudentDetails(ViewStudentDetailsRequestModel model)
        {
            _actionName = "ViewStudentDetails(string StudentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ViewStudentDetails";

                        if (model.StudentFilterStatusId == (int)EnumExamStudentStatus.Addimited || model.StudentFilterStatusId == (int)EnumExamStudentStatus.New_Addimited)
                        {
                            command.Parameters.AddWithValue("@action", "getStudentApplicationData");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@action", "getStudentMasterData");
                        }
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@StudentFilterStatusId", model.StudentFilterStatusId);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    ViewStudentDetailsModel viewStudentDetailsModel = new ViewStudentDetailsModel();
                    viewStudentDetailsModel.ViewStudentDetails = dataSet.Tables[0];
                    viewStudentDetailsModel.Student_QualificationDetails = dataSet.Tables[1];
                    viewStudentDetailsModel.documentDetails = CommonFuncationHelper.ConvertDataTable<List<DocumentDetailsModel>>(dataSet.Tables[2]);
                    return viewStudentDetailsModel;
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
        public async Task<DataTable> ViewStudentAdmittedDetails(ViewStudentDetailsRequestModel model)
        {
            _actionName = "ViewStudentDetails(string StudentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataSet = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ViewAdmittedStudentDetails";
                        command.Parameters.AddWithValue("@action", "getStudentAdmitted");
                        command.Parameters.AddWithValue("@ApplicationID", model.ApplicationID);
                        command.Parameters.AddWithValue("@StudentFilterStatusId", model.StudentFilterStatusId);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync_DataTable();
                    }
                    //var ViewStudentDetailsModel = new ViewStudentDetailsModel();
                    //ViewStudentDetailsModel = CommonFuncationHelper.ConvertDataTable<List<ViewStudentDetailsModel>>(dataSet.Tables[0]);
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
            });
        }

        public async Task<ViewStudentDetailsModel> ITIViewStudentDetails(ViewStudentDetailsRequestModel model)
        {
            _actionName = "ViewStudentDetails(string StudentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_ViewStudentDetails";

                        if (model.StudentFilterStatusId == (int)EnumExamStudentStatus.Addimited || model.StudentFilterStatusId == (int)EnumExamStudentStatus.New_Addimited)
                        {
                            command.Parameters.AddWithValue("@action", "getStudentApplicationData");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@action", "getStudentMasterData");
                        }
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@StudentFilterStatusId", model.StudentFilterStatusId);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@StudentExamID", model.StudentExamID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }
                    ViewStudentDetailsModel viewStudentDetailsModel = new ViewStudentDetailsModel();
                    viewStudentDetailsModel.ViewStudentDetails = dataSet.Tables[0];
                    viewStudentDetailsModel.Student_QualificationDetails = dataSet.Tables[1];
                    viewStudentDetailsModel.documentDetails = CommonFuncationHelper.ConvertDataTable<List<DocumentDetailsModel>>(dataSet.Tables[2]);
                    return viewStudentDetailsModel;
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

        public async Task<DataTable> GetExamShift(int DepartmentID)
        {
            _actionName = "GetExamShift()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExamShift";
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
        public async Task<DataTable> GetPaperList()
        {
            _actionName = "GetPaperList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PaperMasterList";

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
        public async Task<DataTable> GetSubjectList(int DepartmentID)
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
                        command.CommandText = "USP_GetSubjectList";
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
        public async Task<DataTable> GetFinancialYear()
        {
            _actionName = "GetFinancialYear()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_FinancialYear";

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
        public async Task<DataTable> GetCommonMasterData(string MasterCode = "", int DepartmentID = 0, int CourseTypeID = 0)
        {
            _actionName = "GetFinancialYear()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CommonMasterData";
                        //command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@MasterCode", MasterCode);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);

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
        public async Task<DataTable> GetExamType()
        {
            _actionName = "GetExamType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExamType";

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
        public async Task<DataTable> GetMonths()
        {
            _actionName = "GetMonths()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_MonthList";

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
        public async Task<DataTable> GetPaperType()
        {
            _actionName = "GetPaperType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPaperType";

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
        public async Task<DataTable> PassingYear()
        {
            _actionName = "PassingYear()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_PassingYear";

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

        public async Task<DataTable> AdmissionPassingYear()
        {
            _actionName = "USP_DDL_AddmissionPassingYear()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_AddmissionPassingYear";

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

        public async Task<StudentMasterModel> PreExam_StudentMaster(int StudentID, int statusId, int DepartmentID, int Eng_NonEng, int status, int EndTermID, int StudentExamID)
        {
            _actionName = "PreExam_StudentMaster(string StudentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    StudentMasterModel studentMaster = new StudentMasterModel();
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Enrollment_StudentMaster";

                        // Determine the action based on statusId
                        if (statusId == (int)EnumExamStudentStatus.Addimited || statusId == (int)EnumExamStudentStatus.New_Addimited)
                        {
                            command.Parameters.AddWithValue("@action", "getStudentApplicationData");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@action", "getStudentMasterData");
                        }

                        command.Parameters.AddWithValue("@StudentID", StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", Eng_NonEng);
                        command.Parameters.AddWithValue("@StudentExamID", StudentExamID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }

                    if (dataSet.Tables.Count >= 3)
                    {
                        // First table contains student data
                        studentMaster = CommonFuncationHelper.ConvertDataTable<StudentMasterModel>(dataSet.Tables[0]);

                        // Second table contains qualification details
                        studentMaster.QualificationDetails = CommonFuncationHelper.ConvertDataTable<List<StudentMaster_QualificationDetails>>(dataSet.Tables[1]);




                        // If there are more than 2 tables, the third table contains subjects (papers)
                        //if (dataSet.Tables.Count > 2)
                        //{
                        //    StringBuilder sb = new StringBuilder();
                        //    for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
                        //    {
                        //        sb.AppendJoin(',', Convert.ToString(dataSet.Tables[2].Rows[i]["SubjectName"]));
                        //    }
                        //    studentMaster.Papers = sb.ToString();
                        //}
                        //else
                        //{
                        //    // If no third table, set Papers to an empty string or handle as needed
                        //    studentMaster.Papers = string.Empty;
                        //}

                        studentMaster.DocumentDetails = CommonFuncationHelper.ConvertDataTable<List<DocumentDetailsModel>>(dataSet.Tables[3]);



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

        public async Task<StudentMasterModel> ITIPreExam_StudentMaster(int StudentID, int statusId, int DepartmentID, int Eng_NonEng, int status, int EndTermID, int StudentExamID)
        {
            _actionName = "PreExam_StudentMaster(string StudentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    StudentMasterModel studentMaster = new StudentMasterModel();
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_PreExam_StudentMaster";

                        // Determine the action based on statusId
                        if (statusId == (int)EnumExamStudentStatus.Addimited || statusId == (int)EnumExamStudentStatus.New_Addimited)
                        {
                            command.Parameters.AddWithValue("@action", "getStudentApplicationData");
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@action", "getStudentMasterData");
                        }

                        command.Parameters.AddWithValue("@StudentID", StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);
                        command.Parameters.AddWithValue("@StudentExamID", StudentExamID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataSet = await command.FillAsync();
                    }

                    if (dataSet.Tables.Count >= 3)
                    {
                        // First table contains student data
                        studentMaster = CommonFuncationHelper.ConvertDataTable<StudentMasterModel>(dataSet.Tables[0]);

                        // Second table contains qualification details
                        studentMaster.QualificationDetails = CommonFuncationHelper.ConvertDataTable<List<StudentMaster_QualificationDetails>>(dataSet.Tables[1]);




                        // If there are more than 2 tables, the third table contains subjects (papers)
                        //if (dataSet.Tables.Count > 2)
                        //{
                        //    StringBuilder sb = new StringBuilder();
                        //    for (int i = 0; i < dataSet.Tables[2].Rows.Count; i++)
                        //    {
                        //        sb.AppendJoin(',', Convert.ToString(dataSet.Tables[2].Rows[i]["SubjectName"]));
                        //    }
                        //    studentMaster.Papers = sb.ToString();
                        //}
                        //else
                        //{
                        //    // If no third table, set Papers to an empty string or handle as needed
                        //    studentMaster.Papers = string.Empty;
                        //}

                        studentMaster.DocumentDetails = CommonFuncationHelper.ConvertDataTable<List<DocumentDetailsModel>>(dataSet.Tables[3]);



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

        public async Task<DataTable> UploadFilePath()
        {
            _actionName = "UploadFilePath()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = " select * from V#ImagePath ";

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
        public async Task<List<CommonDDLModel>> GetSubjectMasterDDL(int DepartmentID)
        {
            _actionName = "GetSubjectMasterDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SubjectMaster";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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
        public async Task<List<CommonDDLModel>> GetCommonMasterDDLByType(string type)
        {
            _actionName = "GetCommonMasterDDLByType(string type)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CommonMaster";

                        command.Parameters.AddWithValue("@action", "_getCMDDLByType");
                        command.Parameters.AddWithValue("@type", type);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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
        public async Task<List<CommonDDLModel>> GetCampusPostMasterDDL(int DepartmentID)
        {
            _actionName = "GetCampusPostMasterDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CampusPostMaster";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        command.Parameters.AddWithValue("@action", "_getCampusPostMasterDDL");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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


        public async Task<List<CommonDDLModel>> GetCategoryDMasterDDL(int MeritalStatus)
        {
            _actionName = "GetCategoryDMasterDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "GetCategoryDMasterDDL";
                        command.Parameters.AddWithValue("@MeritalStatus", MeritalStatus);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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


        public async Task<List<CommonDDLModel>> GetCampusWiseHiringRoleDDL(int campusPostId, int DepartmentID)
        {
            _actionName = "GetCampusWiseHiringRoleDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_GetCampusWiseHiringRole";

                        command.Parameters.AddWithValue("@PostID", campusPostId);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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
        public async Task<List<CommonDDLModel>> PlacementCompanyMaster(int DepartmentID)
        {
            _actionName = "PlacementCompanyMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_PlacementCompanyMaster";
                        command.Parameters.AddWithValue("@DepartmentID", 1);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 0)
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

        public async Task<List<CommonDDLModel>> PrefentialCategoryMaster(int DepartmentID, int CourseTypeId, int PrefentialCategoryType)
        {
            _actionName = "PrefentialCategoryMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_PrefentialCategory";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeId", CourseTypeId);
                        command.Parameters.AddWithValue("@PrefentialCategoryType", PrefentialCategoryType);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 0)
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

        public async Task<DataTable> PlacementCompanyMaster_IDWise(int ID, int DepartmentID)
        {
            _actionName = "PlacementCompanyMaster_IDWise(int ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PlacementCompanyMaster_IDWise";
                        command.Parameters.AddWithValue("@ID", ID);
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
        public async Task<DataTable> CollegeType()
        {
            _actionName = "StudentType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CollegeType";

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
        public async Task<List<CommonDDLModel>> GetStateMaster()
        {
            _actionName = "GetStateMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_StateMaster";

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

        public async Task<List<CommonDDLModel>> DistrictMaster_StateIDWise(int StateID)
        {
            _actionName = "DistrictMaster_StateIDWise(int StateID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_DistrictMaster_StateIDWise";
                        command.Parameters.AddWithValue("@StateID", StateID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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
        public async Task<List<CommonDDLModel>> DistrictMaster_DivisionIDWise(int DivisionID)
        {
            _actionName = "DistrictMaster_DivisionIDWise(int DivisionID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_DistrictMaster_dIVISIONIDWise";
                        command.Parameters.AddWithValue("@DivisionID", DivisionID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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
        public async Task<List<CommonDDLModel>> TehsilMaster_DistrictIDWise(int DistrictID)
        {
            _actionName = "TehsilMaster_DistrictIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_TehsilMaster_DistrictIDWise";
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<List<CommonDDLModel>> SubDivisionMaster_DistrictIDWise(int DistrictID)
        {
            _actionName = "SubDivisionMaster_DistrictIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SubDivisionMaster_DistrictIDWise";
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<List<CommonDDLModel>> AssemblyMaster_DistrictIDWise(int DistrictID)
        {
            _actionName = "AssemblyMaster_DistrictIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_AssemblyMaster_DistrictIDWise";
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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
                        command.CommandText = "USP_DDL_HiringRoleMaster";

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
        public async Task<List<CommonDDLModel>> GetRoleMasterDDL(int DepartmentID = 0, int EngNonEng = 0)
        {
            _actionName = "GetRoleMasterDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDLGetRoleMaster";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@EngNonEng", EngNonEng);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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
        public async Task<List<CommonDDLModel>> GetParentSubjectDDL(SubjectSearchModel model)
        {
            _actionName = "GetParentSubjectDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ParentSubject";
                        command.Parameters.AddWithValue("@action", "getallData");
                        command.Parameters.AddWithValue("@BranchID", model.BranchID);
                        command.Parameters.AddWithValue("@Year_SemID", model.SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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
        public async Task<List<CommonDDLModel>> SubjectMaster_SemesterIDWise(int SemesterID, int DepartmentID)
        {
            _actionName = "SubjectMaster_SemesterIDWise(int SemesterID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> subjectMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SubjectName_SemesterIDWise";
                        command.Parameters.AddWithValue("@SemesterID", SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        subjectMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return subjectMasters;
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

        public async Task<List<CommonDDLModel>> SubjectMaster_SubjectCode_SemesterIDWise(int SemesterID, int DepartmentID, string SubjectCode)
        {
            _actionName = "SubjectMaster_SubjectCode_SemesterIDWise(int SemesterID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> subjectMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SubjectName_SubjectCode_SemesterIDWise";
                        command.Parameters.AddWithValue("@SemesterID", SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@SubjectCode", SubjectCode);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        subjectMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return subjectMasters;
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

        public async Task<List<CommonDDLModel>> SubjectMaster_StreamIDWise(int StreamID, int DepartmentID, int SemesterID)
        {
            _actionName = "SubjectMaster_StreamIDWise(int StreamID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> subjectMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SubjectMaster_StreamIDWise";
                        command.Parameters.AddWithValue("@StreamID", StreamID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@SemesterID", SemesterID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        subjectMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return subjectMasters;
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
        public async Task<List<CommonDDLModel>> GetStaffTypeDDL()
        {
            _actionName = "GetRoleMasterDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_StaffType";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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
        public async Task<List<CommonDDLModel>> GetGroupCode(CommonDDLSubjectMasterModel model)
        {
            _actionName = "GetGroupCode(CommonDDLSubjectMasterModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_GroupCode";
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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
        public async Task<DataTable> GetExamName()
        {
            _actionName = "StudentType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExamMasterList";

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
        public async Task<DataTable> DDL_InvigilatorSSOID(DDL_InvigilatorSSOID_DataModel model)
        {
            _actionName = "StudentType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_InvigilatorSSOID";
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);

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
        public async Task<DataTable> ParentMenu(int DepartmentID)
        {
            _actionName = "StudentType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_MenuListMaster";
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

        public async Task<DataTable> CityMasterDistrictWise(int DistrictID)
        {
            _actionName = "StudentType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CityListMaster";
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);
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


        public async Task<DataTable> PanchayatSamiti(int DistrictID)
        {
            _actionName = "StudentType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_PanchayatSamiti";
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);
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


        public async Task<DataTable> GramPanchayatSamiti(int TehsilID)
        {
            _actionName = "StudentType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_GramPanchayatSamiti";
                        command.Parameters.AddWithValue("@TehsilID", TehsilID);
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


        public async Task<DataTable> VillageMaster(int ID)
        {
            _actionName = "StudentType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_VillageMaster";
                        command.Parameters.AddWithValue("@ID", ID);
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


        public async Task<DataTable> ExamStudentStatus(int roleId, int type)
        {
            _actionName = "ExamStudentStatus(int roleId)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ExamStudentStatus";
                        command.Parameters.AddWithValue("@roleid", roleId);
                        command.Parameters.AddWithValue("@action", "_getMarkAsDDL");
                        command.Parameters.AddWithValue("@type", type);

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

        public async Task<DataTable> ITIExamStudentStatus(int roleId, int type)
        {
            _actionName = "ExamStudentStatus(int roleId)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ITI_ExamStudentStatus";
                        command.Parameters.AddWithValue("@roleid", roleId);
                        command.Parameters.AddWithValue("@action", "_getMarkAsDDL");
                        command.Parameters.AddWithValue("@type", type);
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

        public async Task<List<CommonDDLModel>> GetExamerSSoidDDL(int DepartmentID)
        {
            _actionName = "GetSubjectMasterDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ExaminerSSOID";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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
        public async Task<List<CommonDDLModel>> GetConfigurationType(int RoleId = 0, int TypeID = 0)
        {
            _actionName = "GetConfigurationType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ConfigurationTypes";
                        command.Parameters.AddWithValue("@RoleID", RoleId);
                        command.Parameters.AddWithValue("@TypeID", TypeID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        #region RPP Payment
        public async Task<RPPPaymentGatewayDataModel> RPPGetpaymentGatewayDetails(RPPPaymentGatewayDataModel model)
        {
            return await Task.Run(async () =>
            {
                _actionName = "RPPGetpaymentGatewayDetails(RPPPaymentGatewayDataModel model)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PaymentGateway_GetData";
                        command.Parameters.AddWithValue("@PaymentGateway", model.PaymentGateway);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@action", "GetRecord");
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();

                    }
                    // class
                    var data = new RPPPaymentGatewayDataModel();
                    data = CommonFuncationHelper.ConvertDataTable<RPPPaymentGatewayDataModel>(dt);
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
        public async Task<bool> RPPCreatePaymentRequest(RPPPaymentRequestModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "RPPCreatePaymentRequest(RPPPaymentRequestModel request)";
                try
                {
                    bool result = false;

                    string strPaymentStatus = "";
                    if (request.REQUESTPARAMETERS.RequestType == (int)EnmPaymetRequest.PaymentRequest)
                        strPaymentStatus = "PaymentRequest";
                    else if (request.REQUESTPARAMETERS.RequestType == (int)EnmPaymetRequest.RefundRequest)
                        strPaymentStatus = "RefundRequest";
                    else
                        strPaymentStatus = "PaymentRequest";

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CreatePaymentTransation";

                        command.Parameters.AddWithValue("@action", "CreatePaymentRequest");
                        command.Parameters.AddWithValue("@PaymentStatus", strPaymentStatus);
                        command.Parameters.AddWithValue("@PRNNO", request.REQUESTPARAMETERS.PRN);
                        command.Parameters.AddWithValue("@CheckSum", request.REQUESTPARAMETERS.CHECKSUM);
                        command.Parameters.AddWithValue("@Amount", request.REQUESTPARAMETERS.AMOUNT);
                        command.Parameters.AddWithValue("@MerchantCode", request.REQUESTPARAMETERS.MERCHANTCODE);
                        command.Parameters.AddWithValue("@PaymentAmount", request.REQUESTPARAMETERS.AMOUNT);
                        command.Parameters.AddWithValue("@UDF1", request.REQUESTPARAMETERS.UDF1);
                        command.Parameters.AddWithValue("@UDF2", request.REQUESTPARAMETERS.UDF2);
                        command.Parameters.AddWithValue("@REQUESTJSON", request.REQUESTJSON);
                        command.Parameters.AddWithValue("@ENCDATA", request.ENCDATA);
                        command.Parameters.AddWithValue("@RequestType", request.REQUESTPARAMETERS.RequestType);
                        command.Parameters.AddWithValue("@RPPTXNID", request.REQUESTPARAMETERS.RPPTXNID);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        var r = await command.ExecuteNonQueryAsync();
                        if (r > 0)
                            result = true;
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
        public async Task<bool> RPPSaveData(RPPPaymentResponseModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "RPPSaveData(RPPPaymentResponse request)";
                try
                {
                    bool result = false;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CreatePaymentTransation";

                        command.Parameters.AddWithValue("@action", "UpdatePaymentStatus");
                        command.Parameters.AddWithValue("@PaymentStatus", request.STATUS);
                        command.Parameters.AddWithValue("@CheckSumValid", request.CHECKSUMVALID);
                        command.Parameters.AddWithValue("@ResponseMessage", request.RESPONSEPARAMETERS.RESPONSEMESSAGE);
                        command.Parameters.AddWithValue("@PRNNO", request.RESPONSEPARAMETERS.PRN);
                        command.Parameters.AddWithValue("@CheckSum", request.RESPONSEPARAMETERS.CHECKSUM);
                        command.Parameters.AddWithValue("@Amount", request.RESPONSEPARAMETERS.AMOUNT);
                        command.Parameters.AddWithValue("@MerchantCode", request.RESPONSEPARAMETERS.MERCHANTCODE);
                        command.Parameters.AddWithValue("@PaymentAmount", request.RESPONSEPARAMETERS.PAYMENTAMOUNT);
                        command.Parameters.AddWithValue("@PaymentMode", request.RESPONSEPARAMETERS.PAYMENTMODE);
                        command.Parameters.AddWithValue("@ENCDATA", request.ENCDATA);
                        command.Parameters.AddWithValue("@PaymentModeBID", request.RESPONSEPARAMETERS.PAYMENTMODEBID);
                        command.Parameters.AddWithValue("@PaymentModeTimeStamp", request.RESPONSEPARAMETERS.PAYMENTMODETIMESTAMP);
                        command.Parameters.AddWithValue("@ReqTimeStamp", request.RESPONSEPARAMETERS.REQTIMESTAMP);
                        command.Parameters.AddWithValue("@ResponseCode", request.RESPONSEPARAMETERS.RESPONSECODE);
                        command.Parameters.AddWithValue("@RPPTXNID", request.RESPONSEPARAMETERS.RPPTXNID);
                        command.Parameters.AddWithValue("@STATUS", request.RESPONSEPARAMETERS.STATUS);
                        command.Parameters.AddWithValue("@UDF1", request.RESPONSEPARAMETERS.UDF1);
                        command.Parameters.AddWithValue("@UDF2", request.RESPONSEPARAMETERS.UDF2);
                        command.Parameters.AddWithValue("@RESPONSEJSON", request.RESPONSEJSON);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        var r = await command.ExecuteNonQueryAsync();
                        if (r > 0)
                            result = true;
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
        public async Task<bool> RPPUpdateRefundStatus(RPPPaymentResponseModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "RPPUpdateRefundStatus(RPPPaymentResponseModel request)";
                try
                {
                    bool result = false;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CreatePaymentTransation";

                        command.Parameters.AddWithValue("@action", "UpdateRefundStatus");
                        command.Parameters.AddWithValue("@PRNNO", request.RESPONSEPARAMETERS.PRN);
                        command.Parameters.AddWithValue("@ResponseMessage", request.RESPONSEPARAMETERS.RESPONSEMESSAGE);
                        command.Parameters.AddWithValue("@ResponseJson", request.RESPONSEJSON);
                        command.Parameters.AddWithValue("@UDF1", request.RESPONSEPARAMETERS.UDF1);
                        command.Parameters.AddWithValue("@ResponseCode", request.RESPONSEPARAMETERS.RESPONSECODE);
                        command.Parameters.AddWithValue("@STATUS", request.RESPONSEPARAMETERS.STATUS);
                        command.Parameters.AddWithValue("@RefundID", request.RESPONSEPARAMETERS.REFUNDID);
                        command.Parameters.AddWithValue("@RPPTimeStamp", request.RESPONSEPARAMETERS.REFUNDTIMESTAMP);
                        command.Parameters.AddWithValue("@REMARKS", request.RESPONSEPARAMETERS.REMARKS);
                        command.Parameters.AddWithValue("@RPPTXNID", request.RESPONSEPARAMETERS.RPPTXNID);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        var r = await command.ExecuteNonQueryAsync();
                        if (r > 0)
                            result = true;
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
        public async Task<bool> RPPUpdateRefundTransactionStatus(RPPRefundTransactionDataModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "RPPUpdateRefundTransactionStatus(RPPRefundTransactionDataModel request)";
                try
                {
                    bool result = false;

                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CreatePaymentTransation";

                        command.Parameters.AddWithValue("@action", "UpdateRefundStatus");
                        command.Parameters.AddWithValue("@PRNNO", request.PRN);
                        command.Parameters.AddWithValue("@ResponseMessage", request.RESPONSEMESSAGE);
                        command.Parameters.AddWithValue("@ResponseJson", request.RESPONSEJSON);
                        command.Parameters.AddWithValue("@UDF1", request.ApplyNocApplicationID);
                        command.Parameters.AddWithValue("@ResponseCode", request.RESPONSECODE);
                        command.Parameters.AddWithValue("@STATUS", request.STATUS);
                        command.Parameters.AddWithValue("@RPPTXNID", request.RPPTXNID);

                        if (request.TRANSACTIONS != null)
                        {
                            if (request.TRANSACTIONS.Count > 0)
                            {
                                string strRefundID = request.TRANSACTIONS.FirstOrDefault() != null ? Convert.ToString(request.TRANSACTIONS.FirstOrDefault().REFUNDID) : string.Empty;
                                string RPPTimeStamp = request.TRANSACTIONS.FirstOrDefault() != null ? request.TRANSACTIONS.FirstOrDefault().REFUNDTIMESTAMP : string.Empty;
                                string REMARKS = request.TRANSACTIONS.FirstOrDefault() != null ? request.TRANSACTIONS.FirstOrDefault().REMARKS : string.Empty;

                                command.Parameters.AddWithValue("@RefundID", strRefundID);
                                command.Parameters.AddWithValue("@RPPTimeStamp", RPPTimeStamp);
                                command.Parameters.AddWithValue("@REMARKS", REMARKS);
                            }
                        }

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        var r = await command.ExecuteNonQueryAsync();
                        if (r > 0)
                            result = true;
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
        public async Task<List<RPPResponseParametersModel>> RPPGetPaymentListIDWise(string TransactionID)
        {
            return await Task.Run(async () =>
            {
                _actionName = "RPPGetPaymentListIDWise(string TransactionID)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PaymentGateway_GetData";

                        command.Parameters.AddWithValue("@PRNNO", TransactionID);
                        command.Parameters.AddWithValue("@action", "ViewRecord");

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();

                    }
                    // class
                    var data = new List<RPPResponseParametersModel>();
                    data = CommonFuncationHelper.ConvertDataTable<List<RPPResponseParametersModel>>(dt);
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
        public async Task<DataTable> GetRPPTransactionList(RPPTransactionSearchFilterModelModel Model)
        {
            return await Task.Run(async () =>
            {
                _actionName = "RPPGetRPPTransactionList(RPPTransactionSearchFilterModelModel Model)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PaymentTransaction_GetData";

                        command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                        command.Parameters.AddWithValue("@TransactionID", Model.TransactionID);
                        command.Parameters.AddWithValue("@RPPTranID", Model.RPPTranID);
                        command.Parameters.AddWithValue("@CollegeID", Model.CollegeID);
                        command.Parameters.AddWithValue("@RefundID", Model.RefundID);
                        command.Parameters.AddWithValue("@PRNNO", Model.PRN);
                        command.Parameters.AddWithValue("@ApplyNocApplicationID", Model.ApplyNocApplicationID);
                        command.Parameters.AddWithValue("@action", Model.Key);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }

                    return dt;
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

        #region Emitra Payment
        public async Task<EmitraRequstParametersModel> GetEmitraServiceDetails(EmitraRequestDetailsModel Model)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetEmitraServiceDetails(EmitraRequestDetailsModel Model)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEmitraServiceDetails";

                        command.Parameters.AddWithValue("@ServiceId", Model.ServiceID);
                        command.Parameters.AddWithValue("@IsKiosk", Model.IsKiosk);
                        command.Parameters.AddWithValue("@ID", Model.ID);
                        command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                        command.Parameters.AddWithValue("@ExamStudentStatus", Model.ExamStudentStatus);
                        command.Parameters.AddWithValue("@action", "_GetServiceDetails");

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }

                    // class
                    var data = new EmitraRequstParametersModel();
                    data = CommonFuncationHelper.ConvertDataTable<EmitraRequstParametersModel>(dt);
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
        public async Task<EmitraTransactionsModel> CreateEmitraTransation(EmitraTransactionsModel Model)
        {
            return await Task.Run(async () =>
            {
                _actionName = "CreateAddEmitraTransation(EmitraTransactionsModel Model)";
                try
                {
                    var result = 0;
                    var retval_TransactionId = 0;
                    using (var command = _dbContext.CreateCommand(true))// true to control transaction
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InsertEmitraTransactions";

                        command.Parameters.AddWithValue("@ApplicationIdEnc", Model.ApplicationIdEnc);
                        command.Parameters.AddWithValue("@ApplicationNo", Model.ApplicationNo);
                        command.Parameters.AddWithValue("@KioskID", Model.KioskID);
                        command.Parameters.AddWithValue("@ReceiptNo", Model.ReceiptNo);
                        command.Parameters.AddWithValue("@TokenNo", Model.TokenNo);
                        command.Parameters.AddWithValue("@RequestStatus", Model.RequestStatus);
                        command.Parameters.AddWithValue("@StatusMsg", Model.StatusMsg);
                        command.Parameters.AddWithValue("@RequestString", Model.RequestString);
                        command.Parameters.AddWithValue("@ResponseString", Model.ResponseString);
                        command.Parameters.AddWithValue("@ActId", Model.ActId);
                        command.Parameters.AddWithValue("@TransactionId", Model.TransactionId);
                        command.Parameters.AddWithValue("@PRN", Model.PRN);
                        command.Parameters.AddWithValue("@SSOID", Model.SSOID);
                        command.Parameters.AddWithValue("@CreatedIP", Model.CreatedIP);
                        command.Parameters.AddWithValue("@ServiceID", Model.ServiceID);
                        command.Parameters.AddWithValue("@Amount", Model.Amount);
                        command.Parameters.AddWithValue("@AddFeeAmount", Model.EnrollFeeAmount);
                        command.Parameters.AddWithValue("@StudentID", Model.StudentID);
                        command.Parameters.AddWithValue("@SemesterID", Model.SemesterID);
                        command.Parameters.AddWithValue("@action", Model.key);
                        command.Parameters.AddWithValue("@ExamStudentStatus", Model.ExamStudentStatus);
                        command.Parameters.AddWithValue("@TransactionApplicationID", Model.TransactionApplicationID);
                        command.Parameters.AddWithValue("@StudentFeesTransactionItems", JsonConvert.SerializeObject(Model.StudentFeesTransactionItems));
                        command.Parameters.AddWithValue("@IsEmitra", Model.IsEmitra);
                        command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                        command.Parameters.AddWithValue("@UniqueServiceID", Model.UniqueServiceID);
                        command.Parameters.AddWithValue("@FeeFor", Model.FeeFor);
                        command.Parameters.Add("@retval_TransactionId", SqlDbType.Int);// out
                        command.Parameters["@retval_TransactionId"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        result = await command.ExecuteNonQueryAsync();

                        retval_TransactionId = Convert.ToInt32(command.Parameters["@retval_TransactionId"].Value);// out
                    }

                    // class
                    if (result > 0)
                        Model.TransactionId = retval_TransactionId;
                    else
                        Model.TransactionId = 0;
                    return Model;
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


        public async Task<EmitraTransactionsModel> CreateEmitraTransationITI(EmitraTransactionsModel Model)
        {
            return await Task.Run(async () =>
            {
                _actionName = "CreateEmitraTransationITI(EmitraTransactionsModel Model)";
                try
                {
                    var result = 0;
                    var retval_TransactionId = 0;
                    using (var command = _dbContext.CreateCommand(true))// true to control transaction
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_InsertEmitraTransactions";

                        command.Parameters.AddWithValue("@ApplicationIdEnc", Model.ApplicationIdEnc);
                        command.Parameters.AddWithValue("@ApplicationNo", Model.ApplicationNo);
                        command.Parameters.AddWithValue("@KioskID", Model.KioskID);
                        command.Parameters.AddWithValue("@ReceiptNo", Model.ReceiptNo);
                        command.Parameters.AddWithValue("@TokenNo", Model.TokenNo);
                        command.Parameters.AddWithValue("@RequestStatus", Model.RequestStatus);
                        command.Parameters.AddWithValue("@StatusMsg", Model.StatusMsg);
                        command.Parameters.AddWithValue("@RequestString", Model.RequestString);
                        command.Parameters.AddWithValue("@ResponseString", Model.ResponseString);
                        command.Parameters.AddWithValue("@ActId", Model.ActId);
                        command.Parameters.AddWithValue("@TransactionId", Model.TransactionId);
                        command.Parameters.AddWithValue("@PRN", Model.PRN);
                        command.Parameters.AddWithValue("@SSOID", Model.SSOID);
                        command.Parameters.AddWithValue("@CreatedIP", Model.CreatedIP);
                        command.Parameters.AddWithValue("@ServiceID", Model.ServiceID);
                        command.Parameters.AddWithValue("@Amount", Model.Amount);
                        command.Parameters.AddWithValue("@StudentID", Model.StudentID);
                        command.Parameters.AddWithValue("@SemesterID", Model.SemesterID);
                        command.Parameters.AddWithValue("@action", Model.key);
                        command.Parameters.AddWithValue("@ExamStudentStatus", Model.ExamStudentStatus);
                        command.Parameters.AddWithValue("@TransactionApplicationID", Model.TransactionApplicationID);
                        command.Parameters.AddWithValue("@StudentFeesTransactionItems", JsonConvert.SerializeObject(Model.StudentFeesTransactionItems));
                        command.Parameters.AddWithValue("@IsEmitra", Model.IsEmitra);
                        command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                        command.Parameters.AddWithValue("@UniqueServiceID", Model.UniqueServiceID);
                        command.Parameters.AddWithValue("@FeeFor", Model.FeeFor);
                        command.Parameters.Add("@retval_TransactionId", SqlDbType.Int);// out
                        command.Parameters["@retval_TransactionId"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        result = await command.ExecuteNonQueryAsync();

                        retval_TransactionId = Convert.ToInt32(command.Parameters["@retval_TransactionId"].Value);// out
                    }

                    // class
                    if (result > 0)
                        Model.TransactionId = retval_TransactionId;
                    else
                        Model.TransactionId = 0;
                    return Model;
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


        public async Task<EmitraTransactionsModel> CreateEmitraApplicationTransation(EmitraTransactionsModel Model)
        {
            return await Task.Run(async () =>
            {
                _actionName = "CreateAddEmitraTransation(EmitraTransactionsModel Model)";
                try
                {
                    var result = 0;
                    var retval_TransactionId = 0;
                    using (var command = _dbContext.CreateCommand(true))// true to control transaction
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InsertEmitraApplicationTransactions";

                        command.Parameters.AddWithValue("@ApplicationIdEnc", Model.ApplicationIdEnc);
                        command.Parameters.AddWithValue("@ApplicationNo", Model.ApplicationNo);
                        command.Parameters.AddWithValue("@KioskID", Model.KioskID);
                        command.Parameters.AddWithValue("@ReceiptNo", Model.ReceiptNo);
                        command.Parameters.AddWithValue("@TokenNo", Model.TokenNo);
                        command.Parameters.AddWithValue("@RequestStatus", Model.RequestStatus);
                        command.Parameters.AddWithValue("@StatusMsg", Model.StatusMsg);
                        command.Parameters.AddWithValue("@RequestString", Model.RequestString);
                        command.Parameters.AddWithValue("@ResponseString", Model.ResponseString);
                        command.Parameters.AddWithValue("@ActId", Model.ActId);
                        command.Parameters.AddWithValue("@TransactionId", Model.TransactionId);
                        command.Parameters.AddWithValue("@PRN", Model.PRN);
                        command.Parameters.AddWithValue("@SSOID", Model.SSOID);
                        command.Parameters.AddWithValue("@CreatedIP", Model.CreatedIP);
                        command.Parameters.AddWithValue("@ServiceID", Model.ServiceID);
                        command.Parameters.AddWithValue("@Amount", Model.Amount);
                        command.Parameters.AddWithValue("@StudentID", Model.StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                        command.Parameters.AddWithValue("@SemesterID", Model.SemesterID);
                        command.Parameters.AddWithValue("@action", Model.key);
                        command.Parameters.AddWithValue("@TransactionApplicationID", Model.TransactionApplicationID);
                        command.Parameters.AddWithValue("@IsEmitra", Model.IsEmitra);
                        command.Parameters.AddWithValue("@FeeFor", Model.FeeFor);
                        command.Parameters.AddWithValue("@TransactionNo", Model.TransactionNo);
                        command.Parameters.AddWithValue("@PaidAmount", Model.PaidAmount);

                        command.Parameters.Add("@retval_TransactionId", SqlDbType.Int);// out
                        command.Parameters["@retval_TransactionId"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        result = await command.ExecuteNonQueryAsync();

                        retval_TransactionId = Convert.ToInt32(command.Parameters["@retval_TransactionId"].Value);// out
                    }

                    // class
                    if (result > 0)
                        Model.TransactionId = retval_TransactionId;
                    else
                        Model.TransactionId = 0;
                    return Model;
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
        public async Task<List<RPPResponseParametersModel>> GetPreviewPaymentDetails(int CollegeID)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetPreviewPaymentDetails(int CollegeID)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PaymentTransaction_GetData";

                        command.Parameters.AddWithValue("@CollegeID", CollegeID);
                        command.Parameters.AddWithValue("@action", "_GetPreviewPaymentDetails");

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }

                    // class
                    var data = new List<RPPResponseParametersModel>();
                    data = CommonFuncationHelper.ConvertDataTable<List<RPPResponseParametersModel>>(dt);
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
        public async Task<bool> UpdateEmitraPaymentStatus(EmitraResponseParametersModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "UpdateEmitraPaymentStatus(EmitraResponseParametersModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InsertEmitraTransactions";
                        command.Parameters.AddWithValue("@ApplicationIdEnc", request.ApplicationIdEnc);
                        command.Parameters.AddWithValue("@TransactionId", request.TRANSACTIONID);
                        command.Parameters.AddWithValue("@PRN", request.PRN);
                        command.Parameters.AddWithValue("@PaidAmount", request.PAIDAMOUNT);
                        command.Parameters.AddWithValue("@TokenNo", request.RECEIPTNO);
                        command.Parameters.AddWithValue("@StatusMsg", request.RESPONSEMESSAGE);
                        command.Parameters.AddWithValue("@ResponseString", JsonConvert.SerializeObject(request));
                        command.Parameters.AddWithValue("@ReceiptNo", request.RECEIPTNO);
                        command.Parameters.AddWithValue("@RequestStatus", request.STATUS);
                        command.Parameters.AddWithValue("@TransactionNo", request.TransactionNo);
                        command.Parameters.AddWithValue("@ExamStudentStatus", request.ExamStudentStatus);
                        command.Parameters.AddWithValue("@action", "_UpdateEmitraPaymentStatus");
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
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
        public async Task<Int64> UpdateEmitraApplicationPaymentStatus(EmitraResponseParametersModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "UpdateEmitraPaymentStatus(EmitraResponseParametersModel request)";
                try
                {
                    Int64 result = 0;
                    Int64 retval_TransactionId = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InsertEmitraApplicationTransactions";

                        command.Parameters.AddWithValue("@ApplicationIdEnc", request.ApplicationIdEnc);
                        command.Parameters.AddWithValue("@TransactionId", request.TRANSACTIONID);
                        command.Parameters.AddWithValue("@PRN", request.PRN);
                        command.Parameters.AddWithValue("@PaidAmount", request.PAIDAMOUNT);
                        command.Parameters.AddWithValue("@TokenNo", request.RECEIPTNO);
                        command.Parameters.AddWithValue("@StatusMsg", request.RESPONSEMESSAGE);
                        command.Parameters.AddWithValue("@ResponseString", JsonConvert.SerializeObject(request));
                        command.Parameters.AddWithValue("@ReceiptNo", request.RECEIPTNO);
                        command.Parameters.AddWithValue("@RequestStatus", request.STATUS);
                        //command.Parameters.AddWithValue("@ExamStudentStatus", request.ExamStudentStatus);
                        command.Parameters.AddWithValue("@action", "_UpdateEmitraPaymentStatus");
                        command.Parameters.Add("@retval_TransactionId", SqlDbType.Int);// out
                        command.Parameters["@retval_TransactionId"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        result = await command.ExecuteNonQueryAsync();

                        retval_TransactionId = Convert.ToInt64(command.Parameters["@retval_TransactionId"].Value);// out
                    }

                    // class
                    if (result > 0)
                        result = retval_TransactionId;
                    else
                        result = 0;
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
        public async Task<DataTable> GetEmitraTransactionDetails(string PRN)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetEmitraTransactionDetails(string PRN)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEmitraTransactionDetails";

                        command.Parameters.AddWithValue("@PRN", PRN);
                        command.Parameters.AddWithValue("@action", "_GetTransactionDetails");
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }

                    return dt;
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

        public async Task<DataTable> GetEmitraITITransactionDetails(string PRN)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetEmitraTransactionDetails(string PRN)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetEmitraTransactionDetails";
                        command.Parameters.AddWithValue("@PRN", PRN);
                        command.Parameters.AddWithValue("@action", "_GetTransactionDetails");
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }
                    return dt;
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
        public async Task<DataTable> GetEmitraApplicationTransactionDetails(string PRN)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetEmitraApplicationTransactionDetails(string PRN)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEmitraApplicationTransactionDetails";

                        command.Parameters.AddWithValue("@PRN", PRN);
                        command.Parameters.AddWithValue("@action", "_GetTransactionDetails");
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }

                    return dt;
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
        public async Task<DataTable> GetTransactionDetailsActionWise(StudentSearchModel model)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetEmitraTransactionDetails(string PRN)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetEmitraTransactionDetails";

                        command.Parameters.AddWithValue("@PRN", model.PrnNo);
                        command.Parameters.AddWithValue("@StudentID", model.StudentID);
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@TrasactionStatus", model.TrasactionStatus);
                        command.Parameters.AddWithValue("@action", model.Action);
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }

                    return dt;
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
        public async Task<DataTable> GetEgrassDetails_DepartmentWise(int DepartmentID, string PaymentType)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetEgrassDetails_DepartmentWise(int DepartmentID, string PaymentType)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = $"select * from M_EGrassDetails Where DepartmentID={DepartmentID} and PaymentType='{PaymentType}' and ActiveStatus=1 and DeleteStatus=0 order by aid desc";

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }

                    return dt;
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
        public async Task<int> EGrassPaymentDetails_Req_Res(EGrassPaymentDetails_Req_ResModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "EGrassPaymentDetails_Req_Res(EGrassPaymentDetails_Req_ResModel req_Res)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_EGrassPaymentDetails_Req_Res_Save";

                        command.Parameters.AddWithValue("@ApplyNocApplicationID", request.ApplyNocApplicationID);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Head_Name", request.Head_Name);
                        command.Parameters.AddWithValue("@Request_AUIN", request.Request_AUIN);
                        command.Parameters.AddWithValue("@Request_CollegeName", request.Request_CollegeName);
                        command.Parameters.AddWithValue("@Request_SSOID", request.Request_SSOID);
                        command.Parameters.AddWithValue("@Request_AMOUNT", request.Request_AMOUNT);
                        command.Parameters.AddWithValue("@Request_MerchantCode", request.Request_MerchantCode);
                        command.Parameters.AddWithValue("@Request_REGTINNO", request.Request_REGTINNO);
                        command.Parameters.AddWithValue("@Request_OfficeCode", request.Request_OfficeCode);
                        command.Parameters.AddWithValue("@Request_DepartmentCode", request.Request_DepartmentCode);
                        command.Parameters.AddWithValue("@Request_Checksum", request.Request_Checksum);
                        command.Parameters.AddWithValue("@Request_ENCAUIN", request.Request_ENCAUIN);
                        command.Parameters.AddWithValue("@Request_Json", request.Request_Json);
                        command.Parameters.AddWithValue("@Request_JsonENC", request.Request_JsonENC);
                        command.Parameters.AddWithValue("@Response_CIN", request.Response_CIN);
                        command.Parameters.AddWithValue("@Response_BankReferenceNo", request.Response_BankReferenceNo);
                        command.Parameters.AddWithValue("@Response_BANK_CODE", request.Response_BANK_CODE);
                        command.Parameters.AddWithValue("@Response_BankDate", request.Response_BankDate);
                        command.Parameters.AddWithValue("@Response_GRN", request.Response_GRN);
                        command.Parameters.AddWithValue("@Response_Amount", request.Response_Amount);
                        command.Parameters.AddWithValue("@Response_Status", request.Response_Status);
                        command.Parameters.AddWithValue("@Response_checkSum", request.Response_checkSum);
                        command.Parameters.AddWithValue("@Response_Json", request.Response_Json);
                        command.Parameters.AddWithValue("@Response_JsonENC", request.Response_JsonENC);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
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
        public async Task<DataTable> GetOfflinePaymentDetails(int CollegeID)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetOfflinePaymentDetails(int CollegeID)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetOfflinePaymentDetails";
                        command.Parameters.AddWithValue("CollegeID", CollegeID);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }

                    return dt;
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
        public async Task<DataTable> GetEGrass_AUIN_Verify_Data(int EGrassPaymentAID)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetEGrass_AUIN_Verify_Data(int EGrassPaymentAID)";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_EGrass_AUIN_Verify_Data";
                        command.Parameters.AddWithValue("AID", EGrassPaymentAID);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }

                    return dt;
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
        public async Task<int> GRAS_GetPaymentStatus_Req_Res(EGrassPaymentDetails_Req_ResModel req_Res)
        {
            return await Task.Run(async () =>
            {
                _actionName = "GRAS_GetPaymentStatus_Req_Res(EGrassPaymentDetails_Req_ResModel req_Res)";
                try
                {
                    var result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GRAS_PaymentStatusLog_Save";
                        command.Parameters.AddWithValue("RequestData", req_Res.Request_Json);
                        command.Parameters.AddWithValue("ResponseDataEnc", req_Res.Request_JsonENC);
                        command.Parameters.AddWithValue("ResponseData", req_Res.Response_Json);
                        command.Parameters.AddWithValue("AUIN", req_Res.Request_AUIN);
                        command.Parameters.AddWithValue("CIN", req_Res.Response_CIN);
                        command.Parameters.AddWithValue("BankReferenceNo", req_Res.Response_BankReferenceNo);
                        command.Parameters.AddWithValue("BANK_CODE", req_Res.Response_BANK_CODE);
                        command.Parameters.AddWithValue("BankDate", req_Res.Response_BankDate);
                        command.Parameters.AddWithValue("GRN", req_Res.Response_GRN);
                        command.Parameters.AddWithValue("Amount", req_Res.Response_Amount);
                        command.Parameters.AddWithValue("Status", req_Res.Response_Status);
                        command.Parameters.AddWithValue("checkSum", req_Res.Response_checkSum);

                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
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
        #endregion

        #region aadharcard
        public async Task<M_AadharCardServiceMaster> GetAadharCardServiceMaster()
        {
            return await Task.Run(async () =>
            {
                _actionName = "GetAadharCardServiceMaster()";
                try
                {
                    DataTable dt = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        _sqlQuery = " select * from M_AadharCardServiceMaster ";
                        command.CommandText = _sqlQuery;
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                        dt = await command.FillAsync_DataTable();
                    }
                    // class
                    var data = new M_AadharCardServiceMaster();
                    data = CommonFuncationHelper.ConvertDataTable<M_AadharCardServiceMaster>(dt);
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
        #endregion


        public async Task<DataTable> GetCollegeTypeList()
        {
            _actionName = "GetCollegeTypeList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITITrade";
                        command.Parameters.AddWithValue("@Action", "GetCollegeTypeList");

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
        public async Task<DataTable> GetTradeTypesList()
        {
            _actionName = "GetTradeTypesList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITITrade";
                        command.Parameters.AddWithValue("@Action", "GetTradeTypesList");

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
        public async Task<DataTable> GetTradeLevelList()
        {
            _actionName = "GetTradeLevelList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITITrade";
                        command.Parameters.AddWithValue("@Action", "GetTradeLevelList");

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
        public async Task<DataTable> ItiCollegesGetAllData(ItiCollegesSearchModel request)
        {
            _actionName = "ItiCollegesGetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiCollegesList_DistrictWise";
                        command.Parameters.AddWithValue("@action", request.action);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@ManagementType", request.ManagementType);
                        command.Parameters.AddWithValue("@ManagementTypeID", request.ManagementTypeID);

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
        public async Task<DataTable> BterCollegesGetAllData(BterCollegesSearchModel request)
        {
            _actionName = "ItiCollegesGetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (request.ApplicationID > 0)
                        {
                            command.CommandText = "USP_BTER_OptionFormOption";
                        }
                        else
                        {
                            command.CommandText = "USP_BterCollegesList_DistrictWise";
                        }
                        command.Parameters.AddWithValue("@action", request.action);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@ManagementType", request.College_TypeID);
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@StreamType", request.StreamType);

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

        public async Task<DataTable> TradeListGetAllData(ItiTradeSearchModel request)
        {
            _actionName = "TradeListGetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiTradeList_CollegeWise";

                        command.Parameters.AddWithValue("@action", request.action);
                        command.Parameters.AddWithValue("@CollegeID", request.CollegeID);
                        command.Parameters.AddWithValue("@TradeLevel", request.TradeLevel);
                        command.Parameters.AddWithValue("@IsPH", request.IsPH);
                        command.Parameters.AddWithValue("@Age", request.Age);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        //command.Parameters.AddWithValue("@IsPH", request.MathPercentage);
                        //command.Parameters.AddWithValue("@IsPH", request.SciencePercentage);
                        command.Parameters.AddWithValue("@FinancialYear", request.FinancialYear);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@ManagementTypeID", request.ManagementTypeID);

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



        public async Task<DataTable> GetStudentStatusByRole(int roleId, int type)
        {
            _actionName = "ExamStudentStatus()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ExamStudentStatus";
                        command.Parameters.AddWithValue("@roleid", roleId);
                        command.Parameters.AddWithValue("@action", "_getStatusDDL");
                        command.Parameters.AddWithValue("@type", type);

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

        public async Task<DataTable> GetEnrollmentCancelStatusByRole(int roleId, int type)
        {
            _actionName = "ExamStudentStatus()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ExamStudentStatus";
                        command.Parameters.AddWithValue("@roleid", roleId);
                        command.Parameters.AddWithValue("@action", "_getEnrollmentCancelStatusDDL");
                        command.Parameters.AddWithValue("@type", type);

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


        public async Task<DataTable> ItiGetStudentStatusByRole(int roleId, int type)
        {
            _actionName = "ExamStudentStatus()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ITI_ExamStudentStatus";
                        command.Parameters.AddWithValue("@roleid", roleId);
                        command.Parameters.AddWithValue("@action", "_getStatusDDL");
                        command.Parameters.AddWithValue("@type", type);

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



        public async Task<List<CommonDDLModel>> GetCenterMasterDDL(RequestBaseModel request)
        {
            _actionName = "GetCenterMasterDDL(RequestBaseModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_GetCenterMaster";

                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable != null)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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

        public async Task<List<CommonDDLModel>> GetSubjectMasterDDL_New(CommonDDLSubjectMasterModel request)
        {
            _actionName = "GetSubjectMasterDDL_New(CommonDDLSubjectMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SubjectMaster";

                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", request.StreamID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable != null)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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

        public async Task<List<CommonSerialMasterResponseModel>> GetSerialMasterData(CommonSerialMasterRequestModel request)
        {
            _actionName = "GetSerialMasterData(CommonSerialMasterRequestModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonSerialMasterResponseModel> data = new List<CommonSerialMasterResponseModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SerialMaster";

                        command.Parameters.AddWithValue("@action", "_getAllData");
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterId);
                        command.Parameters.AddWithValue("@TypeID", request.TypeID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonSerialMasterResponseModel>>(dataTable);
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

        public async Task<DataTable> GetExaminerGroupCode(CommonDDLExaminerGroupCodeModel model)
        {
            _actionName = "GetExaminerGroupCode(CommonDDLExaminerGroupCodeModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_GetExaminerGroupCode";
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);

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
        public async Task<DataTable> GetExaminerGroupCode_Reval(CommonDDLExaminerGroupCodeModel model)
        {
            _actionName = "GetExaminerGroupCode_Reval(CommonDDLExaminerGroupCodeModel model)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_GetExaminerGroupCode_Reval";
                        command.Parameters.AddWithValue("@SubjectID", model.SubjectID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);

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




        public async Task<List<CommonDDLModel>> GetInstituteMasterByTehsilWise(int TehsilID, int EndTermId)
        {
            _actionName = "GetInstituteMasterByTehsilWise(int StateID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InstituteMaster_TehsilWise";
                        command.Parameters.AddWithValue("@TehsilID", TehsilID);
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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
        public async Task<List<CommonDDLModel>> GetInstituteMasterByDistrictWise(int DistrictID, int EndTermId)
        {
            _actionName = "GetInstituteMasterByDistrictWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_InstituteMaster_DistrictWise";
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<List<CommonDDLModel>> GetSubCasteCategoryA(int CasteCategoryID)
        {
            _actionName = "GetSubCasteCategoryA(int CasteCategoryID)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SubCasteCategoryA";

                        command.Parameters.AddWithValue("@CasteCategoryID", CasteCategoryID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable != null)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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


        public async Task<List<CommonDDLModel>> InsituteMaster_DistrictIDWise(int DistrictID, int EndTermId)
        {
            _actionName = "InsituteMaster_DistrictIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_InsituteMaster_DistrictIDWise";
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<List<CommonDDLModel>> InsituteMaster_DistrictIDWise(int DistrictID, int EndTermId, int DepartmentID)
        {
            _actionName = "InsituteMaster_DistrictIDWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_InsituteMaster_DistrictIDWise_Dept";
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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


        public async Task<UserRequestModel> CheckUserExists(string SSOID)
        {
            _actionName = "Login(string SSOID, string Password)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CheckUserExists";
                        command.Parameters.AddWithValue("@SSOID", SSOID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new UserRequestModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<UserRequestModel>(dataTable);
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
        public async Task<UserRequestModel> CheckSSOIDExists(string SSOID, string RoleID, string InstituteID)
        {
            _actionName = "CheckSSOIDExists()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CheckSSOIDExists";
                        command.Parameters.AddWithValue("@SSOID", SSOID);
                        command.Parameters.AddWithValue("@RoleID", RoleID);
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new UserRequestModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<UserRequestModel>(dataTable);
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

        public async Task<DataSet> GetOptionalSubjectsByStudentID(Int32 StudentID, Int32 DepartmentID, int StudentExamID)
        {
            _actionName = "GetOptionalSubjectsByStudentID(Int32 StudentID, Int32 DepartmentID,int StudentExamID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_OptionalSubjectMaster";
                        command.Parameters.AddWithValue("@Action", "OptionalSubject");
                        command.Parameters.AddWithValue("@StudentID", StudentID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@StudentExamID", StudentExamID);

                        _sqlQuery = command.GetSqlExecutableQuery();
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
            });
        }
        public async Task<List<CommonDDLModel>> GetCastCategory()
        {
            _actionName = "GetCastCategory()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CastCategoryMaster";

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

        public async Task<List<CommonDDLModel>> GetCommonSubjectDDL(CommonDDLCommonSubjectModel model)
        {
            _actionName = "GetCommonSubjectDDL(CommonDDLCommonSubjectModel model)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CommonSubject";

                        command.Parameters.AddWithValue("@action", "_getCommonSubject");
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable != null)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<DataTable> GetActiveTabList(int DepartmentID, int ApplicationID, int RoleID)
        {
            _actionName = "GetActiveTabList()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetApplicationActiveTab";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@RoleID", RoleID);
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


        public async Task<List<CommonDDLModel>> GetQualificationDDL(string type)
        {
            _actionName = "GetQualificationDDL(string type)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CommonMaster";

                        command.Parameters.AddWithValue("@action", "_getEduDDLFor");
                        command.Parameters.AddWithValue("@type", type);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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


        public async Task<List<CommonDDLModel>> GetCategaryCastDDL(string type)
        {
            _actionName = "GetQualificationDDL(string type)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CommonMaster";

                        command.Parameters.AddWithValue("@action", "_getCastDDLFor");
                        command.Parameters.AddWithValue("@type", type);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        public async Task<bool> UpwardMomentUpdate(UpwardMoment model)
        {
            _actionName = "UpwardMomentUpdate(UpwardMoment model)";
            try
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = $" update ITI_StudentSeatAllotment set IsUpword='{model.IsUpward}', ModifyBy='{model.UserID} ',ModifyDate=GETDATE(), IPAddress='{_IPAddress}'Where ApplicationID={model.ApplicationID}";

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

        public async Task<List<EmitraApplicationstatusModel>> GetDataItiStudentApplication(ItiStuAppSearchModelUpward searchModel)
        {
            _actionName = "GetDataItiStudentApplication(StudentSearchModel searchModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GetApplicationStatus";

                        // Add parameters to the stored procedure from the model

                        command.Parameters.AddWithValue("@ApplicationNo", searchModel.ApplicationNo);
                        command.Parameters.AddWithValue("@MobileNumber", searchModel.MobileNumber);
                        command.Parameters.AddWithValue("@DOB", searchModel.DOB);
                        command.Parameters.AddWithValue("@SSOID", searchModel.SSOID);
                        command.Parameters.AddWithValue("@EndTermID", searchModel.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", searchModel.DepartmentID);

                        command.Parameters.AddWithValue("@action", searchModel.action);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<EmitraApplicationstatusModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<EmitraApplicationstatusModel>>(dataTable);
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

        public async Task<List<CommonDDLModel>> GetSubjectCodeMasterDDL(CommonDDLSubjectCodeMasterModel request)
        {
            _actionName = "GetSubjectMasterDDL_New(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SubjectCodeMaster";

                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable != null)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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

        public async Task<List<CommonDDLModel>> GetTimeTableSubjectCodeMasterDDL(CommonDDLSubjectCodeMasterModel request)
        {
            _actionName = "GetTimeTableSubjectCodeMasterDDL(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SubjectCodeMasterTimeTable";

                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable != null)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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


        public async Task<bool> UpdateITIEmitraPaymentStatus(EmitraResponseParametersModel request)
        {
            return await Task.Run(async () =>
            {
                _actionName = "UpdateEmitraPaymentStatus(EmitraResponseParametersModel request)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_InsertEmitraTransactions";
                        command.Parameters.AddWithValue("@ApplicationIdEnc", request.ApplicationIdEnc);
                        command.Parameters.AddWithValue("@TransactionId", request.TRANSACTIONID);
                        command.Parameters.AddWithValue("@PRN", request.PRN);
                        command.Parameters.AddWithValue("@PaidAmount", request.PAIDAMOUNT);
                        command.Parameters.AddWithValue("@TokenNo", request.RECEIPTNO);
                        command.Parameters.AddWithValue("@StatusMsg", request.RESPONSEMESSAGE);
                        command.Parameters.AddWithValue("@ResponseString", JsonConvert.SerializeObject(request));
                        command.Parameters.AddWithValue("@ReceiptNo", request.RECEIPTNO);
                        command.Parameters.AddWithValue("@RequestStatus", request.STATUS);
                        command.Parameters.AddWithValue("@ExamStudentStatus", request.ExamStudentStatus);
                        command.Parameters.AddWithValue("@action", "_UpdateEmitraPaymentStatus");
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
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

        public async Task<List<CommonDDLModel>> Subjects_Semester_SubjectCodeWise(int SemesterID, int DepartmentID, string SubjectCode, int EndTerm, int CourseTypeID)
        {
            _actionName = "SubjectMaster_SubjectCode_SemesterIDWise(int SemesterID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> subjectMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_GetTimetableSubjectCodeMst";
                        command.Parameters.AddWithValue("@SemesterID", SemesterID);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@SubjectCode", SubjectCode);
                        command.Parameters.AddWithValue("@EndTerm", EndTerm);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        subjectMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return subjectMasters;
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

        public async Task<List<CommonDDLModel>> CategoryBDDLData(int DepartmentID)
        {
            _actionName = "CategoryBDDLData(int DepartmentID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CategoryB";

                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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


        public async Task<DataTable> ExamStudentStatusApprovalReject(int roleId, int type)
        {
            _actionName = "ExamStudentStatusApprovalReject(int roleId)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ExamStudentStatus";
                        command.Parameters.AddWithValue("@action", "ApprovalReject");
                        command.Parameters.AddWithValue("@roleid", roleId);
                        command.Parameters.AddWithValue("@type", type);

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


        public async Task<List<CommonDDLModel>> GetITITradeNameDDl(CommonDDLSubjectCodeMasterModel request)
        {
            _actionName = "GetITITradeNameDDl(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITISomeDDl";
                        command.Parameters.AddWithValue("@Action", "GetTradeNameDDl");
                        command.Parameters.AddWithValue("@SemesterId", request.SemesterID);
                        command.Parameters.AddWithValue("@StreamId", request.StreamID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable != null)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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

        public async Task<List<CommonDDLModel>> GetITISubjectNameDDl(CommonDDLSubjectCodeMasterModel request)
        {
            _actionName = "GetITISubjectNameDDl(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITISomeDDl";
                        command.Parameters.AddWithValue("@Action", "GetSubjectNameDDl");
                        command.Parameters.AddWithValue("@SemesterId", request.SemesterID);
                        command.Parameters.AddWithValue("@StreamId", request.StreamID);
                        command.Parameters.AddWithValue("@SubjectType", request.SubjectType);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable != null)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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

        public async Task<List<CommonDDLModel>> GetITISubjectCodeDDl(CommonDDLSubjectCodeMasterModel request)
        {
            _actionName = "GetSubjectMasterDDL_New(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITISomeDDl";
                        command.Parameters.AddWithValue("@Action", "GetSubjectCodeDDl");
                        command.Parameters.AddWithValue("@SemesterId", request.SemesterID);
                        command.Parameters.AddWithValue("@StreamId", request.StreamID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable != null)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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



        public async Task<DataTable> StreamDDL_InstituteWise(StreamDDL_InstituteWiseModel request)
        {
            _actionName = "StreamDDL_InstituteWise(StreamDDL_InstituteWiseModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Stream_InstituteWise";

                        command.Parameters.AddWithValue("@action", "_getDatabyCollege");
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@StreamType", request.StreamType);

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


        public async Task<DataTable> StreamDDLInstituteIdWise(StreamDDL_InstituteWiseModel request)
        {
            _actionName = "StreamDDL_InstituteWise(StreamDDL_InstituteWiseModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StreamInstituteIdWise";
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@StreamType", request.StreamType);
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

        public async Task<DataTable> GetDateSetting(DateSettingConfigModel request)
        {
            _actionName = "USP_GetDateSetting(StreamDDL_InstituteWiseModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetDateSetting";
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeId", request.CourseTypeId);
                        command.Parameters.AddWithValue("@AcademicYearID", request.AcademicYearID);
                        command.Parameters.AddWithValue("@EndTermID", request.EndtermID);
                        command.Parameters.AddWithValue("@Key", request.Key);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
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

        public async Task<DataTable> QualificationDDL(QualificationDDLDataModel request)
        {
            _actionName = "StreamDDL_InstituteWise(QualificationDDLDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_Qualification";
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@QualificationLevel", request.QualificationLevel);
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
        public async Task<List<CommonDDLModel>> GetReletionDDL(string type)
        {
            _actionName = "GetReletionDDL(string type)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CommonMaster";

                        command.Parameters.AddWithValue("@action", "_getCMDDLByType");
                        command.Parameters.AddWithValue("@type", type);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        public async Task<List<CommonDDLModel>> GetRoomTypeDDL(string type)
        {
            _actionName = "GetRoomTypeDDL(string type)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CommonMaster";

                        command.Parameters.AddWithValue("@action", "_getCMDDLByType");
                        command.Parameters.AddWithValue("@type", type);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        public async Task<List<CommonDDLModel>> GetRoomTypeDDLByHostel(string type, int HostelID)
        {
            _actionName = "GetRoomTypeDDLByHostel(string type)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CommonMaster";
                        command.Parameters.AddWithValue("@action", "_getGetRoomTypeDDLByHostel");
                        command.Parameters.AddWithValue("@type", type);
                        command.Parameters.AddWithValue("@HostelID", HostelID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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


        public async Task<DataTable> SessionConfiguration(SessionConfigModel request)
        {
            _actionName = "USP_SessionConfiguration(StreamDDL_InstituteWiseModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_SessionConfiguration";
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@FinancialYearName", request.FinancialYearName);
                        command.Parameters.AddWithValue("@EndTermName", request.EndTermName);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);
                        command.Parameters.AddWithValue("@IsCurrentFY", request.IsCurrentFY);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@Action", request.Action);
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

        public async Task<List<CommonDDLModel>> GetHostelDDL(int DepartmentID, int InstituteID)
        {
            _actionName = "GetHostelDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_M_HostelMaster";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@InstituteID", InstituteID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    // class
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        public async Task<List<CommonDDLModel>> GetTechnicianDDL(int StaffParentID)
        {
            _actionName = "GetTechnicianDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_StaffLevelTypeDDl";
                        command.Parameters.AddWithValue("@StaffParentID", StaffParentID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    // class
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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


        public async Task<List<CommonDDLModel>> GetHOD_DDL(int CourseID)
        {
            _actionName = "GetHOD_DDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Get_Staff_HOD_DDl";
                        command.Parameters.AddWithValue("@CourseID", CourseID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    // class
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        public async Task<List<CommonDDLModel>> ITIPlacementCompanyMaster(int DepartmentID)
        {
            _actionName = "ITIPlacementCompanyMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ITIPlacementCompanyMaster";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 0)
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

        public async Task<List<CommonDDLModel>> GetSubjectForCitizenSugg(int selectedOption)
        {
            _actionName = "ITIPlacementCompanyMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> studentMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CommonMaster";
                        command.Parameters.AddWithValue("@selectedOption", selectedOption);
                        command.Parameters.AddWithValue("@action", "GetSubjectForCitizenSugg");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable.Rows.Count > 0)
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

        public async Task<DataTable> GetManageHostelWardenRole(string SSOID, int RoleID)
        {
            _actionName = "GetManageHostelWardenRole(string SSOID, int RoleID)";

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Manage_Hostel_WardenRole";
                        command.Parameters.AddWithValue("@SSOID", SSOID);
                        command.Parameters.AddWithValue("@RoleID", RoleID);
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



        public async Task<List<CommonDDLModel>> GetSubjectTheoryParctical(CommonDDLSubjectCodeMasterModel request)
        {
            _actionName = "GetSubjectMasterDDL_New(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<CommonDDLModel> roleMaster = new List<CommonDDLModel>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_SubjectTheoryParctical";
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable != null)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return roleMaster;
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

        public async Task<List<SubjectMaster>> GetBackSubjectList(CommonDDLSubjectCodeMasterModel request)
        {
            _actionName = "GetBackSubjectList(CommonDDLSubjectCodeMasterModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    List<SubjectMaster> roleMaster = new List<SubjectMaster>();
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BackSubjectList";
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@SemesterID", request.SemesterID);
                        command.Parameters.AddWithValue("@StreamID", request.StreamID);
                        command.Parameters.AddWithValue("@StudentExamID", request.StudentExamID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();

                    }
                    if (dataTable != null)
                    {
                        roleMaster = CommonFuncationHelper.ConvertDataTable<List<SubjectMaster>>(dataTable);
                    }
                    return roleMaster;
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



        public async Task<DataTable> GetPrintRollAdmitCardSetting(CollegeMasterSearchModel model)


        {
            _actionName = "GetPrintRollAdmitCardSetting(RequestBaseModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPrintRollAdmitCardSetting";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
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

        public async Task<DataTable> Get_ITIPrintRollAdmitCardSetting(CollegeMasterSearchModel model)


        {
            _actionName = "GetPrintRollAdmitCardSetting(RequestBaseModel)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIPrintRollAdmitCardSetting";
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@InstituteID", model.InstituteID);
                        command.Parameters.AddWithValue("@RoleID", model.RoleID);
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



        public async Task<List<CommonDDLModel>> GetDteCategory_BranchWise(int ID)
        {
            _actionName = "GetCategory_TradeWise(int ID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_DteCategory_BranchWise";
                        command.Parameters.AddWithValue("@ID", ID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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


        public async Task<List<CommonDDLModel>> GetDteEquipment_CategoryWise(int ID)
        {
            _actionName = "GetEquipment_CategoryWise(int ID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_DteEquipment_CategoryWise";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@Branch", 0);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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



        public async Task<List<CommonDDLModel>> GetCategory_TradeWise(int ID)
        {
            _actionName = "GetCategory_TradeWise(int ID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_Category_TradeWise";
                        command.Parameters.AddWithValue("@ID", ID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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


        public async Task<List<CommonDDLModel>> GetITICenterDDL(int ID, int CourseType)
        {
            _actionName = "GetITICenterDDL(int ID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITICenterDDL";
                        command.Parameters.AddWithValue("@EndTermID", ID);
                        command.Parameters.AddWithValue("@CourseType", CourseType);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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



        public async Task<List<CommonDDLModel>> GetEquipment_CategoryWise(int ID)
        {
            _actionName = "GetEquipment_CategoryWise(int ID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_Equipment_CategoryWise";
                        command.Parameters.AddWithValue("@ID", ID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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



        public async Task<List<CommonDDLModel>> GetDDl_StatusForGrivience()
        {
            _actionName = "GetDDl_StatusForGrivience()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDl_StatusForGrivience";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    // class
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        public async Task<DataTable> TradeListTradeTypeWise(ItiTradeSearchModel request)
        {
            _actionName = "TradeListGetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ItiTradeList_CollegeWise";

                        command.Parameters.AddWithValue("@action", "TradeListTradeTypeWise");
                        command.Parameters.AddWithValue("@TradeTypeId", request.TradeTypeId);

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

        public async Task<int> Dummy_SaveAndMoveStudentImages(string json)
        {
            _actionName = "Dummy_SaveAndMoveStudentImages(string json)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_SaveStudentImagesData_dummy";
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@rowJson", json);

                        //out
                        command.Parameters.Add("@retval_ID", SqlDbType.Int);
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;

                        _sqlQuery = command.GetSqlExecutableQuery();
                        result = await command.ExecuteNonQueryAsync();

                        //out
                        result = Convert.ToInt32(command.Parameters["@retval_ID"].Value);
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


        public async Task<List<CommonDDLModel>> GetCategory_BranchWise(int ID)
        {
            _actionName = "GetCategory_BranchWise(int ID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_Category_BranchWise";
                        command.Parameters.AddWithValue("@ID", ID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<List<CommonDDLModel>> GetALLEquipmentCategory()
        {
            _actionName = "GetCategory_BranchWise(int ID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_GetALLEquipmentCategory";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<List<CommonDDLModel>> GetDTEEquipment_CategoryWise(int ID)
        {
            _actionName = "GetDTEEquipment_CategoryWise(int ID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_DteEquipment_CategoryWise";
                        command.Parameters.AddWithValue("@ID", ID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<DataTable> GetCenter_DistrictWise(CenterMasterDDLDataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CenterObserver";
                        command.Parameters.AddWithValue("@Action", "GetCenter_DistrictWise");
                        command.Parameters.AddWithValue("@DistrictID", body.DistrictID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
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

        public async Task<DataTable> GetExamDate(CenterMasterDDLDataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetExamDate";
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
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

        public async Task<DataTable> GetStaff_InstituteWise(StaffMasterDDLDataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_Get_Staff_InstituteWise";
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@RoleID", body.RoleID);
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


        public async Task<DataTable> GetITIStaffInstituteWise(StaffMasterDDLDataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetITIStaffInstituteWise";

                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);

                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
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





        public async Task<DataTable> GetCenterCodeInstituteWise(int ID)
        {
            _actionName = "GetCenterCodeInstituteWise(int ID)";
            return await Task.Run(async () =>
            {

                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCenterCodeInstituteWise";
                        command.Parameters.AddWithValue("@InstituteID", ID);

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


        public async Task<List<CommonDDLModel>> GetddlCenterDownloadOrReceived(string Type)
        {
            _actionName = "GetEquipment_CategoryWise(int ID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetddlCenterDownloadOrReceived";
                        command.Parameters.AddWithValue("@Type", Type);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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





        public async Task<List<CommonDDLModel>> GetDDLDispatchNo(int DepartmentID, int CourseTypeID, int EndTermID)
        {
            _actionName = "GetDDLDispatchNo(int DepartmentID, int CourseTypeID, int EndTermID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "UPS_DDLDispatchNo";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<List<CommonDDLModel>> GetRevalDDLDispatchNo(int DepartmentID, int CourseTypeID, int EndTermID)
        {
            _actionName = "GetRevalDDLDispatchNo(int DepartmentID, int CourseTypeID, int EndTermID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDLRevalDispatchNo";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<List<CommonDDLModel>> GovtInstitute_DistrictWise(int DistrictID, int EndTermId)
        {
            _actionName = "GovtInstitute_DistrictWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CenterObserver";
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);
                        command.Parameters.AddWithValue("@EndTermId", EndTermId);
                        command.Parameters.AddWithValue("@Action", "GovtInstitute_DistrictWise");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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


        public async Task<DataTable> GetCurrentAdmissionSession(int DepartmentId)
        {
            _actionName = "GetCurrentAdmissionSession()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetCurrentAdmissionSession";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentId);
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

        public async Task<List<CommonDDLModel>> GetDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID)
        {
            _actionName = "GetDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "UPS_DDLDispatchCompanyName";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<DataTable> GetITIOptionFormData(ItiTradeSearchModel request)
        {
            _actionName = "TradeListGetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_OptionFormOption";

                        command.Parameters.AddWithValue("@Action", request.action);
                        command.Parameters.AddWithValue("@CollegeID", request.CollegeID);
                        command.Parameters.AddWithValue("@TradeLevel", request.TradeLevel);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@FinancialYear", request.FinancialYear);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@ManagementTypeID", request.ManagementTypeID);
                        command.Parameters.AddWithValue("@Age", request.Age);
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

        public async Task<DataTable> DDL_GroupCode_ExaminerWise(DDL_GroupCode_ExaminerWiseModel request)
        {
            _actionName = "GetDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_GroupCode_ExaminerWise";
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);

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


        public async Task<List<CommonDDLModel>> DDL_CampusPostTypeMaster(string type)
        {
            _actionName = "DDL_CampusPostTypeMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CampusPostTypeMaster";
                        command.Parameters.AddWithValue("@action", type);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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



        public async Task<DataTable> PublicInfo(PublicInfoModel request)
        {
            _actionName = "DDL_CampusPostTypeMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_PublicInfo";
                        command.Parameters.AddWithValue("@PublicInfoId", request.PublicInfoId);
                        command.Parameters.AddWithValue("@DepartmentId", request.DepartmentId);
                        command.Parameters.AddWithValue("@CourseTypeId", request.CourseTypeId);
                        command.Parameters.AddWithValue("@PublicInfoType", request.PublicInfoType);
                        command.Parameters.AddWithValue("@DescriptionEn", request.DescriptionEn);
                        command.Parameters.AddWithValue("@DescriptionHi", request.DescriptionHi);
                        command.Parameters.AddWithValue("@LinkUrl", request.LinkUrl);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);
                        command.Parameters.AddWithValue("@PageNumber", request.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", request.PageSize);
                        command.Parameters.AddWithValue("@SortOrder", request.SortOrder);
                        command.Parameters.AddWithValue("@SortColumn", request.SortColumn);
                        command.Parameters.AddWithValue("@Actoin", request.Actoin);
                        command.Parameters.AddWithValue("@FileName", request.FileName);
                        command.Parameters.AddWithValue("@Dis_FileName", request.Dis_FileName);


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

        public async Task<DataTable> GetLateralQualificationBoard(string ExamType)
        {
            _actionName = "GetLateralQualificationBoard()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_LateralQualificationBoard";
                        //command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
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

        public async Task<DataTable> GetApplicationSubmittedSteps(string AppplicationId)
        {
            _actionName = "GetApplicationSubmittedSteps()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetApplicationSubmittedSteps";
                        command.Parameters.AddWithValue("@AppplicationId", AppplicationId);
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


        public async Task<List<CommonDDLModel>> DDL_OfficeMaster(int DepartmentID, int LevelID)
        {
            _actionName = "DDL_OfficeMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GovtEMDDLOffice";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@LevelID", LevelID);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        public async Task<List<CommonDDLModel>> DDL_PostMaster()
        {
            _actionName = "DDL_OfficeMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_GovtEMDDLPost";
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        public async Task<List<CommonDDLModel>> AllDDlManageByTypeCommanMaster(string type)
        {
            _actionName = "AllDDlManageByTypeCommanMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_AllDDlManageByTypeCommanMaster";
                        command.Parameters.AddWithValue("@Type", type);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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


        public async Task<List<CommonDDLModel>> AllDDlCenterMaster(string type)
        {
            _actionName = "AllDDlCenterMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_AllDDlCenterMaster";
                        //command.Parameters.AddWithValue("@Type", type);
                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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


        public async Task<List<CommonDDLModel>> GetDesignationAndPostMaster()
        {
            _actionName = "GetDesignationAndPostMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetDesignationAndPostMaster";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }

                    //class
                    List<CommonDDLModel> dataModels = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        dataModels = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return dataModels;
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

        public async Task<DataTable> CenterSuperitendentDDL(CenterSuperitendentDDL body)
        {
            _actionName = "CenterSuperitendentDDL()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_CenterSuperitendentDDL";
                        command.Parameters.AddWithValue("@Action", "GetCenterSuperitendentDDL");
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", body.EndTermID);
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
        public async Task<string> CommonVerifierApiSSOIDGetSomeDetails(CommonVerifierApiDataModel request)
        {
            _actionName = "CommonVerifierApiSSOIDGetSomeDetails(VerifierApiDataModel request)";

            try
            {
                using var httpClient = new HttpClient();

                string url = $"{CommonDynamicUrls.SSOIDGetSomeDetailsUrl}{request.SSOID}/{request.appName}/{request.password}";

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"SSO API returned status code {response.StatusCode}");
                }

                string responseContent = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseContent))
                {
                    throw new Exception("SSO API response is empty.");
                }

                return responseContent;
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

                string errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                throw new Exception(errorDetails, ex);
            }
        }

        public async Task<bool> HasValidAge(string dateFrom)
        {
            _actionName = "HasValidAge(string dateFrom)";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_hasvalidage";

                        command.Parameters.AddWithValue("@action", "_checkvalidageForBterAdmission");
                        command.Parameters.AddWithValue("@dateFrom", dateFrom);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count == 0)
                    {
                        return false;
                    }
                    bool.TryParse(dataTable.Rows[0]["IsValidAge"]?.ToString(), out bool isvalid);
                    return isvalid;
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

        public async Task<DataTable> GetDteEquipment_Branch_Wise_CategoryWise(int Category)
        {
            _actionName = "GetDteEquipment_Branch_Wise_CategoryWise()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_DteEquipment_CategoryWise";

                        command.Parameters.AddWithValue("@ID", Category);

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

        public async Task<List<CommonDDLModel>> GetITIDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID)
        {
            _actionName = "GetITIDDLCompanyName(int DepartmentID, int CourseTypeID, int EndTermID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "UPS_ITI_Dispatch_DDLDispatchCompanyName";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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


        public async Task<List<CommonDDLModel>> GetITIddlCenterDownloadOrReceived(string Type)
        {
            _actionName = "GetEquipment_CategoryWise(int ID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_Dispatch_GetddlCenterDownloadOrReceived";
                        command.Parameters.AddWithValue("@Type", Type);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 0)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        #region Dummy for test controller
        public async Task<DataTable> Dummy_GetChangeInvalidPathOfDocuments()
        {
            _actionName = "Dummy_ChangeInvalidPathOfDocuments()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Dummy_Test";

                        command.Parameters.AddWithValue("@action", "_Dummy_GetChangeInvalidPathOfDocuments");

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
        public async Task<DataTable> Dummy_GetMoveFilesFromStudentFolderToNewFolderStructure(List<TestTwoPathNew> model)
        {
            _actionName = "Dummy_GetMoveFilesFromStudentFolderToNewFolderStructure()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_Dummy_Test";

                        command.Parameters.AddWithValue("@action", "_Dummy_GetMoveFilesFromStudentFolderToNewFolderStructure");
                        command.Parameters.AddWithValue("@JsonData", JsonConvert.SerializeObject(model));

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
        #endregion



        public async Task<DataTable> GetTableRecordCount(string Table)
        {

            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GETDATA";

                        command.Parameters.AddWithValue("@Action", "GetTableRecordCount");
                        command.Parameters.AddWithValue("@Table", Table);

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

        public async Task<DataTable> GetTableRows(string Table, string PageNumber, string PageSize)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GETDATA";

                        command.Parameters.AddWithValue("@Action", "GetTableRows");
                        command.Parameters.AddWithValue("@Table", Table);
                        command.Parameters.AddWithValue("@PageNumber", PageNumber);
                        command.Parameters.AddWithValue("@PageSize", PageSize);

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

        public async Task<DataTable> GetTables()
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GETDATA";

                        command.Parameters.AddWithValue("@Action", "GetTables");


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

        public async Task<DataTable> GetSPs()
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GETDATA";

                        command.Parameters.AddWithValue("@Action", "GetSPs");


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

        public async Task<DataTable> GetTableColumn(string Table)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GETDATA";

                        command.Parameters.AddWithValue("@Action", "GetTableColumn");
                        command.Parameters.AddWithValue("@Table", Table);

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

        public async Task<string> AddTableRows(DataTable Table)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        var columns = Table.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

                        var columns1 = Table.Columns.Cast<DataColumn>()
                       .Select(x => "[" + x.ColumnName + "]")
                       .ToArray();

                        int index = 1;
                        string sqlQuery = "";
                        foreach (DataRow data in Table.Rows)
                        {
                            sqlQuery += " INSERT INTO [dbo].[" + Table.TableName + "](" + string.Join(",", columns1) + ")values( ";

                            string rowValues = "";
                            foreach (var col in columns)
                            {
                                rowValues += (rowValues != "" ? "," : "") + "" + (string.IsNullOrEmpty(data[col].ToString()) ? "NULL" : "N'" + checkDate(data[col].ToString().Replace("'", "")) + "'") + "";
                            }

                            sqlQuery += rowValues + ")";
                            //sqlQuery += rowValues.Replace("'", "");
                            ++index;
                        }

                        SetIdentityOffOn(Table.TableName, "ON");

                        command.CommandText = sqlQuery;
                        command.ExecuteNonQuery();

                        SetIdentityOffOn(Table.TableName, "OFF");

                    }
                    return "Success";
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
                    //throw new Exception(errordetails, ex);
                    return "Success";
                }
            });
        }



        protected void SetIdentityOffOn(string table, string OffOn)
        {

            string sqlQuery = "SET IDENTITY_INSERT [dbo].[" + table + "] " + OffOn + "";

            using (var command = _dbContext.CreateCommand())
            {
                try
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sqlQuery;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //lblerror.Text = ex.ToString();
                    //Response.Write("<script language='javascript'>alert(" + ex.ToString() + ")</script>");
                }
                finally
                {

                }
            }
        }

        public async Task<string> TruncateTableRow(string table)
        {

            string sqlQuery = "SELECT * INTO " + table + "_" + DateTime.Now.ToString("ddMMyyyyhhss") + " FROM " + table + " TRUNCATE TABLE " + table + "";
            using (var command = _dbContext.CreateCommand())
            {
                try
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = sqlQuery;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //lblerror.Text = ex.ToString();
                    //Response.Write("<script language='javascript'>alert(" + ex.ToString() + ")</script>");
                }
                finally
                {

                }
                return "Success";
            }
        }



        public string checkDate(string date)

        {
            //22-22-2222

            try
            {

                if (date.Length > 9 && date.Substring(0, 10).Split('-').Length == 3)
                {
                    string dt = date.Split(' ')[0];

                    if (dt.Split('-')[2].Split(' ')[0].Length == 4)
                    {


                        if (date.Split(' ').Length > 1)
                        {
                            return dt.Split('-')[2] + "-" + dt.Split('-')[1] + "-" + dt.Split('-')[0] + " " + date.Split(' ')[1];
                        }
                        else
                        {
                            return dt.Split('-')[2] + "-" + dt.Split('-')[1] + "-" + dt.Split('-')[0];

                        }
                    }
                    else
                        return date;
                }
                else
                    return date;
            }
            catch (Exception ex) { return date; }

        }


        public async Task<List<CommonDDLModel>> GovtITICollege_DistrictWise(int DistrictID, int EndTermId)
        {
            _actionName = "GovtITICollege_DistrictWise(int DistrictID)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_CenterObserver";
                        command.Parameters.AddWithValue("@DistrictID", DistrictID);
                        command.Parameters.AddWithValue("@EndTermID", EndTermId);
                        command.Parameters.AddWithValue("@Action", "GovtITICollege_DistrictWise");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable.Rows.Count > 1)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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

        public async Task<DataTable> ITIGetStaff_InstituteWise(StaffMasterDDLDataModel body)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_CenterObserver";
                        command.Parameters.AddWithValue("@Action", "GetUser_InstituteWise");
                        command.Parameters.AddWithValue("@InstituteID", body.InstituteID);
                        command.Parameters.AddWithValue("@Eng_NonEng", body.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
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


        public async Task<EmitraServiceAndFeeModel> GetEmitraServiceAndFeeData(EmitraServiceAndFeeRequestModel model)
        {
            _actionName = "GetEmitraServiceAndFeeData(EmitraRequestDetailsModel Model)";
            try
            {
                DataTable dt = new DataTable();
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetEmitraAndFeeDetails";

                    command.Parameters.AddWithValue("@action", "_getCollegeEmitraAndFee");
                    command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                    command.Parameters.AddWithValue("@CourseType", model.Eng_NonEng);
                    command.Parameters.AddWithValue("@FinancialYearID", model.FinancialYearID);
                    command.Parameters.AddWithValue("@TypeID", model.TypeID);
                    command.Parameters.AddWithValue("@FeeFor", model.FeeFor);

                    _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                    dt = await command.FillAsync_DataTable();
                }

                // class
                var data = new EmitraServiceAndFeeModel();
                data = CommonFuncationHelper.ConvertDataTable<EmitraServiceAndFeeModel>(dt);
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
        }

        #region Emitra for college
        public async Task<EmitraCollegeTransactionsModel> SaveEmitraCollegeTransation(EmitraCollegeTransactionsModel Model)
        {
            _actionName = "SaveEmitraCollegeTransation(EmitraCollegeTransactionsModel Model)";
            try
            {
                var result = 0;
                var retval_TransactionId = 0;
                using (var command = _dbContext.CreateCommand(true))// true to control transaction
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_SaveCollegeEmitraTransactions";

                    command.Parameters.AddWithValue("@CollegeIdEnc", Model.CollegeIdEnc);
                    command.Parameters.AddWithValue("@CollegeId", Model.CollegeId);
                    command.Parameters.AddWithValue("@KioskID", Model.KioskID);
                    command.Parameters.AddWithValue("@ReceiptNo", Model.ReceiptNo);
                    command.Parameters.AddWithValue("@TokenNo", Model.TokenNo);
                    command.Parameters.AddWithValue("@RequestStatus", Model.RequestStatus);
                    command.Parameters.AddWithValue("@StatusMsg", Model.StatusMsg);
                    command.Parameters.AddWithValue("@RequestString", Model.RequestString);
                    command.Parameters.AddWithValue("@ResponseString", Model.ResponseString);
                    command.Parameters.AddWithValue("@ActId", Model.ActId);
                    command.Parameters.AddWithValue("@TransactionId", Model.TransactionId);
                    command.Parameters.AddWithValue("@PRN", Model.PRN);
                    command.Parameters.AddWithValue("@SSOID", Model.SSOID);
                    command.Parameters.AddWithValue("@CreatedIP", Model.CreatedIP);
                    command.Parameters.AddWithValue("@ServiceID", Model.ServiceID);
                    command.Parameters.AddWithValue("@Amount", Model.Amount);
                    command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                    command.Parameters.AddWithValue("@action", Model.key);
                    command.Parameters.AddWithValue("@IsEmitra", Model.IsEmitra);
                    command.Parameters.AddWithValue("@FeeFor", Model.FeeFor);
                    command.Parameters.AddWithValue("@TransactionNo", Model.TransactionNo);
                    command.Parameters.AddWithValue("@PaidAmount", Model.PaidAmount);
                    command.Parameters.AddWithValue("@CourseType", Model.Eng_NonEng);

                    command.Parameters.Add("@retval_TransactionId", SqlDbType.Int);// out
                    command.Parameters["@retval_TransactionId"].Direction = ParameterDirection.Output;// out

                    _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                    result = await command.ExecuteNonQueryAsync();

                    retval_TransactionId = Convert.ToInt32(command.Parameters["@retval_TransactionId"].Value);// out
                }

                // class
                if (result > 0)
                    Model.TransactionId = retval_TransactionId;
                else
                    Model.TransactionId = 0;
                return Model;
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

        public async Task<Int64> UpdateEmitraCollegePaymentStatus(EmitraCollegeTransactionsModel model)
        {
            _actionName = "UpdateEmitraCollegePaymentStatus(EmitraCollegeTransactionsModel model)";
            try
            {
                Int64 result = 0;
                using (var command = _dbContext.CreateCommand(true))// true to control transaction
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_SaveCollegeEmitraTransactions";

                    command.Parameters.AddWithValue("@action", "_UpdateEmitraPaymentStatus");

                    command.Parameters.AddWithValue("@CollegeIdEnc", model.CollegeIdEnc);
                    command.Parameters.AddWithValue("@TransactionId", model.TRANSACTIONID);
                    command.Parameters.AddWithValue("@PRN", model.PRN);
                    command.Parameters.AddWithValue("@PaidAmount", model.PaidAmount);
                    command.Parameters.AddWithValue("@TokenNo", model.ReceiptNo);
                    command.Parameters.AddWithValue("@StatusMsg", model.RESPONSEMESSAGE);
                    command.Parameters.AddWithValue("@ResponseString", JsonConvert.SerializeObject(model));
                    command.Parameters.AddWithValue("@ReceiptNo", model.ReceiptNo);
                    command.Parameters.AddWithValue("@RequestStatus", model.STATUS);
                    //command.Parameters.AddWithValue("@ExamStudentStatus", model.ExamStudentStatus);

                    command.Parameters.Add("@retval_TransactionId", SqlDbType.Int);// out
                    command.Parameters["@retval_TransactionId"].Direction = ParameterDirection.Output;// out

                    _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                    result = await command.ExecuteNonQueryAsync();

                    result = Convert.ToInt64(command.Parameters["@retval_TransactionId"].Value);// out
                }

                // class
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
        }

        public async Task<DataTable> GetEmitraCollegeTransactionDetails(string PRN)
        {
            _actionName = "GetEmitraCollegeTransactionDetails(string PRN)";
            try
            {
                DataTable dt = new DataTable();
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "USP_GetEmitraCollegeTransactionDetails";

                    command.Parameters.AddWithValue("@PRN", PRN);
                    command.Parameters.AddWithValue("@action", "_GetTransactionDetails");

                    _sqlQuery = command.GetSqlExecutableQuery();// sql query for log
                    dt = await command.FillAsync_DataTable();
                }

                return dt;
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
        #endregion
        public async Task<DataTable> UploadBTEROriginalDocument(UploadOriginalFileWithPathDataModel Model)
        {
            return await Task.Run(async () =>
            {
                _actionName = "UploadBTEROriginalDocument(UploadOriginalFileWithPathDataModel Model)";
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTEROriginalDocument";
                        command.Parameters.AddWithValue("@Operation", "INSERT");
                        command.Parameters.AddWithValue("@DocumentMasterID", Model.DocumentMasterID);
                        command.Parameters.AddWithValue("@FileName", Model.FileName);
                        command.Parameters.AddWithValue("@Dis_FileName", Model.Dis_FileName);
                        command.Parameters.AddWithValue("@ActiveStatus", 1);
                        command.Parameters.AddWithValue("@DeleteStatus", 0);
                        //command.Parameters.AddWithValue("@Eng_NonEng", Model.Eng_NonEng);
                        //command.Parameters.AddWithValue("@DepartmentID", Model.DepartmentID);
                        //command.Parameters.AddWithValue("@EndTermID", Model.EndTermID);

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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

        public async Task<DataTable> GetBTEROriginalDocument(GetBTEROriginalListModel body)
        {
            return await Task.Run(async () =>
            {
                _actionName = "UploadBTEROriginalDocument(UploadOriginalFileWithPathDataModel Model)";
                try
                {


                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetBTEROriginalDocument";

                        List<BTEROriginalModel> ListModel = new List<BTEROriginalModel>();
                        foreach (var item in body.DocumentMasterID)
                        {
                            BTEROriginalModel bTEROriginalModel = new BTEROriginalModel();
                            bTEROriginalModel.DocumentMasterID = item;
                            ListModel.Add(bTEROriginalModel);
                        }

                        // body is list of DocumentMasterID objects                            
                        command.Parameters.AddWithValue("@rowJson", JsonConvert.SerializeObject(ListModel));

                        _sqlQuery = command.GetSqlExecutableQuery();// Get sql query
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


        public async Task<List<CommonDDLModel>> GetCommonMasterDDLStatusByType(string type)
        {
            _actionName = "GetCommonMasterDDLByType(string type)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_StatusCommonMaster";

                        command.Parameters.AddWithValue("@action", "_GetStatusForRollANdEnroll");
                        command.Parameters.AddWithValue("@type", type);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        #region Nodal Center
        public async Task<int> NodalCenterCreate(NodalCenterModel model)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_NODAL_CENTER";

                        command.Parameters.AddWithValue("@NodalId", model.NodalId);
                        command.Parameters.AddWithValue("@DepartmentId", model.DepartmentId);
                        command.Parameters.AddWithValue("@CourseTypeId", model.CourseTypeId);
                        command.Parameters.AddWithValue("@CenterName", model.CenterName);
                        command.Parameters.AddWithValue("@CenterCode", model.CenterCode);
                        command.Parameters.AddWithValue("@OfficerSSOID", model.OfficerSSOID);
                        command.Parameters.AddWithValue("@OfficerName", model.OfficerName);
                        command.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@Address", model.Address);
                        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", model.IPAddress);
                        command.Parameters.AddWithValue("@Action", model.Action);

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
                        result = await command.ExecuteNonQueryAsync();
                        result = Convert.ToInt32(command.Parameters["@Retval"].Value);// out
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

        public async Task<DataTable> NodalCenterList(NodalCenterModel model)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_NODAL_CENTER";

                        command.Parameters.AddWithValue("@NodalId", model.NodalId);
                        command.Parameters.AddWithValue("@DepartmentId", model.DepartmentId);
                        command.Parameters.AddWithValue("@CourseTypeId", model.CourseTypeId);
                        command.Parameters.AddWithValue("@CenterName", model.CenterName);
                        command.Parameters.AddWithValue("@CenterCode", model.CenterCode);
                        command.Parameters.AddWithValue("@OfficerSSOID", model.OfficerSSOID);
                        command.Parameters.AddWithValue("@OfficerName", model.OfficerName);
                        command.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
                        command.Parameters.AddWithValue("@MobileNo", model.MobileNo);
                        command.Parameters.AddWithValue("@Status", model.Status);
                        command.Parameters.AddWithValue("@Action", model.Action);

                        command.Parameters.Add("@Retval", SqlDbType.Int);// out
                        command.Parameters["@Retval"].Direction = ParameterDirection.Output;// out



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

        #endregion


        public async Task<List<CommonDDLModel>> GetNodalCenter(int InstituteID)
        {
            _actionName = "GetCommonMasterDDLByType(string type)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_NodalExamcentef";


                        command.Parameters.AddWithValue("@InstituteID", InstituteID);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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



        public async Task<List<CommonDDLModel>> GetNodalExamCenterDistrict(int District,int EndTermID)
        {
            _actionName = "GetCommonMasterDDLByType(string type)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_NodalExamcentefDistrictWise";


                        command.Parameters.AddWithValue("@District", District);
                        command.Parameters.AddWithValue("@EndTermID", EndTermID);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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



        public async Task<DataTable> DC2ndYear_BranchesDDL(int CourseType, int CoreBranch)
        {
            _actionName = "GetAllData()";
            try
            {
                return await Task.Run(async () =>
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DC2ndYear_BranchesDDL";
                        command.Parameters.AddWithValue("@CourseType", CourseType);
                        command.Parameters.AddWithValue("@CoreBranch", CoreBranch);
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

        public async Task<DataTable> ITI_SemesterMaster(int parameter1 = 0, string parameter2 = "")
        {
            _actionName = "ITI_SemesterMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ITI_SemesterMaster";
                        command.Parameters.AddWithValue("@para1", parameter1);
                        command.Parameters.AddWithValue("@para2", parameter2);
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

        public async Task<DataTable> ExamSessionConfiguration(SessionConfigModel request)
        {
            _actionName = "ExamSessionConfiguration(StreamDDL_InstituteWiseModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ExamSessionConfiguration";
                        command.Parameters.AddWithValue("@FinancialYearID", request.FinancialYearID);
                        command.Parameters.AddWithValue("@FinancialYearName", request.FinancialYearName);
                        command.Parameters.AddWithValue("@EndTermName", request.EndTermName);
                        command.Parameters.AddWithValue("@Month", request.Month);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@IPAddress", request.IPAddress);
                        command.Parameters.AddWithValue("@IsCurrentFY", request.IsCurrentFY);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseTypeID", request.CourseTypeID);
                        command.Parameters.AddWithValue("@ExamType", request.ExamType);
                        command.Parameters.AddWithValue("@Action", request.Action);

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

        public async Task<DataTable> UnPublishData(UnPublishDataModel model)
        {
            _actionName = "UnPublishData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_BTER_UN_PUBLISH";
                        command.Parameters.AddWithValue("@UnPubishBy", model.UnPubishBy);
                        command.Parameters.AddWithValue("@UnPublishReason", model.UnPublishReason);
                        command.Parameters.AddWithValue("@UnPubishAttachment", model.UnPubishAttachment);
                        command.Parameters.AddWithValue("@UnPublishIPAddress", _IPAddress ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@AllotmentMasterId", model.AllotmentMasterId);
                        command.Parameters.AddWithValue("@AcademicYearID", model.AcademicYearID);
                        command.Parameters.AddWithValue("@CourseTypeId", model.CourseTypeId);
                        command.Parameters.AddWithValue("@Action", model.Action);
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

        public async Task<DataTable> GetCollegeDetails(int collegeID)
        {
            _actionName = "GetCollegeDetails()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "SP_GetInstituteDetails";
                        command.Parameters.AddWithValue("@InstituteID", collegeID);

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


        public async Task<DataTable> BterGetBranchbyCollege(BterCollegesSearchModel request)
        {
            _actionName = "BterGetBranchbyCollege()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //if (request.ApplicationID > 0)
                        //{
                        command.CommandText = "USP_BTER_OptionFormOption";
                        //}
                        //else
                        //{
                        //    command.CommandText = "USP_BterCollegesList_DistrictWise";
                        //}
                        command.Parameters.AddWithValue("@action", "_getDatabyDIRECTCollege");
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@ManagementType", request.College_TypeID);
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@StreamType", request.StreamType);

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

        public async Task<List<CommonDDLModel>> GetCommonSubjectDetailsDDL(CommonDDLCommonSubjectModel model)
        {
            _actionName = "GetCommonSubjectDDL(CommonDDLCommonSubjectModel model)";
            return await Task.Run(async () =>
            {
                List<CommonDDLModel> districtMasters = new List<CommonDDLModel>();
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_CommonSubject";

                        command.Parameters.AddWithValue("@action", "CommonSubjectDetails");
                        command.Parameters.AddWithValue("@SemesterID", model.SemesterID);
                        command.Parameters.AddWithValue("@Eng_NonEng", model.Eng_NonEng);
                        command.Parameters.AddWithValue("@DepartmentID", model.DepartmentID);
                        command.Parameters.AddWithValue("@EndTermID", model.EndTermID);
                        command.Parameters.AddWithValue("@CommonSubjectID", model.CommonSubjectID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    if (dataTable != null)
                    {
                        districtMasters = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
                    }
                    return districtMasters;
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


        public async Task<DataTable> GetAllotmentMaster(CommonDDLCommonSubjectModel request)
        {
            _actionName = "BterGetBranchbyCollege()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                      
                        command.CommandText = "USP_ALLOTMNET_MASTER";                       
                        command.Parameters.AddWithValue("@DepartmentId", request.DepartmentID);
                        command.Parameters.AddWithValue("@AcademicYearID", request.FinancialYearID);

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

        public async Task<DataTable> GetExamResultType()
        {
            _actionName = "ResultType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "Usp_Bter_ResultType";
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

        public async Task<DataTable> ItiShiftUnitDDL(int ID = 0, int FinancialYearID = 0, int CourseTypeID = 0, int InstituteID = 0)
        {
            _actionName = "StreamMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_InternalSlidingUnitITI";
                        command.Parameters.AddWithValue("@ID", ID);
                        command.Parameters.AddWithValue("@FinancialYearID", FinancialYearID);
                        command.Parameters.AddWithValue("@CourseTypeID", CourseTypeID);
                        command.Parameters.AddWithValue("@Collegeid", InstituteID);
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



        public async Task<List<CommonDDLModel>> NodalInstituteList(int InstituteID)
        {
            _actionName = "GetCommonMasterDDLByType(string type)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_NodalInstitutelist";


                        command.Parameters.AddWithValue("@InstituteID", InstituteID);


                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        public async Task<DataTable> DDL_GroupCode_ExaminerWise_Reval(DDL_GroupCode_ExaminerWiseModel request)
        {
            _actionName = "DDL_GroupCode_ExaminerWise_Reval(DDL_GroupCode_ExaminerWiseModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_GroupCode_ExaminerWise_Reval";
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);

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

        public async Task<DataTable> StudentListForAdmitCard_CS(StudentAdmitCardDownloadModel request)
        {
            _actionName = "StudentListForAdmitCard_CS(StudentAdmitCardDownloadModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_StudentList_AdmitCardDownload_CS";
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@Eng_NonEng", request.Eng_NonEng);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@RollNo", request.RollNo);
                        command.Parameters.AddWithValue("@UserID", request.UserID);

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

        public async Task<List<CommonDDLModel>> GetITICampusPostMasterDDL(int DepartmentID)
        {
            _actionName = "GetCampusPostMasterDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (
                    var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ITICampusPostMaster";
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        command.Parameters.AddWithValue("@action", "_getCampusPostMasterDDL");

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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

        public async Task<List<CommonDDLModel>> GetITICampusWiseHiringRoleDDL(int campusPostId, int DepartmentID)
        {
            _actionName = "GetITICampusWiseHiringRoleDDL()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_GetITICampusWiseHiringRole";

                        command.Parameters.AddWithValue("@PostID", campusPostId);
                        command.Parameters.AddWithValue("@DepartmentID", DepartmentID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    var data = new List<CommonDDLModel>();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<CommonDDLModel>>(dataTable);
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


        public async Task<DataTable> ITIStreamMasterByCampus(int CampusPostID, int DepartmentID, int EndTermId = 0)
        {
            _actionName = "StreamMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_DDL_ITIStreamMasterByCampus";
                        command.Parameters.AddWithValue("@CampusPostID", CampusPostID);
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

        public async Task<DataTable> GetPublicInfoStatus(int DepartmentId)
        {
            _actionName = "StreamMaster()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetPublicInfoStatus";             
                        command.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                       // command.Parameters.AddWithValue("@FinancialYearId", FinancialYearId);
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

        public async Task<DataTable> DDL_RWHEffectedEndTerm(DDL_RWHEffectedEndTermModel request)
        {
            _actionName = "DDL_RWHEffectedEndTerm(DDL_RWHEffectedEndTermModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "Usp_Bter_RWHEffectedEndTerm";
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", request.CourseType);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@ResultTypeID", request.ResultTypeID);

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

        public async Task<DataTable> GetMigrationType()
        {
            _actionName = "GetMigrationType()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.CommandText = "Usp_GetMigrationType";
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

        public async Task<DataTable> ITI_DeirectAdmissionOptionFormData(ItiTradeSearchModel request)
        {
            _actionName = "ITI_DeirectAdmissionOptionFormData(ItiTradeSearchModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_ITI_DeirectAdmissionOption";

                        command.Parameters.AddWithValue("@Action", request.action);
                        command.Parameters.AddWithValue("@CollegeID", request.CollegeID);
                        command.Parameters.AddWithValue("@TradeLevel", request.TradeLevel);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@FinancialYear", request.FinancialYear);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@ManagementTypeID", request.ManagementTypeID);
                        command.Parameters.AddWithValue("@Age", request.Age);
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

