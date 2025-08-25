using AspNetCore.Reporting;
using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Vml;
using Kaushal_Darpan.Core.Helper;
using Kaushal_Darpan.Core.Interfaces;
using Kaushal_Darpan.Models.ITICollegeMarksheetDownloadmodel;
using Kaushal_Darpan.Models.ITIResults;
using Kaushal_Darpan.Models.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Data;
using static QRCoder.PayloadGenerator;

namespace Kaushal_Darpan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ITIResultController : BaseController
    {
        public override string PageName => "ITIResultController";
        public override string ActionName { get; set; }

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ITIResultController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GenerateResult")]
        public async Task<ApiResult<DataTable>> GenerateResult([FromBody] ITIResultsModel request)
        {
            ActionName = "GenerateResult()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIResultRepository.GenerateResult(request));
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

        [HttpPost("GetResultData")]
        public async Task<ApiResult<DataTable>> GetResultData([FromBody] ITIResultsModel request)
        {
            ActionName = "GetResultData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIResultRepository.GetResultData(request));
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



        [HttpPost("PublishResult")]
        public async Task<ApiResult<DataTable>> PublishResult([FromBody] ITIResultsModel request)
        {
            ActionName = "PublishResult()";
            return await Task.Run(async () =>
            {
                var result = new ApiResult<DataTable>();
                try
                {
                    result.Data = await _unitOfWork.ITIResultRepository.PublishResult(request);
                    _unitOfWork.SaveChanges();

                    //if (result.Data)
                    //{
                    result.State = EnumStatus.Success;
                    //    result.Message = "Result Published";
                    //}
                    //else
                    //{
                    //    result.State = EnumStatus.Error;
                    //    result.ErrorMessage = "Result Published";
                    //}
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
                    result.State = EnumStatus.Error;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            });
        }


        [HttpPost("GetCurrentResultStatus")]
        public async Task<ApiResult<DataTable>> GetCurrentResultStatus([FromBody] ITIResultsModel request)
        {
            ActionName = "GetCurrentResultStatus()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIResultRepository.GetCurrentStatusOfResult(request));
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

        [HttpPost("GetCFormReport")]
        public async Task<ApiResult<DataSet>> GetCFormReport([FromBody] ITIResultsModel request)
        {
            ActionName = "GetCFormReport()";
            var result = new ApiResult<DataSet>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIResultRepository.GetCFormReport(request));
                result.State = EnumStatus.Success;
                if (result.Data.Tables[0].Rows.Count > 0)
                {


                    var data = await _unitOfWork.ITIResultRepository.GetCFormReport(request);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //data.Tables[0].TableName = "StateTradeCertificate";

                        //data.Tables[0].Rows[0]["logo"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";

                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        //string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/ITIMarksheetCONSOLIDATED.html";

                        //string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        //html = Utility.PDFWorks.ReplaceCustomTag(html);



                        sb1.Append("<table id='pdf-header' style='width:100%' border='0' cellpadding='5' cellspacing='0'>");
                        sb1.Append("<tr><td style='text-align: center; padding: 10px; font-weight: bold; font-size: 15px;'>Rajasthan Council For Vocational Education And Training, Rajasthan</td></tr>");
                        sb1.Append("<tr><td style='text-align: center; padding: 10px; font-weight: bold; font-size: 14px;'>Department of Skill, Employment & Entrepreneurship</td></tr>");
                        sb1.Append("<tr><td style='text-align: center; padding: 10px; font-weight: bold; font-size: 11px;'>SCVT Yearly " + data.Tables[0].Rows[0]["AcadSession"].ToString() + " " + (request.ExamType == 1 ? "Main" : "Supplementary") + "  Examination Result</td></tr>");
                        sb1.Append("</table>");


                        foreach (DataRow row in data.Tables[0].Rows)
                        {

                            int TotalTrainee = 0;
                            int PassTrainee = 0;
                            int FailTrainee = 0;
                            float PercentageTrainee = 0;

                            sb1.Append("<table style='border-collapse: collapse; width: 100%; font-family: Arial; font-size:14px' border='0' cellpadding='5' cellspacing='0'>");


                            //@for(trade of TradeList; track trade) {
                            //<!-- ✅ Trade Name -->
                            int TradeId = Convert.ToInt32(row["TradeId"].ToString());
                            var colspan = data.Tables[1].AsEnumerable()
                                .Where(row => row.Field<int>("TradeId") == TradeId)
                                .Select(row => row.Field<int>("SubjectID"))
                                .Distinct()
                                .Count() + 9;
                            //var colspan = data.Tables[0].AsEnumerable().Where(row => row.Field<string>("QualificationID") == "10").CopyToDataTable();

                            sb1.Append("<tr>");
                            sb1.Append("<th colspan='" + colspan + "' style='text-align: left; padding: 10px; font-weight: bold;border:1px solid gray; '>");
                            sb1.Append("<table style='width:100%;'><tr><td style='text-align: left; text-decoration: underline; font-size: 13px;'><b> " + request.SemesterID + " Year (Annual Examination) " + row["DurationYear"] + " Year Trades</b></td><td  style='text-align: right; font-size: 13px;'><b>  Exam Month Year: " + data.Tables[0].Rows[0]["AcadSession"].ToString() + " | Result Dec Date: _______________</b></td></tr></table>");
                            sb1.Append("</th>");

                            //sb1.Append("<th style='text-align: right; padding: 10px; font-weight: bold; text-decoration: underline; font-size: 15px;border:1px solid gray; '>");
                            //sb1.Append("<b>  Exam Month Year: "+ data.Tables[0].Rows[0]["AcadSession"].ToString() + " | Result Dec Date: _______________</b>");
                            //sb1.Append("</th>'");

                            sb1.Append("</tr>");
                            sb1.Append("<tr>");
                            sb1.Append("<th colspan='" + colspan + "' style='text-align: left; padding: 10px; font-weight: bold; text-decoration: underline; font-size: 13px;border:1px solid gray; '>");
                            sb1.Append("<b> Trade: " + row["TradeName"] + "</b>");
                            sb1.Append("</th>");
                            sb1.Append("</tr>");

                            //<!-- ✅ Institute Loop -->
                            //@for(institute of getInstitutesByTrade(trade.TradeId); track institute) {



                            DataTable instituteData = GetInstitutesByTrade(TradeId, data.Tables[1]);
                            DataTable subjectData = GetSubjectNameDataList(TradeId, data.Tables[1]);

                            foreach (DataRow rowIns in instituteData.Rows)
                            {


                                //<!-- ✅ Institutename -->
                                sb1.Append("<tr>");
                                sb1.Append("<th colspan='" + colspan + "' style='text-align:left;padding:10px;font-weight:bold;text-decoration:underline;font-size:11px;border:1px solid gray;'>");
                                sb1.Append("<b> Institute: " + rowIns["InstituteName"] + "</b>");
                                sb1.Append("</th>");
                                sb1.Append("</tr>");

                                //<!-- ✅ Column Headers ONLY here -->
                                sb1.Append("<tr>");
                                sb1.Append("<th rowspan='2' style='text-align: left;font-size: 10px;border:1px solid gray; '>S.No.</th>");
                                sb1.Append("<th rowspan='2' style='text-align:left; font-size: 10px;border:1px solid gray;'>Trainee's Name / Father's Name</th>");
                                sb1.Append("<th rowspan='2' style='text-align:left; font-size: 10px;border:1px solid gray;'>Enrollment/Date of Birth</th>");
                                sb1.Append("<th rowspan='2' style='text-align:left; font-size: 10px;border:1px solid gray;'>Roll Number</th>");
                                sb1.Append("<th rowspan='2' style='text-align:left; font-size: 10px;border:1px solid gray;'>Last Appeared</th>");
                                //@for(subj of ReporSubjectNameDataList(trade.TradeId); let idx2 = $index; track subj) {
                                foreach (DataRow rowSub in subjectData.Rows)
                                {
                                    sb1.Append("<th style='text-align: center; font-size: 10px;border:1px solid gray; '>" + rowSub["SubjectName"] + "</th>");
                                }


                                sb1.Append("<th style='text-align: center; font-size: 10px;border:1px solid gray;'>Grand Total                 </th>");
                                sb1.Append("<th rowspan='2' style='text-align: center; font-size: 10px;border:1px solid gray;'>Result                      </th>");
                                sb1.Append("<th rowspan='2' style='text-align: center; font-size: 10px;border:1px solid gray;'>Original Certificate Number </th>");
                                sb1.Append("<th rowspan='2' style='text-align: center; font-size: 10px;border:1px solid gray;'>Acad Session                </th>");


                                sb1.Append("</tr>");
                                sb1.Append("<tr>");
                                //@for(subj of ReporSubjectNameDataList(trade.TradeId); let idx2 = $index; track subj) {
                                int grantTotal = 0;
                                foreach (DataRow rowSub in subjectData.Rows)
                                {
                                    grantTotal += Convert.ToInt32(rowSub["MaxMarks"]);
                                    sb1.Append("<th style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowSub["MinMarks"] + "/" + rowSub["MaxMarks"] + "</th>");
                                }
                                sb1.Append("<th style='text-align: center; font-size: 10px;border:1px solid gray;'>" + grantTotal + "</th>");
                                sb1.Append("</tr>");

                                //<!-- ✅ Trainee List -->
                                DataTable traineeData = GetTraineesByInstitute(TradeId, Convert.ToInt32(rowIns["InstituteID"].ToString()), data.Tables[1]);
                                //@for(trainee of getTraineesByInstitute(trade.TradeId, institute.InstituteID); let idx = $index; track trainee) {
                                int idx = 1;
                                foreach (DataRow rowTrnee in traineeData.Rows)
                                {

                                    TotalTrainee += 1;
                                    PassTrainee = PassTrainee + (rowTrnee["Result"].ToString() == "P" ? 1 : 0);
                                    FailTrainee = FailTrainee + (rowTrnee["Result"].ToString() == "F" ? 1 : 0);
                                    sb1.Append("<tr>");

                                    sb1.Append("<td style='text-align: left;   font-size: 10px;border:1px solid gray;'>" + (idx++) + "</td>");
                                    sb1.Append("<td style='text-align: left;   font-size: 10px;border:1px solid gray;'> " + rowTrnee["TraineeName"] + "</td>");
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["EnrollmentNo"] + "<br />" + rowTrnee["DOB"] + " </td>");
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["RollNo"] + "</td>");
                                    sb1.Append("<td style='text-align:left;    font-size: 10px;border:1px solid gray;'>" + rowTrnee["LastAppeared"] + "</td>");
                                    //@for(subj of ReporSubjectNameDataList(trade.TradeId); let idx2 = $index; track subj) {
                                    foreach (DataRow rowSub in subjectData.Rows)
                                    {
                                        sb1.Append("<th style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee[rowSub["SubjectName"].ToString()] + "</th>");
                                    }

                                    //}
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["GrandTotal"] + "</td>");
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["Result"] + "</td>");
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["OriginalCertificateNumber"] + "</td>");
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["AcadSession"] + "</td>");



                                    sb1.Append("</tr>");
                                }
                                //}
                            }
                            int col = Convert.ToInt16((colspan - (colspan / 4) * 4)) + Convert.ToInt16((colspan / 4));

                            PercentageTrainee = (PassTrainee / TotalTrainee) * 100;

                            sb1.Append("<tr>");
                            sb1.Append("<td colspan='" + colspan / 4 + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>Total Trainee :" + TotalTrainee + " </b></td>");
                            sb1.Append("<td colspan='" + colspan / 4 + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>Pass Trainee :" + PassTrainee + " </b></td>");
                            sb1.Append("<td colspan='" + colspan / 4 + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>Fail Trainee :" + FailTrainee + " </b></td>");
                            sb1.Append("<td colspan='" + col + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>Percentage Trainee :" + PercentageTrainee + "</b></td>");
                            sb1.Append("</tr>");
                            if (request.InstituteId == 0)
                            {
                                sb1.Append("<tr>");
                                sb1.Append("<td colspan='" + colspan + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>Checked 1______________________ Checked 2______________________ all marks are entered online by concerned examiners and submitted hard copy at RCVET.</b></td>");
                                sb1.Append("</tr>");

                                sb1.Append("<tr>");
                                sb1.Append("<td colspan='" + colspan + "' style='text-align: left;   font-size: 11px;border:1px solid gray'>");
                                sb1.Append("<table  style='border-collapse: collapse; width: 100%;margin-top:30px;margin-bottom:15px;' border='0' cellpadding='5' cellspacing='0'>");
                                sb1.Append("<tr>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(Lalit Baral)<br/>Senior Instructor</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(PremPrakash Rathore)<br/>Senior Instructor</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(Surendra Baghmar)<br/>Group Instructor</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(HukamSingh Rathore)<br/>DeputyDirector</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(Dr. Jagdish Prasad)<br/>DeputyDirector</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(Praveen Kumar Verma)<br/>S.A. (Joint Director)</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(Sugar Singh Meena)<br/>Director, RCVET</b></td>");
                                sb1.Append("</tr>");
                                sb1.Append("</table>");
                                sb1.Append("</td>");
                                sb1.Append("</tr>");
                            }
                            else
                            {
                                sb1.Append("<tr>");
                                sb1.Append("<td colspan='" + colspan + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>This is computer generated report, therefore it does not require any physical signature or attestation. Incase if any issue the same can be verified from RCVET, Jodhpur</b></td>");
                                sb1.Append("</tr>");

                            }
                            sb1.Append("</table>");
                            sb1.Append("<div style='margin-top:10px;'>&nbsp;</div>");
                        }


                        result.Data = new DataSet();

                        DataTable resultData = new DataTable();
                        resultData.Columns.Add("Reportdata");


                        var row1 = resultData.NewRow();
                        row1["Reportdata"] = sb1.ToString();
                        resultData.Rows.Add(row1);
                        result.Data.Tables.Add(resultData);

                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        //var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";

                        //byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "LANDSCAPE A4");

                        //result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
                    }
                    else
                    {
                        result.State = EnumStatus.Warning;
                        result.Message = Constants.MSG_DATA_NOT_FOUND;
                    }


                    result.State = EnumStatus.Success;
                    result.Message = "Data load successfully .!";


                }
                else
                {
                    result.State = EnumStatus.Success;
                    result.Message = "No record found.!";
                    return result;
                }

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



        [HttpPost("DownloadCFormReport")]
        public async Task<ApiResult<string>> DownloadCFormReport([FromBody] ITIResultsModel request)
        {
            ActionName = "DownloadCFormReport(string ApplicationID)";

            return await Task.Run(async () =>
            {
                var result = new ApiResult<string>();
                try
                {
                    var data = await _unitOfWork.ITIResultRepository.GetCFormReport(request);
                    if (data?.Tables?.Count > 0 && data.Tables[0].Rows.Count > 0)
                    {

                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        //data.Tables[0].TableName = "StateTradeCertificate";

                        //data.Tables[0].Rows[0]["logo"] = $"{ConfigurationHelper.StaticFileRootPath}/NE-100.png";
                        //data.Tables[0].Rows[0]["signlogo"] = $"{ConfigurationHelper.StaticFileRootPath}/iti_signlogo.png";

                        string devFontSize = "15px";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        //string htmlTemplatePath = $"{ConfigurationHelper.RootPath}{Constants.StateTradeCertificateITI}/ITIMarksheetCONSOLIDATED.html";

                        //string html = Utility.PDFWorks.GetHtml(htmlTemplatePath, data);

                        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                        //html = Utility.PDFWorks.ReplaceCustomTag(html);



                        sb1.Append("<table id='pdf-header' style='width:100%' border='0' cellpadding='5' cellspacing='0'>");
                        sb1.Append("<tr><td style='text-align: center; padding: 10px; font-weight: bold; font-size: 15px;'>Rajasthan Council For Vocational Education And Training, Rajasthan</td></tr>");
                        sb1.Append("<tr><td style='text-align: center; padding: 10px; font-weight: bold; font-size: 14px;'>Department of Skill, Employment & Entrepreneurship</td></tr>");
                        sb1.Append("<tr><td style='text-align: center; padding: 10px; font-weight: bold; font-size: 11px;'>SCVT Yearly " + data.Tables[0].Rows[0]["AcadSession"].ToString() + " " + (request.ExamType == 1 ? "Main" : "Supplementary") + "  Examination Result</td></tr>");
                        sb1.Append("</table>");


                        foreach (DataRow row in data.Tables[0].Rows)
                        {

                            int TotalTrainee = 0;
                            int PassTrainee = 0;
                            int FailTrainee = 0;
                            float PercentageTrainee = 0;

                            sb1.Append("<table style='border-collapse: collapse; width: 100%; font-family: Arial; font-size:14px' border='0' cellpadding='5' cellspacing='0'>");


                            //@for(trade of TradeList; track trade) {
                            //<!-- ✅ Trade Name -->
                            int TradeId = Convert.ToInt32(row["TradeId"].ToString());
                            var colspan = data.Tables[1].AsEnumerable()
                                .Where(row => row.Field<int>("TradeId") == TradeId)
                                .Select(row => row.Field<int>("SubjectID"))
                                .Distinct()
                                .Count() + 9;
                            //var colspan = data.Tables[0].AsEnumerable().Where(row => row.Field<string>("QualificationID") == "10").CopyToDataTable();

                            sb1.Append("<tr>");
                            sb1.Append("<th colspan='" + colspan + "' style='text-align: left; padding: 10px; font-weight: bold;border:1px solid gray; '>");
                            sb1.Append("<table style='width:100%;'><tr><td style='text-align: left; text-decoration: underline; font-size: 13px;'><b> " + request.SemesterID + " Year (Annual Examination) " + row["DurationYear"] + " Year Trades</b></td><td  style='text-align: right; font-size: 13px;'><b>  Exam Month Year: " + data.Tables[0].Rows[0]["AcadSession"].ToString() + " | Result Dec Date: _______________</b></td></tr></table>");
                            sb1.Append("</th>");

                            //sb1.Append("<th style='text-align: right; padding: 10px; font-weight: bold; text-decoration: underline; font-size: 15px;border:1px solid gray; '>");
                            //sb1.Append("<b>  Exam Month Year: "+ data.Tables[0].Rows[0]["AcadSession"].ToString() + " | Result Dec Date: _______________</b>");
                            //sb1.Append("</th>'");

                            sb1.Append("</tr>");
                            sb1.Append("<tr>");
                            sb1.Append("<th colspan='" + colspan + "' style='text-align: left; padding: 10px; font-weight: bold; text-decoration: underline; font-size: 13px;border:1px solid gray; '>");
                            sb1.Append("<b> Trade: " + row["TradeName"] + "</b>");
                            sb1.Append("</th>");
                            sb1.Append("</tr>");

                            //<!-- ✅ Institute Loop -->
                            //@for(institute of getInstitutesByTrade(trade.TradeId); track institute) {



                            DataTable instituteData = GetInstitutesByTrade(TradeId, data.Tables[1]);
                            DataTable subjectData = GetSubjectNameDataList(TradeId, data.Tables[1]);

                            foreach (DataRow rowIns in instituteData.Rows)
                            {


                                //<!-- ✅ Institutename -->
                                sb1.Append("<tr>");
                                sb1.Append("<th colspan='" + colspan + "' style='text-align:left;padding:10px;font-weight:bold;text-decoration:underline;font-size:11px;border:1px solid gray;'>");
                                sb1.Append("<b> Institute: " + rowIns["InstituteName"] + "</b>");
                                sb1.Append("</th>");
                                sb1.Append("</tr>");

                                //<!-- ✅ Column Headers ONLY here -->
                                sb1.Append("<tr>");
                                sb1.Append("<th rowspan='2' style='text-align: left;font-size: 10px;border:1px solid gray; '>S.No.</th>");
                                sb1.Append("<th rowspan='2' style='text-align:left; font-size: 10px;border:1px solid gray;'>Trainee's Name / Father's Name</th>");
                                sb1.Append("<th rowspan='2' style='text-align:left; font-size: 10px;border:1px solid gray;'>Enrollment/Date of Birth</th>");
                                sb1.Append("<th rowspan='2' style='text-align:left; font-size: 10px;border:1px solid gray;'>Roll Number</th>");
                                sb1.Append("<th rowspan='2' style='text-align:left; font-size: 10px;border:1px solid gray;'>Last Appeared</th>");
                                //@for(subj of ReporSubjectNameDataList(trade.TradeId); let idx2 = $index; track subj) {
                                foreach (DataRow rowSub in subjectData.Rows)
                                {
                                    sb1.Append("<th style='text-align: center; font-size: 10px;border:1px solid gray; '>" + rowSub["SubjectName"] + "</th>");
                                }


                                sb1.Append("<th style='text-align: center; font-size: 10px;border:1px solid gray;'>Grand Total                 </th>");
                                sb1.Append("<th rowspan='2' style='text-align: center; font-size: 10px;border:1px solid gray;'>Result                      </th>");
                                sb1.Append("<th rowspan='2' style='text-align: center; font-size: 10px;border:1px solid gray;'>Original Certificate Number </th>");
                                sb1.Append("<th rowspan='2' style='text-align: center; font-size: 10px;border:1px solid gray;'>Acad Session                </th>");


                                sb1.Append("</tr>");
                                sb1.Append("<tr>");
                                //@for(subj of ReporSubjectNameDataList(trade.TradeId); let idx2 = $index; track subj) {
                                int grantTotal = 0;
                                foreach (DataRow rowSub in subjectData.Rows)
                                {
                                    grantTotal += Convert.ToInt32(rowSub["MaxMarks"]);
                                    sb1.Append("<th style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowSub["MinMarks"] + "/" + rowSub["MaxMarks"] + "</th>");
                                }
                                sb1.Append("<th style='text-align: center; font-size: 10px;border:1px solid gray;'>" + grantTotal + "</th>");
                                sb1.Append("</tr>");

                                //<!-- ✅ Trainee List -->
                                DataTable traineeData = GetTraineesByInstitute(TradeId, Convert.ToInt32(rowIns["InstituteID"].ToString()), data.Tables[1]);
                                //@for(trainee of getTraineesByInstitute(trade.TradeId, institute.InstituteID); let idx = $index; track trainee) {
                                int idx = 1;
                                foreach (DataRow rowTrnee in traineeData.Rows)
                                {

                                    TotalTrainee += 1;
                                    PassTrainee = PassTrainee + (rowTrnee["Result"].ToString() == "P" ? 1 : 0);
                                    FailTrainee = FailTrainee + (rowTrnee["Result"].ToString() == "F" ? 1 : 0);
                                    sb1.Append("<tr>");

                                    sb1.Append("<td style='text-align: left;   font-size: 10px;border:1px solid gray;'>" + (idx++) + "</td>");
                                    sb1.Append("<td style='text-align: left;   font-size: 10px;border:1px solid gray;'> " + rowTrnee["TraineeName"] + "</td>");
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["EnrollmentNo"] + "<br />" + rowTrnee["DOB"] + " </td>");
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["RollNo"] + "</td>");
                                    sb1.Append("<td style='text-align:left;    font-size: 10px;border:1px solid gray;'>" + rowTrnee["LastAppeared"] + "</td>");
                                    //@for(subj of ReporSubjectNameDataList(trade.TradeId); let idx2 = $index; track subj) {
                                    foreach (DataRow rowSub in subjectData.Rows)
                                    {
                                        sb1.Append("<th style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee[rowSub["SubjectName"].ToString()] + "</th>");
                                    }

                                    //}
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["GrandTotal"] + "</td>");
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["Result"] + "</td>");
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["OriginalCertificateNumber"] + "</td>");
                                    sb1.Append("<td style='text-align: center; font-size: 10px;border:1px solid gray;'>" + rowTrnee["AcadSession"] + "</td>");



                                    sb1.Append("</tr>");
                                }
                                //}
                            }
                            int col = Convert.ToInt16((colspan - (colspan / 4) * 4)) + Convert.ToInt16((colspan / 4));

                            PercentageTrainee = (PassTrainee / TotalTrainee) * 100;

                            sb1.Append("<tr>");
                            sb1.Append("<td colspan='" + colspan / 4 + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>Total Trainee :" + TotalTrainee + " </b></td>");
                            sb1.Append("<td colspan='" + colspan / 4 + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>Pass Trainee :" + PassTrainee + " </b></td>");
                            sb1.Append("<td colspan='" + colspan / 4 + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>Fail Trainee :" + FailTrainee + " </b></td>");
                            sb1.Append("<td colspan='" + col + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>Percentage Trainee :" + PercentageTrainee + "</b></td>");
                            sb1.Append("</tr>");

                            if (request.InstituteId == 0)
                            {
                                sb1.Append("<tr>");
                                sb1.Append("<td colspan='" + colspan + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>Checked 1______________________ Checked 2______________________ all marks are entered online by concerned examiners and submitted hard copy at RCVET.</b></td>");
                                sb1.Append("</tr>");

                                sb1.Append("<tr>");
                                sb1.Append("<td colspan='" + colspan + "' style='text-align: left;   font-size: 11px;border:1px solid gray'>");
                                sb1.Append("<table  style='border-collapse: collapse; width: 100%;margin-top:30px;margin-bottom:15px;' border='0' cellpadding='5' cellspacing='0'>");
                                sb1.Append("<tr>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(Lalit Baral)<br/>Senior Instructor</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(PremPrakash Rathore)<br/>Senior Instructor</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(Surendra Baghmar)<br/>Group Instructor</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(HukamSingh Rathore)<br/>DeputyDirector</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(Dr. Jagdish Prasad)<br/>DeputyDirector</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(Praveen Kumar Verma)<br/>S.A. (Joint Director)</b></td>");
                                sb1.Append("<td style='text-align:center;font-size: 11px;'>______________<br/><b>(Sugar Singh Meena)<br/>Director, RCVET</b></td>");
                                sb1.Append("</tr>");
                                sb1.Append("</table>");
                                sb1.Append("</td>");
                                sb1.Append("</tr>");
                            }
                            else
                            {
                                sb1.Append("<tr>");
                                sb1.Append("<td colspan='" + colspan + "' style='text-align: left;   font-size: 11px;border:1px solid gray;'><b>This is computer generated report, therefore it does not require any physical signature or attestation. Incase if any issue the same can be verified from RCVET, Jodhpur</b></td>");
                                sb1.Append("</tr>");

                            }

                            sb1.Append("</table>");
                            sb1.Append("<div style='margin-top:10px;'>&nbsp;</div>");
                        }





                        //sb1.Append(UnicodeToKrutidev.FindAndReplaceKrutidev(html.Replace("<br>", "<br/>"), true, devFontSize));

                        var watermarkImagePath = $"{ConfigurationHelper.StaticFileRootPath}/ITILogo.jpg";

                        byte[] pdfBytes = Utility.PDFWorks.GeneratePDFGetByte(sb1, "LANDSCAPE A4");

                        result.Data = Convert.ToBase64String(pdfBytes); ;
                        result.State = EnumStatus.Success;
                        result.Message = "Success";
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

        private DataTable GetInstitutesByTrade(int tradeId, DataTable reportData)
        {
            var distinctInstitutes = reportData.AsEnumerable()
                .Where(row => row.Field<int>("TradeId") == tradeId)
                .Select(row => new
                {
                    InstituteID = row.Field<int>("InstituteID"),
                    InstituteName = row.Field<string>("InstituteName")
                })
                .Distinct();

            // Create the result table manually
            DataTable result = new DataTable();
            result.Columns.Add("InstituteID", typeof(int));
            result.Columns.Add("InstituteName", typeof(string));

            foreach (var item in distinctInstitutes)
            {
                result.Rows.Add(item.InstituteID, item.InstituteName);
            }

            return result;
        }


        private DataTable GetSubjectNameDataList(int tradeId, DataTable reportData)
        {
            // Clone schema with required columns
            DataTable result = new DataTable();
            result.Columns.Add("SubjectID", typeof(int));
            result.Columns.Add("SubjectName", typeof(string));
            result.Columns.Add("MaxMarks", typeof(decimal));
            result.Columns.Add("MinMarks", typeof(decimal));

            var filtered = reportData.AsEnumerable()
                .Where(row => row.Field<int>("TradeId") == tradeId)
                .GroupBy(row => row.Field<int>("SubjectID"))
                .Select(g =>
                {
                    var first = g.First();
                    var newRow = result.NewRow();
                    newRow["SubjectID"] = first.Field<int>("SubjectID");
                    newRow["SubjectName"] = first.Field<string>("SubjectName");

                    // Convert Int16 or Int32 to Decimal safely
                    newRow["MaxMarks"] = Convert.ToDecimal(first["MaxMarks"]);
                    newRow["MinMarks"] = Convert.ToDecimal(first["MinMarks"]);

                    return newRow;
                });

            // If filtered is empty, handle exception
            if (filtered.Any())
                result = filtered.CopyToDataTable();

            return result;
        }

        private DataTable GetTraineesByInstitute(int tradeId, int instituteId, DataTable reportData)
        {
            var filtered = reportData.AsEnumerable()
                .Where(x => x.Field<int>("TradeId") == tradeId && x.Field<int>("InstituteID") == instituteId)
                .ToList();

            var subjectNames = filtered
                .Select(x => x.Field<string>("SubjectName"))
                .Distinct()
                .ToList();

            // Define the output table schema
            DataTable result = new DataTable();
            result.Columns.Add("StudentID", typeof(int));
            result.Columns.Add("TraineeName", typeof(string));
            result.Columns.Add("DOB", typeof(DateTime));
            result.Columns.Add("RollNo", typeof(string));
            result.Columns.Add("LastAppeared", typeof(string));
            result.Columns.Add("GrandTotal", typeof(decimal));
            result.Columns.Add("Result", typeof(string));
            result.Columns.Add("OriginalCertificateNumber", typeof(string));
            result.Columns.Add("AcadSession", typeof(string));
            result.Columns.Add("EnrollmentNo", typeof(string));

            subjectNames.ForEach(s => result.Columns.Add(s));

            // Create DataRows with LINQ
            var rows = filtered
                .GroupBy(x => x.Field<int>("StudentID"))
                .Select(g =>
                {
                    var first = g.First();
                    var row = result.NewRow();

                    row["StudentID"] = first["StudentID"];
                    row["TraineeName"] = first["TraineeName"];
                    row["DOB"] = first["DOB"];
                    row["RollNo"] = first["RollNo"];
                    row["LastAppeared"] = first["LastAppeared"];
                    row["OriginalCertificateNumber"] = first["OriginalCertificateNumber"];
                    row["AcadSession"] = first["AcadSession"];
                    row["EnrollmentNo"] = first["EnrollmentNo"];

                    row["Result"] = first["Result"].ToString() == "1" ? "P"
                                  : first["Result"].ToString() == "2" ? "F" : "";

                    row["GrandTotal"] = g.Sum(x => Convert.ToDecimal(x["ObtainedMarks"]));

                    g.ToList().ForEach(x =>
                    {
                        string subjectName = x.Field<string>("SubjectName");
                        row[subjectName] = x["ObtainedMarks"];
                    });
                    return row;
                });

            // Convert rows to DataTable using CopyToDataTable() after projecting DataRow
            return rows.Any() ? rows.CopyToDataTable() : result.Clone(); // Return empty schema if no data
        }

        [HttpPost("GetStudentPassFailResultData")]
        public async Task<ApiResult<DataTable>> GetStudentPassFailResultData([FromBody] ITIStudentPassFailResultsModel request)
        {
            ActionName = "GetStudentPassFailResultData()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIResultRepository.GetStudentPassFailResultData(request));
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


        // new changes  
        [HttpPost("GetCurrentPassFailResultStatus")]
        public async Task<ApiResult<DataTable>> GetCurrentPassFailResultStatus([FromBody] ITIResultsModel request)
        {
            ActionName = "GetCurrentPassFailResultStatus()";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIResultRepository.GetCurrentStatusOfResult(request));
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


        // Add Trade list 

        [HttpPost("GetITITradeList")]
        public async Task<ApiResult<DataTable>> GetITITradeList([FromBody] ITIStudentPassFailResultsModel body)
        {
            ActionName = "GetITITradeList([FromBody] ITICollegeStudentMarksheetSearchModel body)";
            var result = new ApiResult<DataTable>();
            try
            {
                result.Data = await Task.Run(() => _unitOfWork.ITIResultRepository.GetITITradeList(body));
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
