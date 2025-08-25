using AspNetCore.Reporting;
using AutoMapper;
using Kaushal_Darpan.Api.Code.Attribute;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.Examiners;
using Kaushal_Darpan.Models.HrMaster;
using Kaushal_Darpan.Models.MarksheetDownloadModel;
using Kaushal_Darpan.Models.PaperSetter;
using Kaushal_Darpan.Models.SetExamAttendanceMaster;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kaushal_Darpan.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidationActionFilter]
    public class MarksheetDownloadController : BaseController
    {
        public override string PageName => "MarksheetDownloadController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MarksheetDownloadController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetStudents")]
        public async Task<ApiResult<DataTable>> GetStudents([FromBody] MarksheetDownloadSearchModel body)
        {
            ActionName = "GetTeacherForExaminer()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.GetStudents(body));
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

        [HttpPost("MarksheetLetterDownload")]
        public async Task<ApiResult<string>> MarksheetLetterDownload([FromBody] MarksheetDownloadSearchModel model)
        {
            ActionName = "MarksheetLetterDownload([FromBody] MarksheetDownloadSearchModel model)";
            var folderPath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    
                    var data = await _unitOfWork.MarksheetDownloadRepository.MarksheetLetterDownload(model);
                    if (data != null)
                    {
                        var fileName = $"MarksheetLetter_{data.Tables[0].Rows[0]["InstituteCode"]}_{data.Tables[0].Rows[0]["SemesterCode"]}_{data.Tables[0].Rows[0]["EndTermName"]}_{System.DateTime.Now:MMMddyyyyhhmmssffffff}.pdf";
                        string filepath = $"{ConfigurationHelper.StaticFileRootPath}{Constants.ReportsFolder}/{fileName}";
                        string rdlcpath = $"{ConfigurationHelper.RootPath}{Constants.RDLCFolderBTER}/MarksheetLetter.rdlc";


                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        LocalReport localReport = new LocalReport(rdlcpath);

                        localReport.AddDataSource("MarksheetLetterDetails", data.Tables[0]);
                        localReport.AddDataSource("MarksheetLetterTableDetails", data.Tables[1]);
                        localReport.AddDataSource("MarksheetLetterAdditionalDetails", data.Tables[2]);
                        var reportResult = localReport.Execute(RenderType.Pdf);

                        //check file exists
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        System.IO.File.WriteAllBytes(filepath, reportResult.MainStream);

                        result.Data = fileName;
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
                    _unitOfWork.Dispose();
                    // Write error log
                    var nex = new NewException
                    {
                        PageName = PageName,
                        ActionName = ActionName,
                        Ex = ex,
                    };
                    await CreateErrorLog(nex, _unitOfWork);
                    //
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }

        [HttpPost("Get5thSemBackPaperReport")]
        public async Task<ApiResult<DataTable>> Get5thSemBackPaperReport([FromBody] BackPaperReportDataModel body)
        {
            ActionName = "Get5thSemBackPaperReport([FromBody] BackPaperReportDataModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.MarksheetDownloadRepository.Get5thSemBackPaperReport(body));
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
