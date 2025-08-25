using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.CertificateDownload;
using Kaushal_Darpan.Models.CheckListModel;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]

    public class CheckListController : BaseController
    {
        public override string PageName => "CheckList";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CheckListController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetCheckListQuestion")]
        public async Task<ApiResult<DataTable>> GetCheckListQuestion([FromBody] CheckListSearchModel body)
        {
            ActionName = "GetCheckListQuestion()";
            var result = new ApiResult<DataTable>();
            try
            {

                // Pass the entire model to the repository
                result.Data = await _unitOfWork.CheckListRepository.GetCheckListQuestion(body);

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



        [HttpPost("SaveChecklistAnswers")]
        public async Task<ApiResult<int>> SaveChecklistAnswers([FromBody] ChecklistAnswerRequest request)
        {
            var result = new ApiResult<int>();
            try
            {
                result.Data = await _unitOfWork.CheckListRepository.SaveChecklistAnswers(request);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                result.Data = 1;
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
                // Log the error
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


        [HttpPost("SaveChecklistAnswers_ITI")]
        public async Task<ApiResult<int>> SaveChecklistAnswers_ITI([FromBody] ChecklistAnswerRequest request)
        {
            var result = new ApiResult<int>();
            try
            {
                result.Data = await _unitOfWork.CheckListRepository.SaveChecklistAnswers_ITI(request);
                _unitOfWork.SaveChanges();
                if (result.Data > 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = Constants.MSG_SAVE_SUCCESS;
                }
                else
                {
                    result.State = EnumStatus.Warning;
                    result.Message = Constants.MSG_DATA_NOT_FOUND;
                }
                result.Data = 1;
            }
            catch (Exception ex)
            {
                _unitOfWork.Dispose();
                result.State = EnumStatus.Error;
                result.ErrorMessage = ex.Message;
                // Log the error
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

