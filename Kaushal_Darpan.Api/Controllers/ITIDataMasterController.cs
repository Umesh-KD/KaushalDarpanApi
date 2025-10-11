using AutoMapper;
using ExcelDataReader;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.BTEReatsDistributionsMaster;
using Kaushal_Darpan.Models.CompanyMaster;
using Kaushal_Darpan.Models.ITI_SeatIntakeMaster;
using Kaushal_Darpan.Models.ITI_DataMasterModel;
using Kaushal_Darpan.Models.ITIApplication;
using Kaushal_Darpan.Models.MenuMaster;
using Kaushal_Darpan.Models.TSPAreaMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class ITIDataMasterController : BaseController
    {
        public override string PageName => "ITIDataMasterController   ";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIDataMasterController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

      
        [HttpPost("GetAllData")]
        public async Task<ApiResult<string>> GetAllData(DataListSearchModel request)
        {
            ActionName = "GetAllData(SeatIntakeSearchModel request)";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                   var data = await _unitOfWork.ITIDataMasterRepository.GetAllData(request);
                    if (data != null)
                    {
                        //var mappedData = _mapper.Map<DataTable>(data);
                        //result.Data = mappedData.rows[0];
                        result.Data = data.Rows[0]["data"].ToString();
                        result.State = EnumStatus.Success;
                        result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }


                    //result.Data = await _unitOfWork.ITIDataMasterRepository.GetAllData(request);
                    //if (result.Data.Rows.Count > 0)
                    //{
                    //    result.State = EnumStatus.Success;
                    //    result.Message = Constants.MSG_DATA_LOAD_SUCCESS;
                    //}
                    //else
                    //{
                    //    result.State = EnumStatus.Warning;
                    //    result.Message = Constants.MSG_DATA_NOT_FOUND;
                    //}




                }
                catch (Exception ex)
                {
                    await _unitOfWork.DisposeAsync();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

  


    
    }
}
