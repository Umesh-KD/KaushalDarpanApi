using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.PlacementVerifiedStudentTPOMaster;

using Microsoft.AspNetCore.Mvc;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    [ValidationActionFilter]
    public class PlacementVerifiedStudentTPOController : BaseController
    {
        public override string PageName => "PlacementVerifiedStudentTPOController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PlacementVerifiedStudentTPOController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetAllData")]
        public async Task<ApiResult<List<PlacementVerifiedStudentTPOResponseModel>>> GetAllData([FromBody] PlacementVerifiedStudentTPOSearchModel searchModel)
        {
            ActionName = "GetAllData([FromBody] PlacementVerifiedStudentTPOSearchModel searchModel)";
            var result = new ApiResult<List<PlacementVerifiedStudentTPOResponseModel>>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.PlacementVerifiedStudentTPORepository.GetAllData(searchModel);

                if (result.Data.Count > 0)
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
                _unitOfWork.Dispose();
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

    }
}


