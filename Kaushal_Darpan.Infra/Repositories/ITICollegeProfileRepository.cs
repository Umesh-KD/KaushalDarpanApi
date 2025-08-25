using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CollegeMaster;

namespace Kaushal_Darpan.Infra.Repositories
{

    public class ITICollegeProfileRepository : IITICollegeProfileRepository
    {

        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public ITICollegeProfileRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "ITICollegeProfileRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }

        public async Task<ITICollegeProfileDataModel> GetById(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = _dbContext.CreateCommand())
                    {
                        //command.CommandText = " select * from ITI_Colleges Where Id='" + PK_ID + "' ";
                        command.CommandText = "USP_ITI_CollegeProfile_GetById";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CollegeID", PK_ID);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                    }
                    
                    var data = new ITICollegeProfileDataModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ITICollegeProfileDataModel>(dataTable);
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
        public async Task<bool> SaveData(ITICollegeProfileDataModel request)
        {
            _actionName = "SaveData(ITICollegeProfileDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_ITI_CollegeProfile_IU";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", request.Id);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@website", request.website);
                        command.Parameters.AddWithValue("@SecretaryName", request.SecretaryName);
                        command.Parameters.AddWithValue("@SecretaryMobile", request.SecretaryMobile);
                        command.Parameters.AddWithValue("@ItiEmail", request.ItiEmail);
                        command.Parameters.AddWithValue("@Ssoid", request.Ssoid);
                        command.Parameters.AddWithValue("@PrincipalName", request.PrincipalName);
                        command.Parameters.AddWithValue("@PrincipalMobile", request.PrincipalMobile);
                        command.Parameters.AddWithValue("@Dis_Name", request.Dis_Name);
                        command.Parameters.AddWithValue("@Logo", request.Logo);
                        command.Parameters.AddWithValue("@InstitutionCategoryId", request.InstitutionCategoryId);
                        command.Parameters.AddWithValue("@DivisionId", request.DivisionId);
                        command.Parameters.AddWithValue("@DistrictId", request.DistrictId);
                        command.Parameters.AddWithValue("@SubDivisionId", request.SubDivisionId);
                        command.Parameters.AddWithValue("@TehsilId", request.TehsilId);
                        command.Parameters.AddWithValue("@UrbanRural", request.UrbanRural);
                        command.Parameters.AddWithValue("@ParliamentId", request.ParliamentId);
                        command.Parameters.AddWithValue("@AssemblyId", request.AssemblyId);
                        command.Parameters.AddWithValue("@Pincode", request.Pincode);
                        command.Parameters.AddWithValue("@PlotNo", request.PlotNo);
                        command.Parameters.AddWithValue("@Address", request.Address);
                        command.Parameters.AddWithValue("@Street", request.Street);
                        command.Parameters.AddWithValue("@Area", request.Area);
                        command.Parameters.AddWithValue("@Landmark", request.Landmark);
                        command.Parameters.AddWithValue("@ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@RTS", request.RTS);
                        command.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@ModifyBy", request.ModifyBy);
                        command.Parameters.AddWithValue("@ModifyDate", request.ModifyDate);
                        command.Parameters.AddWithValue("@IPAddress", _IPAddress);
                        command.Parameters.AddWithValue("@DepartmentID", request.DepartmentID);
                        command.Parameters.AddWithValue("@FinancialYearId", request.FinancialYearId);
                        command.Parameters.AddWithValue("@EndTermID", request.EndTermID);
                        command.Parameters.AddWithValue("@Altemail", request.Email);
                        command.Parameters.AddWithValue("@AdministrativeId", request.AdministrativeId);
                        command.Parameters.AddWithValue("@NagarNigamName", request.NagarNigamName);
                        command.Parameters.AddWithValue("@NagarPalikaName", request.NagarPalikaName);
                        command.Parameters.AddWithValue("@NagarParishadName", request.NagarParishadName);
                        command.Parameters.AddWithValue("@CantonmentBoardName", request.CantonmentBoardName);
                        command.Parameters.AddWithValue("@CityID", request.CityID);
                        command.Parameters.AddWithValue("@Ward", request.Ward);
                        command.Parameters.AddWithValue("@VillageID", request.VillageId);
                        command.Parameters.AddWithValue("@GramPanchayatID", request.GrampanchayatId);
                        command.Parameters.AddWithValue("@PanchayatSamitiID", request.PanchayatsamityId);
                        command.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        // Execute the command
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

    }
}
