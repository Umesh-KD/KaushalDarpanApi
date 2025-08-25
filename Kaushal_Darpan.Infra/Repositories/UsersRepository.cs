using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Kaushal_Darpan.Models.StudentMaster;
using Kaushal_Darpan.Models.UserMaster;
using System.Data;


namespace Kaushal_Darpan.Infra.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        public UsersRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "UsersRepository";
        }

        public async Task<int> CreateUser(Users user)
        {
            _actionName = "CreateUser(Users user)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Trn_Users";
                        command.Parameters.AddWithValue("@action", "_addUser");
                        command.Parameters.AddWithValue("@UserName", user.UserName);
                        command.Parameters.AddWithValue("@UserEmail", user.UserEmail);
                        command.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                        command.Parameters.AddWithValue("@CreatedDate", user.CreatedDate);
                        command.Parameters.AddWithValue("@ModifiedDate", user.ModifiedDate);
                        command.Parameters.AddWithValue("@CreatedBy", user.CreatedBy);
                        command.Parameters.AddWithValue("@ModifiedBy", user.ModifiedBy);
                        command.Parameters.AddWithValue("@IsActive", user.IsActive);
                        command.Parameters.AddWithValue("@IsDelete", user.IsDelete);

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<int> DeleteUserById(Users user)
        {
            _actionName = "DeleteUserById(Users user)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Trn_Users";
                        command.Parameters.AddWithValue("@action", "_deleteUserById");
                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@ModifiedDate", user.ModifiedDate);
                        command.Parameters.AddWithValue("@ModifiedBy", user.ModifiedBy);
                        command.Parameters.AddWithValue("@IsActive", user.IsActive);
                        command.Parameters.AddWithValue("@IsDelete", user.IsDelete);

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<List<Users>> GetAllUser(GenericPaginationSpecification specification)
        {
            _actionName = "GetAllUser(GenericPaginationSpecification specification)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet ds = null;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Users";
                        command.Parameters.AddWithValue("@action", "_GetAllUser");
                        // input pagination
                        command.Parameters.AddWithValue("@PageNumber", specification.PageNumber);
                        command.Parameters.AddWithValue("@PageSize", specification.PageSize);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    // class
                    var data = new List<Users>();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<List<Users>>(ds.Tables[0]);
                        }
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

        public async Task<Users> GetUserById(int id)
        {
            _actionName = "GetUserById(int id)";
            return await Task.Run(async () =>
            {
                _actionName = "GetUserById(int id)";
                try
                {
                    DataSet ds = null;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Users";
                        command.Parameters.AddWithValue("@action", "_GetUserById");
                        command.Parameters.AddWithValue("@Id", id);
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
                        ds = await command.FillAsync();
                    }
                    // class
                    var data = new Users();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<Users>(ds.Tables[0]);
                        }
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

        public async Task<int> UpdateUserById(Users user)
        {
            _actionName = "UpdateUserById(Users user)";
            return await Task.Run(async () =>
            {
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Trn_Users";
                        command.Parameters.AddWithValue("@action", "_UpdateUserById");
                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@UserName", user.UserName);
                        command.Parameters.AddWithValue("@UserEmail", user.UserEmail);
                        command.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                        command.Parameters.AddWithValue("@ModifiedDate", user.ModifiedDate);
                        command.Parameters.AddWithValue("@ModifiedBy", user.ModifiedBy);

                        _sqlQuery = command.GetSqlExecutableQuery();
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

        public async Task<Users> GetUserByUserEmailAndPass(string userEmail, string userPassword)
        {
            _actionName = "GetUserByUserEmailAndPass(string userEmail, string userPassword)";
            return await Task.Run(async () =>
            {
                try
                {
                    DataSet ds = null;
                    using (var command = _dbContext.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Users";
                        command.Parameters.AddWithValue("@action", "_GetUserByUserEmail_Pass");
                        command.Parameters.AddWithValue("@UserEmail", userEmail);
                        command.Parameters.AddWithValue("@UserPassword", userPassword);

                        _sqlQuery = command.GetSqlExecutableQuery();
                        ds = await command.FillAsync();
                    }
                    // class
                    var data = new Users();
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            data = CommonFuncationHelper.ConvertDataTable<Users>(ds.Tables[0]);
                        }
                        if (ds.Tables.Count > 1)
                        {
                            //data.UserRoles = CommonFuncationHelper.ConvertDataTable<List<Roles>>(ds.Tables[1]);
                        }
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


       


    }
}
