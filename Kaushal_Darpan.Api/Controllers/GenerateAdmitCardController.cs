using AutoMapper;

using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Infra.Repositories;
using Kaushal_Darpan.Models.GenerateAdmitCard;
using Kaushal_Darpan.Models.GenerateEnroll;
using Kaushal_Darpan.Models.PlacementSelectedStudentMaster;
using Kaushal_Darpan.Models.PlacementShortListStudentMaster;
using Kaushal_Darpan.Models.PreExamStudent;
using Kaushal_Darpan.Models.StudentMaster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    //[ValidationActionFilter]
    public class GenerateAdmitCardController : BaseController
    {
        public override string PageName => "GenerateEnrollController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GenerateAdmitCardController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetGenerateAdmitCardData")]
        public async Task<ApiResult<DataTable>> GetGenerateAdmitCardData(GenerateAdmitCardSearchModel model)
        {
            ActionName = "GetGenerateAdmitCardData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.GenerateAdmitCardRepository.GetGenerateAdmitCardData(model));
                result.State = EnumStatus.Success;
                if (result.Data.Rows.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
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
        }


        [HttpPost("GetGenerateAdmitCardDataBulk")]
        public async Task<ApiResult<List<DownloadDataPagingListModel>>> GetGenerateAdmitCardDataBulk(GenerateAdmitCardSearchModel model)
        {
            ActionName = "GetGenerateAdmitCardDataBulk()";
            var result = new ApiResult<List<DownloadDataPagingListModel>>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.GenerateAdmitCardRepository.GetGenerateAdmitCardDataBulk(model));
                result.State = EnumStatus.Success;
                if (result.Data.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
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
        }



        [HttpPost("ITIGetGenerateAdmitCardDataBulk")]
        public async Task<ApiResult<List<DownloadDataPagingListModel>>> ITIGetGenerateAdmitCardDataBulk(GenerateAdmitCardSearchModel model)
        {
            ActionName = "ITIGetGenerateAdmitCardDataBulk()";
            var result = new ApiResult<List<DownloadDataPagingListModel>>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.GenerateAdmitCardRepository.ITIGetGenerateAdmitCardDataBulk(model));
                result.State = EnumStatus.Success;
                if (result.Data.Count == 0)
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }
                result.State = EnumStatus.Success;
                result.Message = "Data load successfully .!";
            }
            catch (System.Exception ex)
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
        }

        [HttpPost("SaveEnrolledData")]
        public async Task<ApiResult<bool>> SaveEnrolledData([FromBody] List<GenerateEnrollMaster> request)
        {
            ActionName = "SaveEnrolledData([FromBody] List<GenerateEnrollMaster> request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<bool>();
                try
                {
                   
                    // Pass the list to the repository for batch update
                    var isSave = await _unitOfWork.GenerateEnrollRepository.SaveEnrolledData(request);
                    _unitOfWork.SaveChanges();  // Commit changes if everything is successful

                    if (isSave == -1)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_NO_DATA_SAVE;
                    }
                    else if (isSave > 0)
                    {
                        result.Data = true;
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_SAVE_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Error;
                        result.ErrorMessage = Constants.MSG_ADD_ERROR;
                    }
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
            });
        }





    


    }
}
