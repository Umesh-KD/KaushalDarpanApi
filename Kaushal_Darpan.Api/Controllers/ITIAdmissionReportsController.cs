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

        //[HttpGet("downloadITISeatOfferedExcel")]
        //public async Task<IActionResult> DownloadExcel()
        //{
        //    var ds = await _unitOfWork.ITI_AdmissionReports.GetITISeatOffered();
        //    var dt = ds.Tables[0];

        //    using (var wb = new ClosedXML.Excel.XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt, "Report");

        //        using (var stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);

        //            return File(stream.ToArray(),
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "ITI_Report.xlsx");
        //        }
        //    }
        //}

        [HttpGet("downloadITISeatOfferedExcel")]
        public async Task<IActionResult> DownloadExcel()
        {
            var ds = await _unitOfWork.ITI_AdmissionReports.GetITISeatOffered();

            var list = CommonFuncationHelper
                .ConvertDataTable<List<ITIAdmissionSeatOfferedResponseModel>>(ds.Tables[0]);

            using (var wb = new ClosedXML.Excel.XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Report");

                int row = 1;

                // ✅ Title
                ws.Range(row, 1, row, 13).Merge();
                ws.Cell(row, 1).Value = list?.FirstOrDefault()?.TableHeading;
                ws.Cell(row, 1).Style.Font.Bold = true;
                ws.Cell(row, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                row += 2;

                // ✅ First Header Row
                ws.Range(row, 1, row + 1, 1).Merge().Value = "Session";

                ws.Range(row, 2, row, 5).Merge().Value = "Government ITI";
                ws.Range(row, 6, row, 9).Merge().Value = "Private ITI";
                ws.Range(row, 10, row, 13).Merge().Value = "Grand Total(Govt. & Pvt ITIs)";

                // ✅ Second Header Row
                row++;

                ws.Cell(row, 2).Value = "No. of ITI";
                ws.Cell(row, 3).Value = "Seat offered";
                ws.Cell(row, 4).Value = "Admission";
                ws.Cell(row, 5).Value = "%";

                ws.Cell(row, 6).Value = "No. of ITI";
                ws.Cell(row, 7).Value = "Seat offered";
                ws.Cell(row, 8).Value = "Admission";
                ws.Cell(row, 9).Value = "%";

                ws.Cell(row, 10).Value = "No. of ITI";
                ws.Cell(row, 11).Value = "Seat offered";
                ws.Cell(row, 12).Value = "Admission";
                ws.Cell(row, 13).Value = "%";

                // Style headers
                ws.Range(row - 1, 1, row, 13).Style.Font.Bold = true;
                ws.Range(row - 1, 1, row, 13).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                ws.Range(row - 1, 1, row, 13).Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;

                row++;

                // ✅ Data Rows
                foreach (var x in list)
                {
                    ws.Cell(row, 1).Value = x.Session;

                    ws.Cell(row, 2).Value = x.Govt_ITI_Count;
                    ws.Cell(row, 3).Value = x.Govt_Seat_Offered;
                    ws.Cell(row, 4).Value = x.Govt_Admission;
                    ws.Cell(row, 5).Value = x.Govt_Percentage;

                    ws.Cell(row, 6).Value = x.Pvt_ITI_Count;
                    ws.Cell(row, 7).Value = x.Pvt_Seat_Offered;
                    ws.Cell(row, 8).Value = x.Pvt_Admission;
                    ws.Cell(row, 9).Value = x.Pvt_Percentage;

                    ws.Cell(row, 10).Value = x.Total_ITI_Count;
                    ws.Cell(row, 11).Value = x.Total_Seat_Offered;
                    ws.Cell(row, 12).Value = x.Total_Admission;
                    ws.Cell(row, 13).Value = x.Total_Percentage;

                    row++;
                }

                // ✅ Borders
                ws.Range(3, 1, row - 1, 13).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                ws.Range(3, 1, row - 1, 13).Style.Border.InsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;

                // ✅ Auto fit columns
                ws.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ITI_Report.xlsx");
                }
            }
        }


        [HttpGet("getITIStatisticsList")]
        public async Task<IActionResult> GetITIStatisticsList()
        {
            var ds = await _unitOfWork.ITI_AdmissionReports.GetITIStatistics();

            var list = CommonFuncationHelper
                .ConvertDataTable<List<ITIStatisticsResponseModel>>(ds.Tables[0]);

            return Ok(list);
        }

        [HttpGet("downloadITIStatisticsPDF")]
        public async Task<IActionResult> DownloadITIStatisticsPDF()
        {
            var ds = await _unitOfWork.ITI_AdmissionReports.GetITIStatistics();

            var list = CommonFuncationHelper
                .ConvertDataTable<List<ITIStatisticsResponseModel>>(ds.Tables[0]);

            var rows = string.Join("", list.Select(x => $@"
<tr>
    <td>{x.Session}</td>

    <td>{x.GovtITI_No}</td>
    <td>{x.GovtITI_TrainingSeats}</td>
    <td>{x.GovtITI_Enrolled}</td>

    <td>{x.PvtITI_No}</td>
    <td>{x.PvtITI_TrainingSeats}</td>
    <td>{x.PvtITI_Enrolled}</td>

    <td>{x.FemaleSeats}</td>
    <td>{x.FemaleEnrolled}</td>

    <td>{x.DevITI_No}</td>
    <td>{x.DevSeats}</td>
    <td>{x.DevEnrolled}</td>
</tr>
"));

            var html = $@"
<html>
<body style='font-family:Arial;font-size:11px;'>

<h2 style='text-align:center;'>{list?.FirstOrDefault()?.TableHeading}</h2>

<table border='1' style='width:100%;border-collapse:collapse;text-align:center;'>

<tr>
    <th rowspan='2'>Session</th>
    <th colspan='3'>Government ITI</th>
    <th colspan='3'>Private ITI</th>
    <th colspan='2'>Female Participation</th>
    <th colspan='3'>Dev Narayan Yojna</th>
</tr>

<tr>
    <th>No of ITI</th>
    <th>Training Seats</th>
    <th>Enroll Trainees</th>

    <th>No of ITI</th>
    <th>Training Seats</th>
    <th>Enroll Trainees</th>

    <th>Available Seats</th>
    <th>Enroll Trainees</th>

    <th>No of ITI</th>
    <th>Available Seats</th>
    <th>Enroll Trainees</th>
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

            return File(pdf, "application/pdf", "ITI_Last5Year_Statistics.pdf");
        }

        [HttpGet("downloadITIStatisticsExcel")]
        public async Task<IActionResult> DownloadITIStatisticsExcel()
        {
            var ds = await _unitOfWork.ITI_AdmissionReports.GetITIStatistics();

            var list = CommonFuncationHelper
                .ConvertDataTable<List<ITIStatisticsResponseModel>>(ds.Tables[0]);

            using (var wb = new ClosedXML.Excel.XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Statistics");

                int row = 1;

                // ✅ Title
                ws.Range(row, 1, row, 12).Merge();
                ws.Cell(row, 1).Value = list?.FirstOrDefault()?.TableHeading;
                ws.Cell(row, 1).Style.Font.Bold = true;
                ws.Cell(row, 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;

                row += 2;

                // ✅ Header Row 1
                ws.Range(row, 1, row + 1, 1).Merge().Value = "Session";

                ws.Range(row, 2, row, 4).Merge().Value = "Government ITI";
                ws.Range(row, 5, row, 7).Merge().Value = "Private ITI";
                ws.Range(row, 8, row, 9).Merge().Value = "Female Participation";
                ws.Range(row, 10, row, 12).Merge().Value = "Dev Narayan Yojna";

                // ✅ Header Row 2
                row++;

                // Govt
                ws.Cell(row, 2).Value = "No of ITI";
                ws.Cell(row, 3).Value = "Training Seats";
                ws.Cell(row, 4).Value = "Enroll Trainees";

                // Pvt
                ws.Cell(row, 5).Value = "No of ITI";
                ws.Cell(row, 6).Value = "Training Seats";
                ws.Cell(row, 7).Value = "Enroll Trainees";

                // Female
                ws.Cell(row, 8).Value = "Available Seats";
                ws.Cell(row, 9).Value = "Enroll Trainees";

                // Dev Narayan
                ws.Cell(row, 10).Value = "No of ITI";
                ws.Cell(row, 11).Value = "Available Seats";
                ws.Cell(row, 12).Value = "Enroll Trainees";

                // ✅ Header Styling
                ws.Range(row - 1, 1, row, 12).Style.Font.Bold = true;
                ws.Range(row - 1, 1, row, 12).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                ws.Range(row - 1, 1, row, 12).Style.Alignment.Vertical = ClosedXML.Excel.XLAlignmentVerticalValues.Center;

                row++;

                // ✅ Data Rows
                foreach (var x in list)
                {
                    ws.Cell(row, 1).Value = x.Session;

                    ws.Cell(row, 2).Value = x.GovtITI_No;
                    ws.Cell(row, 3).Value = x.GovtITI_TrainingSeats;
                    ws.Cell(row, 4).Value = x.GovtITI_Enrolled;

                    ws.Cell(row, 5).Value = x.PvtITI_No;
                    ws.Cell(row, 6).Value = x.PvtITI_TrainingSeats;
                    ws.Cell(row, 7).Value = x.PvtITI_Enrolled;

                    ws.Cell(row, 8).Value = x.FemaleSeats;
                    ws.Cell(row, 9).Value = x.FemaleEnrolled;

                    ws.Cell(row, 10).Value = x.DevITI_No;
                    ws.Cell(row, 11).Value = x.DevSeats;
                    ws.Cell(row, 12).Value = x.DevEnrolled;

                    row++;
                }

                // ✅ Borders
                ws.Range(3, 1, row - 1, 12).Style.Border.OutsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
                ws.Range(3, 1, row - 1, 12).Style.Border.InsideBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;

                // ✅ Auto-fit
                ws.Columns().AdjustToContents();

                // Optional styling (same feel as PDF)
                ws.Style.Font.FontName = "Arial";
                ws.Style.Font.FontSize = 11;

                using (var stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "ITI_Last5Year_Statistics.xlsx");
                }
            }
        }
    }
}
