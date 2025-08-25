using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Api.Models;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    [ValidationActionFilter]
    public class ProductController : BaseController
    {
        public override string PageName => "ProductController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllProduct")]
        public async Task<ApiResult<List<ProductDetailsModel>>> GetAllProduct([FromBody] GenericPaginationSpecification specification)
        {

            return await Task.Run(async () =>
            {
                var result = new ApiResult<List<ProductDetailsModel>>();
                try
                {
                    var data = await _unitOfWork.Products.GetAllProduct(specification);
                    var modelData = _mapper.Map<List<ProductDetailsModel>>(data);

                    // pagination
                    var paginationData = CommonFuncationHelper.GetPaginationData(data, data.FirstOrDefault()?.TotalRecord, specification.PageNumber, specification.PageSize);
                    Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(paginationData));

                    result.State = EnumStatus.Success;
                    result.Data = modelData;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpGet("GetProductById")]
        public async Task<ApiResult<ProductDetailsModel>> GetProductById([FromQuery] int productId)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<ProductDetailsModel>();
                try
                {
                    var data = await _unitOfWork.Products.GetProductById(productId);
                    var modelData = _mapper.Map<ProductDetailsModel>(data);

                    result.State = EnumStatus.Success;
                    result.Data = modelData;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("CreateProduct")]
        public async Task<ApiResult<int>> CreateProduct([FromBody] ProductDetailsCreateModel model)
        {
            return await Task.Run(async () =>
            {
                ActionName = "CreateProduct([FromBody] ProductDetailsCreateModel model)";
                var result = new ApiResult<int>();
                try
                {
                    var entityData = _mapper.Map<ProductDetails>(model);
                    var dataId = await _unitOfWork.Products.CreateProduct(entityData);
                    //test
                    var pc = new List<ProductChildDetails>
                    {
                        new ProductChildDetails
                        {
                            ProductId = dataId,
                            ProductChildName = "sdsad1"
                        },
                        new ProductChildDetails
                        {
                            ProductId = dataId,
                            ProductChildName = "dvd2"
                        },
                        new ProductChildDetails
                        {
                            ProductId = dataId,
                            ProductChildName = "fgfd3"
                        },
                    };

                    var data1 = await _unitOfWork.Products.AddProductChild(pc);
                    _unitOfWork.SaveChanges();

                    result.State = EnumStatus.Success;
                    result.Data = data1;
                    return result;
                }
                catch (Exception ex)
                {
                    _unitOfWork.Dispose();
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                    // write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                }
                return result;
            });
        }

        /*put is used to full update the existing record*/
        [HttpPut("UpdateProduct")]
        public async Task<ApiResult<int>> UpdateProduct([FromBody] ProductDetailsCreateModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {

                    var entityModel = _mapper.Map<ProductDetails>(model);
                    var data = await _unitOfWork.Products.UpdateProductById(entityModel);
                    _unitOfWork.SaveChanges();

                    result.State = EnumStatus.Success;
                    result.Data = data;
                    return result;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        /*delete is used to remove the existing record*/
        [HttpDelete("DeleteProduct")]
        public async Task<ApiResult<int>> DeleteProduct([FromQuery] int productId, int updatedBy)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var productDetails = new ProductDetails
                    {
                        Id = productId,
                        ModifiedBy = updatedBy,
                        ModifiedDate = DateTime.Now,
                        IsActive = false,
                        IsDelete = true,
                    };
                    var data = await _unitOfWork.Products.DeleteProductById(productDetails);
                    _unitOfWork.SaveChanges();

                    result.State = EnumStatus.Success;
                    result.Data = data;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("IsActiveProduct")]
        public async Task<ApiResult<int>> IsActiveProduct([FromBody] ProductDetailsIsActiveModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<int>();
                try
                {
                    var entityData = _mapper.Map<ProductDetails>(model);
                    entityData.ModifiedDate = DateTime.Now;
                    var data = await _unitOfWork.Products.DeleteProductById(entityData);
                    _unitOfWork.SaveChanges();

                    result.State = EnumStatus.Success;
                    result.Data = data;
                }
                catch (Exception ex)
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

    }
}
