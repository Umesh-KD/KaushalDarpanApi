using AutoMapper;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.AppointExaminer;
using Kaushal_Darpan.Models.CampusDetailsWeb;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[CustomeAuthorize(EnumRole.Admin,EnumRole.Guest)]
    //[ValidationActionFilter]
    public class CampusDetailsWebController : BaseController
    {
        public override string PageName => "CampusDetailsWebController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CampusDetailsWebController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetAllPost/{post}/{DepartmentID:int}/{StreamID:int}/{FinancialYearID:int}/{InstituteID:int}")]
        public async Task<ApiResult<DataTable>> GetAllPost( int post, int DepartmentID, int StreamID, int FinancialYearID, int InstituteID, [FromQuery] string? CampusFromDate = null, [FromQuery] string? CampusToDate = null)
        {
            ActionName = "GetAllPost(int post = 0)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CampusDetailsWebRepository.GetAllPost( post,DepartmentID, StreamID, FinancialYearID, InstituteID, CampusFromDate, CampusToDate));
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

        //website company data
        [HttpPost("GetAllPlacementCompany")]
        public async Task<ApiResult<DataTable>> GetAllPlacementCompany([FromBody] CampusDetailsWebSearchModel model)
        {
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    var data = await _unitOfWork.CampusDetailsWebRepository.GetAllPlacementCompany(model);
                    if (data.Rows.Count > 0)
                    {
                        result.Data = data;
                        result.State = EnumStatus.Success;
                        result.Message = "Data load successfully .!";

                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = "No record found.!";
                    }
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


        [HttpGet("GetAllPostExNonList/{post}/{DepartmentID:int}/{StreamID:int}/{FinancialYearID:int}/{InstituteID:int}")]
        public async Task<ApiResult<DataTable>> GetAllPostExNonList(int post, int DepartmentID, int StreamID, int FinancialYearID, int InstituteID, [FromQuery] string? CampusFromDate = null, [FromQuery] string? CampusToDate = null)
        {
            ActionName = "GetAllPost(int post = 0)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.CampusDetailsWebRepository.GetAllPostExNonList(post, DepartmentID, StreamID, FinancialYearID, InstituteID, CampusFromDate, CampusToDate)); 
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
    }
}
