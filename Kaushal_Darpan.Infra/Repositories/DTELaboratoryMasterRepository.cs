using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.CenterMaster;
using Kaushal_Darpan.Models.DTEInventoryModels; 
using Kaushal_Darpan.Models.ItemCategoryMasterModel;
using System.Data; 

namespace Kaushal_Darpan.Infra.Repositories
{
    public class DTELaboratoryMasterRepository : IDTELaboratoryMasterRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        private string _IPAddress;
        public DTELaboratoryMasterRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "DTELaboratoryMasterRepository";
            _IPAddress = CommonFuncationHelper.GetIpAddress();
        }
        public async Task<DataTable> GetAllData(DTELaboratoryDataModel modal)
        {
            _actionName = "GetAllData()";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ActionName", modal.ActionName);
                        command.Parameters.AddWithValue("@Lab_Id", modal.LabID);  
                        command.CommandText = "USP_DTELabMaster_Operation";
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
        public async Task<DTELaboratoryDataModel> GetById(int PK_ID)
        {
            _actionName = "GetById(int PK_ID)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataTable dataTable = new DataTable();
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandText = "select Lab_Id AS LabID,Lab_Name AS LabName,Lab_DepartmentId AS DepartmentID,Lab_BranchId  AS StreamID, "+
                        "Lab_CollegeId AS InstituteID,Lab_TechnicianId AS staffID,Lab_ActiveStatus AS ActiveStatus,Lab_DeleteStatus AS DeleteStatus , " +
                        "Lab_CreatedBy AS CreatedBy,Lab_ModifyBy AS ModifyBy from M_DTELabMaster Where Lab_Id ='" + PK_ID + "' ";

                        _sqlQuery = command.GetSqlExecutableQuery();
                        dataTable = await command.FillAsync_DataTable();
                        }
                    var data = new DTELaboratoryDataModel();
                    if (dataTable != null)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<DTELaboratoryDataModel>(dataTable);
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
        public async Task<bool> SaveData(DTELaboratoryDataModel request)
        {
            _actionName = "SaveData(DTELaboratoryDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        // Set the stored procedure name and type
                        command.CommandText = "USP_DTELabMaster_Operation";
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters with appropriate null handling
                        command.Parameters.AddWithValue("@ActionName", request.ActionName);
                        command.Parameters.AddWithValue("@Lab_Id", request.LabID);
                        command.Parameters.AddWithValue("@Lab_Name", request.LabName);
                        command.Parameters.AddWithValue("@Lab_DepartmentId", request.DepartmentID);
                        command.Parameters.AddWithValue("@Lab_BranchId", request.StreamID);
                        command.Parameters.AddWithValue("@Lab_CollegeId", request.InstituteID);
                        command.Parameters.AddWithValue("@Lab_TechnicianId", request.staffID);
                        command.Parameters.AddWithValue("@Lab_ActiveStatus", request.ActiveStatus);
                        command.Parameters.AddWithValue("@Lab_DeleteStatus", request.DeleteStatus);
                        command.Parameters.AddWithValue("@Lab_CreatedBy", request.CreatedBy);
                        command.Parameters.AddWithValue("@Lab_ModifyBy", request.ModifyBy); 


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
        public async Task<bool> DeleteDataByID(DTELaboratoryDataModel request)
        {
            _actionName = "DeleteDataByID(DTELaboratoryDataModel request)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = await _dbContext.CreateCommandAsync())
                    {
                        command.CommandText = $"update M_DTELabMaster  set Lab_ActiveStatus=0,Lab_DeleteStatus=1,Lab_ModifyBy='{request.ModifyBy} ',Lab_ModifyDate=GETDATE(),Lab_IPAddress='{_IPAddress}'Where Lab_Id = {request.LabID}";

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
                    var errordetails = CommonFuncationHelper.MakeError(errorDesc);
                    throw new Exception(errordetails, ex);
                }
            });
        }
    }
}
