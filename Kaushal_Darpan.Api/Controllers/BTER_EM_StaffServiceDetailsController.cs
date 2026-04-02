using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BTER_EstablishManagement;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class BTER_EM_StaffServiceDetailsController : BaseController
    {
        public override string PageName => "BTER_EM_StaffServiceDetailsController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BTER_EM_StaffServiceDetailsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Save_StaffTrainingDetails")]
        public async Task<ApiResult<int>> Save_StaffTrainingDetails([FromBody] StaffTrainingDetailDataModel body)
        {

            ActionName = "Save_StaffTrainingDetails([FromBody] StaffTrainingDetailDataModel body)";
            var result = new ApiResult<int>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.Save_StaffTrainingDetails(body);
                await _unitOfWork.SaveChangesAsync();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else if (result.Data == -1)
                {
                    result.State = EnumStatus.Warning;
                    result.ErrorMessage = Constants.MSG_SAVE_Duplicate;
                }
                else
                {
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = Constants.MSG_ADD_ERROR;
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

        [HttpPost("StaffTrainingDetails_GetData")]
        public async Task<ApiResult<DataTable>> StaffTrainingDetails_GetData([FromBody] StaffTrainingDetailSearchData body)
        {
            ActionName = "StaffTrainingDetails_GetData([FromBody] StaffTrainingDetailSearchData body)";
            var result = new ApiResult<DataTable>();
            try
            {
                // Pass the entire model to the repository
                result.Data = await _unitOfWork.BTER_EM_StaffServiceDetailsRepository.StaffTrainingDetails_GetData(body);

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
    }
}
