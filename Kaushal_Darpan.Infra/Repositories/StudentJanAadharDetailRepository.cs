using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.StudentJanAadharDetail;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class StudentJanAadharDetailRepository : IStudentJanAadharDetailRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;  

        public StudentJanAadharDetailRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "StudentJanAadharDetailRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }


        public async Task<int> SaveData(ApplicationStudentDatamodel request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_StudentJanAadharDetail_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@StudentName", request.StudentName);
                        command.Parameters.AddWithValue("@FatherName", request.FatherName);
                        command.Parameters.AddWithValue("@MotherName", request.MotherName);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@DOB", request.DOB);
                        command.Parameters.AddWithValue("@CertificateGeneratDate", request.CertificateGeneratDate);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@CasteCertificateNo", request.CasteCertificateNo);
                        command.Parameters.AddWithValue("@CategoryA", request.CategoryA);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@JanAadharNo", request.JanAadharNo);
                        command.Parameters.AddWithValue("@JanAadharMemberID", request.JanAadharMemberID);
                        command.Parameters.AddWithValue("@CategoryPre", request.ENR_ID);
                        command.Parameters.AddWithValue("@IsRajasthani", request.IsRajasthani);
                        command.Parameters.AddWithValue("@CourseType", request.coursetype);

                        command.Parameters.AddWithValue("@TradeLevel", request.TradeLevel);
                        command.Parameters.AddWithValue("@TradeID", request.TradeID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@DirectAdmissionTypeID", request.DirectAdmissionTypeID);
                        command.Parameters.AddWithValue("@BranchID", request.BranchID);
                        command.Parameters.AddWithValue("@ApaarID", request.Apaarid);

                        command.Parameters.AddWithValue("@AadharNo", request.AadharNo);
                        command.Parameters.AddWithValue("@DepartmentName", request.DepartmentName);
                        command.Parameters.AddWithValue("@CreateByRoleID", request.RoleID);


                        command.Parameters.AddWithValue("@adds_addressEng", request.adds_addressEng);
                        command.Parameters.AddWithValue("@adds_districtName", request.adds_districtName);
                        command.Parameters.AddWithValue("@adds_block_city", request.adds_block_city);
                        command.Parameters.AddWithValue("@adds_gp", request.adds_gp);
                        command.Parameters.AddWithValue("@adds_village", request.adds_village);
                        command.Parameters.AddWithValue("@adds_pin", request.adds_pin);
                        command.Parameters.AddWithValue("@adds_addressHnd", request.adds_addressHnd);

                        //command.Parameters.AddWithValue("@IsEws", request.IsEws);

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        // Add the return parameter
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            }); 
        }


        public async Task<int> SaveDTEApplicationData(ApplicationDTEStudentDatamodel request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        if(request.isCorrectMerit == true)
                        {
                            command.CommandText = "USP_BTER_CorrectMerit_PersonalDetails";
                        }
                        else
                        {
                            command.CommandText = "USP_DTEStudentJanAadharDetail_IU";
                        }

                        // Set the stored procedure name and type
                        
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@StudentName", request.StudentName);
                        command.Parameters.AddWithValue("@FatherName", request.FatherName);
                        command.Parameters.AddWithValue("@MotherName", request.MotherName);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@DOB", request.DOB);
                        command.Parameters.AddWithValue("@CertificateGeneratDate", request.CertificateGeneratDate);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@CasteCertificateNo", request.CasteCertificateNo);
                        command.Parameters.AddWithValue("@CategoryA", request.CategoryA);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@JanAadharNo", request.JanAadharNo);
                        command.Parameters.AddWithValue("@JanAadharMemberID", request.JanAadharMemberID);
                        command.Parameters.AddWithValue("@CategoryPre", request.ENR_ID);
                        command.Parameters.AddWithValue("@IsRajasthani", request.IsRajasthani);
                        command.Parameters.AddWithValue("@CourseType", request.coursetype);

                        command.Parameters.AddWithValue("@TradeLevel", request.TradeLevel);
                        command.Parameters.AddWithValue("@TradeID", request.TradeID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        command.Parameters.AddWithValue("@DirectAdmissionTypeID", request.DirectAdmissionTypeID);
                        command.Parameters.AddWithValue("@BranchID", request.BranchID);
                        command.Parameters.AddWithValue("@ApaarID", request.Apaarid);

                        command.Parameters.AddWithValue("@AadharNo", request.AadharNo);
                        command.Parameters.AddWithValue("@DepartmentName", request.DepartmentName);
                        command.Parameters.AddWithValue("@PrefentialCategoryType", request.PrefentialCategoryType);
                        //command.Parameters.AddWithValue("@IsEws", request.IsEws);

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.AddWithValue("@StudentNameHindi", request.StudentNameHindi);
                        command.Parameters.AddWithValue("@FatherNameHindi", request.FatherNameHindi);                      
                        command.Parameters.AddWithValue("@MotherNameHindi", request.MotherNameHindi);    
                        command.Parameters.AddWithValue("@WhatsNumber", request.WhatsNumber);
                        command.Parameters.AddWithValue("@LandlineNumber", request.LandlineNumber);
                        command.Parameters.AddWithValue("@IndentyProff", request.IndentyProff);
                        command.Parameters.AddWithValue("@DetailID", request.DetailID);
                        command.Parameters.AddWithValue("@Maritial", request.Maritial);
                        command.Parameters.AddWithValue("@Religion", request.Religion);
                        command.Parameters.AddWithValue("@Nationality", request.Nationality);
                        command.Parameters.AddWithValue("@CategoryB", request.CategoryB);
                        command.Parameters.AddWithValue("@CategoryC", request.CategoryC);
                        command.Parameters.AddWithValue("@CategoryE", request.CategoryE);
                        //command.Parameters.AddWithValue("@Prefential", request.Prefential);
                        command.Parameters.AddWithValue("@IsMinority", request.IsMinority);
                        command.Parameters.AddWithValue("@IsTSP", request.IsTSP);
                        command.Parameters.AddWithValue("@IsSaharia", request.IsSaharia);
                        command.Parameters.AddWithValue("@TspDistrictID", request.TspDistrictID);
                        command.Parameters.AddWithValue("@IsDevnarayan", request.IsDevnarayan);
                        command.Parameters.AddWithValue("@DevnarayanDistrictID", request.DevnarayanDistrictID);
                        command.Parameters.AddWithValue("@DevnarayanTehsilID", request.DevnarayanTehsilID);
                        command.Parameters.AddWithValue("@TSPTehsilID", request.TSPTehsilID);
                        command.Parameters.AddWithValue("@subCategory", request.subCategory);
                        command.Parameters.AddWithValue("@CategoryD", request.CategoryD);
                        command.Parameters.AddWithValue("@IsPH", request.IsPH);
                        command.Parameters.AddWithValue("@IsKM", request.IsKM);
                        command.Parameters.AddWithValue("@CreateByRoleID", request.RoleID);
                        command.Parameters.AddWithValue("@IsMBCCertificate", request.IsMBCCertificate);

                        // Add the return parameter
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }

        public async Task<int> SaveDTEDirectApplicationData(ApplicationDTEStudentDatamodel request)
        {
            _actionName = "SaveData(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        if (request.isCorrectMerit == true)
                        {
                            command.CommandText = "USP_BTER_CorrectMerit_PersonalDetails";
                        }
                        else
                        {
                            command.CommandText = "USP_DTEDirect_StudentJanAadharDetail_IU";
                        }

                        // Set the stored procedure name and type

                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ApplicationID", request.ApplicationID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@StudentName", request.StudentName);
                        command.Parameters.AddWithValue("@FatherName", request.FatherName);
                        command.Parameters.AddWithValue("@MotherName", request.MotherName);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@DOB", request.DOB);
                        command.Parameters.AddWithValue("@CertificateGeneratDate", request.CertificateGeneratDate);
                        command.Parameters.AddWithValue("@Gender", request.Gender);
                        command.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                        command.Parameters.AddWithValue("@CasteCertificateNo", request.CasteCertificateNo);
                        command.Parameters.AddWithValue("@CategoryA", request.CategoryA);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@JanAadharNo", request.JanAadharNo);
                        command.Parameters.AddWithValue("@JanAadharMemberID", request.JanAadharMemberID);
                        command.Parameters.AddWithValue("@CategoryPre", request.ENR_ID);
                        command.Parameters.AddWithValue("@IsRajasthani", request.IsRajasthani);
                        command.Parameters.AddWithValue("@CourseType", request.coursetype);

                        command.Parameters.AddWithValue("@TradeLevel", request.TradeLevel);
                        command.Parameters.AddWithValue("@TradeID", request.TradeID);
                        command.Parameters.AddWithValue("@InstituteID", request.InstituteID);
                        //command.Parameters.AddWithValue("@DirectAdmissionTypeID", request.DirectAdmissionTypeID);
                        command.Parameters.AddWithValue("@DirectAdmissionTypeID", "1");
                        command.Parameters.AddWithValue("@BranchID", request.BranchID);
                        command.Parameters.AddWithValue("@ApaarID", request.Apaarid);

                        command.Parameters.AddWithValue("@AadharNo", request.AadharNo);
                        command.Parameters.AddWithValue("@DepartmentName", request.DepartmentName);
                        command.Parameters.AddWithValue("@PrefentialCategoryType", request.PrefentialCategoryType);
                        //command.Parameters.AddWithValue("@IsEws", request.IsEws);

                        // Add IP Address parameter
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);

                        command.Parameters.AddWithValue("@StudentNameHindi", request.StudentNameHindi);
                        command.Parameters.AddWithValue("@FatherNameHindi", request.FatherNameHindi);
                        command.Parameters.AddWithValue("@MotherNameHindi", request.MotherNameHindi);
                        command.Parameters.AddWithValue("@WhatsNumber", request.WhatsNumber);
                        command.Parameters.AddWithValue("@LandlineNumber", request.LandlineNumber);
                        command.Parameters.AddWithValue("@IndentyProff", request.IndentyProff);
                        command.Parameters.AddWithValue("@DetailID", request.DetailID);
                        command.Parameters.AddWithValue("@Maritial", request.Maritial);
                        command.Parameters.AddWithValue("@Religion", request.Religion);
                        command.Parameters.AddWithValue("@Nationality", request.Nationality);
                        command.Parameters.AddWithValue("@CategoryB", request.CategoryB);
                        command.Parameters.AddWithValue("@CategoryC", request.CategoryC);
                        command.Parameters.AddWithValue("@CategoryE", request.CategoryE);
                        //command.Parameters.AddWithValue("@Prefential", request.Prefential);
                        command.Parameters.AddWithValue("@IsMinority", request.IsMinority);
                        command.Parameters.AddWithValue("@IsTSP", request.IsTSP);
                        command.Parameters.AddWithValue("@IsSaharia", request.IsSaharia);
                        command.Parameters.AddWithValue("@TspDistrictID", request.TspDistrictID);
                        command.Parameters.AddWithValue("@IsDevnarayan", request.IsDevnarayan);
                        command.Parameters.AddWithValue("@DevnarayanDistrictID", request.DevnarayanDistrictID);
                        command.Parameters.AddWithValue("@DevnarayanTehsilID", request.DevnarayanTehsilID);
                        command.Parameters.AddWithValue("@TSPTehsilID", request.TSPTehsilID);
                        command.Parameters.AddWithValue("@subCategory", request.subCategory);
                        command.Parameters.AddWithValue("@CategoryD", request.CategoryD);
                        command.Parameters.AddWithValue("@IsPH", request.IsPH);
                        command.Parameters.AddWithValue("@IsKM", request.IsKM);
                        command.Parameters.AddWithValue("@CreateByRoleID", request.RoleID);
                        command.Parameters.AddWithValue("@IsMBCCertificate", request.IsMBCCertificate);

                        // Add the return parameter
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
                    var errorDetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errorDetails, ex);
                }
            });
        }

        public async Task<DataTable> GetApplicationId(SearchApplicationStudentDatamodel body)
        {
            _actionName = "GetApplicationId()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "USP_GetDataBySSODepartment";
                        command.Parameters.AddWithValue("@DepartmentID", body.DepartmentID);
                        command.Parameters.AddWithValue("@SSOID", body.SSOID);
                        command.Parameters.AddWithValue("@JanAadharMemberId", body.JanAadharMemberId);
                        command.Parameters.AddWithValue("@Action", body.Action);
                        command.Parameters.AddWithValue("@CourseTypeID", body.CourseTypeID);
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
