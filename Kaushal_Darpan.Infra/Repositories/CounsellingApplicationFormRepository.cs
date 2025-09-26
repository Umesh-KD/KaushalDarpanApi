using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.ApplicationData;
using Kaushal_Darpan.Models.CounsellingMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class CounsellingApplicationFormRepository : ICounsellingApplicationFormRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public CounsellingApplicationFormRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "CounsellingApplicationFormRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<int> SaveData(CounsellingApplicationFormDataModel request)
        {
            _actionName = "CounsellingApplicationFormDataModel(ApplicationDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_Counselling_PersonalDetails_IU";
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@CandidateID", request.CandidateID);
                        command.Parameters.AddWithValue("@SSOID", request.SSOID);
                        command.Parameters.AddWithValue("@CandidateName", request.CandidateName);
                        command.Parameters.AddWithValue("@FatherName", request.FatherName);
                        command.Parameters.AddWithValue("@MotherName", request.MotherName);
                        command.Parameters.AddWithValue("@GenderId", request.GenderId);
                        command.Parameters.AddWithValue("@DOB", request.DOB);
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        command.Parameters.AddWithValue("@Email", request.Email);
                        command.Parameters.AddWithValue("@Address1", request.Address1);
                        command.Parameters.AddWithValue("@Address2", request.Address2);
                        command.Parameters.AddWithValue("@Address3", request.Address3);
                        command.Parameters.AddWithValue("@StateID", request.StateID);
                        command.Parameters.AddWithValue("@DistrictID", request.DistrictID);
                        command.Parameters.AddWithValue("@BlockID", request.BlockID);
                        command.Parameters.AddWithValue("@Pincode", request.Pincode);
                        command.Parameters.AddWithValue("@AadharNo", request.AadharNo);
                        command.Parameters.AddWithValue("@JanAadharNo", request.JanAadharNo);
                        command.Parameters.AddWithValue("@JanAadharMobileNo", request.JanAadharMobileNo);
                        command.Parameters.AddWithValue("@JanAadharName", request.JanAadharName);
                        command.Parameters.AddWithValue("@JanAadharMemberId", request.JanAadharMemberId);
                        command.Parameters.AddWithValue("@Remark", request.Remark);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@CourseType", request.CourseType);
                        command.Parameters.AddWithValue("@ProfileStatus", request.ProfileStatus);
                        command.Parameters.AddWithValue("@ApplicationNo", request.ApplicationNo);
                        command.Parameters.AddWithValue("@ReligionID", request.ReligionID);
                        command.Parameters.AddWithValue("@NationalityID", request.NationalityID);
                        command.Parameters.AddWithValue("@MaritialID", request.MaritialID);
                        command.Parameters.AddWithValue("@PWDCategoryID", request.PWDCategoryID);
                        command.Parameters.AddWithValue("@IsMinority", request.IsMinority);
                        command.Parameters.AddWithValue("@IsFinalSubmit", request.IsFinalSubmit);
                        command.Parameters.AddWithValue("@DepartmentName", request.DepartmentName);
                        command.Parameters.AddWithValue("@SubmittedStep", request.SubmittedStep);

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
    }
}
