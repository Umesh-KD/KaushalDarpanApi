using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CollegeWiseScholarship;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITICollegeWiseScholarship;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class ITICollegeWiseScholarshipController : BaseController
    {
        public override string PageName => "ITICollegeWiseScholarshipController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITICollegeWiseScholarshipController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
         
        [HttpPost("GetCollegeWiseScholarshipList")]
        public async Task<ApiResult<DataTable>> GetCollegeWiseScholarshipList([FromBody] ITICollegeWiseScholarshipSearchModel body)
        {
            ActionName = "GetCollegeWiseScholarshipList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                //result.Data = await _unitOfWork.CollegeWiseScholarshipRepository.GetCollegeWiseScholarshipList(body);
                result.Data = await _unitOfWork.ITICollegeWiseScholarshipRepository.GetCollegeWiseScholarshipList(body);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }


        [HttpGet("GetSchemeList")]
        public async Task<ApiResult<DataTable>> GetSchemeList()
        {
            ActionName = "GetSchemeList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITICollegeWiseScholarshipRepository.GetSchemeList();

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }


        [HttpGet("GetTypeList")]
        public async Task<ApiResult<DataTable>> GetTypeList()
        {
            ActionName = "GetTypeList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITICollegeWiseScholarshipRepository.GetTypeList();

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }

        [HttpGet("GetDetailsById/{id}")]
        public async Task<ApiResult<DataTable>> GetDetailsById(int id)
        {
            ActionName = "GetTypeList()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.ITICollegeWiseScholarshipRepository.GetDetailList(id);

                if (result.Data.Rows.Count > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
            }
            catch (Exception ex)
            {
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;

                // Log the error
                await _unitOfWork.DisposeAsync();
                var nex = new NewException
                {
                    PageName = PageName,
                    ActionName = ActionName,
                    Ex = ex,
                };
                await CreateErrorLog(nex, _unitOfWork);
            }
            return result;
        }


       

        [HttpPost("SaveCollegeWiseScholarshipDetails")]
        public async Task<ApiResult<bool>> SaveCollegeWiseScholarshipDetails([FromBody] List<SaveITICollegeWiseScholershipDetails> request)
        {
            ActionName = "SaveITICollegeWiseScholarshipDetails([FromBody] CompanyMaster_Action request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {

                    if (!ModelState.IsValid)
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = "Validation failed!";
                        return result;
                    }


                    result.Data = await _unitOfWork.ITICollegeWiseScholarshipRepository.SaveCollegeWiseScholarshipDetails(request);
                    await _unitOfWork.SaveChangesAsync();
                    if (result.Data)
                    {
                        result.State = EnumStatus.Success;
                        result.Message = "Updated successfully .!";
                    }
                    else
                    {
                        result.State = EnumStatus.Error;

                        result.ErrorMessage = "There was an error updating data.!";
                    }
                }
                catch (System.Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
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
         
    }


}


