using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Kaushal_Darpan.Api.HtmlTempleteFile;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ITI_AdmissionReports;
using Microsoft.AspNetCore.Mvc;

namespace Kaushal_Darpan.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ITIAdmissionReportsController : BaseController
    {
        public override string PageName => "ITIAdmissionReportsController";
        public override string ActionName { get; set; }
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //public ReportController(IMapper mapper, IUnitOfWork unitOfWork, IEmailService emailService)
        private readonly IConverter _converter;
        private readonly IPrintHtmlFile _printHtmlFile;
        public ITIAdmissionReportsController(IMapper mapper, IUnitOfWork unitOfWork, IConverter converter, IPrintHtmlFile printHtmlFile)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_emailService = emailService;
            _converter = converter;
            _printHtmlFile = printHtmlFile;
        }

        [HttpGet("getITISeatOfferedList")]
        public async Task<IActionResult> GetITISeatOfferedList()
        {
            var ds = await _unitOfWork.ITI_AdmissionReports.GetITISeatOffered();

            var list = CommonFuncationHelper
                .ConvertDataTable<List<ITIAdmissionSeatOfferedResponseModel>>(ds.Tables[0]);

            return Ok(list);
        }

        [HttpGet("downloadITISeatOfferedPDF")]
        public async Task<IActionResult> DownloadPDF()
        {
            var ds = await _unitOfWork.ITI_AdmissionReports.GetITISeatOffered();

            var list = CommonFuncationHelper
                .ConvertDataTable<List<ITIAdmissionSeatOfferedResponseModel>>(ds.Tables[0]);

            var rows = string.Join("", list.Select(x => $@"
    <tr>
        <td>{x.Session}</td>
        <td>{x.Govt_ITI_Count}</td>
        <td>{x.Govt_Seat_Offered}</td>
        <td>{x.Govt_Admission}</td>
        <td>{x.Govt_Percentage}</td>
        <td>{x.Pvt_ITI_Count}</td>
        <td>{x.Pvt_Seat_Offered}</td>
        <td>{x.Pvt_Admission}</td>
        <td>{x.Pvt_Percentage}</td>
        <td>{x.Total_ITI_Count}</td>
        <td>{x.Total_Seat_Offered}</td>
        <td>{x.Total_Admission}</td>
        <td>{x.Total_Percentage}</td>
    </tr>
    "));

            var html = $@"
    <html>
    <body style='font-family:Arial;font-size:11px;'>

    <h1 style='text-align:center;'>{list?.FirstOrDefault()?.TableHeading}</h1>

    <table border='1' style='width:100%;border-collapse:collapse;text-align:center;'>

    <tr>
        <th rowspan='2'>Session</th>
        <th colspan='4'>Government ITI</th>
        <th colspan='4'>Private ITI</th>
        <th colspan='4'>Grand Total(Govt. & Pvt ITIs)</th>
    </tr>

    <tr>
        <th>No. of ITI</th><th>Seat offered</th><th>Admission</th><th>%</th>
        <th>No. of ITI</th><th>Seat offered</th><th>Admission</th><th>%</th>
        <th>No. of ITI</th><th>Seat offered</th><th>Admission</th><th>%</th>
    </tr>

    {rows}
    </table>

    </body>
    </html>";

            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    Orientation = Orientation.Landscape
                },
                Objects = { new ObjectSettings { HtmlContent = html } }
            };

            var pdf = _converter.Convert(doc);

            return File(pdf, "application/pdf", "ITI_Report.pdf");
        }

        [HttpGet("downloadITISeatOfferedExcel")]
        public async Task<IActionResult> DownloadExcel()
        {
            var ds = await _unitOfWork.ITI_AdmissionReports.GetITISeatOffered();
            var dt = ds.Tables[0];

            using (var wb = new ClosedXML.Excel.XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Report");

                using (var stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ITI_Report.xlsx");
                }
            }
        }
    }
}
