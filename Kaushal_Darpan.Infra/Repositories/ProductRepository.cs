using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Helper;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBContext _dbContext;
        private readonly string _pageName;
        private string _actionName;
        private string _sqlQuery;
        public ProductRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _pageName = "UsersRepository";
        }

        public async Task<int> CreateProduct(ProductDetails productDetails)
        {
            return await Task.Run(async () =>
            {
                _actionName = "CreateProduct(ProductDetails productDetails)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Trn_Productdetails";
                        command.Parameters.AddWithValue("@action", "_addProduct");
                        command.Parameters.AddWithValue("@ProductName", productDetails.ProductName);
                        command.Parameters.AddWithValue("@ProductDescription", productDetails.ProductDescription);
                        command.Parameters.AddWithValue("@ProductPrice", productDetails.ProductPrice);
                        command.Parameters.AddWithValue("@ProductStock", productDetails.ProductStock);
                        command.Parameters.AddWithValue("@CreatedDate", productDetails.CreatedDate);
                        command.Parameters.AddWithValue("@ModifiedDate", productDetails.ModifiedDate);
                        command.Parameters.AddWithValue("@CreatedBy", productDetails.CreatedBy);
                        command.Parameters.AddWithValue("@ModifiedBy", productDetails.ModifiedBy);
                        command.Parameters.AddWithValue("@IsActive", productDetails.IsActive);
                        command.Parameters.AddWithValue("@IsDelete", productDetails.IsDelete);
                        command.Parameters.Add("@retval_ID", SqlDbType.Int);// out
                        command.Parameters["@retval_ID"].Direction = ParameterDirection.Output;// out
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
                        await command.ExecuteNonQueryAsync();
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

        public async Task<int> AddProductChild(List<ProductChildDetails> productChildDetails)
        {
            return await Task.Run(async () =>
            {
                _actionName = "AddProductChild(ProductChildDetails productChildDetails)";
                try
                {
                    int result = 0;
                    using (var command = _dbContext.CreateCommand(true))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "usp_Trn_Productdetails";
                        command.Parameters.AddWithValue("@action", "_addProductChild");
                        command.Parameters.AddWithValue("@ProductChild_Json", JsonConvert.SerializeObject(productChildDetails));
                        _sqlQuery = command.GetSqlExecutableQuery();// sql query
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

        public async Task<int> DeleteProductById(ProductDetails productDetails)
        {
            return await Task.Run(async () =>
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_Trn_Productdetails";
                    command.Parameters.AddWithValue("@action", "_delProductById");
                    command.Parameters.AddWithValue("@id", productDetails.Id);
                    command.Parameters.AddWithValue("@ModifiedDate", productDetails.ModifiedDate);
                    command.Parameters.AddWithValue("@ModifiedBy", productDetails.ModifiedBy);
                    command.Parameters.AddWithValue("@IsActive", productDetails.IsActive);
                    command.Parameters.AddWithValue("@IsDelete", productDetails.IsDelete);
                    result = await command.ExecuteNonQueryAsync();
                }
                return result;
            });
        }

        public async Task<List<ProductDetails>> GetAllProduct(GenericPaginationSpecification specification)
        {
            return await Task.Run(async () =>
            {
                DataSet ds = null;
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_Productdetails";
                    command.Parameters.AddWithValue("@action", "_GetAllProduct");
                    // input pagination
                    command.Parameters.AddWithValue("@PageNumber", specification.PageNumber);
                    command.Parameters.AddWithValue("@PageSize", specification.PageSize);
                    ds = await command.FillAsync();
                }
                // class
                var data = new List<ProductDetails>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<List<ProductDetails>>(ds.Tables[0]);
                    }
                }
                return data;
            });
        }

        public async Task<ProductDetails> GetProductById(int productId)
        {
            return await Task.Run(async () =>
            {
                DataSet ds = null;
                using (var command = _dbContext.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_Productdetails";
                    command.Parameters.AddWithValue("@action", "_GetProductById");
                    command.Parameters.AddWithValue("@Id", productId);
                    ds = await command.FillAsync();
                }
                // class
                var data = new ProductDetails();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        data = CommonFuncationHelper.ConvertDataTable<ProductDetails>(ds.Tables[0]);
                    }
                }
                return data;
            });
        }

        public async Task<int> UpdateProductById(ProductDetails productDetails)
        {
            return await Task.Run(async () =>
            {
                int result = 0;
                using (var command = _dbContext.CreateCommand(true))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_Trn_Productdetails";
                    command.Parameters.AddWithValue("@action", "_UpdAllProductById");
                    command.Parameters.AddWithValue("@Id", productDetails.Id);
                    command.Parameters.AddWithValue("@ProductName", productDetails.ProductName);
                    command.Parameters.AddWithValue("@ProductDescription", productDetails.ProductDescription);
                    command.Parameters.AddWithValue("@ProductPrice", productDetails.ProductPrice);
                    command.Parameters.AddWithValue("@ProductStock", productDetails.ProductStock);
                    command.Parameters.AddWithValue("@ModifiedDate", productDetails.ModifiedDate);
                    command.Parameters.AddWithValue("@ModifiedBy", productDetails.ModifiedBy);
                    result = await command.ExecuteNonQueryAsync();
                }
                return result;
            });
        }
    }
}
