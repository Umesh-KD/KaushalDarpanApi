using AspNetCore.Reporting;
using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using System.Security.Permissions;
using System.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kaushal_Darpan.Models.GenerateEnroll;
using System.Diagnostics;
using Kaushal_Darpan.Models.ReportFeesTransactionModel;
using static Kaushal_Darpan.Models.ReportFeesTransactionModel.ReportFeesTransaction;


namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[CustomeAuthorize]
    //[ValidationActionFilter]
    public class ITIReportFeesTransactionController : BaseController
    {
        public override string PageName => "ITIReportFeesTransactionController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIReportFeesTransactionController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #region Student Fees Transaction
        [HttpPost("GetITIStudentFeesTransactionHistoryRpt")]
        public async Task<ApiResult<DataTable>> GetITIStudentFeesTransactionHistoryRpt([FromBody] ITIReportFeesTransactionSearchModel body)
        {
            ActionName = "GetStudentFeesTransactionHistoryRpt()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIReportFeesTransactionRepository.GetITIStudentFeesTransactionHistoryRpt(body));
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


        #endregion

    }
}


